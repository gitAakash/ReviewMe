using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewMe.ViewModel
{
    public class ReviewDetailsViewModel : EntityBaseView
    {
        public ReviewDetailsViewModel()
        {
            ReviewDetailsViewModelList = new List<ReviewDetailsViewModel>();
        }
        public long ReviewerId { get; set; }
        public long RevieweeId { get; set; }
        [Required]
        public string Comment { get; set; }
        [Required]
        public string Title { get; set; }
        
        [Required]
        public DateTime ReviewDate { get; set; }

        public string ReviewDateString { get; set; }

        public string ReviewerName { get; set; }
        //Added By : Ramchandra Rane, 23rd June 2015
        public List<ReviewDetailsViewModel> ReviewDetailsViewModelList { get; set; }
    }
}
