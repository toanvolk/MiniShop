(function () {

    //----------------------------------DEFINE----------------------------
    var assetRoot = '';
    var assetRootArray = undefined;
    var assetFonts = [];

    (function () {
        //init
        assetRoot = unescape($('#d-f-standand').text());
        assetRootArray = assetRoot.split(' ');

        assetFonts.push(assetRoot);

        let _fonts = JSON.parse($('#d-f-sign').text());
        _fonts.map(o => {
            assetFonts.push(unescape(o.fontSign));
        });

        let _fontTemplates = JSON.parse($('#d-f-sign-template').text());
        _fontTemplates.map(o => {
            let _html = '<li>' + unescape(o.fontSign) + '</li>';
            $('ul.font-template-items').append(_html);
        });

        //load
        $('.load-convert').hide();
        //statu copy
        $('span.copy-status').hide();
        //
        let liTag = '';
        $(assetFonts).each(function (index, item) {
            liTag += "<li></li>";
        });
        $('.mshop-tool-post.tool ul').html(liTag);
    })();

    var isCopy = false;
    //----------------------------------FUNCTION----------------------------
    function convertCharFont(indexFont, chars) {
        $('.load-convert').show();
        let charReturn = '';
        let fonts = assetFonts[indexFont].split(' ');
        chars.forEach(function (c) {
            //get index in root
            let i = assetRootArray.indexOf(c);
            if (i == -1)
                charReturn += c;
            else
                charReturn += fonts[i];
        });
        $('.load-convert').hide();
        return charReturn;
    }
    function clearFont(text) {
        $('.load-convert').show();
        let returnText = '';
        let cTextEscape = escape(text);
        let spaceEscape = escape(' ');

        while (cTextEscape != '') {
            let oldText = cTextEscape;
            assetFonts.forEach(function (itemFont) {
                let chars = itemFont.split(' ');
                chars.forEach(function (item, index) {
                    let charEscape = escape(item);
                    if (cTextEscape.startsWith(spaceEscape)) {
                        returnText += ' ';
                        cTextEscape = cTextEscape.replace(spaceEscape, '');
                    }

                    if (cTextEscape.startsWith(charEscape)) {
                        returnText += assetRootArray[index];
                        cTextEscape = cTextEscape.replace(charEscape, '');
                        return;
                    }
                });
            });

            //end loop
            if (oldText == cTextEscape) {
                let cTexts = cTextEscape.split(spaceEscape);
                if (cTexts.length > 0) {
                    returnText += unescape(cTexts[0]);
                    cTextEscape = cTextEscape.replace(cTexts[0], '');
                }
            }
        }

        $('.load-convert').hide();
        return returnText;
    }
    //----------------------------------EVENT----------------------------
    $('.mshop-tool-post.tool ul li').on('click', function (e) {
        let startSelect = $(e.target).data('start-select');
        let endSelect = $(e.target).data('end-select');
        let text = $(e.target).data('vlue');

        let textContain = $('.content-edit textarea').val();
        let textReplace = textContain.substring(0, startSelect) + text + textContain.substring(endSelect);

        $('.content-edit textarea').val(textReplace);

        $('.content-edit textarea')[0].setSelectionRange(startSelect, startSelect + text.length);
        $(".content-edit textarea")[0].focus();
    });
    $(".mshop-tool-post.content textarea").select(function (e) {
        let textRoot = $(e.target).val();
        let startSelect = $(e.target)[0].selectionStart;
        let endSelect = $(e.target)[0].selectionEnd;
        let textSelected = textRoot.substring(startSelect, endSelect);

        if (!isCopy) {
            //clear font
            textSelected = clearFont(textSelected); console.log(textSelected);

            $('.mshop-tool-post.tool ul li').each(function (index, selector) {
                let chars = textSelected.split('');
                let text = convertCharFont(index, chars);
                let ctexts = text.split(' ');

                let textDisplay = '';
                let i = 0;
                ctexts.forEach(function (item) {
                    if (i <= 8) {
                        textDisplay += item + ' ';
                    }
                    i++;
                });
                let signHas = '';
                if (ctexts.length > 9) signHas = '...';
                $(selector).data('vlue', text);
                $(selector).text(textDisplay + signHas);

            });

            $('.mshop-tool-post.tool ul li').data('start-select', startSelect);
            $('.mshop-tool-post.tool ul li').data('end-select', endSelect);
        }
    });
    $('.category p').on('click', function (e) {
        let icon = $(e.target).text();
        let startSelect = $(".content-edit textarea")[0].selectionStart;
        let endSelect = $(".content-edit textarea")[0].selectionEnd;

        let textContain = $('.content-edit textarea').val();
        let textReplace = textContain.substring(0, startSelect) + icon + textContain.substring(endSelect);
        $('.content-edit textarea').val(textReplace);
        //$('.content-edit textarea')[0].setSelectionRange(startSelect, startSelect + icon.length);
        $(".content-edit textarea")[0].selectionEnd = startSelect + icon.length;
        $(".content-edit textarea")[0].focus();
    });
    $('#btn-copy').on('click', function () {
        //let len = $(".mshop-tool-post.content textarea")[0].textLength;
        //let text = $(".mshop-tool-post.content textarea").val();
        //$(".mshop-tool-post.content textarea")[0].setSelectionRange(0, len);

        isCopy = true;

        $(".mshop-tool-post.content textarea").select();
        document.execCommand("copy");
        $('span.copy-status').show();
        setTimeout(function () {
            $("span.copy-status").fadeOut(1500);
            isCopy = false;
        }, 1500);
    });
    $('.template ul li').on('click', function (e) {
        let text = $(e.target).text();
        let startSelect = $(".content-edit textarea")[0].selectionStart;
        let endSelect = $(".content-edit textarea")[0].selectionEnd;

        let textContain = $('.content-edit textarea').val();
        let textReplace = textContain.substring(0, startSelect) + text + textContain.substring(endSelect);
        $('.content-edit textarea').val(textReplace);
        //$('.content-edit textarea')[0].setSelectionRange(startSelect, startSelect + icon.length);
        $(".content-edit textarea")[0].selectionEnd = startSelect + text.length;
        $(".content-edit textarea")[0].focus();
    });
})();
(function ($conntentRoot) {
    var btnAction = $conntentRoot.find('button').first();
    var inputName = $conntentRoot.find('input[name=name]').first();
    var inputDescription = $conntentRoot.find('input[name=description]').first();

    btnAction.on('click', function () {
        if (inputName.val() == '' || inputDescription.val() == '') return;

        $.post('/tool/posts/feedback', {
            name: inputName.val(),
            description: inputDescription.val()
        });
    });


})($("#feedback-form"));