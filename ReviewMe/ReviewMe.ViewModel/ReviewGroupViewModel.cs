using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewMe.ViewModel
{
    public class ReviewGroupViewModel
    {
        public ReviewGroupViewModel()
        {
            Reviewees = new List<UserGroupViewModel>();
        }
        public UserGroupViewModel Reviewer { get; set; }
        public List<UserGroupViewModel> Reviewees { get; set; }
    }
}
