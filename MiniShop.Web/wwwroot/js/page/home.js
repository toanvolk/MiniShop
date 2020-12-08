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
    let _clickViewTemplate = function (data) {
        let _htmlTableBody = `
                <tr>
                    <td class="text-truncate">{#:url}</td>
                    <td class="text-truncate">
                        <span>
                            <i class="ft-arrow-up success"></i> {#:clickCount}
                        </span>
                    </td>
                    <td class="text-truncat mr-1">
                        <a class="detail-view" data-url="{#:url}">
                            <i class="ft-eye blue-grey lighten-2 font-medium-5 ml-1"></i>
                        </a>
                    </td>
                </tr>
                `;
        let _html = '';
        $(data).each(function (index, item) {
            _html += _htmlTableBody
                .replace(new RegExp("{#:url}", "gi"), item.url)
                .replace(new RegExp("{#:clickCount}", "gi"), item.clickCount);
        });
        $('#click-view-table tbody').html(_html);
    }
    let _clickViewDetailTemplate = function (data) {
        let _htmlTableBody = `
                <tr>
                    <td class="text-truncate">{#:date}</td>
                    <td class="text-truncate">{#:addressId}</td>
                </tr>
                `;
        let _html = '';
        $(data).each(function (index, item) {
            _html += _htmlTableBody
                .replace(new RegExp("{#:date}", "gi"), moment.utc(item.clickDate).local().format('DD/MM/YYYY HH:mm') )
                .replace(new RegExp("{#:addressId}", "gi"), item.addressId);
        });
        $('#click-view-detail-table tbody').html(_html);
    }
    //registry event
    //function
    let _clickViewsLoad = function (fromDate, toDate) {
        if (!fromDate) {
            let _now = new Date();
            _now.setDate(_now.getDate() - cdA.home.periodDayProductReviewDefault);
            fromDate = _now;
            toDate = new Date();
        }
        fromDate = fromDate.toJSON();
        toDate = toDate.toJSON();
        //----------------------
        let _url = 'admin/home/GetClickView';
        $.post(_url, { fromDate: fromDate, toDate: toDate }, function (res) {
            if (res) {
                _clickViewTemplate(res);                
            }
        });
    };
    let _clickViewDetailLoad = function (url) {
        let _url = 'admin/home/GetClickViewDetail';
        $.post(_url, { url: url }, function (res) {
            if (res) {
                _clickViewDetailTemplate(res);
            }
        });
    }
    //init event
    _clickViewsLoad();
    $(document).on('click', '#click-view-table tbody a.detail-view', function (e) {
        let _url = $(e.target).data('url');
        if (typeof (_url) == "undefined") _url = $(e.target).closest('a').data('url');
        _clickViewDetailLoad(_url);
    });
    bridgeHandle.clickViewsLoad = _clickViewsLoad; 
})(helper.bridgeHandle);