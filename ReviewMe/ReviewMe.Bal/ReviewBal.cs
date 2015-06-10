using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReviewMe.DataAccess;
using ReviewMe.DataAccess.Repository;
using ReviewMe.Model;
using ReviewMe.ViewModel;
using ReviewMe.Common.Authorization;

namespace ReviewMe.Bal
{
    public class ReviewBal
    {
        private readonly Repository<Review> _reviewRepository = new Repository<Review>(new EntityContext());

        // Get All Reviews
        public ReviewViewModelLong GetAllReviews()
        {
            try
            {
                List<Review> reviewList = _reviewRepository.GetAll();
                var reviewViewModelLong = new ReviewViewModelLong();
                foreach (Review review in reviewList)
                {
                    reviewViewModelLong.ReviewViewModelList.Add(new ReviewViewModel()
                    {
                        Id = review.Id,
                        UserId = review.UserId,
                        ReviewDate = review.ReviewDate,
                        ProjectId = review.ProjectId,
                        ModuleName = review.ModuleName,
                        FileReviewed = review.FileReviewed,
                        MethodsReviewed = review.MethodsReviewed,
                        Remarks = review.Remarks,
                        Status = review.Status,
                        CodeOptimizationRating = review.CodeOptimizationRating,
                        CodingStandardRating = review.CodingStandardRating,
                        QueryOptimizationRating = review.QueryOptimizationRating,
                        ProjectArchitecture = review.ProjectArchitecture,
                        CreatedBy = review.CreatedBy,
                        CreatedOn = review.CreatedOn,
                        ModifiedBy = review.ModifiedBy,
                        ModifiedOn = review.ModifiedOn,
                        IsActive = review.IsActive
                    });
                }
                return reviewViewModelLong;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Get ReviewModel by Id
        public ReviewViewModel GetReviewById(long id)
        {
            try
            {
                Review review = _reviewRepository.GetById(id);
                var reviewViewModel = new ReviewViewModel()
                {
                    Id = review.Id,
                    UserId = review.UserId,
                    ReviewDate = review.ReviewDate,
                    ProjectId = review.ProjectId,
                    ModuleName = review.ModuleName,
                    FileReviewed = review.FileReviewed,
                    MethodsReviewed = review.MethodsReviewed,
                    Remarks = review.Remarks,
                    Status = review.Status,
                    CodeOptimizationRating = review.CodeOptimizationRating,
                    CodingStandardRating = review.CodingStandardRating,
                    QueryOptimizationRating = review.QueryOptimizationRating,
                    ProjectArchitecture = review.ProjectArchitecture,
                    CreatedBy = review.CreatedBy,
                    ModifiedBy = review.ModifiedBy,
                    CreatedOn = review.CreatedOn,
                    ModifiedOn = review.ModifiedOn,
                    IsActive = review.IsActive,
                };
                return reviewViewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Add New Review
        public bool AddReview(ReviewViewModel reviewViewModel)
        {
            try
            {
                var review = new Review()
                {
                    Id = reviewViewModel.Id,
                    UserId = 1, // Need to discuss
                    ReviewDate = reviewViewModel.ReviewDate,
                    ProjectId = 1, // Need to discuss
                    ModuleName = reviewViewModel.ModuleName,
                    FileReviewed = reviewViewModel.FileReviewed,
                    MethodsReviewed = reviewViewModel.MethodsReviewed,
                    Remarks = reviewViewModel.Remarks,
                    Status = reviewViewModel.Status,
                    CodeOptimizationRating = reviewViewModel.CodeOptimizationRating,
                    CodingStandardRating = reviewViewModel.CodingStandardRating,
                    QueryOptimizationRating = reviewViewModel.QueryOptimizationRating,
                    ProjectArchitecture = reviewViewModel.ProjectArchitecture,
                    CreatedBy = SessionManager.GetCurrentlyLoggedInUserId(),
                    CreatedOn = DateTime.Now,
                    //ModifiedBy = 1,
                    //ModifiedOn = DateTime.Now,
                    IsActive = true
                };

                var responsemodel = _reviewRepository.Add(review);
                if (responsemodel != null)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Update Review 
        public bool SaveOrUpdateReview(ReviewViewModel reviewViewModel)
        {
            try
            {
                Review review = _reviewRepository.GetById(reviewViewModel.Id);
                if( review!= null)
                {                    
                    review.UserId = 1; // Need to discuss
                    review.ReviewDate = reviewViewModel.ReviewDate;
                    review.ProjectId = 1; // Need to discuss
                    review.ModuleName = reviewViewModel.ModuleName;
                    review.FileReviewed = reviewViewModel.FileReviewed;
                    review.MethodsReviewed = reviewViewModel.MethodsReviewed;
                    review.Remarks = reviewViewModel.Remarks;
                    review.Status = reviewViewModel.Status;
                    review.CodeOptimizationRating = reviewViewModel.CodeOptimizationRating;
                    review.CodingStandardRating = reviewViewModel.CodingStandardRating;
                    review.QueryOptimizationRating = reviewViewModel.QueryOptimizationRating;
                    review.ProjectArchitecture = reviewViewModel.ProjectArchitecture;
                    review.ModifiedBy = SessionManager.GetCurrentlyLoggedInUserId();
                    review.ModifiedOn = DateTime.Now;                    
                
                    Review responseModel = _reviewRepository.SaveOrUpdate(review);
                    if (responseModel != null)                   
                        return true;
                }                
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Delete Review
        public bool DeleteReview(long id)
        {
            try
            {
                var response = _reviewRepository.Delete(id);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
