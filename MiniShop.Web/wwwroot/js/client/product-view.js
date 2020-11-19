(function ($, document) {
    let _mnshop_product_view = "#mnshop-product-view";
    let _formatProductView = function (data) {
        let _template = `
                       <iframe src="{#:trackingLink}">
                        </iframe>
                    `;
        _template = _template
            .replaceAll(new RegExp("{#:bigPicture}", "gi"), data.bigPicture)
            .replaceAll(new RegExp("{#:id}", "gi"), data.id)
            .replaceAll(new RegExp("{#:name}", "gi"), data.name)
            .replaceAll(new RegExp("{#:price}", "gi"), helper.formatNumber.k(data.price))
            .replaceAll(new RegExp("{#:description}", "gi"), data.description ? helper.formatString.truncate(helper.formatString.decodeHtml(data.description, { normal: true }), 150) : "")
            .replaceAll(new RegExp("{#:trackingLink}", "gi"), data.trackingLink);
        return _template;
    };
    
    let _genericView = async function () {
        let _id = $('input[name=ProductId]').val();
        let _url = '/san-pham/data/{#:productId}';
        _url = _url.replaceAll(new RegExp("{#:productId}","gi"), _id);

        let _html = '';
        await $.get(_url, function (res) {
            if (res.source) {
                _html += _formatProductView(res.source);
            }
        });
        $(_mnshop_product_view + ' .mnshop-product-content').html(_html);
        _genericShare();
    }
    let _genericShare = function () {
        $(".social-items").cShare({
            description: 'jQuery plugin - C Share buttons...',
            showButtons: ['fb', 'line', 'plurk', 'weibo', 'twitter', 'tumblr', 'email']
        });
    }
    // init
    _genericView();
}($, document));