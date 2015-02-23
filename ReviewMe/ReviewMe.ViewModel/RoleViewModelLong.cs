using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewMe.ViewModel
{
    public class RoleViewModelLong
    {
        public RoleViewModelLong()
        {
            RoleViewModelList = new List<RoleViewModel>();
        }

        public RoleViewModel RoleViewModel { get; set; }
        public List<RoleViewModel> RoleViewModelList { get; set; }
    }
}
