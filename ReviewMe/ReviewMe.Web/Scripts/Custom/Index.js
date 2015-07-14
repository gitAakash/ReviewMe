function BindLineChart(data) {
    debugger;
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
        debugger;
        $.ajax({
            url: '/Home/GetRatingsByMonth',
            data: { month: $('#month').val(), year: $('#year').val() },
            success: function (data) {
                $('#line-chart').empty();
                BindLineChart(data);
            },
            error: function (data) {

            }
        });
    });

    $('#year').change(function () {
        debugger;
        $.ajax({
            url: '/Home/GetRatingsByMonth',
            data: { month: $('#month').val(), year: $('#year').val() },
            success: function (data) {
                $('#line-chart').empty();
                BindLineChart(data);
            },
            error: function (data) {

            }
        });
    });
}


