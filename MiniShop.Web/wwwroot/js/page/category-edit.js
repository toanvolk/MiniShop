var categoryEditConst = {

};
var categoryEditIndex = {

    clickEvent: function (e) {
        let _eventName = $(e).data('ename'); _handle = categoryEditHandle();
    },
    changeEvent: function (e) {
        let _eventName = $(e).data('ename'); _handle = categoryEditHandle();
    }
    //child event

};
var categoryEditHandle = function () {

    return {
        data: helper.formData,
        formatNumber: helper.formatNumber,
        validate: helper.inputValidate
    }
}