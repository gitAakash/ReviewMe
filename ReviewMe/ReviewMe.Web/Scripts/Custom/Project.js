function DeleteProject(element) {
    var projectId = $(element).attr('id').split('_')[0];
    bootbox.confirm({
        message: "Do you want to delete \"" + $(element).attr('id').split('_')[1] + "\" ?",
        callback: function (result) {
            debugger;
            if (result) {
                $.ajax({
                    url: "/Project/DeleteProject",
                    type: "POST",
                    data: { 'id': projectId },
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