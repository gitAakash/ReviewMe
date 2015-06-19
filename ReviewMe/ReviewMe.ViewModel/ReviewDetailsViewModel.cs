using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewMe.ViewModel
{
    public class ReviewDetailsViewModel : EntityBaseView
    {
        public long ReviewerId { get; set; }
        public long RevieweeId { get; set; }
        public string Comment { get; set; }
        public string Title { get; set; }
    }
}
