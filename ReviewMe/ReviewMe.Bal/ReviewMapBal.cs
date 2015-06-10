using ReviewMe.DataAccess;
using ReviewMe.DataAccess.Repository;
using ReviewMe.Model;
using ReviewMe.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReviewMe.ViewModel;
using ReviewMe.Common.Authorization;
using ReviewMe.Common.Extensions;
using System.Globalization;

namespace ReviewMe.Bal
{
    public class ReviewMapBal
    {
        private readonly Repository<ReviewMap> _reviewMapRepository = new Repository<ReviewMap>(new EntityContext());

        // Get details for Add Group form
        public ReviewMapViewModel GetAddReviewMapDetails()
        {
            try
            {
                List<User> userList = new UserBal().GetListOfUserByTeamLeadId(SessionManager.GetCurrentlyLoggedInUserId());

                var reviewMapViewModel = new ReviewMapViewModel()
                {
                    DropDownForReviewer = userList.Select(p => new SerializableSelectListItem()
                    {
                        Text = p.FName,
                        Value = p.Id.ToString(CultureInfo.InvariantCulture)
                    }),
                    DropDownForReviewee = userList.Select(p => new SerializableSelectListItem()
                    {
                        Text = p.FName,
                        Value = p.Id.ToString(CultureInfo.InvariantCulture)
                    })
                };

                return reviewMapViewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Get all ReviewMaps
        public ReviewMapViewModelLong GetAllReviewMaps()
        {
            try
            {
                List<ReviewMap> reviewMapList = _reviewMapRepository.GetAll().Where(a => a.IsActive).ToList();

                var reviewMapViewModelLong = new ReviewMapViewModelLong();

                foreach (ReviewMap reviewMap in reviewMapList)
                {
                    reviewMapViewModelLong.ReviewMapViewModelList.Add(new ReviewMapViewModel 
                    {
                        Id = reviewMap.Id,
                        ReviewerId = reviewMap.ReviewerId,
                        DevloperId = reviewMap.DevloperId,
                        CreatedBy = reviewMap.CreatedBy,
                        ModifiedBy = reviewMap.ModifiedBy,
                        CreatedOn = reviewMap.CreatedOn,
                        ModifiedOn = reviewMap.ModifiedOn,
                    });
                }

                return reviewMapViewModelLong;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Get ReviewMapById
        public ReviewMapViewModel GetReviewMapById(long id)
        {
            try
            {
                ReviewMap reviewMap = _reviewMapRepository.GetById(id);

                var reviewMapModel = new ReviewMapViewModel 
                {
                    Id = reviewMap.Id,
                    ReviewerId = reviewMap.ReviewerId,
                    DevloperId = reviewMap.DevloperId,
                    CreatedBy = reviewMap.CreatedBy,
                    ModifiedBy = reviewMap.ModifiedBy,
                    CreatedOn = reviewMap.CreatedOn,
                    ModifiedOn = reviewMap.ModifiedOn,
                    IsActive = reviewMap.IsActive
                };

                return reviewMapModel;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        // Get By ReviewerId
        public ReviewMapViewModel GetReviewMapByReviewerId(long id)
        {
            try
            {
                ReviewMap reviewMap = _reviewMapRepository.GetAll().FirstOrDefault(a => a.ReviewerId == id);

                var reviewMapModel = new ReviewMapViewModel { 
                    Id = reviewMap.Id,
                    ReviewerId = reviewMap.ReviewerId,
                    DevloperId = reviewMap.DevloperId,
                    CreatedBy = reviewMap.CreatedBy,
                    ModifiedBy = reviewMap.ModifiedBy,
                    CreatedOn = reviewMap.CreatedOn,
                    ModifiedOn = reviewMap.ModifiedOn,
                    IsActive = reviewMap.IsActive
                };

                return reviewMapModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Add New ReviewMap
        public bool AddReviewMap(ReviewMapViewModel reviewMapViewModel)
        {
            try
            {
                Int64 ReviewerId = reviewMapViewModel.ReviewerId;
                Int64 RevieweeId = 0;
                string[] arr = reviewMapViewModel.SelectedListValues.Split(',');

                foreach (string item in arr)
                {
                    RevieweeId = Convert.ToInt64(item);

                    var reviewMapModel = new ReviewMap 
                    { 
                        ReviewerId = ReviewerId,
                        DevloperId = RevieweeId,
                        CreatedBy = SessionManager.GetCurrentlyLoggedInUserId(),
                        CreatedOn = DateTime.Now,
                        IsActive = true
                    };

                    ReviewMap responseModel = _reviewMapRepository.Add(reviewMapModel);
                    _reviewMapRepository.SaveChanges();
                }

                return true;

                //var reviewMapModel1 = new ReviewMap
                //{
                //    Id = reviewMapViewModel.Id,
                //    ReviewerId = reviewMapViewModel.ReviewerId,
                //    DevloperId = reviewMapViewModel.DevloperId,
                //    CreatedBy = SessionManager.GetCurrentlyLoggedInUserId(),
                //    CreatedOn = DateTime.Now,
                //    //ModifiedBy = 1,
                //    //ModifiedOn = DateTime.Now,
                //    IsActive = true
                //};
                //ReviewMap responseModel1 = _reviewMapRepository.Add(reviewMapModel1);
                //_reviewMapRepository.SaveChanges();

                //if (responseModel1 != null)
                //    return true;
                //return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }

        // Update ReviewMap
        public bool SaveOrUpdateRole(ReviewMapViewModel reviewMapViewModel)
        {
            try
            {
                ReviewMap reviewMap = _reviewMapRepository.GetById(reviewMapViewModel.Id);
                if (reviewMap != null)
                {
                    reviewMap.ReviewerId = reviewMapViewModel.ReviewerId;
                    reviewMap.DevloperId = reviewMapViewModel.DevloperId;

                    reviewMap.ModifiedBy = SessionManager.GetCurrentlyLoggedInUserId();
                    reviewMap.ModifiedOn = DateTime.Now;

                    ReviewMap responseModel = _reviewMapRepository.SaveOrUpdate(reviewMap);

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
    }
}
