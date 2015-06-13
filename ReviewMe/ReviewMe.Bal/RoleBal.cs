using System;
using System.Collections.Generic;
using System.Linq;
using ReviewMe.DataAccess;
using ReviewMe.DataAccess.Repository;
using ReviewMe.Model;
using ReviewMe.ViewModel;
using ReviewMe.Common.Authorization;

namespace ReviewMe.Bal
{
    public class RoleBal
    {
        private readonly Repository<Role> _roleRepository = new Repository<Role>(new EntityContext());

        // Get All Roles
        public RoleViewModelLong GetAllRoles()
        {
            try
            {
                List<Role> roleList = _roleRepository.GetAll().Where(a=>a.IsActive).ToList();
                var roleViewModelLong = new RoleViewModelLong();
                foreach (Role role in roleList)
                {
                    roleViewModelLong.RoleViewModelList.Add(new RoleViewModel
                    {
                        Id = role.Id,
                        RoleName = role.RoleName,
                        CreatedBy = role.CreatedBy,
                        ModifiedBy = role.ModifiedBy,
                        CreatedOn = role.CreatedOn,
                        ModifiedOn = role.ModifiedOn,
                    });
                }
                return roleViewModelLong;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Get Role By Id
        public RoleViewModel GetRoleById(long id)
        {
            try
            {
                Role role = _roleRepository.GetById(id);
                // Added By : Ramchandra Rane, 13th Jun 2015, Issue was exception throws,if role id came to 0
                if(role!=null)
                {
                    var roleModel = new RoleViewModel
                    {
                        Id = role.Id,
                        RoleName = role.RoleName,
                        CreatedBy = role.CreatedBy,
                        ModifiedBy = role.ModifiedBy,
                        CreatedOn = role.CreatedOn,
                        ModifiedOn = role.ModifiedOn,
                        IsActive = role.IsActive
                    };
                    return roleModel;
                }
                return null;               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Add new Role
        public bool AddRole(RoleViewModel roleviewModel)
        {
            try
            {
                var roleModel = new Role
                {
                    Id = roleviewModel.Id,
                    RoleName = roleviewModel.RoleName,
                    CreatedBy = SessionManager.GetCurrentlyLoggedInUserId(),
                    CreatedOn = DateTime.Now,
                    //ModifiedBy = SessionManager.GetCurrentlyLoggedInUserId(),
                    //ModifiedOn = DateTime.Now,
                    IsActive = true
                };
                Role responseModel = _roleRepository.Add(roleModel);
                _roleRepository.SaveChanges();

                if (responseModel != null)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Update Role
        public bool SaveOrUpdateRole(RoleViewModel roleviewModel)
        {
            try
            {
                Role role = _roleRepository.GetById(roleviewModel.Id);
                if (role != null)
                {
                    role.RoleName = roleviewModel.RoleName;
                    role.ModifiedBy = SessionManager.GetCurrentlyLoggedInUserId();
                    role.ModifiedOn = DateTime.Now;

                    Role responseModel = _roleRepository.SaveOrUpdate(role);                  

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

        // Delete Role
        public bool DeleteRole(long id)
        {
            try
            {
                bool response = _roleRepository.Delete(id);
                _roleRepository.SaveChanges();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}