﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReviewMe.DataAccess;
using ReviewMe.DataAccess.Repository;
using ReviewMe.Model;

namespace ReviewMe.Bal
{
    public class ReviewSettingBal
    {
        private readonly Repository<ReviewSetting> _reviewSettingRepository = new Repository<ReviewSetting>(new EntityContext());

        public List<ReviewSetting> GetAllReviewSettings()
        {
            try { 
            List<ReviewSetting> reviewSettingList = _reviewSettingRepository.GetAll();
                if (reviewSettingList != null)
                {
                    return reviewSettingList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public ReviewSetting GetReviewSettingById(long id)
        {
            try { 
            ReviewSetting reviewSetting = _reviewSettingRepository.GetById(id);
                if (reviewSetting != null)
                {
                    return reviewSetting;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public bool AddReviewSetting(ReviewSetting reviewSetting)
        {
            try
            {
                var model = _reviewSettingRepository.Add(reviewSetting);
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

        public ReviewSetting SaveOrUpdateReviewSetting(ReviewSetting reviewSetting)
        {
            try 
            { 
            ReviewSetting entity = _reviewSettingRepository.SaveOrUpdate(reviewSetting);
                if (entity != null)
                {
                    return entity;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public bool DeleteReviewSetting(long id)
        {
            try
            {
                var response = _reviewSettingRepository.Delete(id);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
