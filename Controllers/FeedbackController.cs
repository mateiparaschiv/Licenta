using Microsoft.AspNetCore.Mvc;

namespace LicentaApp.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly FeedbackService _feedbackService;

        public FeedbackController(FeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }
        public async Task<IActionResult> Index()
        {
            var feedbackList = await _feedbackService.GetAsync();
            return View(feedbackList);
        }
    }
}
