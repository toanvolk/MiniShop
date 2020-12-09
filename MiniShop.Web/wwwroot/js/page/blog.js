
//index
(function (selectorRoot) {
    $(document).on('click', selectorRoot +  ' .btn.ac-blog-add', function (e) {
        open("/blog/add");
    });

    //init grid
    (function () {
        let _read = function (options, params) {
            if (params) {
                $.each(params, function (key, value) {
                    options.data[key] = value;
                })
            }
            $.ajax({
                url: '/admin/blog/getdataadmin',
                type: "POST",
                data: options.data,
                success: function (result) {
                    options.success(result);
                }
            })

        };
        //$(categoryConst.gridSelectorName).kendoGrid({
        //    dataSource: {
        //        transport: {
        //            read: function (options) { return _read(options) }
        //        },
        //        schema: {
        //            data: "data",
        //            total: "total"
        //        },
        //        serverPaging: true,
        //        pageSize: cdA.pageSize,
        //    },
        //    pageable: false,
        //    columns: [
        //        {
        //            field: "index",
        //            title: "#",
        //            width: 35
        //        },
        //        {
        //            field: "title",
        //            title: "Tên",
        //            width: "15%"
        //        },
        //        {
        //            field: "sescriptionShort",
        //            title: "Nội dung ngắn"
        //        },
        //        {
        //            field: "notUse",
        //            title: "Status",
        //            template: function (item) {
        //                let _html = `<div class="custom-switch">
        //                                    <input type="checkbox" class="custom-control-input" id="{id}" {checked} onchange="categoryIndex.changeEvent(this)" data-ename= "categoryConst.statuChange" data-id="{id}">
        //                                    <label class="custom-control-label" for="{id}"></label>
        //                                </div>`
        //                return _html
        //                    .replaceAll(new RegExp("{id}", "gi"), item.id)
        //                    .replaceAll(new RegExp("{checked}", "gi"), item.notUse == true ? "" : "checked")
        //            },
        //            width: "120px"
        //        },
        //        {
        //            field: "",
        //            width: "15%",
        //            template: function (item) {
        //                let _html = '<button type="button" class="btn btn-outline-primary round mr-1 mb-1" onclick="categoryIndex.clickEvent(this)" data-id="' + item.id + '" data-ename= "categoryConst.edit"><i class="ft-edit-3"></i> Sửa</button>';
        //                _html += '<button type="button" class="btn btn-outline-danger round mr-1 mb-1" onclick="categoryIndex.clickEvent(this)" data-id="' + item.id + '"><i class="ft-trash-2"></i> Xóa</button>';
        //                return _html;
        //            },
        //        }
        //    ]
        //})
    })();
})(".mnshop-blog");

//add
(function (selectorRoot) {
    helper.editor.init($(selectorRoot + ' #content'));
    $(document).on('click', selectorRoot + ' .btn.save', function (e) {
        console.log(helper.formData.inputToObject($(selectorRoot)));
    });
})("section#mnshop-blog-add");