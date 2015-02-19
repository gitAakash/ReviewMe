using System;
using System.Collections.Generic;
using ReviewMe.DataAccess;
using ReviewMe.DataAccess.Repository;
using ReviewMe.Model;

namespace ReviewMe.Bal
{
    public class ProjectBal
    {
        private readonly Repository<Project> _projectRepository = new Repository<Project>(new EntityContext());

        public List<Project> GetAllProjects()
        {
            try
            {
                List<Project> projectList = _projectRepository.GetAll();
                return projectList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Project GetProjectById(long id)
        {
            try
            {
                Project project = _projectRepository.GetById(id);
                return project;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AddProject(Project project)
        {
            try
            {
                var model = _projectRepository.Add(project);
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

        public Project SaveOrUpdateProject(Project project)
        {
            try 
            { 
                Project entity = _projectRepository.SaveOrUpdate(project);
                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteProject(long id)
        {
            try
            {
                var response = _projectRepository.Delete(id);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}