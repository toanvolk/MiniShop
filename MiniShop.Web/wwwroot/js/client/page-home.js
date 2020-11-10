(function ($, document) {
    let _paginationClass = ".mnshop-pagination";
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
                            <a href="javascript:void(0);" class="btn btn-info">More info</a>
						</div>
						<div class="col-md-12">
							<div class="rating">Rating:
								<label for="stars-rating-5"><i class="fa fa-star text-danger"></i></label>
								<label for="stars-rating-4"><i class="fa fa-star text-danger"></i></label>
								<label for="stars-rating-3"><i class="fa fa-star text-danger"></i></label>
								<label for="stars-rating-2"><i class="fa fa-star text-warning"></i></label>
								<label for="stars-rating-1"><i class="fa fa-star text-warning"></i></label>
							</div>
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
            .replaceAll(new RegExp("{#:price}", "gi"), data.price)
            .replaceAll(new RegExp("{#:description}", "gi"), data.description || "")
			.replaceAll(new RegExp("{#:categoryName}", "gi"), data.categoryName || "")
        return _template;
    }
    //$(_productPageId).load("product");
	$(_paginationClass).pagination({
		//dataSource: 'home/productpage',
		dataSource: function (done) {
			$.get('home/productpage', {}, function (res) { done(res.source);});
		},
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
            console.log(data, pagination);
            let _$containPagination = $(pagination.el).closest('.row');
            let _$containRoot = $(pagination.el).closest('.mshop-product-client');
            let _cardId = $(_$containPagination).data("cardId");

            let _html = '';
            $(data).each(function (index, value) {
                _html += _formatTemplate(value);
            });
            $(_$containRoot).find('#' + _cardId).html(_html);
        }
    });
}($, document));