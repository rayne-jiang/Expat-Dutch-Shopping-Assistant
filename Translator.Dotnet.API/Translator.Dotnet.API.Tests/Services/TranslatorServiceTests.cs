using System.Threading.Tasks;
using Moq;
using RestSharp;
using Translator.Dotnet.API.Services;
using Xunit;
using System.Threading;
using DotNetEnv;
using System.IO;
using System.Reflection;

public class TranslatorServiceHappyPathTest
{
    public TranslatorServiceHappyPathTest()
    {
        // Load environment variables from .env file in project root
        var envPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", ".env");
        Env.Load(envPath);
    }

    [Fact]
    public async Task TranslateTextAsync_ReturnsExpectedTranslation_WhenApiResponseIsSuccessful()
    {
        // Arrange
        var inputText = "你好";
        var targetLanguage = "NL";
        var expectedTranslation = "Hoe gaat het met je?";

        var translatorService = new TranslatorService();

        var result = await translatorService.TranslateTextAsync(inputText, targetLanguage);

        Assert.Equal(expectedTranslation, result);
    }
}
