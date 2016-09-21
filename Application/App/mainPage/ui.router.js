(function () {
    'use strict'

    angular
        .module('mainPageApp')
        .config(configuration);

    configuration.$inject = ['$stateProvider', '$urlRouterProvider'];

    function configuration($stateProvider, $urlRouterProvider) {
        $urlRouterProvider.otherwise("/managerWorkspace");

        $stateProvider
        .state('managerWorkspace', {
            url: '/managerWorkspace',            
            views: {
                '@': {
                    templateUrl: '/app/mainPage/managerWorkspace/managerWorkspace.html'
                },
                'employeeList@managerWorkspace': {
                    templateUrl: '/app/mainPage/managerWorkspace/employeeList.html',
                    controller: 'EmployeeListController',
                    controllerAs: 'elist'
                },
                'employeeInformation@managerWorkspace' : {
                    templateUrl: '/app/mainPage/managerWorkspace/employeeInfo.html',
                    controller: 'EmployeeInfoController',
                    controllerAs: 'einfo'
                },
                'employeeProjectList@managerWorkspace': {
                    templateUrl: '/app/mainPage/managerWorkspace/employeeProjectList.html',
                    controller: 'EmployeeProjectListController',
                    controllerAs: 'plist'
                }                                
            }
        })
        .state('managerProjects', {
            url: '/managerProjects',
            views: {
                '@': {
                    templateUrl: '/app/mainPage/managerProjects/managerProjects.html'
                },
                'projectList@managerProjects': {
                    templateUrl: '/app/mainPage/managerProjects/projectList.html',
                    controller: 'ProjectListController',
                    controllerAs: 'projectList'
                }
                ,
                'projectInformation@managerProjects': {
                    templateUrl: '/app/mainPage/managerProjects/projectInformation.html',
                    controller: 'ProjectInformationController',
                    controllerAs: 'projectInformation'
                }
            }
        });
    }
})();