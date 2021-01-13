
var productEditConst = {
    save: "save",
    cancel: "cancel",
    cardContent: "#mnshop-product-edit",
    chooseImage: 'choose-image',
    fileManager: '#mnshop-product-edit #filemanager',
    categorySelected: "category-selected",
    categoryContent: ".mnshop-category-content"
};
var productEditIndex = {

    clickEvent: function (e) {
        let _handle = productEditHandle();
        if (eval($(e).data('ename')) == productEditConst.save) productEditIndex.save(e, _handle);
        if (eval($(e).data('ename')) == productEditConst.cancel) productEditIndex.cancel(e, _handle);
        if (eval($(e).data('ename')) == productEditConst.chooseImage) productEditIndex.chooseImage(e, _handle);
    },
    changeEvent: function (e) {
        let _handle = productEditHandle();
    },
    //child event
    save: function (e, handle) {
        //validate
        let _$rootContent = $(e).closest(productEditConst.cardContent);
        if (!handle.validate.checkRequired({ content: _$rootContent })) return;
        let _data = handle.data.inputToObject(_$rootContent, function (obj) {
            obj.Price = parseFloat(obj.Price.replaceAll(',', ''));
            
            obj.CategoryDto = { Id: $('.mnshop-category-content .item.active').data('id') };
            obj.IsRedirectToPageRoot = $('[name=IsRedirectToPageRoot]').prop('checked');
        });
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
    chooseImage: function (e, handle) {
        handle.fileDialog(
            content = $(e).closest(productEditConst.cardContent),
            objConfig = { title: "Choose file" },
            callbackAfterChoose = function (selects) {
                let _filenames = [];
                $(selects).each(function (index, item) { _filenames.push(item.path); });
                $(e).parent().prev().val(_filenames);
            }
        );
    }
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
        showDialog: helper.showDialog,
        closeDialog: helper.closeDialog,
        newId: helper.createGUID,
        fileDialog: helper.file.fileDialog
    }
}