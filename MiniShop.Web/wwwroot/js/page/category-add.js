var categoryAddConst = {

};
var categoryAddIndex = {

    clickEvent: function (e) {
        let _eventName = $(e).data('ename'); _handle = categoryAddHandle();
    },
    changeEvent: function (e) {
        let _eventName = $(e).data('ename'); _handle = categoryAddHandle();
    }
    //child event

};
var categoryAddHandle = function () {

    return {
        data: helper.formData,
        formatNumber: helper.formatNumber,
        validate: helper.inputValidate
    }
}