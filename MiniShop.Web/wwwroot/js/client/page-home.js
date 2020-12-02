﻿(function ($, document) {
    let _paginationClass = ".mnshop-pagination";
    let _btnProductSearch = "#mnshop-btn-product-search";
    let _mshop_product_client = ".mshop-product-client";
    let _mnshop_product_hero = ".mnshop-product-hero";
    let _mshop_filter = ".mshop-filter";
    let _search_result = ".search__result";
    let _btn_categorys = ".btn-categorys";
    var timeDelay;
    var numberDeplayLazy = 0;
    var numberCountMax = 0;

    let _formatCardTemplate = function (data) {
        let _template = `
            <div class="col-xs-12 col-md-6 col-lg-4">
	<!-- First product box start here-->
	<div class="prod-info-main prod-wrap clearfix">
		<div class="row">
				<div class="col-md-5 col-sm-12 col-xs-12">
					<div class="product-image"> 
                        <div class="border"></div>
                        <div class="main-element">
                            <img src="{#:picture}" class="img-responsive">   
                        </div>
						
					</div>
				</div>
				<div class="col-md-7 col-sm-12 col-xs-12">
				<div class="product-deatil">
						<h5 class="name">
							<a href="{#:trackingLink}" style="font-weight: 700;">
								{#:name}
							</a>
							<a href="#">
								<span style="margin-top:5px">Nhóm sản phẩm {#:categoryName}</span>
							</a>   
						</h5>
						<p class="price-container">
							<span>{#:price}</span>
                            <a title="Giá tham khảo, có thể chênh lệch với giá thực tế" class="text-decoration-none">
<svg width="0.6em" height="0.6em" viewBox="0 0 16 16" class="bi bi-info-circle" fill="currentColor" xmlns="http://www.w3.org/2000/svg">
  <path fill-rule="evenodd" d="M8 15A7 7 0 1 0 8 1a7 7 0 0 0 0 14zm0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16z"/>
  <path d="M8.93 6.588l-2.29.287-.082.38.45.083c.294.07.352.176.288.469l-.738 3.468c-.194.897.105 1.319.808 1.319.545 0 1.178-.252 1.465-.598l.088-.416c-.2.176-.492.246-.686.246-.275 0-.375-.193-.304-.533L8.93 6.588z"/>
  <circle cx="8" cy="4.5" r="1"/>
</svg>
</a>
						</p>
						<span class="tag1">
							<img src="{#:picture-hot}" style="width: 55px">  
						</span>
				</div>
				<div class="description">
					<p>{#:description}</p>
				</div>
				<div class="product-info smart-form">
					<div class="row">
						<div class="col-md-12"> 
                            <button class="btn-read-more" data-link="{#:link}" data-id="{#:id}" style="margin: 0 auto">
                              <span>Chi tiết</span>
                            </button>
						</div>						
					</div>
				</div>
			</div>
		</div>
	</div>
	<!-- end product -->
</div>
                        `;

        let _pathImg = '';
        if (data.tag == cdC.productTag.HOT) _pathImg = '/shared/Icon/icon-hot-2.gif';
        if (data.tag == cdC.productTag.NEW) _pathImg = '/shared/Icon/icon-new-1.gif';
        _template = _template
            .replaceAll(new RegExp("{#:picture}", "gi"), data.picture)
            .replaceAll(new RegExp("{#:id}", "gi"), data.id)
            .replaceAll(new RegExp("{#:picture-hot}", "gi"), _pathImg)
            .replaceAll(new RegExp("{#:name}", "gi"), data.name)
            .replaceAll(new RegExp("{#:trackingLink}", "gi"), '/san-pham/' + data.code)
            .replaceAll(new RegExp("{#:link}", "gi"), '/san-pham/' + data.code)
            .replaceAll(new RegExp("{#:price}", "gi"), helper.formatNumber.k(data.price))
            .replaceAll(new RegExp("{#:description}", "gi"), data.description ? helper.formatString.truncate(helper.formatString.decodeHtml(data.description, { normal: true }), 150) : "")
            .replaceAll(new RegExp("{#:categoryName}", "gi"), data.categoryName || "")
        return _template;
    };
    let _formatProductHero = function (data) {
        let _template = `
                        <li>
                            <div class="img-hero" style="background-image: url({#:bigPicture}); width: 100%;
    float: left;
    margin-right: -100%;
    opacity: 1;
    z-index: 2;
    background-repeat: no-repeat;
    background-size: 100% 100%;"></div>
                            <div class="overlay-gradient"></div>
                            <div class="container">
                                <div class="col-md-6 col-md-offset-3 col-md-pull-3 js-fullheight slider-text">
                                    <div class="slider-text-inner button-pulse">
                                        <div class="desc">
                                            <span class="price">{#:price}</span>
                                            <h2>{#:name}</h2>
                                            <p style="color: #e5c72a;">{#:description}</p>
                                            <p class="button"><a href="{#:trackingLink}" target="_blank" class="btn-read-more pulse" data-id="{#:id}">Xem chi tiết</a></p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </li>
                    `;
        _template = _template
            .replaceAll(new RegExp("{#:bigPicture}", "gi"), data.bigPicture)
            .replaceAll(new RegExp("{#:id}", "gi"), data.id)
            .replaceAll(new RegExp("{#:name}", "gi"), data.name)
            .replaceAll(new RegExp("{#:price}", "gi"), helper.formatNumber.k(data.price))
            .replaceAll(new RegExp("{#:description}", "gi"), data.description ? helper.formatString.truncate(helper.formatString.decodeHtml(data.description, { normal: true }), 150) : "")
            .replaceAll(new RegExp("{#:trackingLink}", "gi"), '/san-pham/' + data.code);
        return _template;
    };
    let _formatCategoryTemplate = function (data) {
        let _html = '<a class="btn" data-id="{#:id}" data-name= "{#:name}">{#:name}</a>';

        return _html.replaceAll(new RegExp("{#:id}", "gi"), data.id)
            .replaceAll(new RegExp("{#:name}", "gi"), data.name);
    };

    $(_btnProductSearch).keyup(function (e) {
        clearTimeout(timeDelay);
        _captionSearchResult("");

        $(_mshop_filter + ' .loader').show();
        timeDelay = setTimeout(function () {
            //_genericPagination();
            _loadLazy(true);
            $(_mshop_filter + ' .loader').hide();
        }, 2500);
    });
    let _genericHero = async function () {
        let _url = 'home/producthero';
        let _html = '';
        await $.get(_url, function (res) {
            if (res.source) {
                $(res.source).each(function (index, item) {
                    _html += _formatProductHero(item);
                });
            }
        });
        $(_mnshop_product_hero + ' ul.slides').html(_html);
        helper.bridgeHandle.sliderMain();
    }
    let _genericCategoryButton = async function () {
        let _url = 'home/getcategorys';
        let _html = '<a class="btn" data-id="ALL" data-name="Tất cả">Tất cả</a>'
        await $.get(_url, {}, function (res) {
            if (res) {
                $(res).each(function (index, item) {
                    _html += _formatCategoryTemplate(item);
                });
            }
        });
        $(_mshop_product_client + ' ' + _btn_categorys).html(_html);
    }
    let _captionSearchResult = function (text) {
        $(_mshop_filter + ' ' + _search_result).text(text);
    }
    let _loadLazy = function (reload) {
        let _cardId = $(".mshop-filter").data("cardId");
        if (reload) {
            numberDeplayLazy = 0;
            numberCountMax = 0;
            $(_mshop_product_client).find('#' + _cardId).html('');
        }
        let _url = 'home/productpage{#:_paramStrs}';
        let _paramStrs = "";
        //get values
        let _values = [];
        $(_mshop_product_client + ' ' + _btn_categorys + ' a.active').each(function (index, item) {
            let _id = $(item).data("id");
            if (_id != "ALL")
                _values.push($(item).data("id"));
        });
        let _textSearch = $(_mshop_filter).find(_btnProductSearch).first().val();
        //parse filter
        if (_textSearch || _values != []) {
            let _paramObject = {
                TextSearch: _textSearch,
                CategoryIds: _values,
                SkipCount: numberDeplayLazy,
                TakeRecords: cdC.pageSize
            }
            _paramStrs = "?paramStrs=" + JSON.stringify(_paramObject);
        };
        _url = _url.replaceAll(new RegExp("{#:_paramStrs}", "gi"), _paramStrs || "");

        $.get(_url, {}, function (res) {
            let _html = '';
            $(res.source).each(function (index, value) {
                _html += _formatCardTemplate(value);
            });
            //set total row into numberCountMax
            numberCountMax = res.total;
            if ($(_btnProductSearch).val()) {
                let _resultText = "Tìm thấy {#:totalItem} kết quả";
                _resultText = _resultText.replaceAll(new RegExp("{#:totalItem}", "gi"), res.total);
                _captionSearchResult(_resultText);
            }
            //cập nhật tham số load lazy
            numberDeplayLazy += res.source.length;
            $(_mshop_product_client).find('#' + _cardId).append(_html);

            console.log("numberDeplayLazy: " + numberDeplayLazy, "numberCountMax :" + numberCountMax)
        });


    }
    // init
    _genericHero();
    _genericCategoryButton();
    $(_mshop_filter + ' .loader').hide();
    //event
    $(document).on('click',
        _mshop_product_client + ' .btn-read-more, ' + _mnshop_product_hero + ' .btn-read-more',
        function (e) {
            let _url = $(e.target).data('link');
            if (!_url) _url = $(e.target).parent().data('link');
            let _id = $(e.target).data('id');

            open(_url);
        });
    $(document).on('click', _mshop_product_client + ' ' + _btn_categorys + ' a', function (e) {
        e.preventDefault();
        if ($(e.target).hasClass("active"))
            $(e.target).removeClass("active");
        else
            $(e.target).addClass("active");

        if ($(e.target).data('id') == "ALL") {
            $(_mshop_product_client + ' ' + _btn_categorys + ' a').removeClass("active");
            $(e.target).addClass("active");
        }
        else {
            $(_mshop_product_client + ' ' + _btn_categorys + ' a[data-id=ALL]').removeClass("active");
        }

        _loadLazy(true);
    })
    //scroll
    $(window).scroll(function () {
        if (($(window).scrollTop() >= $(document).height() - $(window).height() - $("#fh5co-footer")[0].offsetHeight)) {   
            clearTimeout(timeDelay);
            if (numberDeplayLazy == numberCountMax && numberDeplayLazy != 0) {
                $(_mshop_product_client + ' .loader-tomato').hide();
                return;
            }
            $(_mshop_product_client + ' .loader-tomato').show();
            timeDelay = setTimeout(function () {
                _loadLazy();
                $(_mshop_product_client + ' .loader-tomato').hide();
            }, 2200);
            
        }
    });
}($, document));