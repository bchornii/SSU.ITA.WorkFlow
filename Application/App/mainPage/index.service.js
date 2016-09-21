(function () {
    'use strict';
    angular
        .module('mainPageApp')
        .factory('indexService', indexService);

    indexService.$inject = ['localStorageService'];

    function indexService(localStorageService) {

        var indexServiceFactory = {
            logout: _logout
        };

        return indexServiceFactory;

        function _logout() {
            localStorageService.remove('authorizationData');            
        }
    }
})();