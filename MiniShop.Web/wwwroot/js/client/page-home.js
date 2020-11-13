(function ($, document) {
    let _paginationClass = ".mnshop-pagination";
    let _btnProductSearch = "#mnshop-btn-product-search";
    let _mshop_product_client = ".mshop-product-client";
    let _mnshop_product_hero = ".mnshop-product-hero";
    let _formatCardTemplate = function (data) {
        let _template = `
            <div class="col-xs-12 col-md-6">
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
                            <button class="btn-read-more" data-link="{#:link}" style="margin: 0 auto">
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
        _template = _template
            .replaceAll(new RegExp("{#:picture}", "gi"), data.picture)
            .replaceAll(new RegExp("{#:picture-hot}", "gi"), '/shared/Icon/icon-hot-2.gif')
            .replaceAll(new RegExp("{#:name}", "gi"), data.name)
            .replaceAll(new RegExp("{#:trackingLink}", "gi"), data.trackingLink)
            .replaceAll(new RegExp("{#:link}", "gi"), data.trackingLink)
            .replaceAll(new RegExp("{#:price}", "gi"), helper.formatNumber.k(data.price))
            .replaceAll(new RegExp("{#:description}", "gi"), data.description ? helper.formatString.truncate(helper.formatString.decodeHtml(data.description, {normal: true}),150) : "")
            .replaceAll(new RegExp("{#:categoryName}", "gi"), data.categoryName || "")
        return _template;
    };
    let _formatProductHero = function (data) {
        let _template = `
                        <li style="background-image: url({#:bigPicture})">
                            <div class="overlay-gradient"></div>
                            <div class="container">
                                <div class="col-md-6 col-md-offset-3 col-md-pull-3 js-fullheight slider-text">
                                    <div class="slider-text-inner">
                                        <div class="desc">
                                            <span class="price">{#:price}</span>
                                            <h2>{#:name}</h2>
                                            <p>{#:description}</p>
                                            <p><a href="{#:trackingLink}" class="btn btn-primary btn-outline btn-lg">Xem chi tiết</a></p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </li>
                    `;
        _template = _template
            .replaceAll(new RegExp("{#:bigPicture}", "gi"), data.bigPicture)
            .replaceAll(new RegExp("{#:name}", "gi"), data.name)
            .replaceAll(new RegExp("{#:price}", "gi"), helper.formatNumber.k(data.price))
            .replaceAll(new RegExp("{#:description}", "gi"), data.description ? helper.formatString.truncate(helper.formatString.decodeHtml(data.description, { normal: true }), 150) : "")
            .replaceAll(new RegExp("{#:trackingLink}", "gi"), data.trackingLink);
        return _template;
    }

    var myVar;
    $(_btnProductSearch).keyup(function (e) {
        clearTimeout(myVar);
        $(_btnProductSearch).addClass('load_stating');
        myVar = setTimeout(function () {
            let _textSearch = $(e.target).closest('.mshop-filter').find(_btnProductSearch).val();
            _genericPagination($(_paginationClass), _textSearch);

            $(_btnProductSearch).removeClass('load_stating');
        }, 2500);
    });
    //
    let _genericPagination = function (content, textSearch) {
        let _url = 'home/productpage{#:textSearch}';
        if (textSearch) {
            content.pagination('destroy');
            textSearch = "?textSearch=" + textSearch;
        };
        _url = _url.replaceAll(new RegExp("{#:textSearch}", "gi"), textSearch || "");

        content.pagination({
            dataSource: _url,
            className: 'paginationjs-big',
            locator: 'source',
            pageNumber: 1,
            totalNumberLocator: function (response) {
                // you can return totalNumber by analyzing response content
                return response.total;
            },
            pageSize: cdC.pageSize,
            ajax: {
                beforeSend: function () {

                }
            },
            callback: function (data, pagination) {
                let _$containPagination = $(pagination.el).closest('.row');
                let _$containRoot = $(pagination.el).closest(_mshop_product_client);
                let _cardId = $(_$containPagination).data("cardId");

                let _html = '';
                $(data).each(function (index, value) {
                    _html += _formatCardTemplate(value);
                });
                $(_$containRoot).find('#' + _cardId).html(_html);
            }
        });
    }
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
    // init
    _genericHero();
    _genericPagination($(_paginationClass));
    //event
    $(document).on('click',_mshop_product_client+' .btn-read-more',  function (e) {
        let _url = $(e.target).data('link');
        console.log(_url);
    });
}($, document));