using AIDueDiligence.DTOs;
using AIDueDiligence.Services;
using Microsoft.AspNetCore.Mvc;

namespace AIDueDiligence.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly IAnswerSearchService _searchService;
    private readonly IGeminiService _geminiService;
    public ChatController(IAnswerSearchService searchService, IGeminiService geminiService  )
    {
        _searchService = searchService;
        _geminiService = geminiService;
    }

    [HttpPost("ask")]
    public async Task<IActionResult> Ask(
     AskQuestionRequest request)
    {
        var matches =
            await _searchService.SearchAsync(
                request.Question);

        var historicalAnswers =
            matches.Select(x => x.Answer)
                   .ToList();

        var aiResponse =
            await _geminiService.GenerateResponseAsync(
                request.Question,
                historicalAnswers);

        return Ok(new
        {
            response = aiResponse
        });
    }
}