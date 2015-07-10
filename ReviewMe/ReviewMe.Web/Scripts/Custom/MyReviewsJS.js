var IsFirstRequest = true;
$(document).ready(function () {
    
    $('#calendar').fullCalendar({
        defaultView: 'month',
        editable: true,
        selectable: true,
        //header and other values
        select: function (start, end, allDay) {
            var endtime = end;
            var starttime = start;
            var mywhen = starttime + ' - ' + endtime;
            $('#createEventModal #apptStartTime').val(start);
            $('#createEventModal #apptEndTime').val(end);
            $('#createEventModal #apptAllDay').val(allDay);
            $('#createEventModal #when').text(mywhen);
        },
        dayClick: function () {

            $('#userAddReviewDetails').modal({
                backdrop: 'static',
                keyboard: false
            });

            var id = $(this).find('div').attr('id');
            if (id != undefined) {
                $.ajax({
                    url: "/ReviewMap/ViewReviewDetails",
                    type: "GET",
                    data: { 'Id': id },
                    success: function (data) {
                        $('#userAddEditModalBody').empty();
                        $('#userAddEditModalBody').append(data);
                    },
                    error: function (data) {

                    }
                });
            }
            else {
                $('#userAddEditModalBody').empty();
                $('#userAddReviewDetails').modal('hide');
                notif({
                    msg: "<b>There is no review available for this date.",
                    type: "error"
                });

            }
        }
    });
});


function fillCalender(date) {
    if (IsFirstRequest)
    {
        $.each(jQuery.parseJSON($('#ReviewData').val()), function (key, item) {
            formatItem(item);
        });
        IsFirstRequest = false;
    }
    else {
    
        var par_date = date._d.getUTCMonth() + 1 + "/" + date._d.getUTCDate() + "/" + date._d.getUTCFullYear();
        var url = "GetMonthWiseLoginuserReviewDetails?revieweeDate=" + par_date;
        var jqxhr = $.getJSON(url, function (result) {
            // On success, 'data' contains a list of products.         
            $.each(result.Result, function (key, item) {
                formatItem(item);
            });

        }).fail(function () {
            alert("error");
        });
    }
}
//split the result

function formatItem(item) {

    var tdCollections = $('.fc-bg').find('table').find('tbody').find('tr').find('td');
    var lines = tdCollections.map(function (index, tda) {

        if ($(tda).attr('data-date') == item.ReviewDateString) {

            $(tda).html(" <br/> <div style='max-height:95px' class='ellipsis dayreview' data-trigger='hover' data-toggle='popover' data-content='" + item.Comment + "' id='" + item.Id + "' > " + item.Title + " </div>  <br/>");

        }
        //$('.dayreview').popover('show');
        $('[data-toggle="popover"]').popover({
            trigger: 'hover',
            animation: true,
            placement: 'bottom'
        });
        //$('.dayreview').popover('show');
    });
}