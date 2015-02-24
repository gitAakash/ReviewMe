using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewMe.ViewModel
{
    public class ProjectViewModelLong
    {
        public ProjectViewModelLong()
        {
            ProjectViewModelList = new List<ProjectViewModel>();
        }

        public ProjectViewModel ProjectViewModel { get; set; }
        public List<ProjectViewModel> ProjectViewModelList { get; set; }
    }
}
