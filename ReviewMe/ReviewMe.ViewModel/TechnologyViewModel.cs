using System.ComponentModel.DataAnnotations;

namespace ReviewMe.ViewModel
{
    public class TechnologyViewModel : EntityBaseView
    {
        [Required(ErrorMessage = "Technology is required.")]
        public string TechnologyName { get; set; }
    }
}