(function () {
    'use strict';

    angular
        .module('mainPageApp')
        .controller('EmployeeInfoController', EmployeeInfoController);
    EmployeeInfoController.$inject = ['$scope', '$q', '$timeout', 'toaster',
        'employeeInfoService', 'notificationService', 'mainConstants', 'employeeConstants'];

    function EmployeeInfoController($scope, $q, $timeout, toaster, employeeInfoService,
            notificationService, mainConstants, employeeConstants) {
        var vm = this;
        var selectedEmployeeId = mainConstants.INDEX_OUT_OF_RANGE;

        activate();
        function activate() {
            notificationService.subscribe('employeeSelected', employeeSelected);
            
        }
        function employeeSelected(userId) {
            if (userId != mainConstants.INDEX_OUT_OF_RANGE) {
                selectedEmployeeId = userId;
                return vm.getEmployeeInfo();
            }
        }
        vm.employee = {
            userId: '',
            name: '',
            surName: '',
            address: '',
            phoneNumber: '',
            email: '',
            roleId: '',
            roleName: ''
        };
        vm.roles = [];


        vm.getEmployeeInfo = function () {
            return employeeInfoService.getEmployeeInfo(selectedEmployeeId)
                .then(onSuccess)
                .catch(onFailed);

            function onSuccess(response) {
                vm.employee = response.data;
                vm.getRolesInfo();
            }

            function onFailed(reject) {
                $q.reject(reject);
            }
        }

        vm.getRolesInfo = function () {
            return employeeInfoService.getRoles()
                .then(onSuccess)
                .catch(onFailed)
            function onSuccess(response) {
                vm.roles = response.data;
            }
            function onFailed(reject) {
                $q.reject(reject);
            }
        }

        vm.updateEmployeeInfo = function () {
            employeeInfoService.sendChanges(vm.employee)
                .then(onSuccess)
                .catch(onFailed);

            function onSuccess(response) {
                notificationService.fire('employeeUpdated', vm.employee);
                toaster.success({ body: employeeConstants.CHANGES_SAVED_MESSAGE });
            }

            function onFailed(reject) {
                $q.reject(reject);
            };
        }
        vm.selectRole = function (role) {
            vm.employee.roleId = role.roleId;
            vm.employee.roleName = role.roleName;
        }

    }
})();