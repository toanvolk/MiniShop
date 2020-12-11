(function () {
    //init
    (function () {
        let _htmlTemplate = `
            <div class="col-lg-6 col-md-6 col-xs-12">
                <div class="blog-card {#:alt}">
                    <div class="meta">
                        <div class="photo" style="background-image: url({#:picturePath})"></div>
                        <ul class="details">
                            <li class="author"><a href="#">{#:author}</a></li>
                            <li class="date">{#:publishDate}</li>
                            <li class="tags">
                                <ul>
                                            {#:htmlHashTag}
                                </ul>
                            </li>
                        </ul>
                    </div>
                    <div class="description">
                        <h1>{#:title}</h1>
                        <h2>{#:category}</h2>
                        <p style="max-height: 120px; height:120px">{#:descriptionShort}</p>
                        <p class="read-more">
                            <a href="{#:readMorePath}" target="_blank">Đọc thêm</a>
                        </p>
                    </div>
                </div>
            </div>            
            `;
        let _htmlHashTag = ' <li><a href="#">{#:tag}</a></li>'

        $.get('/blog/page',
            { pageNumber: 1, pageSize: 6 },
            function (res) {
                if (res.source) {
                    let _html = '';
                    $(res.source).each(function (index, item) {                        
                        if (index % 2 == 0) _html = "<div class='row'>";
                        index++;
                        if (index % 2 == 0) alt = "alt"; else alt = "";
                        //content html
                        let _tagHtml = '';
                        if (item.hashTag) {
                            let _tags = item.hashTag.split(',');
                            $(_tags).each(function (index, tag) {
                                _tagHtml += _htmlHashTag.replaceAll(new RegExp("{#:tag}","gi"), tag);
                            });
                        }
                        _html += _htmlTemplate
                            .replaceAll(new RegExp("{#:alt}", "gi"), alt)
                            .replaceAll(new RegExp("{#:picturePath}", "gi"), item.picturePath)
                            .replaceAll(new RegExp("{#:author}", "gi"), item.author)
                            .replaceAll(new RegExp("{#:publishDate}", "gi"), item.publishDate)
                            .replaceAll(new RegExp("{#:title}", "gi"), item.title)
                            .replaceAll(new RegExp("{#:category}", "gi"), item.category)
                            .replaceAll(new RegExp("{#:descriptionShort}", "gi"), item.descriptionShort)
                            .replaceAll(new RegExp("{#:readMorePath}", "gi"), item.readMorePath)
                            .replaceAll(new RegExp("{#:htmlHashTag}", "gi"), _tagHtml)

                        if (index % 2 == 0 || index == res.source.length) _html += "</div>";      

                    });
                    $('.mshop-blog-client').html(_html);
                }
            })
    })();
})();