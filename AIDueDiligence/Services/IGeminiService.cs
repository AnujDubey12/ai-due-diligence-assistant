namespace AIDueDiligence.Services
{
    public interface IGeminiService
    {
        Task<string> GenerateResponseAsync(string question, List<string> historicalAnswers);
    }
}
