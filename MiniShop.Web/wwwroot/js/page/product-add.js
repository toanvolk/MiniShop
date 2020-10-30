var productAddConst = {
    save: "save",
    cancel: "cancel",
    cardContent: "#mnshop-product-add",
    chooseImage: 'choose-image',
    fileManager: '#mnshop-product-add #filemanager'
};
var productAddIndex = {

    clickEvent: function (e) {
        let _handle = productAddHandle();
        if (eval($(e).data('ename')) == productAddConst.save) productAddIndex.save(e, _handle);
        if (eval($(e).data('ename')) == productAddConst.cancel) productAddIndex.cancel(e, _handle);
        if (eval($(e).data('ename')) == productAddConst.chooseImage) productAddIndex.chooseImage(e, _handle);
    },
    changeEvent: function (e) {
        let _handle = productAddHandle();
    },
    //child event
    save: function (e, handle) {
        //validate
        let _$rootContent = $(e).closest(productAddConst.cardContent);
        if (!handle.validate.checkRequired({ content: _$rootContent })) return;
        let _data = handle.data.inputToObject(_$rootContent, function (obj) {
            obj.Id = handle.newId();
        });
        console.log(_data);
        //save
        handle.save(_data, function (res) {
            if (res.statu == 200) {
                handle.closeDialog($(e).closest(productAddConst.cardContent));
            }
            else {
                swal(res.statu, res.message, 'error');
            }
        });
    },
    chooseImage: function (e, handle) {
        //console.log('choose image');
        //handle.showDialog({
        //    contentData: {
        //        url: "/admin/filemanager"
        //    },
        //    config: {
        //        width: 800
        //    }
        //});
        handle.fileManager($(e));
    }
};
var productAddHandle = function () {
    let _save = function (data, callback) {
        let _url = 'product/create';
        $.post(_url, { data: data }, function (res) {
            callback(res);
        });
    }

    return {
        save: _save,
        data: helper.formData,
        formatNumber: helper.formatNumber,
        validate: helper.inputValidate,
        showDialog: helper.showDialog,
        closeDialog: helper.closeDialog,
        newId: helper.createGUID,
        fileManager: helper.file.fileManager
    }
}