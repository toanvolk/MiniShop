'use strict';

(function () {
    //declare
    var _htmlContent = '\n            <div class="header">\n                <h3>{#:title}</h3>\n                <ul class="info">\n                    <li class="publish-date">Ngày đăng {#:publishDate}</li>\n                    <li class="author">| Nguồn: {#:author}</li>\n                </ul>\n            </div>\n            <div class="content">{#:content}</div>\n        ';

    //init
    (function () {
        var _htmlTemplate = '\n            <div class="items-body-content">\n                    <span><a href="/{#:readMorePath}"> {#:title} </a></span>\n                    <i class="icon">\n                        <svg class="svg-icon" viewBox="0 0 20 20">\n                            <path fill="none" d="M1.683,3.39h16.676C18.713,3.39,19,3.103,19,2.749s-0.287-0.642-0.642-0.642H1.683\n\t\t\t\t\t\t\t\tc-0.354,0-0.641,0.287-0.641,0.642S1.328,3.39,1.683,3.39z M1.683,7.879h11.545c0.354,0,0.642-0.287,0.642-0.641\n\t\t\t\t\t\t\t\ts-0.287-0.642-0.642-0.642H1.683c-0.354,0-0.641,0.287-0.641,0.642S1.328,7.879,1.683,7.879z M18.358,11.087H1.683\n\t\t\t\t\t\t\t\tc-0.354,0-0.641,0.286-0.641,0.641s0.287,0.642,0.641,0.642h16.676c0.354,0,0.642-0.287,0.642-0.642S18.713,11.087,18.358,11.087z\n\t\t\t\t\t\t\t\t M11.304,15.576H1.683c-0.354,0-0.641,0.287-0.641,0.642s0.287,0.641,0.641,0.641h9.621c0.354,0,0.642-0.286,0.642-0.641\n\t\t\t\t\t\t\t\tS11.657,15.576,11.304,15.576z"></path>\n                        </svg>\n                    </i>\n                </div> \n            ';
        $.get('/blog/page', { pageNumber: 1, pageSize: 6 }, function (res) {
            if (res.source) {
                var _html = '';
                $(res.source).each(function (index, item) {

                    _html += _htmlTemplate.replaceAll(new RegExp("{#:readMorePath}", "gi"), item.readMorePath).replaceAll(new RegExp("{#:title}", "gi"), item.title);
                });
                $('.mshop-blog-more-client .items-body').html(_html);
            }
        });
    })();
    //event
    //$(document).on('click', '.mshop-blog-more-client .items-body-content', function (e) {
    //    let _id = $(e.target).data('id');
    //    if (typeof (_id) == "undefined") _id = $(e.target).closest('.items-body-content').data('id');
    //    console.log(_id);

    //});
    //fixed scroll
    (function () {
        window.onscroll = function () {
            if (window.innerWidth >= 1024) setFixed();
        };
        var areaMore = $('.mshop-blog-more-client .container')[0];
        var areaAds = $('.mshop-blog-ads-client .container')[0];
        var sticky = areaMore.offsetTop;

        function setFixed() {
            if (window.pageYOffset > sticky) {
                $(areaMore).addClass("sticky");
                $(areaAds).addClass("sticky");
            } else {
                $(areaMore).removeClass("sticky");
                $(areaAds).removeClass("sticky");
            }
        }
    })();
})();

