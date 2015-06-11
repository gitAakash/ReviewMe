$(function () {

    $(window).load(function () {
        $('#AddGroup').click(function () {
            //debugger;
            $('#reviewMapAddDetails').modal({
                backdrop: 'static',
                keyboard: false
            });

            $.ajax({
                url: "/ReviewMap/AddEditGroup",
                type: "GET",
                success: function (data) {
                    //debugger;
                    $('#reviewMapModalBody').empty();
                    $('#reviewMapModalBody').append(data);
                },
                error: function (response) {
                    debugger;
                }
            });
        });
    });
});
