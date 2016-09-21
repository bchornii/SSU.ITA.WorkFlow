
(function () {
    'use strict';
    angular
        .module('landingApp')
        .controller('LoginController', LoginController);

    LoginController.$inject = ['$window', 'toaster', 'loginService', 'mainConstants'];

    function LoginController($window, toaster, loginService, mainConstants) {
        var vm = this;

        vm.loginData = {
            userName: '',
            password: ''
        };
        vm.login = login;

        function login() {
            loginService.login(vm.loginData)
                        .then(onLoginSuccess)
                        .catch(onLoginFail)
                        .finally(clearInputs);
           
            function onLoginFail(response) {
                toaster.error({ body: mainConstants.AUTHENTIFICATION_FAILED });
            }

            function onLoginSuccess(response) {                                           
                $window.location.href = mainConstants.WORKSPACE_URL;
            }            
        }

        function clearInputs() {
            vm.loginData.userName = '';
            vm.loginData.password = '';            
        }
    }

})();