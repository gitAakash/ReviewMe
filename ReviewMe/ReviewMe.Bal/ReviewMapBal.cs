using AutoMapper;
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
        private readonly Repository<ReviewDetails> _reviewDetailsRepository = new Repository<ReviewDetails>(new EntityContext());
        private readonly Repository<User> _UserRepository = new Repository<User>(new EntityContext());

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

        // Get Reviewee List
        public ReviewMapViewModel GetRevieweeBalList(Int64 ReviewerId, string IsEdit)
        {
            try
            {
                List<User> userList = new UserBal().GetListOfUserByTeamLeadId(SessionManager.GetCurrentlyLoggedInUserId());
                List<Int64> lstDevelopers = new ReviewMapBal().GetAlreadyReviewedList();

                // Create
                if (IsEdit == "2")
                {
                    var reviewMapViewModel = new ReviewMapViewModel()
                    {
                        DropDownForReviewee = userList.Where(p => p.Id != ReviewerId && !lstDevelopers.Contains(p.Id)).Select(p => new SerializableSelectListItem()
                        {
                            Text = p.FName,
                            Value = p.Id.ToString(CultureInfo.InvariantCulture)
                        })
                    };

                    return reviewMapViewModel;
                }
                else // Edit
                {
                    List<Int64> lstReviewee = _reviewMapRepository.GetAll().Where(p => p.ReviewerId == ReviewerId && p.IsActive == true).Select(r => r.DevloperId).ToList();

                    // Get list of all the users who are not yet assigned any Reviewer
                    List<Int64> alreadyAssignedList = _reviewMapRepository.GetAll().Where(p => p.IsActive == true).Select(q => q.DevloperId).ToList();

                    List<User> lstAllUsers = new UserBal().GetListOfUsersExceptAdmin();
                    foreach (User rec in lstAllUsers)
                    {
                        if (!alreadyAssignedList.Contains(rec.Id))
                        {
                            lstReviewee.Add(rec.Id);
                        }
                    }

                    var reviewMapViewModel = new ReviewMapViewModel()
                    {
                        DropDownForReviewer = null,
                        ReviewerId = ReviewerId,
                        DropDownForReviewee = userList.Where(r => lstReviewee.Contains(r.Id)).Select(p => new SerializableSelectListItem()
                        {
                            Text = p.FName,
                            Value = p.Id.ToString(CultureInfo.InvariantCulture)
                        })
                    };

                    return reviewMapViewModel;
                }


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
            catch (Exception ex)
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ReviewMapViewModel> GetRevieweeByReviewerId(long id)
        {
            var ReviewMapViewModelList = new List<ReviewMapViewModel>();

            try
            {
                var reviewMapList = _reviewMapRepository.GetAll().Where(a => a.ReviewerId == id && a.IsActive == true).ToList();
                foreach (var item in reviewMapList)
                {
                    ReviewMapViewModel reviewMapViewModel =
                       Mapper.Map<ReviewMap, ReviewMapViewModel>(item);
                    ReviewMapViewModelList.Add(reviewMapViewModel);
                }
                return ReviewMapViewModelList;
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
        }

        // Edit ReviewMap
        public bool EditReviewMap(ReviewMapViewModel reviewMapViewModel)
        {
            try
            {
                List<Int64> lstOriginal = reviewMapViewModel.EditOriginalReviewee.Split(',').Select(Int64.Parse).ToList();
                List<Int64> lstSelected = reviewMapViewModel.SelectedListValues.Split(',').Select(Int64.Parse).ToList();
                Int64 ReviewerId = reviewMapViewModel.ReviewerId;

                foreach (Int64 item in lstSelected)
                {
                    if (!lstOriginal.Contains(item))
                    {
                        var reviewMapModel = new ReviewMap
                        {
                            ReviewerId = ReviewerId,
                            DevloperId = item,
                            CreatedBy = SessionManager.GetCurrentlyLoggedInUserId(),
                            CreatedOn = DateTime.Now,
                            IsActive = true
                        };

                        ReviewMap responseModel = _reviewMapRepository.Add(reviewMapModel);
                        _reviewMapRepository.SaveChanges();
                    }
                }

                List<ReviewMap> lstReviewMap = _reviewMapRepository.GetAll();
                foreach (Int64 item in lstOriginal)
                {
                    if (!lstSelected.Contains(item))
                    {
                        ReviewMap reviewMap = lstReviewMap.FirstOrDefault(p => p.DevloperId == item && p.ReviewerId == ReviewerId && p.IsActive == true);
                        _reviewMapRepository.Delete(reviewMap);
                        _reviewMapRepository.SaveChanges();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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

        // Get List of Developers who already got Reviewer assigned.
        public List<Int64> GetAlreadyReviewedList()
        {
            try
            {
                List<Int64> lstReviewer = _reviewMapRepository.GetAll().Where(m => m.IsActive == true).Select(p => p.DevloperId).ToList();
                return lstReviewer;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Get ReviewGroup Details
        public List<ReviewGroupViewModel> GetReviewGroupDetails()
        {
            try
            {
                List<ReviewGroupViewModel> lstReviewGroupDetails = new List<ViewModel.ReviewGroupViewModel>();
                List<ReviewMap> lstReviewer = _reviewMapRepository.GetAll().Where(p => p.IsActive == true).ToList();

                foreach (ReviewMap item in lstReviewer)
                {
                    if (lstReviewGroupDetails.Select(p => p.Reviewer.Id).Contains(item.ReviewerId))
                    {
                        lstReviewGroupDetails.FirstOrDefault(p => p.Reviewer.Id == item.ReviewerId).Reviewees.Add(new UserGroupViewModel { Id = item.Id, Name = item.DeveloperUser.FName + " " + item.DeveloperUser.LName });
                    }
                    else
                    {
                        lstReviewGroupDetails.Add(new ReviewGroupViewModel { Reviewer = new UserGroupViewModel { Id = item.ReviewerUser.Id, Name = item.ReviewerUser.FName + " " + item.ReviewerUser.LName }, Reviewees = new List<ViewModel.UserGroupViewModel>() { new UserGroupViewModel { Id = item.DeveloperUser.Id, Name = item.DeveloperUser.FName + " " + item.DeveloperUser.LName } } });
                    }
                }

                return lstReviewGroupDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Delete UserMap by ReviewerId
        public bool DeleteUserMapByReviewerId(Int64 id)
        {
            try
            {
                List<ReviewMap> lstRev = _reviewMapRepository.GetAll().Where(p => p.ReviewerId == id && p.IsActive == true).ToList();
                foreach (ReviewMap rec in lstRev)
                {
                    _reviewMapRepository.Delete(rec);
                    _reviewMapRepository.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Get Edit ReviewerMap group Edit Details
        public ReviewMapViewModel GetEditReviewMapById(long id)
        {
            try
            {
                UserViewModel reviewer = new UserBal().GetUserById(id);
                List<Int64> lstReviewee = _reviewMapRepository.GetAll().Where(p => p.ReviewerId == id && p.IsActive == true).Select(r => r.DevloperId).ToList();

                string OriginalReviewees = string.Join(",", lstReviewee);

                // Get list of all the users who are not yet assigned any Reviewer
                List<Int64> alreadyAssignedList = _reviewMapRepository.GetAll().Where(p => p.IsActive == true).Select(q => q.DevloperId).ToList();

                List<User> lstAllUsers = new UserBal().GetListOfUsersExceptAdmin();
                foreach (User rec in lstAllUsers)
                {
                    if (!alreadyAssignedList.Contains(rec.Id))
                    {
                        lstReviewee.Add(rec.Id);
                    }
                }

                List<User> userList = new UserBal().GetListOfUserByTeamLeadId(SessionManager.GetCurrentlyLoggedInUserId());

                var reviewMapViewModel = new ReviewMapViewModel()
                {
                    DropDownForReviewer = new List<SerializableSelectListItem>() { new SerializableSelectListItem() { Text = reviewer.FName, Value = reviewer.Id.ToString(CultureInfo.InvariantCulture) } },
                    ReviewerId = id,
                    DropDownForReviewee = userList.Where(r => lstReviewee.Contains(r.Id)).Select(p => new SerializableSelectListItem()
                    {
                        Text = p.FName,
                        Value = p.Id.ToString(CultureInfo.InvariantCulture)
                    }),
                    EditOriginalReviewee = OriginalReviewees
                };

                return reviewMapViewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Added By    : Ramchandra Rane, 23rd June 2015
        /// Description : Get All Review by using reviewee id.
        /// </summary>
        /// <param name="revieweeId"></param>
        /// <returns></returns>
        public ReviewDetailsViewModel GetReviewDetailsByRevieweeId(long revieweeId, long reviewerId, DateTime startDate, DateTime endDate)
        {
            try
            {
                List<ReviewDetails> reviewDetailsList = _reviewDetailsRepository.GetAll().Where(a => a.IsActive && a.RevieweeId == revieweeId && a.ReviewerId == reviewerId && a.ReviewDate >= startDate && a.ReviewDate <= endDate).ToList();

                var reviewDetailsViewModel = new ReviewDetailsViewModel();

                foreach (ReviewDetails reviewDetails in reviewDetailsList)
                {
                    reviewDetailsViewModel.ReviewDetailsViewModelList.Add(new ReviewDetailsViewModel
                    {
                        Id = reviewDetails.Id,
                        ReviewerId = reviewDetails.ReviewerId,
                        Title = reviewDetails.Title,
                        Comment = reviewDetails.Comment,
                        CreatedBy = reviewDetails.CreatedBy,
                        ReviewDateString = reviewDetails.ReviewDate.Year + "-" + (reviewDetails.ReviewDate.Month <= 9 ? "0" + reviewDetails.ReviewDate.Month : reviewDetails.ReviewDate.Month.ToString()) + "-" + (reviewDetails.ReviewDate.Day <= 9 ? "0" + reviewDetails.ReviewDate.Day : reviewDetails.ReviewDate.Day.ToString()),
                        CreatedOn = reviewDetails.CreatedOn,
                        //ModifiedBy = reviewDetails.ModifiedBy,
                        //ModifiedOn = reviewDetails.ModifiedOn,

                    });
                }

                return reviewDetailsViewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ReviewDetailsViewModel GetAllReviewDetailsByRevieweeId(long reviewerId, DateTime startDate, DateTime endDate)
        {
            try
            {
                List<ReviewDetails> reviewDetailsList = _reviewDetailsRepository.GetAll().Where(a => a.IsActive && a.RevieweeId == reviewerId && a.ReviewDate >= startDate && a.ReviewDate <= endDate).ToList();

                var reviewDetailsViewModel = new ReviewDetailsViewModel();

                foreach (ReviewDetails reviewDetails in reviewDetailsList)
                {
                    reviewDetailsViewModel.ReviewDetailsViewModelList.Add(new ReviewDetailsViewModel
                    {
                        Id = reviewDetails.Id,
                        ReviewerId = reviewDetails.ReviewerId,
                        Title = reviewDetails.Title,
                        Comment = reviewDetails.Comment,
                        CreatedBy = reviewDetails.CreatedBy,
                        ReviewDateString = reviewDetails.ReviewDate.Year + "-" + (reviewDetails.ReviewDate.Month <= 9 ? "0" + reviewDetails.ReviewDate.Month : reviewDetails.ReviewDate.Month.ToString()) + "-" + (reviewDetails.ReviewDate.Day <= 9 ? "0" + reviewDetails.ReviewDate.Day : reviewDetails.ReviewDate.Day.ToString()),
                        CreatedOn = reviewDetails.CreatedOn,

                    });
                }

                return reviewDetailsViewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Add New ReviewDetails
        public bool AddReviewDetails(ReviewDetailsViewModel reviewDetailsViewModel)
        {
            try
            {
                ReviewDetails model = new ReviewDetails
                {
                    Title = reviewDetailsViewModel.Title,
                    Comment = reviewDetailsViewModel.Comment,
                    CreatedBy = SessionManager.GetCurrentlyLoggedInUserId(),
                    CreatedOn = System.DateTime.Now,
                    RevieweeId = reviewDetailsViewModel.RevieweeId,
                    ReviewerId = reviewDetailsViewModel.ReviewerId,
                    ReviewDate = reviewDetailsViewModel.ReviewDate,
                    IsActive = true
                };

                ReviewDetails responseModel = _reviewDetailsRepository.Add(model);
                _reviewMapRepository.SaveChanges();
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
                return false;
            }
        }

        public bool EditReviewDetails(ReviewDetailsViewModel reviewDetailsViewModel)
        {
            try
            {
                ReviewDetails reviewDetails = _reviewDetailsRepository.GetById(reviewDetailsViewModel.Id);
                if (reviewDetails != null)
                {
                    reviewDetails.Comment = reviewDetailsViewModel.Comment;
                    reviewDetails.Title = reviewDetailsViewModel.Title;

                    reviewDetails.ModifiedBy = SessionManager.GetCurrentlyLoggedInUserId();
                    reviewDetails.ModifiedOn = DateTime.Now;

                    ReviewDetails responseModel = _reviewDetailsRepository.SaveOrUpdate(reviewDetails);

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

        // Delete Review by Id
        public bool DeleteReviewById(Int64 id)
        {
            try
            {
                ReviewDetails reviewDetails = _reviewDetailsRepository.GetById(id);
                if (reviewDetails != null)
                {
                    reviewDetails.ModifiedBy = SessionManager.GetCurrentlyLoggedInUserId();
                    reviewDetails.ModifiedOn = DateTime.Now;
                    reviewDetails.IsActive = false;
                    ReviewDetails responseModel = _reviewDetailsRepository.SaveOrUpdate(reviewDetails);
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
        
        /// <summary>
        ///  Added By : Ramchandra Rane
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ReviewDetailsViewModel GetReviewDetailsById(long id)
        {
            try
            {
                ReviewDetails reviewDetails = _reviewDetailsRepository.GetAll().SingleOrDefault(a => a.IsActive && a.Id == id);

                var reviewDetailsViewModel = new ReviewDetailsViewModel();

                if (reviewDetails!=null)
                {
                    reviewDetailsViewModel.Id = reviewDetails.Id;
                    reviewDetailsViewModel.Comment = reviewDetails.Comment;
                    reviewDetailsViewModel.Title = reviewDetails.Title;
                    reviewDetailsViewModel.ReviewDateString = reviewDetails.ReviewDate.ToShortDateString();
                    reviewDetailsViewModel.ReviewerName = _UserRepository.GetAll().SingleOrDefault(r => r.Id == reviewDetails.CreatedBy).FName +" "+_UserRepository.GetAll().SingleOrDefault(r => r.Id == reviewDetails.CreatedBy).LName;
                }

                return reviewDetailsViewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
