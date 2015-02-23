using System.ComponentModel.DataAnnotations;

namespace ReviewMe.ViewModel
{
    public class RoleViewModel : EntityBaseView
    {
        [Required(ErrorMessage = "Role is required.")]
        public string RoleName { get; set; }
    }
}