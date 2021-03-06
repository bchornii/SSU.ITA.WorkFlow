﻿
(function () {
    'use strict';

    angular.module('WorkFlowConstants', [])
           .constant('OK', 200)
           .constant('BAD_REQUEST', 400)
           .constant('UNAUTHORIZED', 401)
           .constant('NOT_FOUND', 404)
           .constant('CONFLICT', 409)
           .constant('INTERNAL_SERVER_ERROR', 500)
           .constant('NOT_IMPLEMENTED', 501)
           .constant('SERVICE_UNAVALIABLE', 503)
           .constant('GATEWAY_TIMEOUT', 504)
           .constant('INDEX_OUT_OF_RANGE', -1)
           .constant('baseUrl', 'http://localhost:4446/')
           .constant('loginUrl', "http://localhost:4446/token")
           .constant('registerUrl', "http://localhost:4446/api/account/register")
           .constant('workspaceUrl', "http://localhost:4446/workspace/home")
           .constant('selfRegistrationUrl', "http://localhost:4446/api/selfregistration/selfregister")
           .constant('projectsUrl', 'http://localhost:4446/api/projects/')
           .constant('employeesUrl', 'http://localhost:4446/api/employees/')
           .constant('rolesUrl', 'http://localhost:4446/api/roles/get')
           .constant('mainConstants', {
               OK: 200,
               BAD_REQUEST: 400,
               UNAUTHORIZED: 401,
               NOT_FOUND: 404,
               CONFLICT: 409,
               INTERNAL_SERVER_ERROR: 500,
               NOT_IMPLEMENTED: 501,
               SERVICE_UNAVALIABLE: 503,
               GATEWAY_TIMEOUT: 504,
               INDEX_OUT_OF_RANGE: -1,
               BASE_URL: 'http://localhost:4446/',
               LOGIN_URL: 'http://localhost:4446/token',
               REGISTER_URL: 'http://localhost:4446/api/account/register',
               WORKSPACE_URL: 'http://localhost:4446/workspace/home',
               SELF_REGISTRATION_URL: 'http://localhost:4446/api/selfregistration/selfregister',
               PROJECT_URL: 'http://localhost:4446/api/projects/',
               EMPLOYEES_URL: 'http://localhost:4446/api/employees/',
               ROLES_URL: 'http://localhost:4446/api/roles/get',
               AUTHENTIFICATION_FAILED: 'Authentication failed. Please check login and password.'
           })
            .constant('landingConstants', {
                REGISTRATION_SUCCESS_MESSAGE: 'Company has been registered successfully!',
                REGISTRATION_URL: 'http://localhost:4446/api/account/register'
            })
           .constant('employeeConstants', {
               LOADING_MESSAGE: 'Employees loading...',
               EMPTY_LIST_MESSAGE: 'Employee list is empty.',
               NO_DATA_RECEIVED: 'No data has been received from server. Status code : ',
               CHANGES_SAVED_MESSAGE: 'Changes successfully saved',
               CHOOSE_EMPLOYEE_MESSAGE: 'Please choose employee first.',
               PROJECT_LOADING_MESSAGE: 'Projects information is loading...',
               NOT_INVOLVED_MESSAGE: 'Employee has not been involved.',
               NO_DATA_MESSAGE: 'No data has been received.',
               TITLE_EDIT: 'Edit task information',
               TITLE_NEW: 'Add new task'
           })
           .constant('projectConstants', {
               LOADING_MESSAGE: 'Projects loading...',
               EMPTY_LIST_MESSAGE: 'Project list is empty.',
               NO_PROJECT_SELECTED_MESSAGE: 'Project is not selected',
               NO_DATA_RECEIVED: 'No data has been received from server. Status code : ',
               PROJECT_CREATED_MESSAGE: 'New project successfully saved',
               CHANGES_SAVED_MESSAGE: 'Changes successfully saved',
               EMPLOYEES_ADDED_POPUP_MESSAGE: "Employees added, press 'Save changes' to confirm",
               UNSELECTED_INDEX: -1,
               VISIBLE_PROJECTS_NUMBER: 10
           });
})();
