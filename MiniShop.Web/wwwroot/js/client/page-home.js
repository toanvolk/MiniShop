(function ($, document){
    let _paginationClass = ".mnshop-pagination";
    let _formatTemplate = function (data) {
        let _template = `
            <div class="col-md-4 text-center animate-box">
                <div class="product">
                    <div class="product-grid" style="background-image:url({#:picture});">
                        <div class="inner">
                            <p>
                                <a href="single.html" class="icon"><i class="icon-shopping-cart"></i></a>
                                <a href="single.html" class="icon"><i class="icon-eye"></i></a>
                            </p>
                        </div>
                    </div>
                    <div class="desc">
                        <h3><a href="{#:trackingLink}">{#:name}</a></h3>
                        <span class="price">{#:price}</span>
                    </div>
                </div>
            </div>
                        `;
        _template = _template
            .replaceAll(new RegExp("{#:picture}", "gi"), data.picture)
            .replaceAll(new RegExp("{#:name}", "gi"), data.name)
            .replaceAll(new RegExp("{#:trackingLink}", "gi"), data.trackingLink)
            .replaceAll(new RegExp("{#:price}", "gi"), data.price)
        return _template;
    }
    //$(_productPageId).load("product");
    $(_paginationClass).pagination({
        dataSource: 'home/productpage',
        locator: 'source',
        pageNumber: 1,
        totalNumberLocator: function (response) {
            // you can return totalNumber by analyzing response content
            return response.total;
        },
        pageSize: 2,
        ajax: {
            beforeSend: function () {

            }
        },
        callback: function (data, pagination) {
            // template method of yourself
            //var html = template(data);
            //dataContainer.html(html);
            console.log(data, pagination);
            let _$containPagination = $(pagination.el).closest('.row');
            let _$containRoot = $(pagination.el).closest('.mshop-product-client');
            let _cardId = $(_$containPagination).data("cardId");

            let _html = '';
            $(data).each(function (index, value) {
                _html += _formatTemplate(value);
            });
            $(_$containRoot).find('#' + _cardId).html(_html);
            //contentWayPoint();
        }
    });
}($, document));