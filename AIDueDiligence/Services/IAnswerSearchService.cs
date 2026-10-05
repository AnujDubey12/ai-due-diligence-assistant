using AIDueDiligence.Model;

namespace AIDueDiligence.Services
{
    public interface IAnswerSearchService
    {
        Task<List<QuestionnaireAnswer>> SearchAsync(string question);
    }
}
