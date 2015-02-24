function DeleteTechnology(element) {
    var technologyId = $(element).attr('id').split('_')[0];
    bootbox.confirm({
        message: "Do you want to delete \"" + $(element).attr('id').split('_')[1] + "\" ?",
        callback: function (result) {
            debugger;
            if (result) {
                $.ajax({
                    url: "/Technology/DeleteTechnology",
                    type: "POST",
                    data: { 'id': technologyId },
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