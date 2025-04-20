# Use the SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project files
COPY ["Translator.Dotnet.API.csproj", "./"]
RUN dotnet restore

# Copy the rest of the code
COPY . .

# Build the application
RUN dotnet build "Translator.Dotnet.API.csproj" -c Release -o /app/build

# Publish the application
RUN dotnet publish "Translator.Dotnet.API.csproj" -c Release -o /app/publish

# Use the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copy the published application
COPY --from=build /app/publish .

# Set environment variables
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Production

# Expose the port the app runs on
EXPOSE 80

# Start the application
ENTRYPOINT ["dotnet", "Translator.Dotnet.API.dll"] 