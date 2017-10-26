var arrCharts = [];

$(document).ready(function () {

    var charts = $('.chart');
    var dashboardId = $('#display-container')[0].getAttribute('dashboardId');


    for (var i = 0; i < charts.length; i++) {

         var c = c3.generate({
            bindto: charts[i],
            data: {
                columns: [
                    ['data1']
                    ['x', 0]
                ],
                axis: {
                    type: 'timeseries',
                    tick: {
                        format: '%m-%d-%h'
                    }
                },
                type: 'area-spline'
            },
        });

        window.arrCharts[i] = c;
        $.ajax({
            type: 'GET',
            async: false,
            url: '/Component/GetChartData',
            data: {
                'id': charts[i].getAttribute('chart-id'),
                'arduinoId': dashboardId
            },
            success: function (data) {
                PopulateChart(JSON.parse(data), i);
            }
        });
    };


    function PopulateChart(data, id) {
        while (data["Values"].length > 0) {
            var val = data["Values"][0].Value;
            var time = data["Values"][0].Time;
            arrCharts[id].flow({
                columns: [
                    ['data1', val],
                    ['x', time]
                ]
            });
            data["Values"].shift();
            setTimeout(function () { }, 500);
        }
    };
});