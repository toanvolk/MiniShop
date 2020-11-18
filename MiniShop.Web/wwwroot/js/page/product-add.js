var productAddConst = {
    save: "save",
    cancel: "cancel",
    cardContent: "#mnshop-product-add",
    chooseImage: 'choose-image',
    fileManager: '#mnshop-product-add #filemanager',
    categorySelected: "category-selected",
    categoryContent:".mnshop-category-content"
};
var productAddIndex = {

    clickEvent: function (e) {
        let _handle = productAddHandle();
        if (eval($(e).data('ename')) == productAddConst.save) productAddIndex.save(e, _handle);
        if (eval($(e).data('ename')) == productAddConst.cancel) productAddIndex.cancel(e, _handle);
        if (eval($(e).data('ename')) == productAddConst.chooseImage) productAddIndex.chooseImage(e, _handle);
        if (eval($(e).data('ename')) == productAddConst.categorySelected) productAddIndex.categorySelected(e, _handle);
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
            obj.Price = parseFloat(obj.Price.replaceAll(',', ''));

            let _catetorys = [];
            $(productAddConst.cardContent + ' ' + productAddConst.categoryContent + ' a.active').each(function (index, item) {
                _catetorys.push($(item).data('id'));
            });
            obj.CategoryIds = _catetorys.toString();
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
        handle.fileDialog(
            content = $(e).closest(productAddConst.cardContent),
            objConfig = { title: "Choose file"},
            callbackAfterChoose = function (selects) {
                let _filenames = [];
                $(selects).each(function (index, item) { _filenames.push(item.path); });
                $(e).parent().prev().val(_filenames);
            }
        );
    },
    categorySelected: function (e, handle) {
        if ($(e).hasClass('active'))
            $(e).removeClass('active');
        else
            $(e).addClass('active');
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
        fileDialog: helper.file.fileDialog
    }
}