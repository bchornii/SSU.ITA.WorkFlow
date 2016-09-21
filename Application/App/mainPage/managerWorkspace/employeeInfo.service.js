(function () {
    'use strict';
    angular
        .module('mainPageApp')
        .factory('employeeInfoService', employeeInfoService);
    employeeInfoService.$inject = ['$http', '$q', 'mainConstants'];

    function employeeInfoService($http, $q, mainConstants) {

        var employeeInformationServiceFactory = {
            getEmployeeInfo: _getEmployeeInfo,
            sendChanges: _sendChanges,
            getRoles: _getRoles
        };

        return employeeInformationServiceFactory;

        function _getEmployeeInfo(userId) {
            return $http.get(mainConstants.EMPLOYEES_URL + userId);
        }

        function _sendChanges(user) {
            return $http.post(mainConstants.EMPLOYEES_URL + 'update', user);
        }
        function _getRoles() {
            return $http.get(mainConstants.ROLES_URL);
        }

    }

})();