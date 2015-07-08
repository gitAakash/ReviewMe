var isEditable = true;

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
        if ($("#addEditReviewForm").valid()) { //if you use validation
            //var currentObject = $(this);
            //currentObject.attr('data-dismiss', 'modal');
            if (IsValidaFrom())
            {
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
                        //currentObject.removeAttr('data-dismiss');
                    }
                });
            }
            else
            {
                return false;
            }           
        }       
    });

    function IsValidaFrom()
    {     
        var CodingStandardRating = parseFloat($("#CodingStandardRating").val());
        var ProjectArchitecture = parseFloat($("#ProjectArchitecture").val());
        var CodeOptimizationRating = parseFloat($("#CodeOptimizationRating").val());
        var QueryOptimizationRating = parseFloat($("#QueryOptimizationRating").val());
        if (CodingStandardRating == 0) {
            notif({
                msg: "<b> Coding standard rating is required. ",
                type: "error"
            });
            return false;
        }
        else if (ProjectArchitecture == 0) {
            notif({
                msg: "<b> Project architecture rating is required. ",
                type: "error"
            });
            return false;
        }
        else if (CodeOptimizationRating == 0) {
            notif({
                msg: "<b> Code optimization rating is required. ",
                type: "error"
            });
            return false;
        }
        else if (QueryOptimizationRating == 0) {
            notif({
                msg: "<b> Query Optimization Rating is required. ",
                type: "error"
            });
            return false;
        }
        return true;

    }

    // Review Edit : Click Event.
    $('#btnEditReview').click(function (e) {

        e.preventDefault();

        if ($("#addEditReviewForm").valid()) { //if you use validation
          
            if (IsValidaFrom()) {
                var currentObject = $(this);
                currentObject.attr('data-dismiss', 'modal');
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
                        currentObject.removeAttr('data-dismiss');
                    }
                   
                });
            }
            else
            {
                return false;
            }
        }
    });

});

//This function is calling from star-rating.js
jQuery(document).ready(function () {
    $(".input-21f").rating({
        starCaptions: function (val) {

            if (val < 3) {
                return val;
            } else {
                return 'high';
            }
        },
        starCaptionClasses: function (val) {
            if (val < 3) {
                return 'label label-danger';
            } else {
                return 'label label-success';
            }
        },
        hoverOnClear: false
    });

    $('#rating-input').rating({
        min: 0,
        max: 5,
        step: 1,
        size: 'lg',
        showClear: false
    });
});

function SetValueToHiddenField(Name, Value) {
    $('#' + Name).val(Value);
}

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