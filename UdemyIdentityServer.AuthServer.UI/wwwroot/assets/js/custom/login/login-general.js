"use strict";

// Class Definition
var KTLogin = function () {
    var _login;

    var _showForm = function (form) {
        var cls = 'login-' + form + '-on';
        var form = 'kt_login_' + form + '_form';

        _login.removeClass('login-forgot-on');
        _login.removeClass('login-signin-on');
        _login.removeClass('login-signup-on');

        _login.addClass(cls);

    }

    var _handleSignInForm = function () {
        var validation;


        $('#kt_login_signin_submit').on('click', function (e) {
            e.preventDefault();
            swal.fire({
                text: "All is cool! Now you submit this form",
                icon: "success",
                buttonsStyling: false,
                confirmButtonText: "Ok, got it!",
                customClass: {
                    confirmButton: "btn font-weight-bold btn-light-primary"
                }
            }).then(function () {
                KTUtil.scrollTop();
                $('#kt_login_signin_submit').submit();
            });
        });


    }



    // Public Functions
    return {
        // public functions
        init: function () {
            _login = $('#kt_login');

            _handleSignInForm();

        }
    };
}();

// Class Initialization
jQuery(document).ready(function () {
    KTLogin.init();
});
