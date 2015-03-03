using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ReviewMe.Common.Extensions;

namespace ReviewMe.ViewModel
{
    public class UserViewModel : EntityBaseView
    {
        [Required(ErrorMessage = "Please enter First Name")]
        public string FName { get; set; }

        public string LName { get; set; }
        public string MName { get; set; }

        [Required(ErrorMessage = "Please enter Birth date")]
        public DateTimeOffset Dob { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public bool Gender { get; set; }

        [Required(ErrorMessage = "Please enter EmailId")]
        public string EmailId { get; set; }

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please enter MobileNo")]
        [RegularExpression(@"^[0-9]\d{9}$", ErrorMessage = "Invalid Mobile Number")]
        public string MobileNo { get; set; }

        [RegularExpression(@"^[0-9]\d{9}$", ErrorMessage = "Invalid Mobile Number")]
        public string AlternateContactNo { get; set; }

        public string UserImage { get; set; }
        public string Address { get; set; }
        public string EmployeeCode { get; set; }
        public bool OnClient { get; set; }
        public bool OnProject { get; set; }
        public bool OnTask { get; set; }
        public double Rating { get; set; }

        [Required(ErrorMessage = "Please select Technology")]
        public long SelectedTechnologyId { get; set; }

        [Required(ErrorMessage = "Please select Role")]
        public long SelectedRoleId { get; set; }

        public long? SelectedTeamLeadId { get; set; }

        public IEnumerable<SerializableSelectListItem> DropDownForRoles { get; set; }
        public IEnumerable<SerializableSelectListItem> DropDownForTechnology { get; set; }
        public IEnumerable<SerializableSelectListItem> DropDownForTeamLeader { get; set; }
    }
}