using System;
using System.Collections.Generic;
using System.Linq;
using ReviewMe.DataAccess;
using ReviewMe.DataAccess.Repository;
using ReviewMe.Model;
using ReviewMe.ViewModel;

namespace ReviewMe.Bal
{
    public class RoleBal
    {
        private readonly Repository<Role> _roleRepository = new Repository<Role>(new EntityContext());

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

        public RoleViewModel GetRoleById(long id)
        {
            try
            {
                Role role = _roleRepository.GetById(id);
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AddRole(RoleViewModel roleviewModel)
        {
            try
            {
                var roleModel = new Role
                {
                    Id = roleviewModel.Id,
                    RoleName = roleviewModel.RoleName,
                    CreatedBy = 1,
                    CreatedOn = DateTime.Now,
                    ModifiedBy = 1,
                    ModifiedOn = DateTime.Now,
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

        public bool SaveOrUpdateRole(RoleViewModel roleviewModel)
        {
            try
            {
                var roleModel = new Role
                {
                    Id = roleviewModel.Id,
                    RoleName = roleviewModel.RoleName,
                    CreatedBy = 1,
                    CreatedOn = DateTime.Now,
                    ModifiedBy = 1,
                    ModifiedOn = DateTime.Now,
                    IsActive = true
                };
                Role responseModel = _roleRepository.SaveOrUpdate(roleModel);
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