
(function () {
    //---------------------------FUNCTION---------------------------------
    function _getData() {
        return helper.formData.inputToObject("#data-edit", function (obj) {
            obj.FontSign = escape(obj.FontSign);
        });
    }
    function _setData(data) {
        helper.formData.objectToInput(data
            , $("#data-edit")
            , [
                {
                    "inputName": "Id",
                    "propertyName": "id",
                },
                {
                    "inputName": "FontName",
                    "propertyName": "fontName",
                }
            ]
            , function (obj, content) {
                content.find('textarea[name=FontSign]').val(unescape(obj["fontSign"]));
                content.find("input[name=PostType][value=" + obj["postType"] + "]").prop('checked', true);
            });
    }
    function _setDataDefault() {
        let _data = {
            id: '',
            fontName: '',
            fontSign: '',
            postType: 0
        };
        helper.formData.objectToInput(_data
            , $("#data-edit")
            , [
                {
                    "inputName": "Id",
                    "propertyName": "id",
                },
                {
                    "inputName": "FontName",
                    "propertyName": "fontName",
                }
            ]
            , function (obj, content) {
                content.find('textarea[name=FontSign]').val(unescape(obj["fontSign"]));
                content.find("input[name=PostType][value=" + obj["postType"] + "]").prop('checked', true);
            });
    }
    async function _getDataById(id) {
        let _url = '/admin/tool/GetDataByIdPost';
        let _data = {};
        await $.get(_url, { postId: id }, function (res) {
            _data = res.data;
        });
        return _data;
    }
    function _insert(data, callback) {
        let _url = '/admin/tool/CreatePost';
        $.post(_url, { data: data }, function (res) {
            $('.btn-action label.info-action').show();
            $('.btn-action label.info-action').text(res.message);
            setTimeout(function () { $('.btn-action label.info-action').hide(); callback(); }, 1250);
        });
    }
    function _update(data, callback) {
        let _url = '/admin/tool/UpdatePost';

        $.post(_url, { data: data }, function (res) {
            $('.btn-action label.info-action').show();
            $('.btn-action label.info-action').text(res.message);
            setTimeout(function () { $('.btn-action label.info-action').hide(); callback(); }, 1250);
        });
    }
    function _delete(id) {
        $.post(
            '/admin/tool/deletePost',
            { postId: id },
            function (res) {
                if (res.statu == cdA.CodeStatus.OK) {
                    swal('Đã xóa!', res.message, 'success');
                }
                else {
                    swal('Xóa không thành công!', res.message, 'error');
                }
            }
        );
    }
    function _load() {
        let _url = '/admin/tool/loadPost';
        let _html = `
                                                        <li>
        <label for="icon-right">{#:fontName}</label>
        <span class="k-textbox k-space-right" style=" width: 100%; padding-right: 4em;">
            <div>{#:fontSign}</div>
            <a href="#" class="k-icon k-i-edit btn-edit" style="right: 2em;" data-id="{#:id}">&nbsp;</a>
            <a href="#" class="k-icon k-i-delete btn-delete" data-id="{#:id}" data-name="{#:fontName}">&nbsp;</a>
        </span>
    </li>
`;
        $.get(_url, function (res) {
            if (res.data) {
                let _htmlContent = '';
                let _htmlTempleteContent = '';
                res.data.source.forEach(function (item) {
                    if (item["postType"] == 0) {
                        _htmlContent += _html.replaceAll(new RegExp("{#:fontName}", "gi"), item.fontName)
                            .replaceAll(new RegExp("{#:fontSign}", "gi"), unescape(item.fontSign))
                            .replaceAll(new RegExp("{#:id}", "gi"), item.id);
                    } else {
                        _htmlTempleteContent += _html.replaceAll(new RegExp("{#:fontName}", "gi"), item.fontName)
                            .replaceAll(new RegExp("{#:fontSign}", "gi"), unescape(item.fontSign))
                            .replaceAll(new RegExp("{#:id}", "gi"), item.id);
                    }

                });
                $('ul.content-area').html(_htmlContent);
                $('ul.content-template-area').html(_htmlTempleteContent);
            }
        });
    }
    //---------------------------INIT---------------------------------
    (function () {
        $('.btn-action label.info-action').hide();
        _load();
    })();
    //---------------------------EVENT---------------------------------
    $('#btn-save').on('click', function (e) {
        //get data
        let _data = _getData();
        //validate
        if (_data.FontName == '' || _data.FontSign == '') {
            swal('Thiếu thông tin cần thiết!', 'Hãy bổ sung thêm!', 'error');
            return;
        }

        if (_data.Id == '') {
            _data.Id = helper.createGUID();
            _insert(_data, function () { return _load() });
        }
        else {
            _data.Id = _data.Id;
            _update(_data, function () { return _load() });
        };
        //clear id
        _setDataDefault();
    });

    $(document).on('click', '.btn-delete', function (e) {
        var _data = $(e.target).data();

        swal({
            title: 'Chắc xóa?',
            text: 'Xóa [' + _data.name + ']!',
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            cancelButtonText: 'Quay lại',
            confirmButtonText: 'Vâng, xóa!'
        }).then(function (e) {
            if (e.value == true) {
                _delete(_data.id)
            }

        }).catch(swal.noop);
    });
    $(document).on('click', '.btn-edit', async function (e) {
        var _data = $(e.target).data();
        let dataReponse = await _getDataById(_data.id);
        _setData(dataReponse);
    });
})();
