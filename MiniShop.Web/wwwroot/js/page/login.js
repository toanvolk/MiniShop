var loginConst = {
    Card: "#mnshop-login",
    LoginAction: "login"
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
    //child event
    init: function () {
        
    },
    loginAction: function (e, handle) {
        let _$content = $(e).closest(loginConst.Card);
        let _data = handle.data.inputToObject(_$content);
        console.log(_data);
        handle.login(_data, function (res) { console.log(res)});
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