using Microsoft.AspNetCore.Mvc;

namespace LicentaApp.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [Route("/Reviews/Index")]
        public async Task<IActionResult> Index()
        {
            //ReviewModel r = new ReviewModel("paraschivmatei20@stud.ase.ro", "ReviewTest", "Testing Review", "I am testing the review model.");
            //await _reviewService.CreateAsync(r);
            var reviewList = await _reviewService.GetAsync();
            return View("~/Views/Reviews/Index.cshtml", reviewList);
        }
        [Route("/Reviews/AddReview", Name = "AddReview")]
        public async Task<IActionResult> AddReview()
        {
            return View("~/Views/Reviews/AddReview.cshtml");
        }
    }
}
