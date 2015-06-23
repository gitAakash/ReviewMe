

    $(document).ready(function () {
        debugger;
       /* $('#calendar').fullCalendar('renderEvent',
            {
            title: $('#SelectedProjectId').val(),
            start: new Date($('#apptStartTime').val()),
            end: new Date($('#apptEndTime').val()),
            allDay: ($('#apptAllDay').val() == "true"),
            dayClick: function () {
                alert('a day has been clicked!');
            }
    
            }, true);*/


        $('#calendar').fullCalendar({
            defaultView: 'month',
            editable: true,
            selectable: true,
            //header and other values
            select: function(start, end, allDay) {

                var endtime = end;
                var starttime = start;
                //var endtime = $.fullCalendar.formatDate(end, 'h:mm tt');
                //var starttime = $.fullCalendar.formatDate(start, 'ddd, MMM d, h:mm tt');
                var mywhen = starttime + ' - ' + endtime;
                $('#createEventModal #apptStartTime').val(start);
                $('#createEventModal #apptEndTime').val(end);
                $('#createEventModal #apptAllDay').val(allDay);
                $('#createEventModal #when').text(mywhen);
                //$.ajax({
                //    url: "/Review/AddEditReview",
                //    type: "GET",
                //    data: {id:4},
                //    success: function (data) {
                //        debugger;
                //        $('#createEventModal').empty();
                //        $('#createEventModal').append(data);
                //    },
                //    error: function (data) {
                //    }
                //});
                
            },
            dayClick: function () {
                $('#createEventModal').modal('show');
            }
        });
        
        $('#calendar').fullCalendar('next');

    });

    function ReviewDayDetails() {
        
    }