#!/bin/bash

# Stop and remove existing container if it exists
docker stop translator-api || true
docker rm translator-api || true

# Build the Docker image
echo "Building Docker image..."
docker build -t translator-api .

# Run the container
echo "Starting container..."
docker run -d \
  -p 8080:80 \
  --name translator-api \
  -e DEEPL_API_KEY=$DEEPL_API_KEY \
  -e DEEPL_BASE_URL=${DEEPL_BASE_URL:-"https://api-free.deepl.com/v2/translate"} \
  -e ASPNETCORE_ENVIRONMENT=Production \
  translator-api

echo "Container started. API is available at http://localhost:8080" 