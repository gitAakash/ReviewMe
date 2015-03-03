using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
                foreach (User user in userList)
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
                        ConfirmPassword = user.ConfirmPassword,
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
                        IsActive = user.IsActive
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
                    ConfirmPassword = user.ConfirmPassword,
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
                        userViewModelLong.UserViewModelList.Select(c => new SerializableSelectListItem()
                        {
                            Text = c.FName,
                            Value = c.Id.ToString(CultureInfo.InvariantCulture)
                        })
                };

                return userViewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                        userViewModelLong.UserViewModelList.Select(c => new SerializableSelectListItem()
                        {
                            Text = c.FName,
                            Value = c.Id.ToString(CultureInfo.InvariantCulture)
                        })
                };

                return userViewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Add User
        public bool AddUser(UserViewModel userViewModel)
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
                    ConfirmPassword = userViewModel.ConfirmPassword,
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
                    CreatedBy = 1,
                    ModifiedBy = 1,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    IsActive = true
                };
                User responsemodel = _userRepository.Add(user);
                if (responsemodel != null)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Update User
        public bool SaveOrUpdateUser(UserViewModel userViewModel)
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
                    ConfirmPassword = userViewModel.ConfirmPassword,
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
                    CreatedBy = 1,
                    ModifiedBy = 1,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    IsActive = true
                };
                User responsemodel = _userRepository.SaveOrUpdate(user);
                if (responsemodel != null)
                    return true;
                return false;
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
    }
}