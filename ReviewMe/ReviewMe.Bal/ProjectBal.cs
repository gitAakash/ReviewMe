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
                        ProjectTitle = project.ProjectTitle,
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
                if (project != null)
                {
                    var projectViewModel = new ProjectViewModel()
                    {
                        Id = project.Id,
                        UserId = project.UserId,
                        ProjectTitle = project.ProjectTitle,
                        Description = project.Description,
                        CreatedBy = project.CreatedBy,
                        ModifiedBy = project.ModifiedBy,
                        CreatedOn = project.CreatedOn,
                        ModifiedOn = project.ModifiedOn,
                        IsActive = project.IsActive
                    };
                    return projectViewModel;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        // Add Project
        public bool AddProject(ProjectViewModel projectViewModel)
        {
            try
            {
                var project = new Project()
                {
                    Id = projectViewModel.Id,
                    UserId = SessionManager.GetCurrentlyLoggedInUserId(),
                    Description = projectViewModel.Description,
                    ProjectTitle = projectViewModel.ProjectTitle,
                    CreatedBy = SessionManager.GetCurrentlyLoggedInUserId(),
                    //ModifiedBy = SessionManager.GetCurrentlyLoggedInUserId(),
                    CreatedOn = DateTime.Now,
                    //ModifiedOn = DateTime.Now,
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
                Project project = _projectRepository.GetById(projectViewModel.Id);
                if (project != null)
                {
                    project.UserId = SessionManager.GetCurrentlyLoggedInUserId();
                    project.ProjectTitle = projectViewModel.ProjectTitle;
                    project.Description = projectViewModel.Description;
                    project.ModifiedBy = SessionManager.GetCurrentlyLoggedInUserId();
                    project.ModifiedOn = DateTime.Now;

                    Project responsemodel = _projectRepository.SaveOrUpdate(project);

                    if (responsemodel != null)
                        return true;
                }
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