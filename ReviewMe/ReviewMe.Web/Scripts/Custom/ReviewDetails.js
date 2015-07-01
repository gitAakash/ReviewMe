$(document).ready(function () {  
    $('[data-toggle="popover"]').popover()

    // Commented By : Ramchandra Rane, 23rd June 2015

    //$('#calendar').fullCalendar('next');
    //setTimeout(function () { alert("After 5 second. . . "); $(".ellipsis").ellipsis();  }, 5000);
    //$(".ellipsis").ellipsis();
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


            $(tda).html(" <br/> <div style='max-height:95px' class='ellipsis dayreview' data-trigger='hover' data-toggle='popover' data-content='" + item.Comment + "' id='" + item.Id + "' > " + item.Title + " </div>  <br/>");
            
            // Assigning an action to the mouseover event
            //$('.dayreview').unbind('mouseover').bind("mouseover", function (e) {
             
            //    $(this).next().attr('style', 'display:block');
            //    e.preventDefault();
            //});

           //  Assigning an action to the mouseout event
            //$('.dayreview').unbind('mouseout').bind("mouseout", function (e) {
             
            //    $(this).next().attr('style', 'display:none');
            //    e.preventDefault();
            //});

        }
        //$('.dayreview').popover('show');
        $('[data-toggle="popover"]').popover({
            trigger:'hover',
            animation: true,
            placement:'bottom'
        });
        //$('.dayreview').popover('show');
    });
}
