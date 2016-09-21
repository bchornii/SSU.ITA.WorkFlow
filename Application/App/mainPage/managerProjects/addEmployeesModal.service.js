(function () {
    'use strict';

    angular
        .module('mainPageApp')
        .factory('addEmployeesModalService', addEmployeesModalService);

    addEmployeesModalService.$inject = ['$http', '$q', 'mainConstants', 'localStorageService'];

    function addEmployeesModalService($http, $q, mainConstants, localStorageService) {

        var addEmployeesModalServiceFactory = {
            getEmployees: _getEmployees
        };

        return addEmployeesModalServiceFactory;

        function _getEmployees(EmployeesId) {
            var managerId = localStorageService.get('authorizationData')
                                               .userId;
            return $http({
                method: 'POST',
                url: mainConstants.PROJECT_URL + managerId + '/add',
                data: EmployeesId
            });
        }
    }
})();
