function DeleteUser(element) {
    var userId = $(element).attr('id').split('_')[0];
    bootbox.confirm({
        message: "Do you want to delete \"" + $(element).attr('id').split('_')[1] + "\" ?",
        callback: function (result) {
            debugger;
            if (result) {
                $.ajax({
                    url: "/User/DeleteUser",
                    type: "POST",
                    data: { 'id': userId },
                    success: function (data) {
                        debugger;
                        element.parentElement.parentElement.remove();
                    },
                    error: function (data) {
                    }
                });
            }
        },
        className: "bootbox-sm"
    });
}