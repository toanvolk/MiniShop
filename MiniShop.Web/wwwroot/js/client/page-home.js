﻿(function ($, document) {
    let _paginationClass = ".mnshop-pagination";
    let _btnProductSearch = "#mnshop-btn-product-search";
    let _mshop_product_client = ".mshop-product-client";
    let _formatTemplate = function (data) {
        let _template = `
            <div class="col-xs-12 col-md-6">
	<!-- First product box start here-->
	<div class="prod-info-main prod-wrap clearfix">
		<div class="row">
				<div class="col-md-5 col-sm-12 col-xs-12">
					<div class="product-image"> 
						<img src="{#:picture}" class="img-responsive"> 
						<span class="tag2 hot">
							HOT
						</span> 
					</div>
				</div>
				<div class="col-md-7 col-sm-12 col-xs-12">
				<div class="product-deatil">
						<h5 class="name">
							<a href="{#:trackingLink}">
								{#:name}
							</a>
							<a href="#">
								<span>Sản phẩm [{#:categoryName}]</span>
							</a>   
						</h5>
						<p class="price-container">
							<span>{#:price}</span>
						</p>
						<span class="tag1"></span> 
				</div>
				<div class="description">
					<p>{#:description}</p>
				</div>
				<div class="product-info smart-form">
					<div class="row">
						<div class="col-md-12"> 
                            <button class="btn-read-more" data-link="{#:link}">
                              <span>Chi tiết</span>
                            </button>
						</div>
						<div class="col-md-12">
							<label><i class="fa fa-eye text-warning"></i> 6868</label>
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
            .replaceAll(new RegExp("{#:name}", "gi"), data.name)
            .replaceAll(new RegExp("{#:trackingLink}", "gi"), data.trackingLink)
            .replaceAll(new RegExp("{#:link}", "gi"), data.trackingLink)
            .replaceAll(new RegExp("{#:price}", "gi"), helper.formatNumber.k(data.price))
            .replaceAll(new RegExp("{#:description}", "gi"), data.description || "")
            .replaceAll(new RegExp("{#:categoryName}", "gi"), data.categoryName || "")
        return _template;
    };

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
            className: 'paginationjs-theme-green paginationjs-big',
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
                    _html += _formatTemplate(value);
                });
                $(_$containRoot).find('#' + _cardId).html(_html);
            }
        });
    }
    // init
    _genericPagination($(_paginationClass));
    //event
    $(document).on('click',_mshop_product_client+' .btn-read-more',  function (e) {
        let _url = $(e.target).data('link');
        console.log(_url);
    });
}($, document));