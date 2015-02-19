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
    public class UserBal
    {
        private readonly Repository<User> _userRepository = new Repository<User>(new EntityContext());

        public List<User> GetAllUsers()
        {
            try
            {
                List<User> userList = _userRepository.GetAll();
                return userList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public User GetReviewById(long id)
        {
            try
            {
                User user = _userRepository.GetById(id);
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public bool AddUser(User user)
        {
            try
            {
                var model = _userRepository.Add(user);
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

        public User SaveOrUpdateUser(User user)
        {
            try
            {
                User entity = _userRepository.SaveOrUpdate(user);
                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteTechnology(long id)
        {
            try
            {
                var response = _userRepository.Delete(id);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
