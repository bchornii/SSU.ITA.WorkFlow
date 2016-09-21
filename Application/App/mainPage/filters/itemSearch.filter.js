(function () {
    'use strict';
    angular
        .module('mainPageApp')
        .filter('itemSearchFilter', itemSearchFilter);

    itemSearchFilter.$inject = ['$filter'];

    function itemSearchFilter($filter) {
        return function (value, searchItem, searchCriteria) {
            if (angular.isArray(value) && searchItem) {                
                var orderedValues = $filter('orderBy')(value, searchItem);
                return $filter('filter')(orderedValues, searchCriteria);
            }            
        }
    }
})();