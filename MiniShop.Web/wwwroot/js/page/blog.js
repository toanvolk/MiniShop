//public
(function () {
    helper.editor.init($('.mnshop-blog-modify #content'));
    // Single Date Range Picker
    $('input[name=PublishDate]').daterangepicker({
        singleDatePicker: true,
        showDropdowns: true,
        locale: {
            format: 'DD/MM/YYYY'
        }
    });
})();

//index
(function (selectorRoot) {
    //open new blog
    $(document).on('click', selectorRoot + ' .btn.ac-blog-add', function (e) {
        open("/admin/blog/add");
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
        $(selectorRoot + ' .grid-data').kendoGrid({
            dataSource: {
                transport: {
                    read: function (options) { return _read(options) }
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
                    field: "title",
                    title: "Tên",
                    width: "15%"
                },
                {
                    field: "descriptionShort",
                    title: "Nội dung ngắn"
                },
                {
                    field: "publishDate",
                    title: "Ngày đăng",
                    width: "7%",
                    template: function (item) {
                        return moment(item.publishDate).format('DD/MM/YYYY');
                    },
                },
                {
                    field: "notUse",
                    title: "Status",
                    template: function (item) {
                        let _html = `<div class="custom-switch">
                                            <input type="checkbox" class="select-status custom-control-input " id="{id}" {checked} data-ename= "categoryConst.statuChange" data-id="{id}">
                                            <label class="custom-control-label" for="{id}"></label>
                                        </div>`
                        return _html
                            .replaceAll(new RegExp("{id}", "gi"), item.id)
                            .replaceAll(new RegExp("{checked}", "gi"), item.notUse == true ? "" : "checked")
                    },
                    width: "120px"
                },
                {
                    field: "",
                    width: "15%",
                    template: function (item) {
                        let _html = '<button type="button" class="btn btn-outline-primary round mr-1 mb-1 edit-data" data-id="' + item.id + '"><i class="ft-edit-3"></i> Sửa</button>';
                        _html += '<button type="button" class="btn btn-outline-danger round mr-1 mb-1 delete-data" data-id="' + item.id + '" data-title = "' + item.title + '"><i class="ft-trash-2"></i> Xóa</button>';
                        return _html;
                    },
                }
            ]
        })
    })();

    //delete 
    $(document).on('click', selectorRoot + ' .btn.delete-data', function (e) {
        var _data = $(e.target).data();

        swal({
            title: 'Chắc xóa?',
            text: 'Xóa [' + _data.title + ']!',
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            cancelButtonText: 'Quay lại',
            confirmButtonText: 'Vâng, xóa!'
        }).then(function (e) {
            if (e.value == true) {
                $.post(
                    '/admin/blog/delete',
                    { blogId: _data.id },
                    function (res) {
                        if (res.statu == cdA.CodeStatus.OK) {
                            swal('Đã xóa!', res.message, 'success');
                            $(selectorRoot + ' .grid-data').data("kendoGrid").dataSource.read();
                        }
                        else {
                            swal('Xóa không thành công!', res.message, 'error');
                            $(selectorRoot + ' .grid-data').data("kendoGrid").dataSource.read();
                        }

                    }
                );
            }

        }).catch(swal.noop);
    });

    //update status
    $(document).on('click', selectorRoot + ' input.select-status', function (e) {
        let _id = $(e.target).data('id');
        let _checked = $(e.target).prop('checked');

        console.log(_id, _checked);

        $.post(
            '/admin/blog/UpdateStatu',
            {
                blogId: _id,
                ischecked: _checked
            },
            function (res) {
                if (res.statu == 200) {
                    $(selectorRoot + ' .grid-data').data("kendoGrid").dataSource.read();
                }
                else {
                    swal(res.statu, res.message, 'error');
                }
            }

        );
    });

    //open edit form
    $(document).on('click', selectorRoot + ' .btn.edit-data', function (e) {
        let _id = $(e.target).data("id");
        if (typeof (_id) == "undefined") _id = $(e.target).closest("button").data("id");
        open("/admin/blog/edit/" + _id);
    });

})("section#mnshop-blog");

//add
(function (selectorRoot) {    
    
    $(document).on('click', selectorRoot + ' .btn.save', function (e) {
        let _data = helper.formData.inputToObject($(selectorRoot), function (obj) {
            let _dateString = obj.PublishDate.split("/");
            obj.PublishDate = new Date(_dateString[2] + '/' + _dateString[1] + '/' + _dateString[0]).toJSON();
        });
        console.log(helper.formData.inputToObject($(selectorRoot)));

        $.post(
            '/admin/blog/create',
            {
                blogDto: _data
            },
            function (res) {
                if (res.statu == cdA.CodeStatus.OK) {
                    
                    swal({
                        title: 'Đã lưu!',
                        text: res.message,
                        type: 'success',
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: 'OK'
                    }).then(
                        function (e) {
                            if (e.value == true) {
                                location.reload();
                            }
                        });
                   
                }
            }
        );
    });
})("section#mnshop-blog-add");
//add
(function (selectorRoot) {

    $(document).on('click', selectorRoot + ' .btn.save', function (e) {
        let _data = helper.formData.inputToObject($(selectorRoot), function (obj) {
            let _dateString = obj.PublishDate.split("/");
            obj.PublishDate = new Date(_dateString[2] + '/' + _dateString[1] + '/' + _dateString[0]).toJSON();
        });
        console.log(helper.formData.inputToObject($(selectorRoot)));

        $.post(
            '/admin/blog/update',
            {
                blogDto: _data
            },
            function (res) {
                if (res.statu != cdA.CodeStatus.OK) {
                    swal({
                        title: 'Cập nhật!',
                        text: res.message,
                        type: 'error',
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: 'OK'
                    }).then(
                        function (e) {
                            if (e.value == true) {
                                location.reload();
                            }
                        });

                }
                else {
                    swal({
                        title: 'Cập nhật!',
                        text: res.message,
                        type: 'success',
                        confirmButtonText: 'OK'
                    })
                }
            }
        );
    });
})("section#mnshop-blog-edit");

