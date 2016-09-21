
(function () {
    'use strict';
    angular
        .module('landingApp')
        .factory('interseptorsService', interseptorsService);

    interseptorsService.$inject = ['$q', 'toaster', 'CONFLICT', 'localStorageService'];

    function interseptorsService($q, toaster, CONFLICT, localStorageService) {

        var intercepService = {
            request: _request,
            responseError : _responseError
        };

        return intercepService;

        function _request(config) {

            config.headers = config.headers || {};

            var authData = localStorageService.get('authorizationData');
            if (authData) {
                config.headers.Authorization = 'Bearer ' + authData.token;
            }
            return config;
        }

        function _responseError(reject) {
            if (reject.status === CONFLICT) {
                toaster.error({ body: "Company is already exist." });
            }
            return $q.reject(reject);
        }
    }

})();