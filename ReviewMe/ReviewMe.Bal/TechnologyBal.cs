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
    public class TechnologyBal
    {
        private readonly Repository<Technology> _technologyRepository = new Repository<Technology>(new EntityContext());

        public List<Technology> GetAllTechnologies()
        {
            try
            {
                List<Technology> technologyList = _technologyRepository.GetAll();
                return technologyList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Technology GetTechnologyById(long id)
        {
            try
            {
                Technology technology = _technologyRepository.GetById(id);
                return technology;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AddTechnology(Technology technology)
        {
            try
            {
                var model = _technologyRepository.Add(technology);
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

        public Technology SaveOrUpdateTechnology(Technology technology)
        {
            try
            {
                Technology entity = _technologyRepository.SaveOrUpdate(technology);
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
                var response = _technologyRepository.Delete(id);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
