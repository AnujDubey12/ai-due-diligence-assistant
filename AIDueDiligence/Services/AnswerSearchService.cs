using AIDueDiligence.API.Data;
using AIDueDiligence.Model;
using Microsoft.EntityFrameworkCore;

namespace AIDueDiligence.Services
{
    public class AnswerSearchService : IAnswerSearchService
    {
        private readonly ApplicationDbContext _db;

        public AnswerSearchService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<QuestionnaireAnswer>> SearchAsync(string question)
        {
            var keywords = question
                .ToLower()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);

            return await _db.QuestionnaireAnswers
                .Where(x =>
                    keywords.Any(k =>
                        x.Question.ToLower().Contains(k) ||
                        x.Answer.ToLower().Contains(k)))
                .Take(5)
                .ToListAsync();
        }
    }
}
