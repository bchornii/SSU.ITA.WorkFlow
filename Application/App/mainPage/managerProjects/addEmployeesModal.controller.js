(function () {
    'use strict';

    angular
        .module('mainPageApp')
        .controller('addEmployeesModalController', addEmployeesModalController);

    addEmployeesModalController.$inject = ['$scope', '$filter', '$q', '$uibModalInstance', 'addEmployeesModalService', 'EmployeesId', 'employeeConstants'];

    function addEmployeesModalController($scope, $filter, $q, $uibModalInstance, addEmployeesModalService, EmployeesId, employeeConstants) {
        var vm = this;
        vm.employees = [];
        vm.filteredData = [];
        vm.search = '';
        vm.itemListEmptyMsg;
        vm.isModalServiceIsBusy = false;

        var selectedEmployees = [];
        
        activate();

        function activate() {
            getUsers();            
        }

        function getUsers() {
            vm.itemListEmptyMsg = employeeConstants.LOADING_MESSAGE;
            vm.isModalServiceIsBusy = true;

            return addEmployeesModalService.getEmployees(EmployeesId)
                                           .then(onSuccess)
                                           .catch(onFailed)
                                           .finally(onFinally);

            function onSuccess(response) {
                vm.employees = response.data;               
                vm.itemListEmptyMsg = employeeConstants.EMPTY_LIST_MESSAGE;

                $scope.$watch(valueFunction, listenerFunction);

                function valueFunction() {
                    return vm.search;
                }

                function listenerFunction(newSearchValue) {
                    vm.filteredData = $filter('itemSearchFilter')
                                             (vm.employees, 'secondName', newSearchValue);
                }

                return response;
            }

            function onFailed(reject) {
                vm.itemListEmptyMsg = employeeConstants.NO_DATA_RECEIVED + reject.status;
                $q.reject(reject);
            }

            function onFinally() {
                vm.isModalServiceIsBusy = false;
            }
        }

        vm.getSelectedEmployees = function () {
            vm.employees.forEach(function (item, i) {
                if (item.selected) {
                    selectedEmployees.push(item);
                }
            });
            return selectedEmployees;
        };

        vm.add = function () {
            $uibModalInstance.close(vm.getSelectedEmployees());
        };
        vm.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        }
    }
})();
