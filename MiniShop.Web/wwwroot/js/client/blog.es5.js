'use strict';

(function () {
    //init
    (function () {
        $.get('/blog/page', { pageNumber: 1, pageSize: 6 }, function (res) {});
    })();
})();

