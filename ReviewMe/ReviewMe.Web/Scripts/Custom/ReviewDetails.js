$(document).ready(function () {  
 
    // Commented By : Ramchandra Rane, 23rd June 2015
    //$('#calendar').fullCalendar('next');
  
});

function ReviewDayDetails() {

}


//Fill  reviews in calender
function fillCalender(date) {
   
    var par_date = date._d.getUTCMonth() + 1 + "/" + date._d.getUTCDate() + "/" + date._d.getUTCFullYear();
    var url = "GetMonthWiseReviewDetails?revieweeDate=" + par_date + "&revieweeId=" + $('#RevieweeId').val();
    var jqxhr = $.getJSON(url, function (result) {
        // On success, 'data' contains a list of products.         
        $.each(result.Result, function (key, item) {        
            formatItem(item);          
        });
        
    }).fail(function () {
        alert("error");
    });
}
//split the result

function formatItem(item) {
  
    var tdCollections = $('.fc-bg').find('table').find('tbody').find('tr').find('td');
    var lines = tdCollections.map(function (index, tda) {
        
        if ($(tda).attr('data-date') == item.ReviewDateString) {
            //$(tda).html(" <br/> <a href='#' class='dayreview' id=" + item.Id + " > " + item.Title + " </a> <br/>");
            var temp= "<div  class='ui-tooltip qtip ui-helper-reset ui-tooltip-default ui-tooltip-light ui-tooltip-pos-tc ui-tooltip-focus' style='display:none;z-index:999999999 !important;' > ";
            temp+=" <div class='ui-tooltip-content' id='ui-tooltip-0-content' aria-atomic='true'>";
            temp+=" <div style='float:left; margin:0px 5px 5px 0px;'> ";
            temp += " </div>" + item.Comment;
            temp += " </div> </div>";
            $(tda).html(" <br/> <a href='#' class='dayreview' > " + item.Title + " </a> " + temp + " <br/>");

            // Assigning an action to the mouseover event
            $('.dayreview').unbind('mouseover').bind("mouseover", function (e) {             
                $(this).next().attr('style', 'display:block');
                e.preventDefault();
            });

            // Assigning an action to the mouseout event
            $('.dayreview').unbind('mouseout').bind("mouseout", function (e) {
                $(this).next().attr('style', 'display:none');
                e.preventDefault();
            });

        }
    });
}
