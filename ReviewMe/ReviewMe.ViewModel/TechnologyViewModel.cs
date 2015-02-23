using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ReviewMe.ViewModel
{
    public class TechnologyViewModel:EntityBaseView
    {
        [Required(ErrorMessage = "Technology is required.")]
        public string TechnologyName { get; set; }
    }
}
