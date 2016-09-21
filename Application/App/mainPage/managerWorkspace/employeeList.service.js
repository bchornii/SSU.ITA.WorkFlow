(function () {
    'use strict';

    angular
         .module('mainPageApp')
         .factory('employeeListService', employeeListService);

    employeeListService.$inject = ['$http', '$q', 'localStorageService', 'mainConstants'];

    function employeeListService($http, $q, localStorageService, mainConstants) {

        var empListServiceFactory = {
            getAll: _getAll
        }

        return empListServiceFactory;

        function _getAll() {
            var managerId = localStorageService.get('authorizationData')
                                               .userId;
            return $http({
                method: 'GET',
                url: mainConstants.EMPLOYEES_URL + managerId + '/all'
            });
        }
    }

})();