(function () {
    'use strict';

    angular
        .module('mainPageApp')
        .controller('EmployeeProjectListController', EmployeeProjectListController);

    EmployeeProjectListController.$inject = ['$q', '$scope', '$filter', '$uibModal',
                                             'employeeProjectListService',
                                             'notificationService',
                                             'mainConstants',
                                             'employeeConstants',
                                             'toaster'];

    function EmployeeProjectListController($q, $scope, $filter, $uibModal,
                                           employeeProjectListService,
                                           notificationService,
                                           mainConstants,
                                           employeeConstants,
                                           toaster) {
        var vm = this;
        vm.employeeId = mainConstants.INDEX_OUT_OF_RANGE;
        vm.projects = [];
        vm.pager = {
            size: 5,
            pageNum: 1,
            numPerPage: 10,
            totalEntries: 0
        };
        vm.itemListEmptyMsg = '';
        vm.selectedTask = mainConstants.INDEX_OUT_OF_RANGE;
        vm.searchProjects = searchProjects;
        vm.editTask = editTask;
        vm.addTask = addTask;
        vm.getUniqueId = getUniqueId;
        vm.isOpenForEdit = true;
        vm.getEmployeeProjectsServiceIsBusy = false;

        activate();

        function activate() {
            vm.itemListEmptyMsg = employeeConstants.CHOOSE_EMPLOYEE_MESSAGE;
            notificationService.subscribe('employeeSelected', employeeSelected);
        }

        function employeeSelected(employeeId) {
            if (employeeId !== mainConstants.INDEX_OUT_OF_RANGE) {
                vm.employeeId = employeeId;
                searchProjects();
            } else {
                vm.projects = [];
                vm.itemListEmptyMsg = employeeConstants.CHOOSE_EMPLOYEE_MESSAGE;
            }
        }

        function searchProjects() {
            return getUserProjects(vm.employeeId, vm.pager.pageNum, vm.pager.numPerPage);
        }

        function getUserProjects(employeeId, pageNum, numPerPage) {

            vm.getEmployeeProjectsServiceIsBusy = true;
            vm.itemListEmptyMsg = employeeConstants.PROJECT_LOADING_MESSAGE;

            employeeProjectListService.getProjects(employeeId, pageNum, numPerPage)
                                      .then(onSuccess)
                                      .catch(onFailed)
                                      .finally(onFinally);

            function onSuccess(response) {
                vm.itemListEmptyMsg = employeeConstants.NOT_INVOLVED_MESSAGE;
                vm.projects = $filter('orderBy')
                                     (response.data.items,
                                     'createDate',
                                     true);
                vm.pager.totalEntries = response.data.totalCount;
                return response;
            }

            function onFailed(reject) {
                $q.reject(reject);
            }

            function onFinally() {
                vm.getEmployeeProjectsServiceIsBusy = false;
            }
        }

        function editTask(taskId) {
            vm.selectedTask = taskId;
            openModal(vm.isOpenForEdit);
        }

        function addTask() {
            openModal(!vm.isOpenForEdit);
        }

        function openModal(openMode) {
            var modalInstance = $uibModal.open({
                animatation: true,
                templateUrl: '/app/mainPage/managerWorkspace/employeeTask.html',
                size: 'sm',
                controller: 'employeeTaskController',
                controllerAs: 'etask',
                resolve: {
                    taskObject: function (employeeTaskService) {
                        if (openMode == vm.isOpenForEdit) {
                            return employeeTaskService.getTaskInformation(vm.selectedTask);
                        } else {
                            return null;
                        }
                    }
                }
            });

            modalInstance.result.then(onSave)                         
            function onSave() {                
                searchProjects();
                toaster.success({ body: employeeConstants.CHANGES_SAVED_MESSAGE });
            }
        }

        function getUniqueId(projectId) {
            return vm.employeeId.toString() + projectId.toString();
        }
    }
})();