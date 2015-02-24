using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ReviewMe.ViewModel
{
    public class ReviewViewModelLong
    {
        public ReviewViewModelLong()
        {
            ReviewViewModelList = new List<ReviewViewModel>();
        }

        public ReviewViewModel ReviewViewModel { get; set; }
        public List<ReviewViewModel> ReviewViewModelList { get; set; }
        
    }
}
