var categoryConst = {
    add: 'add',
    edit: 'edit',
    delete: 'delete',
    gridSelectorName: '#mnshop-category .grid',
    urlLoadData: 'category/LoadDataAdmin',
    statuChange: 'statu-change'
};
var categoryIndex = {
    clickEvent: function (e) {
        let _handle = categoryHandle();
        if (eval($(e).data('ename')) == categoryConst.add) categoryIndex.add(e, _handle);
        if (eval($(e).data('ename')) == categoryConst.edit) categoryIndex.edit(e, _handle);
        if (eval($(e).data('ename')) == categoryConst.delete) categoryIndex.delete(e, _handle);
    },
    changeEvent: function (e) {
        let _handle = categoryHandle();
        if (eval($(e).data('ename')) == categoryConst.statuChange) categoryIndex.statuChange(e, _handle);
    },
    //child event
    init: function () {
        $(categoryConst.gridSelectorName).kendoGrid({
            dataSource: {
                transport: {
                    read: {
                        url: categoryConst.urlLoadData,
                        dataType: "json",
                        type: "GET",
                        //data: {
                        //    rootCategoryType: _rootCategoryType
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
                    field: "sortIndex",
                    title: "Thứ tự",
                    width: "120px"
                },
                {
                    field: "NotUse",
                    title: "Status",
                    template: function (item) {
                        let _html = `<div class="custom-switch">
                                            <input type="checkbox" class="custom-control-input" id="{id}" {checked} onchange="categoryIndex.changeEvent(this)" data-ename= "categoryConst.statuChange" data-id="{id}">
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
                        let _html = '<button type="button" class="btn btn-outline-primary round mr-1 mb-1" onclick="categoryIndex.clickEvent(this)" data-id="' + item.id + '" data-ename= "categoryConst.edit"><i class="ft-edit-3"></i> Sửa</button>';
                        _html += '<button type="button" class="btn btn-outline-danger round mr-1 mb-1" onclick="categoryIndex.clickEvent(this)" data-id="' + item.id + '" data-name="' + item.name + '" data-ename= "categoryConst.delete"><i class="ft-trash-2"></i> Xóa</button>';
                        return _html;
                    },
                }
            ]
        })
    },
    add: function (e, handle) {
        handle.dialog({
            contentData: {
                url: "/admin/category/add",
                
            },
            config: {
                title: "TẠO MỚI",
                actions: ["Refresh", "Close"],
                width: 800,
                close: function () { $(categoryConst.gridSelectorName).data("kendoGrid").dataSource.read(); },
                refresh: function () { $(categoryConst.gridSelectorName).data("kendoGrid").dataSource.read(); }
            }
        });
    },
    edit: function (e, handle) {
        let _id = $(e).data('id');
        handle.dialog({
            contentData: {
                url: "/admin/category/edit",
                data: { categoryId: _id }
            },
            config: {
                title: "TẠO MỚI",
                actions: ["Refresh", "Close"],
                width: 650,
                close: function () { $(categoryConst.gridSelectorName).data("kendoGrid").dataSource.read(); },
                refresh: function () { $(categoryConst.gridSelectorName).data("kendoGrid").dataSource.read(); }
            }
        });
    },
    delete: function (e, handle) {
        var _dataCategory = $(e).data();
        swal({
            title: 'Chắc xóa?',
            text: 'Xóa [' + _dataCategory.name + ']!',
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            cancelButtonText: 'Quay lại',
            confirmButtonText: 'Vâng, xóa!'
        }).then(function (e) {
            if (e.value == true) {
                handle.delete(_dataCategory.id, function (res) {
                    swal('Đã xóa!', res.message, 'success');
                    $(categoryConst.gridSelectorName).data("kendoGrid").dataSource.read();
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
                $(categoryConst.gridSelectorName).data("kendoGrid").dataSource.read();
            }
            else {
                swal(res.statu, res.message, 'error');
            }            
        });
    }
};
var categoryHandle = function () {
    let _delete = function (categoryId, callback) {
        let _url = 'category/delete';
        $.post(_url, { categoryId: categoryId}, function (res) {
            callback(res);
        });
    }
    let _statuChange = function (categoryId, checked, callback) {
        let _url = 'category/updateStatu';
        $.post(_url, { categoryId: categoryId, ischecked: checked }, function (res) { callback(res);});
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
categoryIndex.init();