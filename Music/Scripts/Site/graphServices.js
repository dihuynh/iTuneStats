$(document).ready(function () {
    graph(this);
});

function graph(selection) {
    var xChoice = $('#xChoice :selected').text();
  
    var yChoice = $('#yChoice').val();
    var webServiceURL = Site.baseURL + xChoice + yChoice;
    console.log(webServiceURL);
    $.ajax({
        type: "GET",
        url: webServiceURL,
        async: true,
        dataType: "json",
        success: function (response) {
            draw(response);
        },
        error: function (error) {
            alert("Webservice failed: " + error.statusText);
        }
    });
}

function draw(response) {
    var toPlot = [];
    var artistList = [];
    var shorten = [];
    var color = '#ebebeb';
    for (i = 0; i < 15; i++) {
        toPlot.push([parseInt(response[i].Count)]);
        var str = new String(response[i].Item);
        artistList.push(str);

        //shorten strings that are too long
        if (str.length > 15)
            str = str.substring(0, 15);
        shorten.push([str]);
    }

    $("#chart").highcharts({
        chart: {
            type: 'bar',
            style: { color: color },
            backgroundColor: 'rgba(255, 255, 255, 0)'
        },
        title: {
            text: '',
            style: { color: color }

        },
        tooltip:{
            formatter: function () { return artistList[this.x] + "<br /> <b>" + this.y + "</b>" },
            style: {
                color: '#262727'
            }
        },
        xAxis: {
            categories: [],
            labels: {
                formatter: function() {return shorten[this.value]},
                style: { color: color },
            }
        },
        yAxis: {
            min: 0,
            title: {
                text: '',
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