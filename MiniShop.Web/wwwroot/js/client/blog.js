(function () {
    //declare
    let _htmlContent = `
            <div class="header">
                <h3>{#:title}</h3>
                <ul class="info">
                    <li class="publish-date">Ngày đăng {#:publishDate}</li>
                    <li class="author">| Nguồn: {#:author}</li>
                </ul>
            </div>
            <div class="content">{#:content}</div>
        `;

    //init
    (function () {
        let _htmlTemplate = `
            <div class="items-body-content">
                    <span><a href="/blog/{#:id}"> {#:title} </a></span>
                    <i class="icon">
                        <svg class="svg-icon" viewBox="0 0 20 20">
                            <path fill="none" d="M1.683,3.39h16.676C18.713,3.39,19,3.103,19,2.749s-0.287-0.642-0.642-0.642H1.683
								c-0.354,0-0.641,0.287-0.641,0.642S1.328,3.39,1.683,3.39z M1.683,7.879h11.545c0.354,0,0.642-0.287,0.642-0.641
								s-0.287-0.642-0.642-0.642H1.683c-0.354,0-0.641,0.287-0.641,0.642S1.328,7.879,1.683,7.879z M18.358,11.087H1.683
								c-0.354,0-0.641,0.286-0.641,0.641s0.287,0.642,0.641,0.642h16.676c0.354,0,0.642-0.287,0.642-0.642S18.713,11.087,18.358,11.087z
								 M11.304,15.576H1.683c-0.354,0-0.641,0.287-0.641,0.642s0.287,0.641,0.641,0.641h9.621c0.354,0,0.642-0.286,0.642-0.641
								S11.657,15.576,11.304,15.576z"></path>
                        </svg>
                    </i>
                </div> 
            `;
        $.get('/blog/page',
            { pageNumber: 1, pageSize: 6 },
            function (res) {
                if (res.source) {
                    let _html = '';
                    $(res.source).each(function (index, item) {

                        _html += _htmlTemplate
                            .replaceAll(new RegExp("{#:id}", "gi"), item.id)
                            .replaceAll(new RegExp("{#:title}", "gi"), item.title)

                    });
                    $('.mshop-blog-more-client .items-body').html(_html);
                }
            })
    })();
    //event
    //$(document).on('click', '.mshop-blog-more-client .items-body-content', function (e) {
    //    let _id = $(e.target).data('id');
    //    if (typeof (_id) == "undefined") _id = $(e.target).closest('.items-body-content').data('id');
    //    console.log(_id);

    //});
})();