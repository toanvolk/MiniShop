var loginConst = {
    Card: "#mnshop-login",
    LoginAction: "login",
    EnterKey: "enter-key",
    LoginButton: "#mnshop-btn-login"
};
var loginIndex = {
    clickEvent: function (e) {
        let _handle = loginHandle();
        if (eval($(e).data('ename')) == loginConst.LoginAction) loginIndex.loginAction(e, _handle);
    },
    changeEvent: function (e) {
        let _handle = loginHandle();
        //if (eval($(e).data('ename')) == loginConst.statuChange) loginIndex.statuChange(e, _handle);
    },
    keyUpEvent: function (e) {
        let _handle = loginHandle();
        if (eval($(e).data('ename')) == loginConst.EnterKey) loginIndex.enterKey(e, _handle);
    },
    //child event
    init: function () {
        
    },
    loginAction: function (e, handle) {
        let _$content = $(e).closest(loginConst.Card);
        let _data = handle.data.inputToObject(_$content);
        console.log(_data);
        handle.login(_data, function (res) {
            if (res.statu == 200)
                open(res.data, "_self");
            else {
                swal(res.statu.toString(), res.message, 'error');
            }
        });
    },
    enterKey: function (e, handle) {
        if (event.key === 'Enter' || event.keyCode === 13) {
            // Do something
            $(loginConst.Card).find(loginConst.LoginButton).click();
        }
    }
};
var loginHandle = function () {
    let _login = function (data, callback) {
        let _url = '/admin/auth/login'
        $.post(_url, { authUser: data, returnUrl: data.ReturnUrl }, function (res) { callback(res); });
    }
    
    return {
        login: _login,
        data: helper.formData,
        validate: helper.inputValidate,
    }
};
loginIndex.init();