var categoryAddConst = {
    save: "save",
    cancel: "cancel",
    cardContent: "#mnshop-category-add"
};
var categoryAddIndex = {

    clickEvent: function (e) {
        let _handle = categoryAddHandle();
        if (eval($(e).data('ename')) == categoryAddConst.save) categoryAddIndex.save(e, _handle);
        if (eval($(e).data('ename')) == categoryAddConst.cancel) categoryAddIndex.cancel(e, _handle);
    },
    changeEvent: function (e) {
        let _handle = categoryAddHandle();
    },
    //child event
    save: function (e, handle) {
        //validate
        let _$rootContent = $(e).closest(categoryAddConst.cardContent);
        if (!handle.validate.checkRequired({ content: _$rootContent })) return;
        let _data = handle.data.inputToObject(_$rootContent, function (obj) {
            obj.Id = handle.newId();
        });
        console.log(_data);
        //save
        handle.save(_data, function (res) {
            if (res.statu == 200) {
                handle.closeDialog($(e).closest(categoryAddConst.cardContent));
            }
            else {
                swal(res.statu, res.message, 'error');
            }
        });
    },
};
var categoryAddHandle = function () {
    let _save = function (data, callback) {
        let _url = 'category/create';
        $.post(_url, { data: data }, function (res) {
            callback(res);
        });
    }

    return {
        save: _save,
        data: helper.formData,
        formatNumber: helper.formatNumber,
        validate: helper.inputValidate,
        closeDialog: helper.closeDialog,
        newId: helper.createGUID
    }
}