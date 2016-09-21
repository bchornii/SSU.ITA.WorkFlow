
(function () {
    'use strict';

    angular
        .module('landingApp')
        .controller('JoinUs', joinUs);

    joinUs.$inject = ['toaster', 'registrationService', 'landingConstants'];

    function joinUs(toaster, registrationService, landingConstants) {
        var vm = this;

        vm.registration;

        vm.signUp = function () {
            registrationService.saveRegistration(vm.registration)
                               .then(onRegistrationSuccess)
                               .catch(onRegistrationFailed);

            function onRegistrationSuccess() {
                toaster.success({ body: landingConstants.REGISTRATION_SUCCESS_MESSAGE });
                clearAllInputs();
            }

            function onRegistrationFailed() {
                clearMainInputs();
            }

            function clearAllInputs() {
                vm.registration.companyName = '';
                vm.registration.firstName = '';
                vm.registration.lastName = '';
                vm.registration.phone = '';
                vm.registration.email = '';
                vm.registration.password = '';
            }

            function clearMainInputs() {
                vm.registration.email = '';
                vm.registration.password = '';
            }
        };
    }

})();