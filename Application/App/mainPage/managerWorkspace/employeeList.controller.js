(function () {
    'use strict';

    angular
         .module('mainPageApp')
         .controller('EmployeeListController', EmployeeListController);

    EmployeeListController.$inject = ['$scope', '$filter', '$q','$uibModal',
                                      'employeeListService', 'notificationService',
                                      'mainConstants', 'employeeConstants'];

    function EmployeeListController($scope, $filter, $q,$uibModal,
                                    employeeListService, notificationService,
                                    mainConstants, employeeConstants) {
        var vm = this;
        vm.names = [];
        vm.filteredData = [];        
        vm.selectedItem = mainConstants.INDEX_OUT_OF_RANGE;
        vm.maxFilteredNames = 10;
        vm.search = '';
        vm.openModal = openModal;
        vm.itemListEmptyMsg = '';
        vm.getUsersServiceIsBusy = false;
        vm.setActiveItem = setActiveItem;
        vm.getActiveItem = getActiveItem;

        activate();

        function openModal() {

            var modalInstance = $uibModal.open({
                animation: true,
                templateUrl: '/app/mainPage/managerWorkspace/invitationModal.html',
                controller: 'UserInvitation',
                controllerAs: 'invitation'
            });         
        }

        function activate() {
            notificationService.subscribe('employeeUpdated', employeeUpdated);
            return getAllUsers();
        }

        function employeeUpdated(employee) {
            vm.filteredData.forEach(function (item, i) {
                if (item.userId == employee.userId) {
                    item.firstName = employee.name;
                    item.secondName = employee.surName; 
                }
            })  
                
        } 

        function getAllUsers() {
            vm.getUsersServiceIsBusy = true;
            vm.itemListEmptyMsg = employeeConstants.LOADING_MESSAGE;
            return employeeListService.getAll()
                                  .then(onSuccess)
                                  .catch(onFailed)
                                  .finally(onFinally);

            function onSuccess(response) {
                vm.names = response.data;
                vm.itemListEmptyMsg = employeeConstants.EMPTY_LIST_MESSAGE;
                $scope.$watch(valueFunction, listenerFunction);

                function valueFunction() {
                    return vm.search;
                }

                function listenerFunction(newSearchValue) {
                    vm.filteredData = $filter('itemSearchFilter')
                                             (vm.names, 'secondName', newSearchValue);
                    if (!findEmployeeInList(vm.filteredData, vm.selectedItem)) {
                        setActiveItem(mainConstants.INDEX_OUT_OF_RANGE);
                    }
                }
                return response;
            }

            function onFailed(reject) {
                vm.itemListEmptyMsg = employeeConstants.NO_DATA_RECEIVED + reject.status;
                $q.reject(reject);
            }

            function onFinally() {
                vm.getUsersServiceIsBusy = false;
            }
        }

        function setActiveItem(itemId) {
            vm.selectedItem = itemId;
            notificationService.fire('employeeSelected',itemId);
        }

        function getActiveItem() {
            return vm.selectedItem;
        }

        function findEmployeeInList(list, selectedEmployeeId) {
            var listContainsIndex = false;
            if (list.length > 0) {
                list.forEach(function (element, index) {
                    if (element.userId === selectedEmployeeId) {
                        listContainsIndex = true;
                    }
                });
            }
            return listContainsIndex;
        }
    }

})();