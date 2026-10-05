using AIDueDiligence.Model;
using Microsoft.EntityFrameworkCore;

namespace AIDueDiligence.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<QuestionnaireAnswer> QuestionnaireAnswers
        => Set<QuestionnaireAnswer>();
}