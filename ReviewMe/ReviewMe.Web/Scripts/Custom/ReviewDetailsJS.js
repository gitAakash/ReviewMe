$(function () {

    $('#ReviewDate').val($('#hdnRevieweeDate').val());

    // Review Delete : Click Event.
    $('#btnReviewDelete').click(function () {
        var id = $(this).attr('deleteId');
             bootbox.confirm({
            message: "Do you want to delete ?",
            callback: function (result) {           
                if (result) {
                    $.ajax({
                        url: "/ReviewMap/DeleteReviewDetails",
                        type: "GET",
                        data: { 'Id': id },
                        success: function (data) {
                            $('#userAddEditModalBody').empty();
                            $('#userAddReviewDetails').modal('hide');
                            if (data.Status == "S") {
                                clearDeletedItemText();
                                notif({
                                    msg: "<b>" + data.Message,
                                    type: "success"
                                });
                            }
                            else {
                                notif({
                                    msg: "<b>" + data.Message,
                                    type: "error"
                                });
                            }
                        },
                        error: function (data) {

                        }
                    });
                }
            },
            className: "bootbox-sm"
        });      
    });

    // Review Add : Click Event.
    $('#btnSaveReviewDetail').click(function (e) {
        e.preventDefault();
        debugger;
        if ($("#addEditReviewForm").valid()) { //if you use validation
            $(this).attr('data-dismiss', 'modal');
            $.ajax({
                url: $("#addEditReviewForm").attr('action'),
                type: $("#addEditReviewForm").attr('method'),
                data: $("#addEditReviewForm").serialize(),
                success: function (result) {
                    var hdnRevieweeDate = $('#hdnRevieweeDate').val();
                    var RevieweeId = $('#RevieweeId').val();
                    $('#userAddEditModalBody').empty();
                    $('#userAddReviewDetails').modal('hide');

                    if (result.Status = "S") {

                        notif({
                            msg: "<b>" + result.Message,
                            type: "success"
                        });
                        BindCalendar(hdnRevieweeDate, RevieweeId);
                    }
                    else {
                        notif({
                            msg: "<b>" + result.Message,
                            type: "error"
                        });
                    }
                }
            });
        }
    });

    // Review Edit : Click Event.
    $('#btnEditReview').click(function (e) {

        e.preventDefault();

        if ($("#addEditReviewForm").valid()) { //if you use validation
            $(this).attr('data-dismiss', 'modal');
            $.ajax({
                url: $("#addEditReviewForm").attr('action'),
                type: $("#addEditReviewForm").attr('method'),
                data: $("#addEditReviewForm").serialize(),
                success: function (result) {
                    var hdnRevieweeDate = $('#hdnRevieweeDate').val();
                    var RevieweeId = $('#RevieweeId').val();
                    $('#userAddEditModalBody').empty();
                    $('#userAddReviewDetails').modal('hide');

                    if (result.Status = "S") {

                        notif({
                            msg: "<b>" + result.Message,
                            type: "success"
                        });
                        BindCalendar(hdnRevieweeDate, RevieweeId);
                    }
                    else {
                        notif({
                            msg: "<b>" + result.Message,
                            type: "error"
                        });
                    }
                }
            });
        }
    });

});

function BindCalendar(hdnRevieweeDate, RevieweeId) {

    var url = "GetMonthWiseReviewDetails?revieweeDate=" + hdnRevieweeDate + "&revieweeId=" + RevieweeId;

    var jqxhr = $.getJSON(url, function (result) {
        // On success, 'data' contains a list of products.
        $.each(result.Result, function (key, item) {
            formatItem(item);
        });

    }).fail(function () {
        alert("error");
    });
}