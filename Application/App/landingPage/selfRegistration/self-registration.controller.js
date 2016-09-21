(function () {
    'use strict';

    angular
        .module('landingApp')
        .controller('SelfReg', selfReg);

    selfReg.$inject = ['selfRegistrationService', '$window', 'mainConstants'];

    function selfReg(selfRegistrationService, $window, mainConstants) {
        var vm = this;

        vm.selfRegistration = {
            firstName: '',
            lastName: '',
            address : '',
            phoneNumber: '',
            password: '',
            registrationToken: getParameterByName('token')
        };

        vm.signUp = function () {
            selfRegistrationService.saveSelfRegistration(vm.selfRegistration)
                               .then(onRegistrationSuccess)
                               .catch(onRegistrationFailed);

            function onRegistrationSuccess() {
                clearInputs();
                $window.location.href = mainConstants.BASE_URL;
            }

            function onRegistrationFailed(error) {
                alert('Error: ' + error.status + '\nMessage: ' + error.statusText + ' :(');
            }
        };

        function clearInputs() {
            vm.selfRegistration.firstName = '';
            vm.selfRegistration.lastName = '';
            vm.selfRegistration.address = '';
            vm.selfRegistration.phoneNumber = '';
            vm.selfRegistration.password = '';
        };

        function getParameterByName(name) {
            name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
            var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                results = regex.exec(location.search);
            return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
        };
    }
})();