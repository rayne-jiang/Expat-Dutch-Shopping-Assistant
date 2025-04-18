# Dutch to Mandarin Translator API

A .NET API for translating text from Dutch to Mandarin using the DeepL API.

## Prerequisites

- .NET 8.0 SDK
- DeepL API key (get one from https://www.deepl.com/pro-api)

## Setup

1. Clone the repository
2. Navigate to the project directory:
   ```bash
   cd Translator.Dotnet.API/Translator.Dotnet.API
   ```

3. Set up environment variables:
   - Copy the `.env.example` file to `.env`:
     ```bash
     cp .env.example .env
     ```
   - Open `.env` and replace `your-api-key-here` with your actual DeepL API key

4. Install dependencies:
   ```bash
   dotnet restore
   ```

5. Run the application:
   ```bash
   dotnet run
   ```

## Environment Variables

The application requires the following environment variables to be set in the `.env` file:

- `DEEPL_API_KEY`: Your DeepL API key (required)
- `DEEPL_BASE_URL`: The DeepL API endpoint (optional, defaults to free API endpoint)

Example `.env` file:
```
DEEPL_API_KEY=your-api-key-here:fx
DEEPL_BASE_URL=https://api-free.deepl.com/v2/translate
```

## Security Note

Never commit your `.env` file to version control. The `.env` file is already included in `.gitignore` to prevent accidental commits of sensitive information. 