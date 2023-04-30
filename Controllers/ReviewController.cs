using Microsoft.AspNetCore.Mvc;

namespace LicentaApp.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        [Route("/Reviews")]
        public async Task<IActionResult> Index()
        {
            return View("~/Views/Reviews/Index.cshtml", await _reviewRepository.IndexReviewList());
        }

        [HttpPost]
        [Route("/Reviews/AddReview")]
        public async Task<IActionResult> AddReview(ReviewModel newReview)
        {
            //if (ModelState.IsValid)
            //{
            //    // logic to store form data in DB
            //    _reviewRepository.AddReview(newReview);
            //}
            return PartialView("~/Views/Reviews/AddReview.cshtml", newReview);
        }
    }
}
