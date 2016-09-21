(function () {
    'use strict';
    angular
        .module('mainPageApp')
        .controller('IndexController', IndexController);

    IndexController.$inject = ['$window', 'indexService', 'mainConstants'];

    function IndexController($window, indexService, mainConstants) {

        var vm = this;
        vm.logout = logout;

        function logout() {
            indexService.logout();
            $window.location.href = mainConstants.BASE_URL;
        }
    }
})();