using System.Collections.Generic;

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