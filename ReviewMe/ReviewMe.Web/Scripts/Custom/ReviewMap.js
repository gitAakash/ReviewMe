$(function () {

    $(window).load(function () {
        $('#AddGroup').click(function () {
            $("#hdnEditFlag").val("2");
            ////debugger;
            $('#reviewMapAddDetails').modal({
                backdrop: 'static',
                keyboard: false
            });

            $.ajax({
                url: "/ReviewMap/AddEditGroup",
                type: "GET",
                success: function (data) {
                    ////debugger;
                    $('#reviewMapModalBody').empty();
                    $('#reviewMapModalBody').append(data);
                },
                error: function (response) {
                    //debugger;
                }
            });
        });
    });

});

function editGroup(id)
{
    //debugger;

    $("#hdnEditFlag").val("1");
    $("#IsEdit").val("1");
    $("#hdnReviewerId").val(id);

    $('#reviewMapAddDetails').modal({
        backdrop: 'static',
        keyboard: false
    });

    $.ajax({
        url: "/ReviewMap/AddEditGroup",
        type: "GET",
        data: { 'revId' : id },
        success: function (data) {
            //debugger;
            $('#reviewMapModalBody').empty();
            $('#reviewMapModalBody').append(data);
        },
        error: function (response) {
            //debugger;

        }
    });
}

function deleteGroup(id)
{
    ////debugger;
    bootbox.confirm({
        message: "Do you want to delete ?",
        callback: function (result) {
            //debugger;
            if (result) {
                $.ajax({
                    url: "/ReviewMap/DeleteGroup/" + id,
                    type: "POST",
                    success: function (data) {
                        //debugger;
                        if (data == "True") {
                            $("#" + id).remove();
                        }
                    },
                    error: function (response) {
                        //debugger;
                    }
                });
            }
        },
        className: "bootbox-sm"
    });

}
