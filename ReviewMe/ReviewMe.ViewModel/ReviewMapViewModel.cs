using ReviewMe.Common.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewMe.ViewModel
{
    public class ReviewMapViewModel : EntityBaseView
    {
        public IEnumerable<SerializableSelectListItem> DropDownForReviewer { get; set; }
        public IEnumerable<SerializableSelectListItem> DropDownForReviewee { get; set; }
        [Required(ErrorMessage="Reviewer is Required")]
        public Int64 ReviewerId { get; set; }
        public Int64 DevloperId { get; set; }
        public string SelectedListValues { get; set; }
        public string IsEdit { get; set; }
        public string EditOriginalReviewee { get; set; }
    }
}
