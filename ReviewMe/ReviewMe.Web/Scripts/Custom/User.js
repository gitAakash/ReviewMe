function DeleteUser(element) {
    var userId = $(element).attr('id').split('_')[0];
    bootbox.confirm({
        message: "Do you want to delete \"" + $(element).attr('id').split('_')[1] + "\" ?",
        callback: function(result) {
         
            if (result) {
                $.ajax({
                    url: "/User/DeleteUser",
                    type: "POST",
                    data: { 'id': userId },
                    success: function(data) {
                        if (data.Status == "S")
                        {
                            notif({
                                msg: "<b>" + data.Message,
                                type: "success"
                            });
                            element.parentElement.parentElement.remove();
                        }
                        else
                        {
                            notif({
                                msg: "<b>" + data.Message,
                                type: "errro"
                            });
                        }

                        
                    },
                    error: function(data) {
                    }
                });
            }
        },
        className: "bootbox-sm"
    });
}

function EditUser(element) {
   
    var userId = $(element).attr('id');
    $('#userAddDetails').modal({
        backdrop: 'static',
        keyboard: false
    });
    $.ajax({
        url: "/User/AddEditUser",
        type: "GET",
        data: { 'userId': userId },
        success: function(data) {
            $('#userAddEditModalBody').empty();
            $('#userAddEditModalBody').append(data);
        },
        error: function(data) {
        }
    });
}

$(function () {

    $('#AddUser').click(function() {     
     
        $('#userAddDetails').modal({
            backdrop: 'static',
            keyboard: false
        });
        
        $.ajax({
            url: "/User/AddEditUser",
            type: "GET",
            data: {},
            success: function(data) {            
                $('#userAddEditModalBody').empty();
                $('#userAddEditModalBody').append(data);
            },
            error: function (data) {
            }
        });
    });
});

function readURL(input) {
    debugger;

    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            debugger;
            $('#profileImage').attr('src', e.target.result);
        };

        reader.readAsDataURL(input.files[0]);
        //$('#UserImage').val(input.files[0].name);
    }
}