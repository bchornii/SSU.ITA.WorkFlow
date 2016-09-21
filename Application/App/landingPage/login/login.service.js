
(function () {
    'use strict';
    angular
        .module('landingApp')
        .factory('loginService', loginService);

    loginService.$inject = ['$http', '$q', '$window', 'localStorageService', 'mainConstants'];

    function loginService($http, $q, $window, localStorageService, mainConstants) {

        var logService = {
            login: _login,
            fetchAuthenticationData: _fetchAuthenticationData
        };

        return logService;

        function _login(loginData) {

            var credentials = 'grant_type=password&username=' +
                              loginData.userName + '&password=' + loginData.password;
            var def = $q.defer();

            $http({
                method: 'POST',
                url: mainConstants.LOGIN_URL,
                data: credentials,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            }).then(onLoginSuccess)
             .catch(onLoginFailed);

            function onLoginSuccess(response) {
                localStorageService.set('authorizationData', {
                    token: response.data.access_token,
                    userId: response.data.userId,
                    userName: loginData.userName
                });
                def.resolve(response);
            }

            function onLoginFailed(err) {
                _logout();
                def.reject(err);
            }

            return def.promise;
        }

        function _logout() {
            localStorageService.remove('authorizationData');
        }

        function _fetchAuthenticationData() {
            var credentials = localStorageService.get('authorizationData');
            if (credentials) {
                $window.location.href = mainConstants.WORKSPACE_URL;
            }
        }
    }

})();