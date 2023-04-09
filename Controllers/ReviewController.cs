using Microsoft.AspNetCore.Mvc;

namespace LicentaApp.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ReviewService _reviewService;

        public ReviewController(ReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        public async Task<IActionResult> Index()
        {
            var reviewList = await _reviewService.GetAsync();
            return View(reviewList);
        }
    }
}
