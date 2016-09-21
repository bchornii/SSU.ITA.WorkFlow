(function () {
    'use strict';
    angular
        .module('landingApp')
        .run(runApplication);

    runApplication.$inject = ['loginService'];

    function runApplication(loginService) {
        loginService.fetchAuthenticationData();
    }
})();