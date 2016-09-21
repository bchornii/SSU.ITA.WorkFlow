(function () {
    'use strict';

    angular
        .module('landingApp')
        .factory('registrationService', registrationService);

    registrationService.$inject = ['$http', 'landingConstants'];

    function registrationService($http, landingConstants) {

        var regService = {
            saveRegistration: _saveRegistration
        };

        return regService;

        function _saveRegistration(registration) {
            return $http.post(landingConstants.REGISTRATION_URL, registration);
        }
    }
})();