using LicentaApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace LicentaApp.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IReviewService _reviewRepository;

        public ReviewsController(IReviewService reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _reviewRepository.IndexReviewList());
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(ReviewModel newReview)
        {
            if (ModelState.IsValid)
            {
                _reviewRepository.AddReview(newReview);
            }
            return RedirectToAction("Album", "Albums", new { name = newReview.Subject });
        }
    }
}
