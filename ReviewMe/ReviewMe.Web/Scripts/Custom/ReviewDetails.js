$(document).ready(function () {  
    $('[data-toggle="popover"]').popover()
    // Commented By : Ramchandra Rane, 23rd June 2015
    //$('#calendar').fullCalendar('next');
});

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

            $(tda).html(" <br/> <div style='max-height:95px' class='ellipsis dayreview' data-trigger='hover' data-toggle='popover' data-content='" + item.Comment + "' id='" + item.Id + "' > " + item.Title + " </div>  <br/>");
            
         }   
        $('[data-toggle="popover"]').popover({
            trigger:'hover',
            animation: true,
            placement:'bottom'
        });
        //$('.dayreview').popover('show');
    });
}
