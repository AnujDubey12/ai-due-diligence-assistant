
using System.Text;
using System.Text.Json;

namespace AIDueDiligence.Services
{
    public class GeminiService : IGeminiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public GeminiService(
            HttpClient httpClient,
            IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task<string> GenerateResponseAsync(
        string question,
        List<string> historicalAnswers)
        {
            var apiKey =
                _configuration["Gemini:ApiKey"];

            var context =
                string.Join("\n\n", historicalAnswers);

            var prompt = $"""
You are a Due Diligence Assistant.

Historical Answers:

{context}

User Question:

{question}

Generate a professional due diligence response.
""";

            var request = new
            {
                contents = new[]
                {
                new
                {
                    parts = new[]
                    {
                        new
                        {
                            text = prompt
                        }
                    }
                }
            }
            };

            var json =
                JsonSerializer.Serialize(request);

            var content =
                new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json");

            var response =
                await _httpClient.PostAsync(
                    $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={apiKey}",
                    content);

            response.EnsureSuccessStatusCode();

            var result =
                await response.Content.ReadAsStringAsync();

            using var document =
                JsonDocument.Parse(result);

            return document
                .RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString() ?? string.Empty;
        }
    }
}
