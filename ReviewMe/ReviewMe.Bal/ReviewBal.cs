using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReviewMe.DataAccess;
using ReviewMe.DataAccess.Repository;
using ReviewMe.Model;

namespace ReviewMe.Bal
{
    public class ReviewBal
    {
        private readonly Repository<Review> _reviewRepository = new Repository<Review>(new EntityContext());

        public List<Review> GetAllReviews()
        {
            try
            {
                List<Review> reviewList = _reviewRepository.GetAll();
                return reviewList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Review GetReviewById(long id)
        {
            try
            {
                Review review = _reviewRepository.GetById(id);
                return review;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AddTechnology(Review review)
        {
            try
            {
                var model = _reviewRepository.Add(review);
                if (model != null)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Review SaveOrUpdateReview(Review review)
        {
            try
            {
            Review entity = _reviewRepository.SaveOrUpdate(review);
            return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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
