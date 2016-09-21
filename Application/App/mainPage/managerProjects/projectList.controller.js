(function () {
    'use strict';

    angular
        .module('mainPageApp')
        .controller('ProjectListController', ProjectListController);

    ProjectListController.$inject = ['$scope', '$filter', '$q',
                                     'projectListService', 'notificationService', 'projectConstants'];

    function ProjectListController($scope, $filter, $q,
                                    projectListService, notificationService, projectConstants) {
        var vm = this;
        vm.project = [];
        vm.filteredData = [];
        vm.selectedItem = projectConstants.UNSELECTED_INDEX;
        vm.visibleProjectsNumber = projectConstants.VISIBLE_PROJECTS_NUMBER;
        vm.search = '';
        vm.itemListEmptyMsg = '';
        vm.getProjectsServiceIsBusy = false;
        vm.setActiveItem = setActiveItem;
        vm.getActiveItem = getActiveItem;
        vm.addNewProject = addNewProject;

        activate();

        function activate() {
            notificationService.subscribe('projectUpdated', projectUpdated);
            return getAllProjects();
        }

        function projectUpdated(project) {
            var isProjectNew = true;
            vm.project.forEach(function (item, i) {
                if (item.projectId == project.projectId) {
                    vm.project[i].name = project.name;
                    isProjectNew = false;
                }
            })
            if (isProjectNew) {
                var newProject = {
                    projectId: project.projectId,
                    name: project.name
                }
                vm.project.push(newProject);
                vm.filteredData.push(newProject);
                vm.setActiveItem(project.projectId);
            }
        }

        function getAllProjects() {

            vm.getProjectsServiceIsBusy = true;
            vm.itemListEmptyMsg = projectConstants.LOADING_MESSAGE;

            return projectListService.getAll()
                                  .then(onSuccess)
                                  .catch(onFailed)
                                  .finally(onFinally);

            function onSuccess(response) {
                vm.project = response.data;
                vm.itemListEmptyMsg = projectConstants.EMPTY_LIST_MESSAGE;

                $scope.$watch(valueFunction, listenerFunction);

                function valueFunction() {
                    return vm.search;
                }

                function listenerFunction(newSearchValue) {
                    vm.selectedItem = -1;
                    vm.filteredData = $filter('itemSearchFilter')
                                             (vm.project, 'Name', newSearchValue);
                }

                return response;
            }

            function onFailed(reject) {
                vm.itemListEmptyMsg = projectConstants.NO_DATA_RECEIVED + reject.status;
                $q.reject(reject);
            }

            function onFinally() {
                vm.getProjectsServiceIsBusy = false;
            }
        }

        function setActiveItem(itemId) {
            vm.selectedItem = itemId;
            notificationService.fire('projectSelected', itemId);
        }

        function getActiveItem() {
            return vm.selectedItem;
        }

        function addNewProject() {
            vm.selectedItem = 0;
            notificationService.fire('addNewProject', '');
        }
    }
})();
