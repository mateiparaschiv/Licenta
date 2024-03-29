﻿using LicentaApp.Models;
using Microsoft.AspNetCore.Mvc;
namespace LicentaApp.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IReviewService _reviewService;
        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _reviewService.IndexReviewList());
        }
        [HttpPost]
        public async Task<IActionResult> AddReview(ReviewModel newReview, string returnUrl)
        {
            var result = await _reviewService.AddReviewAndRedirect(newReview);

            if (ModelState.IsValid && result.IsSuccess)
            {
                return RedirectToReturnUrl(returnUrl);
            }
            else
            {
                // Handle the failure case as needed, e.g., show an error message.
            }

            // If we got this far, something failed; redisplay the form.
            // You may want to redirect to the returnUrl, or some other error page.
            return RedirectToReturnUrl(returnUrl);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteReview(string reviewId, string returnUrl)
        {
            //var result = await _reviewService.DeleteReviewAndRedirect(reviewId);

            //if (result.IsSuccess)
            //{
            //    return RedirectToReturnUrl(returnUrl);
            //}
            //else
            //{
            //    // Handle the failure case as needed, e.g., show an error message.
            //}

            //// If we got this far, something failed; redisplay the form.
            //// You may want to redirect to the returnUrl, or some other error page.
            //return RedirectToReturnUrl(returnUrl);
            await _reviewService.DeleteReviewAndRedirect(reviewId);
            return Json(new { success = true });
        }
        private IActionResult RedirectToReturnUrl(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                // Fallback redirect if returnUrl is not available
                return RedirectToAction("Index", "Home");
            }
        }
    }
}