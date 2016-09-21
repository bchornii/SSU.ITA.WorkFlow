(function () {
    'use strict';

    angular
        .module('mainPageApp')
        .controller('ProjectInformationController', ProjectInformationController);

    ProjectInformationController.$inject = ['$scope', '$q', '$uibModal',
        'toaster', 'projectInformationService', 'notificationService', 'projectConstants'];

    function ProjectInformationController($scope, $q, $uibModal,
        toaster, projectInformationService, notificationService, projectConstants) {

        var vm = this;
        var isEmployeesAdded = false;
        var newEmployees = [];
        var selectedProjectId = projectConstants.UNSELECTED_INDEX;

        vm.project = {
            name: projectConstants.NO_PROJECT_SELECTED_MESSAGE
        };

        vm.startDateOpened = false;
        vm.endDateOpened = false;
        vm.openStartDate = openStartDate;
        vm.openEndDate = openEndDate;
        vm.saveChanges = saveChanges;
        vm.isProjectSelected = isProjectSelected;
        vm.openModal = openModal;

        activate();

        function activate() {
            notificationService.subscribe('projectSelected', projectSelected);
            notificationService.subscribe('addNewProject', addNewProject);
        }

        function getProject() {
            return projectInformationService.getProject(selectedProjectId)
                .then(onSuccess)
                .catch(onFailed);

            function onSuccess(response) {
                vm.project = response.data;
                vm.project.createDate = stringToDate(vm.project.createDate);
                vm.project.endDate = stringToDate(vm.project.endDate);
            }

            function onFailed(reject) {
                vm.itemListEmptyMsg = projectConstants.NO_DATA_RECEIVED + reject.status;
            }
        }

        function stringToDate(date) {
            return new Date(date);
        }

        function openStartDate() {
            vm.startDateOpened = true;
        }

        function openEndDate() {
            vm.endDateOpened = true;
        }

        function saveChanges() {
            var employees = vm.project.employees;
            vm.project.employees = isEmployeesAdded ? newEmployees : [];

            if (isNewProject()) {
                projectInformationService.createNewProject(vm.project)
                                         .then(onSuccessCreate)
                                         .catch(onFailed);
            }
            else {
                projectInformationService.sendChanges(vm.project)
                                         .then(onSuccessUpdate)
                                         .catch(onFailed);
            }

            function onSuccessCreate(response) {
                vm.project.projectId = response.data
                toaster.success({ body: projectConstants.PROJECT_CREATED_MESSAGE });
                notificationService.fire('projectUpdated', vm.project);
            }

            function onSuccessUpdate(response) {
                toaster.success({ body: projectConstants.CHANGES_SAVED_MESSAGE });
                notificationService.fire('projectUpdated', vm.project);

                if (isEmployeesAdded) {
                    isEmployeesAdded = false;
                    vm.project.employees = employees;
                    newEmployees.splice(0, newEmployees.length);
                }
                else {
                    vm.project.employees = employees;
                }
            }

            function onFailed(reject) {
                vm.itemListEmptyMsg = projectConstants.NO_DATA_RECEIVED + reject.status;
                $q.reject(reject);
            }
        }

        function projectSelected(projectId) {
            selectedProjectId = projectId;
            newEmployees = [];
            return getProject();
        }

        function isProjectSelected() {
            return selectedProjectId == projectConstants.UNSELECTED_INDEX;
        }

        function addEmployees(employeeList, destination) {
            employeeList.forEach(function (employee, i) {
                destination.push(
                {
                    userId: employee.userId,
                    firstName: employee.firstName,
                    secondName: employee.secondName
                });
            });
        }

        function isNewProject() {
            return vm.project.projectId == 0;
        }

        // RP
        function openModal() {

            var modalInstance = $uibModal.open({
                animation: true,
                templateUrl: '/app/mainPage/managerProjects/addEmployeesModal.html',
                controller: 'addEmployeesModalController',
                controllerAs: 'addEmplModalContr',
                resolve: {
                    EmployeesId: function () {
                        return getEmployeesId();
                    }
                }
            });

            modalInstance.result.then(onOk);
                         
            function onOk(result) {
                if (result.length > 0) {
                    isEmployeesAdded = true;
                    addEmployees(result, vm.project.employees);
                    addEmployees(result, newEmployees);
                    toaster.success({ body: projectConstants.EMPLOYEES_ADDED_POPUP_MESSAGE });
                }
            }
        }

        function getEmployeesId() {
            var idList = [];
            vm.project.employees.forEach(function (item, i) {
                idList.push(item.userId);
            });
            return idList;
        }

        function addNewProject(str) {
            selectedProjectId = 0;
            vm.project = {
                projectId: '0',
                name: 'New Project',
                statusId: '3',
                createDate: '',
                endDate: '',
                description: '',
                employees: []
            };;
        }
    }
})();