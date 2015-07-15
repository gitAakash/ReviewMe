$(function () {
    //$('#usersList').hide();
    $('#ddlusersList').append($('<option value="0">--Select--</option>'));
    $('#technology').append($('<option value="0" selected="selected">--Select--</option>'));
    $('#technology').change(function () {
        var techId = $('#technology').val();
        $.ajax({
            url: '/Home/GetUsersByTechnology',
            data: { id: techId },
            success: function (data) {
               // $('#usersList').show();
                $('#ddlusersList').empty();
                $('#ddlusersList').append($('<option value="0">--Select--</option>'));
                for (var i = 0; i < data.usersList.UserViewModelList.length; i++) {
                    var option = $('<option>');
                    option.attr('value', data.usersList.UserViewModelList[i].Id);
                    option.append(data.usersList.UserViewModelList[i].FName + " " + data.usersList.UserViewModelList[i].LName);
                    $('#ddlusersList').append(option);
                }
            },
            error: function() {}
        });
    });
});  

$(function() {
    $('#ddlusersList').change(function() {
        $.ajax({
            url: '/Home/GetRatingsByMonth',
            data: { month: $('#month').val(), year: $('#year').val(), id: $('#ddlusersList').val() },
            success: function (data) {
                $('#line-chart').empty();
                BindLineChart(data);
            },
            error: function (data) {

            }
        });

    });
});

function BindLineChart(data) {
    var dataArray = [];
    for (var i = 0; i < data.reviewDetails.length; i++) {
        var a = new Object();
        a.y = '' + data.reviewDetails[i].ReviewOn + '';
        a.a = data.reviewDetails[i].CodingStandardRating;
        a.b = data.reviewDetails[i].ProjectArchitecture;
        a.c = data.reviewDetails[i].CodeOptimizationRating;
        a.d = data.reviewDetails[i].QueryOptimizationRating;
        dataArray.push(a);
    }

    var data1 = dataArray,
    config = {
        data: data1,
        xkey: 'y',
        ykeys: ['a', 'b', 'c', 'd'],
        labels: ['Coding Standard Rating ', 'Project Architecture ', 'Code Optimization Rating ', 'Query Optimization Rating '],
        fillOpacity: 0.6,
        hideHover: 'auto',
        behaveLikeLine: true,
        resize: true,
        pointFillColors: ['#ffffff'],
        pointStrokeColors: ['black'],
        lineColors: ['#DF3A01', '#5FB404', '#0174DF', '#FFBF00']
    };

    config.element = 'line-chart';
    Morris.Line(config);


    $('#month').change(function () {
      
        $.ajax({
            url: '/Home/GetRatingsByMonth',
            data: { month: $('#month').val(), year: $('#year').val(), id: $('#ddlusersList').val() },
            success: function (data) {
                $('#line-chart').empty();
                if (data.reviewDetails.length != 0) {
                    BindLineChart(data);
                } else {
                    $('#line-chart').append('<h3>No Records Available..!!!</h3>');
                }
            },
            error: function (data) {

            }
        });
    });

    $('#year').change(function () {
        $.ajax({
            url: '/Home/GetRatingsByMonth',
            data: { month: $('#month').val(), year: $('#year').val(), id: $('#ddlusersList').val() },
            success: function (data) {
                $('#line-chart').empty();
                if (data.reviewDetails.length != 0) {
                    BindLineChart(data);
                }
                $('#line-chart').append('<h3>No Records Available..!!!</h3>');
            },
            error: function (data) {

            }
        });
    });
}


