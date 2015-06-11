using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewMe.ViewModel
{
    public class ReviewMapViewModelLong
    {
        public ReviewMapViewModelLong()
        {
            ReviewMapViewModelList = new List<ReviewMapViewModel>();
        }

        public ReviewMapViewModel ReviewMapViewModel { get; set; }
        public List<ReviewMapViewModel> ReviewMapViewModelList { get; set; }
    }
}
