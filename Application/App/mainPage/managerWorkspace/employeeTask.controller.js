(function () {
    'use strict';

    angular
        .module('mainPageApp')
        .controller('employeeTaskController', employeeTaskController);

    employeeTaskController.$inject = ['$uibModalInstance', '$filter',
                                      'taskObject', 'employeeListService',
                                      'employeeTaskService',
                                      'employeeProjectListService',
                                      'employeeConstants'];

    function employeeTaskController($uibModalInstance, $filter,
                                       taskObject, employeeListService,
                                       employeeTaskService,
                                       employeeProjectListService,
                                       employeeConstants) {
        var vm = this;
        vm.employees = [];
        vm.statuses = [];
        vm.projects = [];
        vm.save = save;
        vm.cancel = cancel;
        vm.getEmployees = getEmployees;
        vm.getStatuses = getStatuses;
        vm.getProjects = getProjects;
        vm.selectStatus = selectStatus;
        vm.selectEmployee = selectEmployee;
        vm.selectProject = selectProject; 
        vm.checkPristine = checkPristine;
        vm.modalTitle = '';
        vm.newTaskId = 0;
        vm.projectCanBeSelected = false;
        vm.statusCanBeSelected = false;
        vm.sendDataError = false;
        vm.dataNotRecievedMsg = employeeConstants.NO_DATA_MESSAGE;

        activate();

        function activate() {            
            if (taskObject) {
                vm.data = taskObject.data;
                vm.modalTitle = employeeConstants.TITLE_EDIT;
            } else {
                vm.data = {};
                vm.data.taskId = vm.newTaskId;
                vm.modalTitle = employeeConstants.TITLE_NEW;
            }
            vm.projectCanBeSelected =
                vm.statusCanBeSelected =
                hasValue(vm.data.employeeId);
            vm.employeeTask = {
                taskId: vm.data.taskId,
                taskName: vm.data.taskName,
                projectId: vm.data.projectId,
                userId: vm.data.employeeId,
                statusId: vm.data.statusId,
                description: vm.data.description
            };            
        }

        function save() {
            employeeTaskService.updateTaskInformation(vm.employeeTask)
                               .then(onSuccess)
                               .catch(onFailed);
            function onSuccess() {
                $uibModalInstance.close();
            }
            function onFailed() {
                vm.sendDataError = true;
            }
        }

        function cancel() {
            $uibModalInstance.dismiss('cancel');
        }

        function getEmployees() {
            employeeListService.getAll()
                               .then(onSuccess)
                               .catch(onFailed);
            function onSuccess(response) {
                vm.employees = $filter('orderBy')(response.data, 'secondName');
            }
            function onFailed() {
                vm.data.employeeFirstName =
                    vm.data.employeeSecondName =
                    vm.dataNotRecievedMsg;
            }
        }

        function getProjects() {
            employeeProjectListService.getProjectsList(vm.employeeTask.userId)
                                      .then(onSuccess)
                                      .catch(onFailed);
            function onSuccess(response) {
                vm.projects = response.data;
            }
            function onFailed() {
                vm.data.projectName = vm.dataNotRecievedMsg;
            }
        }      

        function getStatuses() {
            employeeTaskService.getTaskStatuses()
                               .then(onSuccess)
                               .catch(onFailed);
            function onSuccess(response) {
                vm.statuses = response.data;
            }
            function onFailed() {
                vm.data.statusName = vm.dataNotRecievedMsg;
            }
        }

        function selectEmployee(employee) {
            vm.employeeTask.userId = employee.userId;
            vm.employeeTask.projectId = undefined;
            vm.data.projectName = '';
            vm.data.employeeFirstName = employee.firstName;
            vm.data.employeeSecondName = employee.secondName;
            vm.projectCanBeSelected = hasValue(employee);
        }

        function selectProject(project) {
            vm.employeeTask.projectId = project.projectId;
            vm.data.projectName = project.name;
            vm.statusCanBeSelected = hasValue(project);
        }

        function selectStatus(status) {
            vm.employeeTask.statusId = status.id;
            vm.data.statusName = status.name;
        }        

        function checkPristine() {            
            return  hasValue(vm.employeeTask.userId) &&
                    hasValue(vm.employeeTask.projectId) &&
                    hasValue(vm.employeeTask.statusId);
        }

        function hasValue(variable) {
            return variable != undefined && variable != null;
        }
    }

})();