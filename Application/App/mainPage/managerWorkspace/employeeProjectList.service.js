(function () {
    'use strict';

    angular
        .module('mainPageApp')
        .factory('employeeProjectListService', employeeProjectListService);

    employeeProjectListService.$inject = ['$http', '$q', 'mainConstants'];

    function employeeProjectListService($http, $q, mainConstants) {
        var empProjectListServiceFactory = {
            getProjects: _getProjects,
            getProjectsList : _getProjectsList
        };
        return empProjectListServiceFactory;

        function _getProjects(empId, pageNum, numPerPage) {            
            return $http({
                          method: 'GET',
                          url: mainConstants.EMPLOYEES_URL + empId + '/projects/' + pageNum + '/' + numPerPage
                   });
        }

        function _getProjectsList(empId) {            
            return $http({
                          method: 'GET',
                          url: mainConstants.EMPLOYEES_URL + empId + '/projects'
                   });
        }
    }
})();