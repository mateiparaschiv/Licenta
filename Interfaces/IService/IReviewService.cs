﻿using LicentaApp.Models;
namespace LicentaApp.Interfaces.IRepository
{
    public interface IReviewService
    {
        //Task AddReview(ReviewModel newReview);
        Task<List<ReviewModel>> IndexReviewList();
        Task<(bool IsSuccess, string ErrorMessage)> AddReviewAndRedirect(ReviewModel newReview);
        Task<(bool IsSuccess, string ErrorMessage)> DeleteReviewAndRedirect(string reviewId);
    }
}
