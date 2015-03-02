function DeleteUser(element) {
    var userId = $(element).attr('id').split('_')[0];
    bootbox.confirm({
        message: "Do you want to delete \"" + $(element).attr('id').split('_')[1] + "\" ?",
        callback: function(result) {
            debugger;
            if (result) {
                $.ajax({
                    url: "/User/DeleteUser",
                    type: "POST",
                    data: { 'id': userId },
                    success: function(data) {
                        debugger;
                        element.parentElement.parentElement.remove();
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
    debugger;
    var userId = $(element).attr('id');
    $('#userAddDetails').modal('show');
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


$(function() {
    $('#AddUser').click(function() {
        debugger;
        $('#userAddDetails').modal('show');
        $.ajax({
            url: "/User/AddEditUser",
            type: "GET",
            data: {},
            success: function(data) {
                debugger;
                $('#userAddEditModalBody').empty();
                $('#userAddEditModalBody').append(data);
            },
            error: function(data) {
            }
        });
    });
});