//search product
(function () {    
    $('.search input#search-string-value').keypress(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            searchString(this.value);
        }
    });
    $('.search button').on('click', function (e) {
        let text = $('.search input#search-string-value').last().val();
        searchString(text);
    });
    function searchString(text) {
        if (text == "") return;
        let _url = '/san-pham/tim-kiem/' + text;
        open(_url);
    }
})();
//product sort
(function () {
    $(".product.sort select").on('change',function (e) {
        let _v = e.target.value;
        open(_v,"_self");
    });
})();
//raw layout product
(function () {
    showSlides(0);
    function plusSlides(n,e) {
        showSlides(n,e);
    }
    function showSlides(n,e) {
        if (typeof (e) == "undefined") {
            //show item first defaut
            let $categorys = $('.category-product-v');
            for (var i = 0; i < $categorys.length; i++) {
                var groupProducts = $($categorys[i]).find('.mySlides');
                if (groupProducts.length == 0) continue;
                for (ig = 0; ig < groupProducts.length; ig++) {
                    groupProducts[ig].style.display = "none";
                }
                groupProducts[0].style.display = "block";
                groupProducts[0].style.opacity = "1";
            }
        }
        else {
            let $categoryProduct = $(e.target).closest('.category-product-v');
            let iShowOfGroup = $categoryProduct.data('ishow');
            let iShowCurrent = iShowOfGroup + n;
            

            let groupProducts = $categoryProduct.find('.mySlides');
            if (groupProducts.length == 0) return;
            for (i = 0; i < groupProducts.length; i++) {
                groupProducts[i].style.display = "none";
            }

            if (iShowCurrent > (groupProducts.length - 1)) iShowCurrent = 0;
            if (iShowCurrent < 0) iShowCurrent = groupProducts.length - 1;

            groupProducts[iShowCurrent].style.display = "block";
            groupProducts[iShowCurrent].style.opacity = "1";

            $categoryProduct.data('ishow', iShowCurrent);
        }
    }
    //event
    $('.category-product-v a.prev').on('click', function (e) { plusSlides(-1,e);});
    $('.category-product-v a.next').on('click', function (e) { plusSlides(1,e);});
})();