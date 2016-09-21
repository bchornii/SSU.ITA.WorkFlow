(function () {
    'use strict';

    angular
         .module('mainPageApp')
         .controller('UserInvitation', UserInvitation);

    UserInvitation.$inject = ['$scope','$q', '$uibModalInstance', 'toaster', 'invitationService', 'localStorageService'];

    function UserInvitation($scope,$q, $uibModalInstance, toaster, invitationService, localStorageService) {
        var inv = this;
        inv.roles = [];
        inv.invitation = {
            RoleName: "",
            EmployeesEmails: "",
            EmployerEmail: ""
        }
       
        inv.emailPattern = (function () {
            var regexp = /^(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*;\s*|\s*$))*$/;
            return {
                test: function (value) {
                    if ($scope.required === false) {
                        return true;
                    }
                    return regexp.test(value);
                }
            };
        })();

        inv.close = function () {
            $uibModalInstance.close();
        };
        inv.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        }

        inv.sendInvites = function () {
            inv.invitation.EmployerEmail = localStorageService.get('authorizationData').userName;
            return invitationService.sendInvitation(inv.invitation)
                .then(onSuccess)
                .catch(onFail);

            function onSuccess() {              
                toaster.success({ body: "Invitation Sent!" });
                inv.close();
            }

            function onFail() {
                toaster.error({ body: "Something wrong" });
            }
        }

        activate();

        function activate() {
            return getAllRolesC();
        }

        function getAllRolesC() {

            return invitationService.getAllRoles()
                .then(onSuccess)
                .catch(onFailed);

            function onSuccess(response) {
                inv.roles = response.data;
            }

            function onFailed(reject) {
                $q.reject(reject);
            }
        }
    }

})();