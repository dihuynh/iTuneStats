

$(document).ready(function () {
    $("#byPlayCounts").click(function () {
        var webServiceURL = Site.baseURL + Site.byPlayCountsURL;
        console.log(webServiceURL);
        $.ajax({
            type: "GET",
            url: webServiceURL,
            async: true,
            dataType: "json",
            success: function (response) {
                drawPlayCountChart(response);
            },
            error: function (error) {
                alert("Webservice failed: " + error.statusText);
            }

        });
    });

    $("#byArtists").click(function () {
        var webServiceURL = Site.baseURL + Site.byArtistsURL;
        console.log(webServiceURL);
        $.ajax({
            type: "GET",
            url: webServiceURL,
            async: true,
            dataType: "json",
            success: function (response) {
                drawArtistChart(response);
            },
            error: function (error) {
                alert("Webservice failed: " + error.statusText);
            }

        });
    });

    $("#byGenres").click(function () {
        var webServiceURL = Site.baseURL + Site.byGenresURL;
        console.log(webServiceURL);
        $.ajax({
            type: "GET",
            url: webServiceURL,
            async: true,
            dataType: "json",
            success: function (response) {
                drawGenreChart();
            },
            error: function (error) {
                alert("Webservice failed: " + error.statusText);
            }
        });
    });
});


function drawGenreChart() {
    $("#chart").highcharts({
        chart: {
            type: 'column',
            style: {color:'#fff'}

        },
        title: {
            text: 'Data collected on ',
            style: { color: '#fff' }

        },
        xAxis: {
            style: { color: '#fff' }
        },
        yAxis: {
            min: 0,
            title: {
                text: 'Rainfall (mm)'
            },
            style: { color: '#fff' }
        },
        tooltip: {
            headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
            pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                '<td style="padding:0"><b>{point.y:.1f} mm</b></td></tr>',
            footerFormat: '</table>',
            shared: true,
            useHTML: true
        },
        plotOptions: {
            column: {
                pointPadding: 0.2,
                borderWidth: 0
            }
        },
        series: [{
            name: 'Tokyo',
            data: [49.9, 71.5, 106.4, 129.2, 144.0, 176.0, 135.6]

        }, {
            name: 'New York',
            data: [83.6, 78.8, 98.5, 93.4, 106.0, 84.5, 105.0]

        }, {
            name: 'London',
            data: [48.9, 38.8, 39.3, 41.4, 47.0, 48.3, 59.0]

        }, {
            name: 'Berlin',
            data: [42.4, 33.2, 34.5, 39.7, 52.6, 75.5, 57.4]

        }]
    });
};

function drawArtistChart(response) {

    var toPlot = [];

    for (i = 0; i < response.length; i++) {
        toPlot.push([new String(response[i].Artist), parseInt(response[i].Count)]);
    }
    //console.log("To plot" + toPlot);


    $("#chart").highcharts({
        chart: {
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false
        },
        title: {
            text: 'Data collected on '
        },
        tooltip: {
            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
        },
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                dataLabels: {
                    enabled: true
                },
            }
        },
        series: [{
            type: 'pie',
            name: 'Percentage of songs',
            data: toPlot
        }]
    });
};

function drawPlayCountChart(response) {

    var toPlot = [];
    var artistList = [];
    var color = '#ebebeb';
    for (i = 0; i < 20; i++) {
        toPlot.push([parseInt(response[i].count)]);

        //shorten strings that are too long
        var str = new String(response[i].Album);
        if (str.length > 15)
            str = str.substring(0, 15);
        artistList.push([str]);
    }

    $("#chart").highcharts({
        chart: {
            type: 'bar',
            style: {color: color},
            backgroundColor: 'rgba(255, 255, 255, 0.1)'
        },
        title: {
            text: 'The number of plays by albums ',
            style: {color:color}

        },
        xAxis: {
            categories: artistList,
            labels: {
                style: { color: color },
            }

        },
        yAxis: {
            min: 0,
            title: {
                text: 'Number of plays',
                aligh: 'high',
                style: { color: color },
          },
            labels: { style: { color: color } }

        },

        legend: {
            enabled: false
        },
        credits: { endabled: false },
        series: [{ data: toPlot }]
    });
};