var productConst = {
    add: 'add',
    edit: 'edit',
    delete: 'delete',
    gridSelectorName: '#mnshop-product .grid',
    urlLoadData: 'product/loaddatapage',
    statuChange: 'statu-change',
    heroChange: 'hero-change',
    productSearch: 'product-search'
};
var productIndex = {
    clickEvent: function (e) {
        let _handle = productHandle();
        if (eval($(e).data('ename')) == productConst.add) productIndex.add(e, _handle);
        if (eval($(e).data('ename')) == productConst.edit) productIndex.edit(e, _handle);
        if (eval($(e).data('ename')) == productConst.delete) productIndex.delete(e, _handle);
    },
    changeEvent: function (e) {
        let _handle = productHandle();
        if (eval($(e).data('ename')) == productConst.statuChange) productIndex.statuChange(e, _handle);
        if (eval($(e).data('ename')) == productConst.heroChange) productIndex.heroChange(e, _handle);
    },
    keyupEvent: function (e) {
        let _handle = productHandle();
        if (eval($(e).data('ename')) == productConst.productSearch) productIndex.productSearch(e, _handle);        
    },
    //child event
    init: function () {
        $(productConst.gridSelectorName).kendoGrid({
            dataSource: {
                transport: {
                    read: function (options) { return productIndex._read(options) }
                },
                schema: {
                    data: "data",
                    total: "total"
                },
                serverPaging: true,
                pageSize: cdA.pageSize,
            },
            pageable: true,
            columns: [
                {
                    field: "index",
                    title: "#",
                    width: 35
                },
                {
                    field: "picture",
                    title: "Ảnh",
                    width: "7%",
                    template: function (item) {
                        let _html = `
                                    <div class="mnshop-product-image">
                                        <div class="image-tag" {#:hide}>
                                            <img src="{#:link-image-tag}">                                            
                                        </div>
                                        <img src="{#:link-image}">                                        
                                    </div>
                                    `;
                        let _pathImg = '';
                        if (item.tag == cdA.productTag.HOT) _pathImg = '/shared/Icon/icon-hot-2.gif';
                        if (item.tag == cdA.productTag.NEW) _pathImg = '/shared/Icon/icon-new-1.gif';
                        return _html
                            .replaceAll(new RegExp("{#:link-image}", "gi"), item.picture)
                            .replaceAll(new RegExp("{#:link-image-tag}", "gi"), _pathImg)
                            .replaceAll(new RegExp("{#:hide}", "gi"), _pathImg || "hidden");
                    },
                },
                {
                    field: "name",
                    title: "Tên",
                    width: "15%"
                },
                {
                    field: "description",
                    title: "Mô tả",
                    template: function (item) {
                        return helper.formatString.truncate(helper.formatString.decodeHtml(item.description, { normal: true }), 150)
                    }
                },
                {
                    field: "areaCode",
                    title: "Quốc gia",
                    width: "5%",
                },
                {
                    field: "price",
                    title: "Giá",
                    width: "5%",
                    template: "#= kendo.toString(price, '\\#\\#,\\#') #",
                    attributes: { class: "text-right" }
                },
                {
                    field: "trackingLink",
                    title: "Tracking link",
                    width: "20%",
                },
                {
                    field: "isHero",
                    title: "Trang đầu",
                    template: function (item) {
                        let _html = `<div class="custom-switch">
                                            <input type="checkbox" class="custom-control-input" id="{id}" {checked} onchange="productIndex.changeEvent(this)" data-ename= "productConst.heroChange" data-id="{id}">
                                            <label class="custom-control-label" for="{id}"></label>
                                        </div>`
                        return _html
                            .replaceAll(new RegExp("{id}", "gi"), item.id)
                            .replaceAll(new RegExp("{checked}", "gi"), item.isHero ? "checked" : "")
                    },
                    width: "120px"
                },
                {
                    field: "NotUse",
                    title: "Status",
                    template: function (item) {
                        let _html = `<div class="custom-switch">
                                            <input type="checkbox" class="custom-control-input" id="{id}" {checked} onchange="productIndex.changeEvent(this)" data-ename= "productConst.statuChange" data-id="{id}">
                                            <label class="custom-control-label" for="{id}"></label>
                                        </div>`
                        return _html
                            .replaceAll(new RegExp("{id}", "gi"), item.id)
                            .replaceAll(new RegExp("{checked}", "gi"), item.notUse == true ? "" : "checked" )
                    },
                    width: "120px"
                },
                {
                    field: "",
                    width: "15%",
                    template: function (item) {
                        let _html = '<button type="button" class="btn btn-outline-primary round mr-1 mb-1" onclick="productIndex.clickEvent(this)" data-id="' + item.id + '" data-ename= "productConst.edit"><i class="ft-edit-3"></i> Sửa</button>';
                        _html += '<button type="button" class="btn btn-outline-danger round mr-1 mb-1" onclick="productIndex.clickEvent(this)" data-id="' + item.id + '" data-name="' + item.name + '" data-ename= "productConst.delete"><i class="ft-trash-2"></i> Xóa</button>';
                        return _html;
                    },
                }
            ]
        })
    },
    _read: function (options, params) {
        if (params) {
            $.each(params, function (key, value) {
                options.data[key] = value;
            })
        }
        $.ajax({
            url: productConst.urlLoadData,
            type: "POST",
            data: options.data,
            success: function (result) {
                options.success(result);
            }
        })

    },
    add: function (e, handle) {
        handle.dialog({
            contentData: {
                url: "/admin/product/add",
                
            },
            config: {
                title: "TẠO MỚI",
                actions: ["Refresh", "Close"],
                activate: function (e) {
                    handle.initEditor($('#mnshop-product-add #description'));
                    $('.decimal-inputmask').inputmask("decimal", {
                        placeholder: "0",
                        digits: 0,
                        digitsOptional: false,
                        radixPoint: ".",
                        groupSeparator: ",",
                        autoGroup: true,
                        allowPlus: false,
                        allowMinus: true,
                        clearMaskOnLostFocus: false,
                        removeMaskOnSubmit: true
                    });
                },
                width: 1200,
                close: function () { $(productConst.gridSelectorName).data("kendoGrid").dataSource.read(); },
                refresh: function () { $(productConst.gridSelectorName).data("kendoGrid").dataSource.read(); }
            }
        });
    },
    edit: function (e, handle) {
        let _id = $(e).data('id');
        handle.dialog({
            contentData: {
                url: "/admin/product/edit",
                data: { productId: _id }
            },
            config: {
                title: "TẠO MỚI",
                actions: ["Refresh", "Close"],
                activate: function (e) {
                    handle.initEditor($('#mnshop-product-edit #description'));
                    $('.decimal-inputmask').inputmask("decimal", {
                        placeholder: "0",
                        digits: 0,
                        digitsOptional: false,
                        radixPoint: ".",
                        groupSeparator: ",",
                        autoGroup: true,
                        allowPlus: false,
                        allowMinus: true,
                        clearMaskOnLostFocus: false,
                        removeMaskOnSubmit: true
                    });
                },
                width: 1200,
                close: function () { $(productConst.gridSelectorName).data("kendoGrid").dataSource.read(); },
                refresh: function () { $(productConst.gridSelectorName).data("kendoGrid").dataSource.read(); }
            }
        });
    },
    delete: function (e, handle) {
        var _dataProduct = $(e).data();
        swal({
            title: 'Chắc xóa?',
            text: 'Xóa [' + _dataProduct.name + ']!',
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            cancelButtonText: 'Quay lại',
            confirmButtonText: 'Vâng, xóa!'
        }).then(function (e) {
            if (e.value == true) {
                handle.delete(_dataProduct.id, function (res) {
                    swal('Đã xóa!', res.message, 'success');
                    $(productConst.gridSelectorName).data("kendoGrid").dataSource.read();
                });
            }

        }).catch(swal.noop);
    },
    statuChange: function (e, handle) {
        let _id = $(e).data('id');
        let _checked = $(e).prop('checked');

        console.log(_id, _checked);
        handle.statuChange(_id, _checked, function (res) {
            if (res.statu == 200) {
                $(productConst.gridSelectorName).data("kendoGrid").dataSource.read();
            }
            else {
                swal(res.statu, res.message, 'error');
            }            
        });
    },
    heroChange: function (e, handle) {
        let _id = $(e).data('id');
        let _checked = $(e).prop('checked');

        console.log(_id, _checked);
        handle.heroChange(_id, _checked, function (res) {
            if (res.statu == 200) {
                $(productConst.gridSelectorName).data("kendoGrid").dataSource.read();
            }
            else {
                swal(res.statu, res.message, 'error');
            }
        });
    },
    productSearch: function (e, handle) {
        let _parmas = {};
        if (event.key === 'Enter' || event.keyCode === 13) {
            // Do something
            _parmas.textSearch = $(e).val();
            $(productConst.gridSelectorName).data("kendoGrid").dataSource.transport.read = function (options) {
                return productIndex._read(options, _parmas);
            }
            $(productConst.gridSelectorName).data("kendoGrid").dataSource.read();
        }
        
    }
};
var productHandle = function () {
    let _delete = function (productId, callback) {
        let _url = 'product/delete';
        $.post(_url, { productId: productId}, function (res) {
            callback(res);
        });
    }
    let _statuChange = function (productId, checked, callback) {
        let _url = 'product/updateStatu';
        $.post(_url, { productId: productId, ischecked: checked }, function (res) { callback(res);});
    }
    let _heroChange = function (productId, checked, callback) {
        let _url = 'product/updateHero';
        $.post(_url, { productId: productId, ischecked: checked }, function (res) { callback(res); });
    }
    let _loadData = function (options) {
        $.ajax({
            url: productConst.urlLoadData,
            contentType: 'application/json',
            dataType: 'json',
            type: 'POST',
            data: JSON.stringify(options.data),
            success: function (result) {
                options.success(result);
            }
        })        
    }
    return {
        data: helper.formData,
        formatNumber: helper.formatNumber,
        formatString: helper.formatString,
        validate: helper.inputValidate,
        dialog: helper.showDialog,
        delete: _delete,
        statuChange: _statuChange,
        initEditor: helper.editor.init,
        heroChange: _heroChange,
        loadData: _loadData
    }
};
productIndex.init();