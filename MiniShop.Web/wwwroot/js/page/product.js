﻿var productConst = {
    add: 'add',
    edit: 'edit',
    delete: 'delete',
    gridSelectorName: '#mnshop-product .grid',
    urlLoadData: 'product/loaddata',
    statuChange: 'statu-change'
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
    },
    //child event
    init: function () {
        $(productConst.gridSelectorName).kendoGrid({
            dataSource: {
                transport: {
                    read: {
                        url: productConst.urlLoadData,
                        dataType: "json",
                        type: "GET",
                        //data: {
                        //    rootproductType: _rootproductType
                        //}
                    }
                },
                schema: {
                    data: "data",
                    total: "total"
                },
                serverPaging: false
            },
            pageable: false,
            columns: [
                {
                    field: "index",
                    title: "#",
                    width: 35
                },
                {
                    field: "name",
                    title: "Loại",
                    width: "15%"
                },
                {
                    field: "description",
                    title: "Mô tả"
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
    add: function (e, handle) {
        handle.dialog({
            contentData: {
                url: "/admin/product/add",
                
            },
            config: {
                title: "TẠO MỚI",
                actions: ["Refresh", "Close"],
                width: 800,
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
                width: 650,
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
    return {
        data: helper.formData,
        formatNumber: helper.formatNumber,
        validate: helper.inputValidate,
        dialog: helper.showDialog,
        delete: _delete,
        statuChange: _statuChange
    }
};
productIndex.init();