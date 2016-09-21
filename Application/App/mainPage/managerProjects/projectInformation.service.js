(function () {
    'use strict';

    angular
        .module('mainPageApp')
        .factory('projectInformationService', projectInformationService);

    projectInformationService.$inject = ['$http', '$q', 'mainConstants', 'localStorageService'];

    function projectInformationService($http, $q, mainConstants, localStorageService) {

        var projectInformationServiceFactory = {
            sendChanges: _sendChanges,
            getProject: _getProject,
            createNewProject: _createNewProject
        };

        return projectInformationServiceFactory;

        function _getProject(projectId) {          
            return $http.get(mainConstants.PROJECT_URL + projectId)
        }
        
        function _sendChanges(project) {            
            return $http.post(mainConstants.PROJECT_URL + 'update', project);
        }

        function _createNewProject(project) {
            var managerId = localStorageService.get('authorizationData')
                                               .userId;
            project.creatorId = managerId;
            return $http.post(mainConstants.PROJECT_URL + 'create', project);
        }
    }
})();