(function () {
    'use strict';
    angular
        .module('validationModule')
        .directive('passwordValidation', passwordValidation);

    function passwordValidation() {
        var directive = {
            restrict: 'A',
            require: 'ngModel',
            link : _link
        };
        var REG_EXPR = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}$/;        
        return directive;

        function _link(scope, element, attrs, ngModelCtrl) {
            if (ngModelCtrl) {
                ngModelCtrl.$validators.password = function (modelValue, viewValue) {
                    var value = modelValue || viewValue;
                    return ngModelCtrl.$isEmpty(value) || REG_EXPR.test(value);
                }
            }
        }
    }   
})();
