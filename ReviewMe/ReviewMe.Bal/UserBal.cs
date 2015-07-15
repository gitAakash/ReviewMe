using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ReviewMe.Common.Authorization;
using ReviewMe.Common.Enums;
using ReviewMe.Common.Extensions;
using ReviewMe.DataAccess;
using ReviewMe.DataAccess.Repository;
using ReviewMe.Model;
using ReviewMe.ViewModel;

namespace ReviewMe.Bal
{
    public class UserBal
    {
        private readonly Repository<User> _userRepository = new Repository<User>(new EntityContext());

        // Get All Users
        public UserViewModelLong GetAllUsers()
        {
            try
            {
                List<User> userList = _userRepository.GetAll().Where(m => m.IsActive).ToList();
                var userViewModelLong = new UserViewModelLong();
                //Modified By : Ramchandra Rane, 19th June 2015, Description: Added Where condition.
                foreach (User user in userList.Where(r => r.Id != SessionManager.GetCurrentlyLoggedInUserId()))
                {
                    userViewModelLong.UserViewModelList.Add(new UserViewModel
                    {
                        Id = user.Id,
                        FName = user.FName,
                        LName = user.LName,
                        MName = user.MName,
                        Dob = user.Dob,
                        Gender = user.Gender,
                        EmailId = user.EmailId,
                        Password = user.Password,
                        MobileNo = user.MobileNo,
                        AlternateContactNo = user.AlternateContactNo,
                        UserImage = user.UserImage,
                        Address = user.Address,
                        EmployeeCode = user.EmployeeCode,
                        SelectedTeamLeadId = user.TeamLeaderId,
                        SelectedRoleId = user.RoleId,
                        SelectedTechnologyId = user.TechnologyId,
                        OnClient = user.OnClient,
                        OnProject = user.OnProject,
                        OnTask = user.OnTask,
                        Rating = user.Rating,
                        ResetPassword = user.ResetPassword,
                        CreatedBy = user.CreatedBy,
                        ModifiedBy = user.ModifiedBy,
                        CreatedOn = user.CreatedOn,
                        ModifiedOn = user.ModifiedOn,
                        IsActive = user.IsActive,
                        RoleName = user.Role.RoleName
                    });
                }
                return userViewModelLong;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Get List of all TeamLeaders
        public UserViewModelLong GetAllTeamLeaders()
        {
            List<User> userList = _userRepository.GetAll().Where(m => m.RoleId == 2).ToList();
            var userViewModelLong = new UserViewModelLong();
            foreach (User user in userList)
            {
                userViewModelLong.UserViewModelList.Add(new UserViewModel
                {
                    Id = user.Id,
                    FName = user.FName,
                    LName = user.LName,
                    MName = user.MName
                });
            }
            return userViewModelLong;
        }

        // Get User By Id
        public UserViewModel GetUserById(long id)
        {
            try
            {
                User user = _userRepository.GetById(id);
                RoleViewModelLong roleViewModelLong = new RoleBal().GetAllRoles();
                UserViewModelLong userViewModelLong = GetAllTeamLeaders();

                TechnologyViewModelLong technologyViewModelLong = new TechnologyBal().GetAllTechnologies();
                if (user != null)
                {
                    var userViewModel = new UserViewModel
                    {
                        Id = user.Id,
                        FName = user.FName,
                        LName = user.LName,
                        MName = user.MName,
                        Dob = user.Dob,
                        Gender = user.Gender,
                        EmailId = user.EmailId,
                        Password = user.Password,
                        MobileNo = user.MobileNo,
                        AlternateContactNo = user.AlternateContactNo,
                        UserImage = user.UserImage,
                        Address = user.Address,
                        EmployeeCode = user.EmployeeCode,
                        SelectedTeamLeadId = user.TeamLeaderId,
                        SelectedRoleId = user.RoleId,
                        SelectedTechnologyId = user.TechnologyId,
                        OnClient = user.OnClient,
                        OnProject = user.OnProject,
                        OnTask = user.OnTask,
                        Rating = user.Rating,
                        CreatedBy = user.CreatedBy,
                        ModifiedBy = user.ModifiedBy,
                        CreatedOn = user.CreatedOn,
                        ModifiedOn = user.ModifiedOn,
                        IsActive = user.IsActive,
                        ResetPassword = user.ResetPassword,
                        DropDownForRoles = roleViewModelLong.RoleViewModelList.Select(c => new SerializableSelectListItem
                        {
                            Text = c.RoleName,
                            Value = c.Id.ToString(CultureInfo.InvariantCulture)
                        }),
                        DropDownForTechnology =
                            technologyViewModelLong.TechnologyViewModelList.Select(c => new SerializableSelectListItem
                            {
                                Text = c.TechnologyName,
                                Value = c.Id.ToString(CultureInfo.InvariantCulture)
                            }),
                        DropDownForTeamLeader =
                            userViewModelLong.UserViewModelList.Select(c => new SerializableSelectListItem
                            {
                                Text = c.FName,
                                Value = c.Id.ToString(CultureInfo.InvariantCulture)
                            })
                    };
                    return userViewModel;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public UserViewModel GetAddUserViewModel()
        {
            try
            {
                RoleViewModelLong roleViewModelLong = new RoleBal().GetAllRoles();
                TechnologyViewModelLong technologyViewModelLong = new TechnologyBal().GetAllTechnologies();
                UserViewModelLong userViewModelLong = GetAllTeamLeaders();

                var userViewModel = new UserViewModel
                {
                    DropDownForRoles = roleViewModelLong.RoleViewModelList.Select(c => new SerializableSelectListItem
                    {
                        Text = c.RoleName,
                        Value = c.Id.ToString(CultureInfo.InvariantCulture)
                    }),
                    DropDownForTechnology =
                        technologyViewModelLong.TechnologyViewModelList.Select(c => new SerializableSelectListItem
                        {
                            Text = c.TechnologyName,
                            Value = c.Id.ToString(CultureInfo.InvariantCulture)
                        }),
                    DropDownForTeamLeader =
                        userViewModelLong.UserViewModelList.Select(c => new SerializableSelectListItem
                        {
                            Text = c.FName,
                            Value = c.Id.ToString(CultureInfo.InvariantCulture)
                        }),
                    Gender = true
                };

                return userViewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Get UserModel according to Email and Password for Authentication
        public UserViewModel GetAuthenticateUserViewModel(string email, string password)
        {
            try
            {
                var userViewModel = new UserViewModel();
                //User user = _userRepository.GetAll();
                User user = _userRepository.GetAll().SingleOrDefault(m => m.EmailId == email && m.Password == password);
                if (user != null)
                {
                    userViewModel.Id = user.Id;
                    userViewModel.FName = user.FName;
                    userViewModel.LName = user.LName;
                    userViewModel.MName = user.MName;
                    userViewModel.Dob = user.Dob;
                    userViewModel.Gender = user.Gender;
                    userViewModel.EmailId = user.EmailId;
                    userViewModel.Password = user.Password;
                    userViewModel.MobileNo = user.MobileNo;
                    userViewModel.AlternateContactNo = user.AlternateContactNo;
                    userViewModel.UserImage = user.UserImage;
                    userViewModel.Address = user.Address;
                    userViewModel.EmployeeCode = user.EmployeeCode;
                    userViewModel.SelectedTeamLeadId = user.TeamLeaderId;
                    userViewModel.SelectedRoleId = user.RoleId;
                    userViewModel.SelectedTechnologyId = user.TechnologyId;
                    userViewModel.OnClient = user.OnClient;
                    userViewModel.OnProject = user.OnProject;
                    userViewModel.OnTask = user.OnTask;
                    userViewModel.Rating = user.Rating;
                    userViewModel.CreatedBy = user.CreatedBy;
                    userViewModel.ModifiedBy = user.ModifiedBy;
                    userViewModel.CreatedOn = user.CreatedOn;
                    userViewModel.ModifiedOn = user.ModifiedOn;
                    userViewModel.IsActive = user.IsActive;
                    return userViewModel;
                }
                //Added By : Ramchadra Rane, 13th June 2015
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Add User
        public User AddUser(UserViewModel userViewModel)
        {
            try
            {
                var user = new User
                {
                    Id = userViewModel.Id,
                    FName = userViewModel.FName,
                    LName = userViewModel.LName,
                    MName = userViewModel.MName,
                    Dob = userViewModel.Dob,
                    Gender = userViewModel.Gender,
                    EmailId = userViewModel.EmailId,
                    Password = userViewModel.Password,
                    MobileNo = userViewModel.MobileNo,
                    AlternateContactNo = userViewModel.AlternateContactNo,
                    UserImage = userViewModel.UserImage,
                    Address = userViewModel.Address,
                    EmployeeCode = userViewModel.EmployeeCode,
                    TeamLeaderId = userViewModel.SelectedTeamLeadId,
                    RoleId = userViewModel.SelectedRoleId,
                    TechnologyId = userViewModel.SelectedTechnologyId,
                    OnClient = userViewModel.OnClient,
                    OnProject = userViewModel.OnProject,
                    OnTask = userViewModel.OnTask,
                    Rating = userViewModel.Rating,
                    CreatedBy = SessionManager.GetCurrentlyLoggedInUserId(),
                    //ModifiedBy = 1,
                    CreatedOn = DateTime.Now,
                    //ModifiedOn = DateTime.Now,
                    IsActive = true
                };
                User responsemodel = _userRepository.Add(user);
               if (responsemodel != null)
                return responsemodel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        // Update User
        public bool SaveOrUpdateUser(UserViewModel userViewModel)
        {
            try
            {
                User user = _userRepository.GetById(userViewModel.Id);

                if (user != null)
                {
                    user.FName = userViewModel.FName;
                    user.LName = userViewModel.LName;
                    user.MName = userViewModel.MName;
                    user.Dob = userViewModel.Dob;
                    user.Gender = userViewModel.Gender;
                    user.EmailId = userViewModel.EmailId;
                    user.MobileNo = userViewModel.MobileNo;
                    user.AlternateContactNo = userViewModel.AlternateContactNo;
                    if (userViewModel.UserImage != null)
                    {
                        user.UserImage = userViewModel.UserImage;
                    }
                    user.Address = userViewModel.Address;
                    user.TeamLeaderId = userViewModel.SelectedTeamLeadId;
                    user.RoleId = userViewModel.SelectedRoleId;
                    user.TechnologyId = userViewModel.SelectedTechnologyId;
                    user.OnClient = userViewModel.OnClient;
                    user.OnProject = userViewModel.OnProject;
                    user.OnTask = userViewModel.OnTask;
                    user.Rating = userViewModel.Rating;
                    user.ModifiedBy = SessionManager.GetCurrentlyLoggedInUserId();
                    user.ModifiedOn = DateTime.Now;

                    User responsemodel = _userRepository.SaveOrUpdate(user);

                    if (responsemodel != null)
                        return true;
                }
                ;

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public User UpdateUser(UserViewModel userViewModel)
        {
            try
            {
                User user = _userRepository.GetById(userViewModel.Id);

                if (user != null)
                {
                    user.FName = userViewModel.FName;
                    user.LName = userViewModel.LName;
                    user.MName = userViewModel.MName;
                    user.Dob = userViewModel.Dob;
                    user.Gender = userViewModel.Gender;
                    user.EmailId = userViewModel.EmailId;
                    user.MobileNo = userViewModel.MobileNo;
                    user.AlternateContactNo = userViewModel.AlternateContactNo;
                    user.UserImage = userViewModel.UserImage;
                    user.Address = userViewModel.Address;

                    user.OnClient = userViewModel.OnClient;
                    user.OnProject = userViewModel.OnProject;
                    user.OnTask = userViewModel.OnTask;

                    user.ModifiedBy = 1;
                    user.ModifiedOn = DateTime.Now;

                    User responsemodel = _userRepository.SaveOrUpdate(user);

                    return responsemodel;
                }
                ;

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Delete User
        public bool DeleteUser(long id)
        {
            try
            {
                bool response = _userRepository.Delete(id);
                _userRepository.SaveChanges();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Get ReviewerList By TeamLeaderId
        public List<UserViewModel> GetListOfUserByTeamLeadId(long id)
        {
            try
            {
                List<User> lstReviewer =
                    _userRepository.GetAll().Where(m => m.IsActive && (m.TeamLeaderId == id || m.Id == id)).ToList();
                
                if (lstReviewer != null)
                {
                    UserViewModelLong userViewModelLong = new UserViewModelLong();
                    foreach (User user in lstReviewer)
                    {
                        userViewModelLong.UserViewModelList.Add(new UserViewModel
                        {
                            Id = user.Id,
                            FName = user.FName,
                            LName = user.LName,
                            MName = user.MName,
                            Dob = user.Dob,
                            Gender = user.Gender,
                            EmailId = user.EmailId,
                            Password = user.Password,
                            MobileNo = user.MobileNo,
                            AlternateContactNo = user.AlternateContactNo,
                            UserImage = user.UserImage,
                            Address = user.Address,
                            EmployeeCode = user.EmployeeCode,
                            SelectedTeamLeadId = user.TeamLeaderId,
                            SelectedRoleId = user.RoleId,
                            SelectedTechnologyId = user.TechnologyId,
                            OnClient = user.OnClient,
                            OnProject = user.OnProject,
                            OnTask = user.OnTask,
                            Rating = user.Rating,
                            ResetPassword = user.ResetPassword,
                            CreatedBy = user.CreatedBy,
                            ModifiedBy = user.ModifiedBy,
                            CreatedOn = user.CreatedOn,
                            ModifiedOn = user.ModifiedOn,
                            IsActive = user.IsActive,
                            RoleName = user.Role.RoleName
                        });
                    }

                    return userViewModelLong.UserViewModelList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        // Get all UsersList Who are not Admin
        public List<User> GetListOfUsersExceptAdmin()
        {
            try
            {
                List<User> lstReviewer =
                    _userRepository.GetAll()
                        .Where(m => m.IsActive && m.Role.RoleName != UserRoleEnum.Admin.ToString())
                        .ToList();
                if (lstReviewer != null)
                {
                return lstReviewer;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        // Get User By EmailId for ForgotPassword functionality
        public UserViewModel GetUserByEmailId(string emailId)
        {
            try
            {
                User user = _userRepository.GetAll().FirstOrDefault(m => m.EmailId == emailId && m.IsActive);
                var uvModel = new UserViewModel();
                if (user != null)
                {
                    uvModel.Id = user.Id;
                    uvModel.FName = user.FName;
                    uvModel.LName = user.LName;
                    uvModel.MName = user.MName;
                    uvModel.Dob = user.Dob;
                    uvModel.Gender = user.Gender;
                    uvModel.EmailId = user.EmailId;
                    uvModel.Password = user.Password;
                    uvModel.MobileNo = user.MobileNo;
                    uvModel.AlternateContactNo = user.AlternateContactNo;
                    uvModel.UserImage = user.UserImage;
                    uvModel.Address = user.Address;
                    uvModel.EmployeeCode = user.EmployeeCode;
                    uvModel.SelectedTeamLeadId = user.TeamLeaderId;
                    uvModel.SelectedRoleId = user.RoleId;
                    uvModel.SelectedTechnologyId = user.TechnologyId;
                    uvModel.OnClient = user.OnClient;
                    uvModel.OnProject = user.OnProject;
                    uvModel.OnTask = user.OnTask;
                    uvModel.Rating = user.Rating;
                    uvModel.ResetPassword = user.ResetPassword;
                    uvModel.CreatedBy = user.CreatedBy;
                    uvModel.ModifiedBy = user.ModifiedBy;
                    uvModel.CreatedOn = user.CreatedOn;
                    uvModel.ModifiedOn = user.ModifiedOn;
                    uvModel.IsActive = user.IsActive;
                    uvModel.RoleName = user.Role.RoleName;
                }

                return uvModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // To update the password
        public bool UpdatePassword(Int64 id, string password)
        {
            try
            {
                User user = _userRepository.GetById(id);
                if (user != null)
                {
                    user.Password = password;
                    user.ResetPassword = false;
                    user.ModifiedBy = id;
                    user.ModifiedOn = DateTime.Now;
                    User responsemodel = _userRepository.SaveOrUpdate(user);
                    if (responsemodel != null)
                        return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateResetPassword(Int64 id, int resetPassword)
        {
            try
            {
                User user = _userRepository.GetById(id);
                if (user != null)
                {
                    user.ResetPassword = Convert.ToBoolean(resetPassword);
                    User responsemodel = _userRepository.SaveOrUpdate(user);
                    if (responsemodel != null)
                        return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }

        public UserViewModelLong GetUsersByTechnology(long id)
        {
            var users = _userRepository.GetAll().Where(m=>m.TechnologyId==id && m.IsActive==true).ToList();
            UserViewModelLong usersModel = new UserViewModelLong();
             foreach (User user in users)
                {
                    usersModel.UserViewModelList.Add(new UserViewModel
                    {
                        Id = user.Id,
                        FName = user.FName,
                        LName = user.LName,
                    });
                }
            return usersModel;
        }
    }
}