using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ReviewMe.Common.Extensions;
using System.Web;

namespace ReviewMe.ViewModel
{
    public class UserViewModel : EntityBaseView
    {
        [Required(ErrorMessage = "Please enter first name")]
        public string FName { get; set; }

        [Required(ErrorMessage = "Please enter last name")]
        public string LName { get; set; }
        public string MName { get; set; }

        [Required(ErrorMessage = "Please enter birth date")]
        public DateTimeOffset Dob { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public bool Gender { get; set; }

        [Required(ErrorMessage = "Please enter emailId")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",ErrorMessage = "Invalid Email")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter confirm password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please enter mobile number")]
        [RegularExpression(@"^[0-9]\d{9}$", ErrorMessage = "Invalid mobile number")]
        public string MobileNo { get; set; }

        [RegularExpression(@"^[0-9]\d{9}$", ErrorMessage = "Invalid mobile number")]
        public string AlternateContactNo { get; set; }

        public string UserImage { get; set; }

        public HttpPostedFileBase FilePath { get; set; }
        public string Address { get; set; }
        public string EmployeeCode { get; set; }
        public bool OnClient { get; set; }
        public bool OnProject { get; set; }
        public bool OnTask { get; set; }
        public double Rating { get; set; }

        [Required(ErrorMessage = "Please select technology")]
        public long SelectedTechnologyId { get; set; }

        [Required(ErrorMessage = "Please select role")]
        public long SelectedRoleId { get; set; }

        public long? SelectedTeamLeadId { get; set; }

        public IEnumerable<SerializableSelectListItem> DropDownForRoles { get; set; }
        public IEnumerable<SerializableSelectListItem> DropDownForTechnology { get; set; }
        public IEnumerable<SerializableSelectListItem> DropDownForTeamLeader { get; set; }

        public string RoleName { get; set; }
    }

}