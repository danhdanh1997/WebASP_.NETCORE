var loginController = function () {
    this.initialize = function () {
        registerEvent();
    }
    var registerEvent = function () {
        $('#btnLogin').on('click', function (event) {
            event.preventDefault();
            var user = $('#txtUserName').val();
            var pass = $('#txtPassword').val();
            login(user, pass);
        })
    }
    var login = function (user, pass) {
        $.ajax({
            type: 'POST',
            data: {
                UserName: user,
                Password: pass
            },
            dataType: 'json',
            url: "/AdminLogin/Authen",
            success: function (res) {
                if (res.Success) {
                    window.location.href = "/Admin/Index"
                }
                else {
                    shop.notify("Đăng nhập không đúng!", 'error');
                }
            }
        })
    }
}
var logoutController = function () {
    this.initialize = function () {
        registerEvent();
    }
    var registerEvent = function () {
        $('#btnLogout').on('click', function (event) {
            event.preventDefault();
            logout();
        })
    }
    var logout = function () {
        $.ajax({
            type: 'GET',
            url: "/AdminLogin/Logout",
            success: function (res) {
                if (res.Success) {
                    window.location.href = "/AdminLogin/Index"
                }
                else {
                    shop.notify("Đăng xuât không thành công", 'error');
                }
            }
        })
    }
}
