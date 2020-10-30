var productEditConst = {
    save: "save",
    cancel: "cancel",
    cardContent: "#mnshop-product-edit"
};
var productEditIndex = {

    clickEvent: function (e) {
        let _handle = productEditHandle();
        if (eval($(e).data('ename')) == productEditConst.save) productEditIndex.save(e, _handle);
    },
    changeEvent: function (e) {
        let _eventName = $(e).data('ename'); _handle = productEditHandle();
    },
    //child event
    //child event
    save: function (e, handle) {
        //validate
        let _$rootContent = $(e).closest(productEditConst.cardContent);
        if (!handle.validate.checkRequired({ content: _$rootContent })) return;
        let _data = handle.data.inputToObject(_$rootContent);
        console.log(_data);
        //save
        handle.save(_data, function (res) {
            if (res.statu == 200) {
                handle.closeDialog($(e).closest(productEditConst.cardContent));
            }
            else {
                swal(res.statu, res.message, 'error');
            }
        });
    },
};
var productEditHandle = function () {
    let _save = function (data, callback) {
        let _url = 'product/update';
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