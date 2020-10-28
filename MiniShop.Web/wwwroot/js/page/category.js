var categoryConst = {
    add: 'add',
    gridSelectorName: '#mnshop-category .grid',
    urlLoadData: ''
};
var categoryIndex = {
    clickEvent: function (e) {
        let _handle = categoryHandle();
        if (eval($(e).data('ename')) == categoryConst.add) categoryIndex.add(e, _handle);
    },
    changeEvent: function (e) {
        let _eventName = $(e).data('ename'); _handle = categoryHandle();
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
                    field: "NotUse",
                    title: "Status",
                    template: function (item) {
                        let _html = `<div class="onoffswitch">
                                    <input type = "checkbox" name = "onoffswitch" class="onoffswitch-checkbox" id = "oow-{{id}}" onchange="categoryIndex.changeEvent(this, categoryIndex.actionType.UpdateStatu)" {{checked}} data-id="{{id}}">
                                    <label class="onoffswitch-label" for="oow-{{id}}">
                                        <span class="onoffswitch-inner"></span>
                                        <span class="onoffswitch-switch"></span>
                                    </label>
                                    </div >`
                        return _html
                            .replace(RegExp("{{id}}", "gi"), item.id)
                            .replace(RegExp("{{checked}}", "gi"), item.notUse == true ? "" : "checked");
                    },
                    width: "120px"
                },
                {
                    field: "",
                    width: "15%",
                    template: function (item) {
                        let _html = '<button type="button" class="btn btn-outline-primary round mr-1 mb-1" onclick="categoryIndex.clickEvent(this, categoryIndex.actionType.EditForm)" data-id="' + item.id + '"><i class="ft-edit-3"></i> Sửa</button>';
                        _html += '<button type="button" class="btn btn-outline-danger round mr-1 mb-1" onclick="categoryIndex.clickEvent(this, categoryIndex.actionType.Delete)" data-id="' + item.id + '" data-name="' + item.name + '"><i class="ft-trash-2"></i> Xóa</button>';
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
                width: 800
                //close: function () { categoryIndex.loadTable(); },
                //refresh: function () { categoryIndex.loadTable(); }
            }
        });
    }
};
var categoryHandle = function () {

    return {
        data: helper.formData,
        formatNumber: helper.formatNumber,
        validate: helper.inputValidate,
        dialog: helper.showDialog
    }
};
categoryIndex.init();