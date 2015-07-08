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
                        if (data.Status == "S") {
                            notif({
                                msg: "<b>" + data.Message,
                                type: "success"
                            });
                            element.parentElement.parentElement.remove();
                            //$('#uidemo-modals-alerts-success').modal('show');
                        }
                        else {
                            notif({
                                msg: "<b>" + data.Message,
                                type: "errro"
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
}