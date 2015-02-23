using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewMe.ViewModel
{
    public class TechnologyViewModelLong
    {
        public TechnologyViewModelLong()
        {
            TechnologyViewModelList = new List<TechnologyViewModel>();
        }

        public TechnologyViewModel TechnologyViewModel { get; set; }
        public List<TechnologyViewModel> TechnologyViewModelList { get; set; }
    }
}
