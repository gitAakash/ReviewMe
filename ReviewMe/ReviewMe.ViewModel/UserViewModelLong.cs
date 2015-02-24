using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewMe.ViewModel
{
    public class UserViewModelLong
    {
        public UserViewModelLong()
        {
            UserViewModelList = new List<UserViewModel>();
        }

        public UserViewModel UserViewModel { get; set; }
        public List<UserViewModel> UserViewModelList { get; set; }
    }
}
