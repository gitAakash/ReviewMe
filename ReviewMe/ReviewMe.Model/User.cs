using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReviewMe.Model
{
    public class User : EntityBase
    {
        public User()
        {
            Reviews = new List<Review>();
            Comments = new List<Comment>();
            Projects = new List<Project>();
            UsersToBeReviewed = new List<ReviewSetting>();
            ReviewerUsers = new List<ReviewSetting>();
            ReviewSettings = new List<ReviewSetting>();
        }

        public string FName { get; set; }
        public string LName { get; set; }
        public string MName { get; set; }
        public DateTimeOffset Dob { get; set; }
        public bool Gender { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string MobileNo { get; set; }
        public string AlternateContactNo { get; set; }
        public string UserImage { get; set; }
        public string Address { get; set; }
        public string EmployeeCode { get; set; }
        public long? TeamLeaderId { get; set; }
        public long RoleId { get; set; }
        public long TechnologyId { get; set; }
        public bool OnClient { get; set; }
        public bool OnProject { get; set; }
        public bool OnTask { get; set; }
        public double Rating { get; set; }

        [ForeignKey("TeamLeaderId")]
        public virtual User TeamLeader { get; set; }
        public virtual Role Role { get; set; }
        public virtual Technology Technology { get; set; }
        

        public List<Review> Reviews { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Project> Projects { get; set; }
        public List<ReviewSetting> UsersToBeReviewed { get; set; }
        public List<ReviewSetting> ReviewerUsers { get; set; }
        public List<ReviewSetting> ReviewSettings { get; set; }
        public List<ReviewMap> ReviewerMapUsers { get; set; }
        public List<ReviewMap> DeveloperMapUsers { get; set; }

    }
}