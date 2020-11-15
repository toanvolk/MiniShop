(function (bridgeHandle) {
    let _rProductReview = function (name, count) {
        var data = {
            labels: name,
            series: count
        };

        var options = {
            axisY: {
                labelInterpolationFnc: function (value) {
                    return value;
                },
                scaleMinSpace: 30,
            },
            axisX: {
                showGrid: false,
                labelInterpolationFnc: function (value, index) {
                    return value;//index % 6 === 0 ? value : null;
                }
            },
            plugins: [
                Chartist.plugins.tooltip({
                    appendToBody: true,
                    pointClass: 'ct-point',
                    currency: true,
                    currencyFormatCallback: function (value, options) {
                        return value;
                    }
                })
            ],
            seriesBarDistance: 10
        };
        let productReview = Chartist.Bar('#home-product-review-chart'
            , data
            , options
        );
        productReview.on('draw', function (data) {
            if (data.type === 'bar') {
                let _colors = ["#fa626b", "#3fc52f"]; //Expense : #fa626b //Intended: #3fc52f

                data.element.attr({
                    style: 'stroke-width: 10px; stroke:' + _colors[data.seriesIndex],
                    y1: data.y1,
                    x1: data.x1 + 0.001
                });
            }
        });
    };
    //registry event


    //function
    let _prouductReviewLoad = function (fromDate, toDate) {
        if (!fromDate) {
            let _now = new Date();
            _now.setDate(_now.getDate() - cdA.home.periodDayProductReviewDefault);
            fromDate = _now;
            toDate = new Date();
        }
        fromDate = fromDate.toJSON();
        toDate = toDate.toJSON();
        //----------------------
        let _url = 'admin/home/getProductReview';
        $.post(_url, { fromDate: fromDate, toDate: toDate}, function (res) {
           _rProductReview(res.name, res.count);
        });
    };
    //init event
    _prouductReviewLoad();
    bridgeHandle.prouductReviewLoad = _prouductReviewLoad;
})(helper.bridgeHandle);