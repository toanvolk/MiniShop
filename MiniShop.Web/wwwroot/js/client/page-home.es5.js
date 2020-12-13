"use strict";

(function ($, document) {
    var _paginationClass = ".mnshop-pagination";
    var _btnProductSearch = "#mnshop-btn-product-search";
    var _mshop_product_client = ".mshop-product-client";
    var _mnshop_product_hero = ".mnshop-product-hero";
    var _mshop_filter = ".mshop-filter";
    var _search_result = ".search__result";
    var _btn_categorys = ".btn-categorys";
    var timeDelay;
    var numberDeplayLazy = 0;
    var numberCountMax = 0;

    var _formatCardTemplate = function _formatCardTemplate(data) {
        var _template = "\n            <div class=\"col-xs-12 col-md-6 col-lg-3\">\n\t<!-- First product box start here-->\n\t<div class=\"prod-info-main prod-wrap clearfix\">\n\t\t<div class=\"row\">\n\t\t\t\t<div class=\"col-md-3 col-sm-3 col-xs-3\">\n\t\t\t\t\t<div class=\"product-image\"> \n                        <div class=\"border\"></div>\n                        <div class=\"main-element\">\n                            <img src=\"{#:picture}\" class=\"img-responsive\">   \n                        </div>\n\t\t\t\t\t\t<span class=\"tag2\">\n\t\t\t\t\t\t\t<img src=\"{#:picture-hot}\" style=\"width: 30px\">  \n\t\t\t\t\t\t</span>\n\t\t\t\t\t</div>\n\t\t\t\t</div>\n\t\t\t\t<div class=\"col-md-9 col-sm-9 col-xs-9\">\n\t\t\t\t    <div class=\"product-deatil\">\n\t\t\t\t\t\t    <h5 class=\"name\">\n\t\t\t\t\t\t\t    <a href=\"{#:trackingLink}\" style=\"font-weight: 700;\">\n\t\t\t\t\t\t\t\t    {#:name}\n\t\t\t\t\t\t\t    </a>\t\t\t\t\t\t\t \n\t\t\t\t\t\t    </h5>\n                                <span class=\"tag1\">\n\t\t\t\t\t\t\t        <button class=\"btn-read-more\" data-link=\"{#:link}\" data-id=\"{#:id}\" style=\"margin: 0 auto\">\n                                      <span>Chi tiết</span>\n                                    </button>\n\t\t\t\t\t\t        </span>\n\t\t\t\t\t\t    <p class=\"price-container\">\n\t\t\t\t\t\t\t    <span>{#:price}</span>\n                                <a title=\"Giá tham khảo, có thể chênh lệch với giá thực tế\" class=\"text-decoration-none\">\n    <svg width=\"0.6em\" height=\"0.6em\" viewBox=\"0 0 16 16\" class=\"bi bi-info-circle\" fill=\"currentColor\" xmlns=\"http://www.w3.org/2000/svg\">\n      <path fill-rule=\"evenodd\" d=\"M8 15A7 7 0 1 0 8 1a7 7 0 0 0 0 14zm0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16z\"/>\n      <path d=\"M8.93 6.588l-2.29.287-.082.38.45.083c.294.07.352.176.288.469l-.738 3.468c-.194.897.105 1.319.808 1.319.545 0 1.178-.252 1.465-.598l.088-.416c-.2.176-.492.246-.686.246-.275 0-.375-.193-.304-.533L8.93 6.588z\"/>\n      <circle cx=\"8\" cy=\"4.5\" r=\"1\"/>\n    </svg>\n    </a>\n\t\t\t\t\t\t    </p>\n\t\t\t\t\t\t\n\t\t\t\t    </div>\n\t\t\t\t\n\t\t\t</div>\n            <div class=\"description\">\n\t\t\t\t\t<p>{#:description}</p>\n\t\t\t\t</div>\n\t\t</div>\n\t</div>\n\t<!-- end product -->\n</div>\n                        ";

        var _pathImg = '';
        if (data.tag == cdC.productTag.HOT) _pathImg = '/shared/Icon/icon-hot-2.gif';
        if (data.tag == cdC.productTag.NEW) _pathImg = '/shared/Icon/icon-new-1.gif';
        _template = _template.replaceAll(new RegExp("{#:picture}", "gi"), data.picture).replaceAll(new RegExp("{#:id}", "gi"), data.id).replaceAll(new RegExp("{#:picture-hot}", "gi"), _pathImg).replaceAll(new RegExp("{#:name}", "gi"), data.name).replaceAll(new RegExp("{#:trackingLink}", "gi"), '/san-pham/' + data.code).replaceAll(new RegExp("{#:link}", "gi"), '/san-pham/' + data.code).replaceAll(new RegExp("{#:price}", "gi"), helper.formatNumber.k(data.price)).replaceAll(new RegExp("{#:description}", "gi"), data.description ? helper.formatString.truncate(helper.formatString.decodeHtml(data.description, { normal: true }), 150) : "").replaceAll(new RegExp("{#:categoryName}", "gi"), data.categoryName || "");
        return _template;
    };
    var _formatProductHero = function _formatProductHero(data) {
        var _template = "\n                        <li>\n                            <div class=\"img-hero\" style=\"background-image: url({#:bigPicture}); width: 100%;\n    float: left;\n    margin-right: -100%;\n    opacity: 1;\n    z-index: 2;\n    background-repeat: no-repeat;\n    background-size: 100% 100%;\"></div>\n                            <div class=\"overlay-gradient\"></div>\n                            <div class=\"container\">\n                                <div class=\"col-md-6 col-md-offset-3 col-md-pull-3 js-fullheight slider-text\">\n                                    <div class=\"slider-text-inner button-pulse\">\n                                        <div class=\"desc\">\n                                            <span class=\"price\">{#:price}</span>\n                                            <h2>{#:name}</h2>\n                                            <p style=\"color: #e5c72a;\">{#:description}</p>\n                                            <p class=\"button\"><a href=\"{#:trackingLink}\" target=\"_blank\" class=\"btn-read-more pulse\" data-id=\"{#:id}\">Xem chi tiết</a></p>\n                                        </div>\n                                    </div>\n                                </div>\n                            </div>\n                        </li>\n                    ";
        _template = _template.replaceAll(new RegExp("{#:bigPicture}", "gi"), data.bigPicture).replaceAll(new RegExp("{#:id}", "gi"), data.id).replaceAll(new RegExp("{#:name}", "gi"), data.name).replaceAll(new RegExp("{#:price}", "gi"), helper.formatNumber.k(data.price)).replaceAll(new RegExp("{#:description}", "gi"), data.description ? helper.formatString.truncate(helper.formatString.decodeHtml(data.description, { normal: true }), 150) : "").replaceAll(new RegExp("{#:trackingLink}", "gi"), '/san-pham/' + data.code);
        return _template;
    };
    var _formatCategoryTemplate = function _formatCategoryTemplate(data) {
        var _html = '<a class="btn" data-id="{#:id}" data-name= "{#:name}">{#:name}</a>';

        return _html.replaceAll(new RegExp("{#:id}", "gi"), data.id).replaceAll(new RegExp("{#:name}", "gi"), data.name);
    };

    $(_btnProductSearch).keyup(function (e) {
        clearTimeout(timeDelay);
        _captionSearchResult("");

        $(_mshop_filter + ' .loader').show();
        timeDelay = setTimeout(function () {
            //_genericPagination();
            _loadProductMore(true);
            $(_mshop_filter + ' .loader').hide();
        }, 2500);
    });
    var _genericHero = function _genericHero() {
        var _url, _html;

        return regeneratorRuntime.async(function _genericHero$(context$2$0) {
            while (1) switch (context$2$0.prev = context$2$0.next) {
                case 0:
                    _url = '/home/producthero';
                    _html = '';
                    context$2$0.next = 4;
                    return regeneratorRuntime.awrap($.get(_url, function (res) {
                        if (res.source) {
                            $(res.source).each(function (index, item) {
                                _html += _formatProductHero(item);
                            });
                        }
                    }));

                case 4:
                    $(_mnshop_product_hero + ' ul.slides').html(_html);
                    helper.bridgeHandle.sliderMain();

                case 6:
                case "end":
                    return context$2$0.stop();
            }
        }, null, this);
    };
    var _genericCategoryButton = function _genericCategoryButton() {
        var _url, _html;

        return regeneratorRuntime.async(function _genericCategoryButton$(context$2$0) {
            while (1) switch (context$2$0.prev = context$2$0.next) {
                case 0:
                    _url = '/home/getcategorys';
                    _html = '<a class="btn" data-id="ALL" data-name="Tất cả">Tất cả</a>';
                    context$2$0.next = 4;
                    return regeneratorRuntime.awrap($.get(_url, {}, function (res) {
                        if (res) {
                            $(res).each(function (index, item) {
                                _html += _formatCategoryTemplate(item);
                            });
                        }
                    }));

                case 4:
                    $(_mshop_product_client + ' ' + _btn_categorys).html(_html);

                case 5:
                case "end":
                    return context$2$0.stop();
            }
        }, null, this);
    };
    var _captionSearchResult = function _captionSearchResult(text) {
        $(_mshop_filter + ' ' + _search_result).text(text);
    };
    var _loadProductMore = function _loadProductMore(reload) {
        var _cardId = $(".mshop-filter").data("cardId");
        if (reload) {
            numberDeplayLazy = 0;
            numberCountMax = 0;
            $(_mshop_product_client).find('#' + _cardId).html('');
            $(_mshop_product_client + ' .msshop-product-nav.read-more').show();
        }
        var _url = '/home/productpage{#:_paramStrs}';
        var _paramStrs = "";
        //get values
        var _values = [];
        $(_mshop_product_client + ' ' + _btn_categorys + ' a.active').each(function (index, item) {
            var _id = $(item).data("id");
            if (_id != "ALL") _values.push($(item).data("id"));
        });
        var _textSearch = $(_mshop_filter).find(_btnProductSearch).first().val();
        //parse filter
        if (_textSearch || _values != []) {
            var _paramObject = {
                TextSearch: _textSearch,
                CategoryIds: _values,
                SkipCount: numberDeplayLazy,
                TakeRecords: cdC.pageSize
            };
            _paramStrs = "?paramStrs=" + JSON.stringify(_paramObject);
        };
        _url = _url.replaceAll(new RegExp("{#:_paramStrs}", "gi"), _paramStrs || "");

        $.get(_url, {}, function (res) {
            var _html = '';
            $(res.source).each(function (index, value) {
                _html += _formatCardTemplate(value);
            });
            //set total row into numberCountMax
            numberCountMax = res.total;
            if ($(_btnProductSearch).val()) {
                var _resultText = "Tìm thấy {#:totalItem} kết quả";
                _resultText = _resultText.replaceAll(new RegExp("{#:totalItem}", "gi"), res.total);
                _captionSearchResult(_resultText);
            }
            //cập nhật tham số load lazy
            numberDeplayLazy += res.source.length;
            $(_mshop_product_client).find('#' + _cardId).append(_html);

            console.log("numberDeplayLazy: " + numberDeplayLazy, "numberCountMax :" + numberCountMax);
            if (numberDeplayLazy == numberCountMax && numberDeplayLazy != 0) {
                $(_mshop_product_client + ' .msshop-product-nav.read-more').hide();
            }
        });
    };
    var _jumpFocusTag = function _jumpFocusTag(group) {
        //clear focus
        $(_mshop_product_client + ' ' + _btn_categorys + ' a').removeClass("active");

        //Thực phẩm chức năng
        if (group == 'thuc-pham-chuc-nang') {
            $(_mshop_product_client + ' a').each(function (index, item) {
                if (['2fc89e20-b009-427b-8e34-4483bc0638fa', //Sinh lý
                '8829d257-bafd-494b-bd6c-fce6954791b2' // Tăng -giảm cân
                ].indexOf($(item).data('id')) != -1) {
                    $(item).click();
                }
            });
        }
        //Mỹ phẩm
        if (group == 'my-pham') {
            $(_mshop_product_client + ' a').each(function (index, item) {
                if (['ecac91c4-c0a2-41ae-b1e0-526e7134dd02', //Mẹ & bé
                '717db383-b665-40c1-864c-635aa0b711f0', //Làm đẹp
                '68163f53-529a-4177-9835-9ff5825b1741']. //Mỹ phẩm
                indexOf($(item).data('id')) != -1) {
                    $(item).click();
                }
            });
        }
        //Đặc trị
        if (group == 'dac-tri') {
            $(_mshop_product_client + ' a').each(function (index, item) {
                if (['f2919983-d99a-4361-b1df-8be8ed873a41' //Đặc trị     
                ].indexOf($(item).data('id')) != -1) {
                    $(item).click();
                }
            });
        }

        //jump to tag
        helper.jumpTag($('#fh5co-product'));
    };
    // init
    _genericHero();
    _genericCategoryButton();
    $(_mshop_filter + ' .loader').hide();
    //event
    $(document).on('click', _mshop_product_client + ' .btn-read-more, ' + _mnshop_product_hero + ' .btn-read-more', function (e) {
        var _url = $(e.target).data('link');
        if (!_url) _url = $(e.target).parent().data('link');
        var _id = $(e.target).data('id');

        open(_url);
    });
    $(document).on('click', _mshop_product_client + ' ' + _btn_categorys + ' a', function (e) {
        e.preventDefault();
        if ($(e.target).hasClass("active")) $(e.target).removeClass("active");else $(e.target).addClass("active");

        if ($(e.target).data('id') == "ALL") {
            $(_mshop_product_client + ' ' + _btn_categorys + ' a').removeClass("active");
            $(e.target).addClass("active");
        } else {
            $(_mshop_product_client + ' ' + _btn_categorys + ' a[data-id=ALL]').removeClass("active");
        }

        clearTimeout(timeDelay);
        _captionSearchResult("");
        $(_mshop_filter + ' .loader').show();
        timeDelay = setTimeout(function () {
            //_genericPagination();
            _loadProductMore(true);
            $(_mshop_filter + ' .loader').hide();
        }, 700);
        _loadProductMore(true);
    });
    $(document).on('click', _mshop_product_client + ' .msshop-product-nav.read-more p.button', function (e) {
        _loadProductMore();
    });
    $(document).on('click', '.mnshop-service-client a.service-read-more', function (e) {
        var _group = $(e.target).data('group');
        if (typeof _group == "undefined") _group = $(e.target).closest('a').data('group');

        //clear focus
        $(_mshop_product_client + ' ' + _btn_categorys + ' a').removeClass("active");

        _jumpFocusTag(_group);
    });
    _loadProductMore();
    //auto jump
    setTimeout(function () {
        var _path = window.location.pathname;
        if (_path == '/f/thuc-pham-chuc-nang') {
            _jumpFocusTag('thuc-pham-chuc-nang');
        }
        if (_path == '/f/my-pham') {
            _jumpFocusTag('my-pham');
        }
        if (_path == '/f/dac-tri') {
            _jumpFocusTag('dac-tri');
        }
    }, 700);
})($, document);

