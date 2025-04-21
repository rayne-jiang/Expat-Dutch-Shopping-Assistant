# Expat-Dutch-Shopping-Assistant

A full-stack application designed to help expats in the Netherlands with their shopping needs, featuring Dutch-to-Mandarin translation capabilities. Built with .NET 8 for the backend and React with TypeScript for the frontend.

## Project Overview

This project consists of two main components:

1. **Backend API** (.NET 8)
   - RESTful API built with ASP.NET Core
   - Handles product data and translation requests
   - Includes unit tests for reliability

2. **Frontend UI** (React + TypeScript)
   - Modern React application built with Vite
   - TypeScript for type safety
   - ESLint for code quality
   - Material-UI and Ant Design for UI components
   - Product search and translation features

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/) (LTS version recommended)
- [npm](https://www.npmjs.com/) (comes with Node.js)

## Getting Started

### Backend Setup

1. Navigate to the backend directory:
   ```bash
   cd Translator.Dotnet.API
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Run the application:
   ```bash
   dotnet run
   ```
   The API will be available at `http://localhost:5244`

### Frontend Setup

1. Navigate to the frontend directory:
   ```bash
   cd Translator.React.UI
   ```

2. Install dependencies:
   ```bash
   npm install
   ```

3. Start the development server:
   ```bash
   npm run dev
   ```
   The frontend will be available at `http://localhost:5173`

## Development

### Running Tests

Backend tests can be run with:
```bash
cd Translator.Dotnet.API.Tests
dotnet test
```

### Code Quality

The frontend uses ESLint for code quality. Available commands:

```bash
npm run lint        # Check for issues
npm run lint:fix    # Fix issues automatically
npm run lint:strict # Strict check (used in build)
```

### Building for Production

1. Build the frontend:
   ```bash
   cd Translator.React.UI
   npm run build
   ```

2. Build the backend:
   ```bash
   cd Translator.Dotnet.API
   dotnet publish -c Release
   ```

## Project Structure

```
Expat-Dutch-Shopping-Assistant/
├── Translator.Dotnet.API/         # Backend API
│   └── Translator.Dotnet.API.Tests/  # Backend tests
├── Translator.React.UI/           # Frontend application
│   ├── src/                      # Source code
│   ├── public/                   # Static assets
│   └── vite.config.ts           # Vite configuration
└── README.md                    # This file
```