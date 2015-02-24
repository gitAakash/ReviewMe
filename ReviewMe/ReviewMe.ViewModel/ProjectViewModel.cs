using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  System.ComponentModel.DataAnnotations;

namespace ReviewMe.ViewModel
{
    public class ProjectViewModel:EntityBaseView
    {
        public long UserId { get; set; }
        [Required(ErrorMessage = "Project Title is required")]
        public string ProjectTitle { get; set; }
    }
}
