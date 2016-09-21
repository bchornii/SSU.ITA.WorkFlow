
(function () {
    'use strict';
    angular
        .module('landingApp')
        .config(configurating);    
    configurating.$inject = ['$httpProvider'];

    function configurating($httpProvider) {
        $httpProvider.interceptors.push('interseptorsService');
    }

})();