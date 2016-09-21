(function () {
    'use strict';

    angular
        .module('landingApp')
        .factory('selfRegistrationService', selfRegistrationService);

    selfRegistrationService.$inject = ['$http', 'mainConstants'];

    function selfRegistrationService($http, mainConstants) {

        var selfRegService = {
            saveSelfRegistration: _saveSelfRegistration
        };

        return selfRegService;

        function _saveSelfRegistration(selfRegistration) {
            return $http.post(mainConstants.SELF_REGISTRATION_URL, selfRegistration);
        }
    }
})();