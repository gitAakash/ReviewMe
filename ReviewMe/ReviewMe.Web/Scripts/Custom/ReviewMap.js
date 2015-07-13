$(function () {

    $(window).load(function () {
        debugger;
        $('#AddGroup').click(function () {
            debugger;
            $("#hdnEditFlag").val("2");
         
            $('#reviewMapAddDetails').modal({
                backdrop: 'static',
                keyboard: false
            });

            $.ajax({
                url: "/ReviewMap/AddEditGroup",
                type: "GET",
                success: function (data) {
                  
                    $('#reviewMapModalBody').empty();
                    $('#reviewMapModalBody').append(data);
                },
                error: function (response) {
                 
                }
            });
        });
    });

});

function editGroup(id) {
    debugger;
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
            $('#reviewMapModalBody').empty();
            $('#reviewMapModalBody').append(data);
        },
        error: function (response) {          

        }
    });
}

function deleteGroup(id)
{
    bootbox.confirm({
        message: "Do you want to delete ?",
        callback: function (result) {
        
            if (result) {
                $.ajax({
                    url: "/ReviewMap/DeleteGroup/" + id,
                    type: "POST",
                    success: function (data) {
                    
                        if (data == "True") {
                            $("#" + id).parent().remove();
                        }
                    },
                    error: function (response) {
                      
                    }
                });
            }
        },
        className: "bootbox-sm"
    });

}
