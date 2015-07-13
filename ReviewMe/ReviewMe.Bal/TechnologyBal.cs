using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReviewMe.DataAccess;
using ReviewMe.DataAccess.Repository;
using ReviewMe.Model;
using ReviewMe.ViewModel;
using ReviewMe.Common.Authorization;

namespace ReviewMe.Bal
{
    public class TechnologyBal
    {
        private readonly Repository<Technology> _technologyRepository = new Repository<Technology>(new EntityContext());

        // Get All Technologies
        public TechnologyViewModelLong GetAllTechnologies()
        {
            try
            {
                List<Technology> technologyList = _technologyRepository.GetAll().Where(m => m.IsActive).ToList();
                var technologyViewModelLong = new TechnologyViewModelLong();
                foreach (Technology technology in technologyList)
                {
                    technologyViewModelLong.TechnologyViewModelList.Add(new TechnologyViewModel()
                    {
                        Id = technology.Id,
                        TechnologyName = technology.TechnologyName,
                        CreatedBy = technology.CreatedBy,
                        ModifiedBy = technology.ModifiedBy,
                        CreatedOn = technology.CreatedOn,
                        ModifiedOn = technology.ModifiedOn,
                        IsActive = technology.IsActive
                    });
                }
                return technologyViewModelLong;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Get Technology By Id
        public TechnologyViewModel GetTechnologyById(long id)
        {
            try
            {
                Technology technology = _technologyRepository.GetById(id);
                if (technology != null)
                {

                    var technologyViewModel = new TechnologyViewModel()
                    {
                        Id = technology.Id,
                        TechnologyName = technology.TechnologyName,
                        CreatedBy = technology.CreatedBy,
                        ModifiedBy = technology.ModifiedBy,
                        CreatedOn = technology.CreatedOn,
                        ModifiedOn = technology.ModifiedOn,
                        IsActive = technology.IsActive,
                    };
                    return technologyViewModel;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        // Add new Technology
        public bool AddTechnology(TechnologyViewModel technologyViewModel)
        {
            try
            {
                var technologyModel = new Technology()
                {
                    Id = technologyViewModel.Id,
                    TechnologyName = technologyViewModel.TechnologyName,
                    CreatedBy = SessionManager.GetCurrentlyLoggedInUserId(),
                    //ModifiedBy = 1,
                    CreatedOn = DateTime.Now,
                    //ModifiedOn = DateTime.Now,
                    IsActive = true
                };
                Technology responseModel = _technologyRepository.Add(technologyModel);
                if (responseModel != null)
                {
                    _technologyRepository.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Update Technology
        public bool SaveOrUpdateTechnology(TechnologyViewModel technologyViewModel)
        {
            try
            {
                Technology technology = _technologyRepository.GetById(technologyViewModel.Id);
                if (technology != null)
                {
                    technology.Id = technologyViewModel.Id;
                    technology.TechnologyName = technologyViewModel.TechnologyName;
                    technology.ModifiedBy = SessionManager.GetCurrentlyLoggedInUserId();
                    technology.ModifiedOn = DateTime.Now;

                    Technology responseModel = _technologyRepository.SaveOrUpdate(technology);
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

        // Delete Technology
        public bool DeleteTechnology(long id)
        {
            try
            {
                var response = _technologyRepository.Delete(id);
                _technologyRepository.SaveChanges();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
