(function () {
    'use strict';

    angular
        .module('mainPageApp')
        .factory('invitationService', invitationService);

    invitationService.$inject = ['$http', '$q'];

    function invitationService($http, $q) {

        var invitationServiceFactory = {
            getAllRoles: _getAllRoles,
            sendInvitation: _sendInvitation
        }

        return invitationServiceFactory;

        function _getAllRoles() {
            var def = $q.defer();

            $http({
                method: 'GET',
                url: 'http://localhost:4446/api/UserRoles/GetRoles'
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

        function _sendInvitation(invitation) {

            return $http.post('http://localhost:4446/api/EmployeesInvitation/SendInvitation', invitation);
        }
    }
})();