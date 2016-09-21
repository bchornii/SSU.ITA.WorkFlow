(function () {
    'use strict';

    angular
        .module('mainPageApp')
        .factory('projectListService', projectListService);

    projectListService.$inject = ['$http', '$q','localStorageService', 'baseUrl'];

    function projectListService($http, $q, localStorageService, baseUrl) {

        var projectListServiceFactory = {
            getAll: _getAll
        };

        return projectListServiceFactory;

        function _getAll() {
            var def = $q.defer();
            var managerId = localStorageService.get('authorizationData')
                                               .userId;
            $http({
                method: 'GET',
                url: baseUrl + 'api/projects/' + managerId + '/all'
            }).then(onSuccess)
              .catch(onFailed);

            function onSuccess(response) {
                def.resolve(response);
            }

            function onFailed(reject) {
                def.reject(reject);
            }

            return def.promise;
        }
    }
})();