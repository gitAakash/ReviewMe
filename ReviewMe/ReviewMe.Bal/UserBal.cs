using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    userViewModelLong.UserViewModelList.Add(new UserViewModel()
                    {
                        Id = user.Id,
                        FName = user.FName,
                        LName = user.FName,
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
                        EmployeeCode =  user.EmployeeCode,
                        TeamLeaderId = user.TeamLeaderId,
                        RoleId = user.RoleId,
                        TechnologyId = user.TechnologyId,
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

        // Get User By Id
        public UserViewModel GetUserById(long id)
        {
            try
            {
                User user = _userRepository.GetById(id);
                var userViewModel = new UserViewModel()
                {
                    Id = user.Id,
                    FName = user.FName,
                    LName = user.FName,
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
                    EmployeeCode =  user.EmployeeCode,
                    TeamLeaderId = user.TeamLeaderId,
                    RoleId = user.RoleId,
                    TechnologyId = user.TechnologyId,
                    OnClient = user.OnClient,
                    OnProject = user.OnProject,
                    OnTask = user.OnTask,
                    Rating = user.Rating,
                    CreatedBy = user.CreatedBy,
                    ModifiedBy = user.ModifiedBy,
                    CreatedOn = user.CreatedOn,
                    ModifiedOn = user.ModifiedOn,
                    IsActive = user.IsActive
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
                var user = new User()
                {
                    Id = userViewModel.Id,
                    FName = userViewModel.FName,
                    LName = userViewModel.FName,
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
                    TeamLeaderId = userViewModel.TeamLeaderId,
                    RoleId = userViewModel.RoleId,
                    TechnologyId = userViewModel.TechnologyId,
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
                var responsemodel = _userRepository.Add(user);
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
                var user = new User()
                {
                    Id = userViewModel.Id,
                    FName = userViewModel.FName,
                    LName = userViewModel.FName,
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
                    TeamLeaderId = userViewModel.TeamLeaderId,
                    RoleId = userViewModel.RoleId,
                    TechnologyId = userViewModel.TechnologyId,
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
                var response = _userRepository.Delete(id);
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