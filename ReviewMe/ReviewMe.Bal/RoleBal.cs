using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ReviewMe.DataAccess;
using ReviewMe.DataAccess.Repository;
using ReviewMe.Model;

namespace ReviewMe.Bal
{
    public class RoleBal
    {
        private readonly Repository<Role> _roleRepository = new Repository<Role>(new EntityContext());

        public List<Role> GetAllRoles()
        {
            try
            {
                List<Role> roleList = _roleRepository.GetAll();
                return roleList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Role GetRoleById(long id)
        {
            try { 
            Role role = _roleRepository.GetById(id);
            return role;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AddRole(Role role)
        {
            try
            {
                var model = _roleRepository.Add(role);
                if (model!= null)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Role SaveOrUpdateRole(Role role)
        {
            try
            {
                Role entity = _roleRepository.SaveOrUpdate(role);
                return entity;
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
                var response = _roleRepository.Delete(id);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
