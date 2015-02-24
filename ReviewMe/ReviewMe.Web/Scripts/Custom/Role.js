function DeleteRole(element) {
    var roleId = $(element).attr('id').split('_')[0];
    bootbox.confirm({
        message: "Do you want to delete \"" + $(element).attr('id').split('_')[1] + "\" ?",
        callback: function (result) {
            if (result) {
                $.ajax({
                    url: "/Role/DeleteRole",
                    type: "POST",
                    data: { 'id': roleId },
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