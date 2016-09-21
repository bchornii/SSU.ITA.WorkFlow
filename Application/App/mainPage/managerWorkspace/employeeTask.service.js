(function () {
    'use strict';
    angular
        .module('mainPageApp')
        .factory('employeeTaskService', employeeTaskService);

    employeeTaskService.$inject = ['$http', '$q', 'mainConstants'];

    function employeeTaskService($http, $q, mainConstants) {
        var employeeTaskServiceFactory = {
            getTaskStatuses: _getTaskStatuses,            
            getTaskInformation: _getTaskInformation,            
            updateTaskInformation: _updateTaskInformation
        };
        return employeeTaskServiceFactory;

        function _getTaskInformation(taskId) {
            return $http({
                method: 'GET',
                url: mainConstants.BASE_URL + 'api/tasks/' + taskId
            });
        }

        function _getTaskStatuses() {           
            return $http({
                method: 'GET',
                url: mainConstants.BASE_URL + 'api/tasks/statuses'
            });
        }

        function _updateTaskInformation(taskObject) {           
            return $http({
                method: 'POST',
                url: mainConstants.BASE_URL + 'api/tasks/update',
                data: taskObject
            });
        }
    }
})();