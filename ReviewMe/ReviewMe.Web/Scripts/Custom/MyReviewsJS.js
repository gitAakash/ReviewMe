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

            var id = $(this).find('a').attr('id');
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
    $.each(jQuery.parseJSON($('#ReviewData').val()), function (key, item) {     
        formatItem(item);
    });
}
//split the result

function formatItem(item) {

    var tdCollections = $('.fc-bg').find('table').find('tbody').find('tr').find('td');
    var lines = tdCollections.map(function (index, tda) {

        if ($(tda).attr('data-date') == item.ReviewDateString) {
            //$(tda).html(" <br/> <a href='#' class='dayreview' id=" + item.Id + " > " + item.Title + " </a> <br/>");
            var temp = "<div  class='ui-tooltip qtip ui-helper-reset ui-tooltip-default ui-tooltip-light ui-tooltip-pos-tc ui-tooltip-focus' style='display:none;z-index:999999999 !important;' > ";
            temp += " <div class='ui-tooltip-content' id='ui-tooltip-0-content' aria-atomic='true'>";
            temp += " <div style='float:left; margin:0px 5px 5px 0px;'> ";
            temp += " </div>" + item.Comment;
            temp += " </div> </div>";
            $(tda).html(" <br/> <a href='#' class='dayreview' id='" + item.Id + "' > " + item.Title + " </a> " + temp + " <br/>");

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