(function () {
    'use strict';
    angular
        .module('validationModule')
        .directive('emailValidation', emailValidation);

    function emailValidation() {
        var directive = {
            restrict : 'A',
            require : 'ngModel',
            link : _link
        };        
        var REG_EXPR = /^[a-zA-Z0-9~!$%^&*_=+}{\'?]+(\.[a-zA-Z0-9~!$%^&*_=+}/{\'?]+)*@\w+([\.-]?\w+)*(\.\w{2,6})+$/;
        return directive;

        function _link(scope, element, attrs, ngModelCtrl) {
            if (ngModelCtrl && ngModelCtrl.$validators.email) {
                ngModelCtrl.$validators.email = function (modelValue, viewValue) {
                    var value = modelValue || viewValue;
                    return ngModelCtrl.$isEmpty(value) || REG_EXPR.test(value);
                }
            }
        }
    }
})();