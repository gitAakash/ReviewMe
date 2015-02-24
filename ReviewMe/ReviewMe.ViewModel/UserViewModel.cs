using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ReviewMe.ViewModel
{
    public class UserViewModel: EntityBaseView
    {
        [Required(ErrorMessage = "First Name is required")]
        public string FName { get; set; }
        public string LName { get; set; }
        public string MName { get; set; }
        [Required(ErrorMessage = "Birth date is required")]
        public DateTime Dob { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        public bool Gender { get; set; }
        [Required(ErrorMessage = "EmailId is required")]
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "MobileNo is required")]
        public string MobileNo { get; set; }
        public string AlternateContactNo { get; set; }
        public string UserImage { get; set; }
        public string Address { get; set; }
        public string EmployeeCode { get; set; }
        public long TeamLeaderId { get; set; }
        public long RoleId { get; set; }
        public long TechnologyId { get; set; }
        public bool OnClient { get; set; }
        public bool OnProject { get; set; }
        public bool OnTask { get; set; }
        public double Rating { get; set; }
    }
}
