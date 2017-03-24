var urlsCharthControl = {
    
    loadingSelector: "#divLoading",
    chartSelector: "#Measurement_Chart",
    
    loadCharth: function () {
        google.load("visualization", "1",
                {
                    packages: ["corechart"]
                });
        google.setOnLoadCallback(urlsCharthControl.drawChart);
    },
    drawChart: function () {
        param = urlsCharthControl.getParams("idDomain"),
        $.ajax(
        {
            url: '/MeasurementResult/LoadUrlsForCharth',
            dataType: "json",
            data: { idDomain: param },
            type: "GET",
            error: function (xhr, status, error) {
                var err = eval("(" + xhr.responseText + ")");
                toastr.error(err.message);
            },
            beforeSend: function () {
                $(urlsCharthControl.loadingSelector).show();
            },
            success: function (data) {
                urlsCharthControl.UrlListChart(data);
                return false;
            },
            complete: function () {
                $(urlsCharthControl.loadingSelector).hide();
            }
        });
        return false;
    },
    //This function is used to bind the user data to chart
    UrlListChart: function (data) {
        $(urlsCharthControl.chartSelector).show();
        var dataArray = [
        ['Domains', 'max page speed', 'min page speed']
        ];
        $.each(data, function (i, item) {
            dataArray.push([item.Url, item.MaxRespTime, item.MinRespTime]);
        });
        var datacharth = google.visualization.arrayToDataTable(dataArray);
        var options = {
            pointSize: 7,
            chartArea: {
            },
            legend:
            {
                alignment: 'start',
                position: 'top',
                textStyle:
                {
                    fontSize: '14',
                    color: '#4444',
                    fontName: 'Segoe UI',
                    bold: false,
                    italic: false
                }
            },
            lineWidth: 2,
            dataOpacity: 1,
            curveType: 'function',
            colors: ['#339900', '#FF6600'],
            backgroundColor: '#fff',
            areaOpacity: 0.3,
            hAxis:
            {
                baseline: 1,
                baselineColor: 'red',
                title: 'x: domane url',
                titleTextStyle:
                {
                    color: '#444',
                    fontName: 'Segoe UI',
                    fontSize: '14',
                    bold: true,
                    italic: true
                },
                textStyle:
                {
                    fontSize: '12',
                    color: '#4444',
                    fontName: 'Segoe UI',
                    bold: false,
                    italic: true
                },
            },
            vAxis:
            {
                baseline: 0,
                gridlines:
                    {
                        color: '#ddd',
                        count: 10
                    },
                scaleType: 'Linear',
                baselineColor: '#ccc',
                title: 'y: Response time',
                width: 1,
                titleTextStyle:
                {
                    color: '#444',
                    fontName: 'Segoe UI',
                    fontSize: '14',
                    bold: true,
                    italic: true
                },
                textStyle:
                 {
                     fontSize: '12',
                     color: '#4444',
                     fontName: 'Segoe UI',
                     bold: false,
                     italic: false
                 },
                viewWindow:
                {
                    min: 0,
                    max: 1000,
                    format: 'long'
                },
                minorGridlines: { count: 1, color: '#F7F7F7' }
            }
        };       
        var chart = new google.visualization.AreaChart(document.getElementById('Measurement_Chart'));
        chart.draw(datacharth, options);
        return false;
    },
    getParams: function (name) {
        var s = window.location.search;
        s = s.match(new RegExp(name + '=([^&=]+)'));
        return s ? s[1] : false;
    }
};






