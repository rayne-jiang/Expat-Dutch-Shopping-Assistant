#!/bin/bash

# Configuration
AWS_REGION="us-east-1"  # Change this to your region
REPOSITORY_NAME="translator-api"
ACCOUNT_ID=$(aws sts get-caller-identity --query Account --output text)
IMAGE_TAG="latest"

# Colors for output
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m'

# Function to check if there are local changes
check_changes() {
    if [ -n "$(git status --porcelain)" ]; then
        echo -e "${YELLOW}Warning: You have uncommitted changes. Do you want to continue? (y/n)${NC}"
        read -r response
        if [[ "$response" =~ ^([nN][oO]|[nN])$ ]]; then
            echo "Exiting..."
            exit 1
        fi
    fi
}

# Function to push changes to ECR
push_to_ecr() {
    echo -e "${GREEN}Step 1: Building new Docker image...${NC}"
    docker build -t ${REPOSITORY_NAME} .

    echo -e "\n${GREEN}Step 2: Logging in to ECR...${NC}"
    aws ecr get-login-password --region ${AWS_REGION} | docker login --username AWS --password-stdin ${ACCOUNT_ID}.dkr.ecr.${AWS_REGION}.amazonaws.com

    echo -e "\n${GREEN}Step 3: Tagging image for ECR...${NC}"
    docker tag ${REPOSITORY_NAME}:latest ${ACCOUNT_ID}.dkr.ecr.${AWS_REGION}.amazonaws.com/${REPOSITORY_NAME}:${IMAGE_TAG}

    echo -e "\n${GREEN}Step 4: Pushing image to ECR...${NC}"
    docker push ${ACCOUNT_ID}.dkr.ecr.${AWS_REGION}.amazonaws.com/${REPOSITORY_NAME}:${IMAGE_TAG}
}

# Function to deploy from ECR
deploy_from_ecr() {
    echo -e "\n${GREEN}Step 5: Pulling the latest image from ECR...${NC}"
    docker pull ${ACCOUNT_ID}.dkr.ecr.${AWS_REGION}.amazonaws.com/${REPOSITORY_NAME}:${IMAGE_TAG}

    echo -e "\n${GREEN}Step 6: Stopping and removing existing container if it exists...${NC}"
    docker stop translator-api || true
    docker rm translator-api || true

    echo -e "\n${GREEN}Step 7: Running the container...${NC}"
    docker run -d \
      --name translator-api \
      -p 8080:80 \
      -e DEEPL_API_KEY=${DEEPL_API_KEY} \
      -e DEEPL_BASE_URL=${DEEPL_BASE_URL:-"https://api-free.deepl.com/v2/translate"} \
      -e ASPNETCORE_ENVIRONMENT=Production \
      ${ACCOUNT_ID}.dkr.ecr.${AWS_REGION}.amazonaws.com/${REPOSITORY_NAME}:${IMAGE_TAG}

    echo -e "\n${GREEN}Step 8: Verifying container status...${NC}"
    docker ps | grep translator-api
}

# Main workflow
echo -e "${GREEN}Starting deployment workflow...${NC}"

# Check for uncommitted changes
check_changes

# Ask if user wants to push changes
echo -e "${YELLOW}Do you want to push changes to ECR? (y/n)${NC}"
read -r push_response
if [[ "$push_response" =~ ^([yY][eE][sS]|[yY])$ ]]; then
    push_to_ecr
fi

# Always deploy the latest version
deploy_from_ecr

echo -e "\n${GREEN}Deployment workflow complete!${NC}"
echo "API is available at: http://localhost:8080"
echo "To view logs: docker logs translator-api" 