using Microsoft.AspNetCore.Mvc;

namespace LicentaApp.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly IFeedbackRepository _feedbackService;

        public FeedbackController(IFeedbackRepository feedbackService)
        {
            _feedbackService = feedbackService;
        }
        public async Task<IActionResult> Index()
        {
            var feedbackList = await _feedbackService.GetAsync();
            return View(feedbackList);
        }
        public async Task<IActionResult> AddFeeback()
        {
            return View();
        }

    }
}
