﻿function DeleteTechnology(element) {
    var technologyId = $(element).attr('id').split('_')[0];
    bootbox.confirm({
        message: "Do you want to delete \"" + $(element).attr('id').split('_')[1] + "\" ?",
        callback: function (result) {
       
            if (result) {
                $.ajax({
                    url: "/Technology/DeleteTechnology",
                    type: "POST",
                    data: { 'id': technologyId },
                    success: function (data) {
                   
                        element.parentElement.parentElement.remove();
                        notif({
                            msg: "<b>" + data,
                            type: "success"
                        });
                    },
                    error: function (data) {
                        notif({
                            msg: "<b>" + data,
                            type: "Error"
                        });
                    }
                });
            }
        },
        className: "bootbox-sm"
    });
}