function DeleteReview(element) {
    var reviewId = $(element).attr('id');
    bootbox.confirm({
        message: "Do you want to delete this Review ?",
        callback: function (result) {
          
            if (result) {
                $.ajax({
                    url: "/Review/DeleteReview",
                    type: "POST",
                    data: { 'id': reviewId },
                    success: function (data) {
                     
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