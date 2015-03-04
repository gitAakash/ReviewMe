using System;
using System.Collections.Generic;
using System.Linq;
using ReviewMe.DataAccess;
using ReviewMe.DataAccess.Repository;
using ReviewMe.Model;
using ReviewMe.ViewModel;

namespace ReviewMe.Bal
{
    public class ProjectBal
    {
        private readonly Repository<Project> _projectRepository = new Repository<Project>(new EntityContext());

        // Get all Projects
        public ProjectViewModelLong GetAllProjects()
        {
            try
            {
                List<Project> projectList = _projectRepository.GetAll().Where(m => m.IsActive).ToList();
                var projectViewModelLong = new ProjectViewModelLong();
                foreach (Project project in projectList)
                {
                    projectViewModelLong.ProjectViewModelList.Add(new ProjectViewModel()
                    {
                        Id = project.Id,
                        UserId =  project.UserId,
                        Description = project.Description,
                        CreatedBy = project.CreatedBy,
                        ModifiedBy = project.ModifiedBy,
                        CreatedOn = project.CreatedOn,
                        ModifiedOn = project.ModifiedOn,
                        IsActive = project.IsActive
                    });
                }
                return projectViewModelLong;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Get Project By Id
        public ProjectViewModel GetProjectById(long id)
        {
            try
            {
                Project project = _projectRepository.GetById(id);
                var projectViewModel = new ProjectViewModel()
                {
                    Id = project.Id,
                    UserId = project.UserId,
                    
                    Description = project.Description,
                    CreatedBy = project.CreatedBy,
                    ModifiedBy = project.ModifiedBy,
                    CreatedOn = project.CreatedOn,
                    ModifiedOn = project.ModifiedOn,
                    IsActive = project.IsActive
                };
                return projectViewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Add Project
        public bool AddProject(ProjectViewModel projectViewModel)
        {
            try
            {
                var project = new Project()
                {
                    Id = projectViewModel.Id,
                    UserId = 4,
                    Description = projectViewModel.Description,
                    CreatedBy = 1,
                    ModifiedBy = 1,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    IsActive = true
                };
                var responsemodel = _projectRepository.Add(project);
                if (responsemodel != null)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Update Project
        public bool SaveOrUpdateProject(ProjectViewModel projectViewModel)
        {
            try 
            {
                var project = new Project()
                {
                    Id = projectViewModel.Id,
                    UserId = 1,
                    Description = projectViewModel.Description,
                    CreatedBy = 1,
                    ModifiedBy = 1,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    IsActive = true
                };
                Project responsemodel = _projectRepository.SaveOrUpdate(project);
                if (responsemodel != null)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Delete Project 
        public bool DeleteProject(long id)
        {
            try
            {
                var response = _projectRepository.Delete(id);
                _projectRepository.SaveChanges();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}