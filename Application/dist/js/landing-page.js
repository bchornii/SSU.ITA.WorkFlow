angular.module("landingApp",["ngRoute","LocalStorageModule","ngAnimate"]),angular.module("landingApp").constant("loginUrl","http://localhost:4446/token").constant("registerUrl","http://localhost:4446/api/account/register"),function(){function n(){}angular.module("landingApp").config(n)}(),function(){function n(){}angular.module("landingApp").controller("ContactUs",n)}(),function(){function n(){}angular.module("landingApp").directive("contactUs",n)}(),function(){function n(){}angular.module("landingApp").factory("contact",n)}(),angular.module("contactUsApp",[]),function(){function n(){}angular.module("landingApp").config(n)}(),function(){"use strict";function n(n){var t=this;t.savedSuccessfully=!1,t.registration={companyName:"",firstName:"",lastName:"",phone:"",email:"",password:""},t.singUp=function(){function e(n){t.savedSuccessfully=!0,alert("Company has been registred successfully!"),o()}function a(n){alert("Error: "+n.status+"\nMessage: "+n.statusText+" :(")}function o(){t.registration.companyName="",t.registration.firstName="",t.registration.lastName="",t.registration.phone="",t.registration.email="",t.registration.password=""}n.saveRegistration(t.registration).then(e)["catch"](a)}}angular.module("landingApp").controller("JoinUs",n),n.$inject=["registrationService"]}(),function(){function n(){}angular.module("landingApp").directive("joinUs",n)}(),angular.module("joinUsApp",[]),function(){"use strict";function n(n,t){function e(e){return n.post(t,e)}var a={saveRegistration:e};return a}angular.module("landingApp").factory("registrationService",n),n.$inject=["$http","registerUrl"]}(),function(){"use strict";function n(n){n.interceptors.push("interseptorsService")}angular.module("landingApp").config(n),n.$inject=["$httpProvider"]}(),function(){"use strict";function n(n,t){function e(){function e(n){alert("Login successfull!"),t.location.href="https://www.google.com.ua/"}function i(n){400==n.status?alert("Password or email incorrect."):alert("Error : "+n.status+" ; Message : "+n.statusText)}n.login(o.loginData).then(e)["catch"](i)["finally"](a)}function a(){o.loginData.userName="",o.loginData.password=""}var o=this;o.loginData={userName:"",password:""},o.login=e}angular.module("landingApp").controller("Login",n),n.$inject=["loginService","$window"]}(),function(){"use strict";function n(){}angular.module("landingApp").directive("userLogin",n)}(),function(){"use strict";function n(n){function t(t){t.headers=t.headers||{};var e=n.get("authorizationData");return e&&(t.headers.Authorization="Bearer "+e.token),t}function e(n){401==n.status}var a={request:t,responceError:e};return a}angular.module("landingApp").factory("interseptorsService",n),n.$inject=["localStorageService"]}(),angular.module("loginApp",[]),function(){"use strict";function n(n,t,e,a){function o(o){function r(n){s.isAutorized=!0,s.userName=o.userName,t.set("authorizationData",{token:n.data.access_token,userName:o.userName}),l.resolve(n)}function u(n){i(),l.reject(n)}var c="grant_type=password&username="+o.userName+"&password="+o.password,l=a.defer();return n({method:"POST",url:e,data:c,headers:{"Content-Type":"application/x-www-form-urlencoded"}}).then(r)["catch"](u),l.promise}function i(){t.remove("authorizationData"),s.isAutorized=!1,s.userName=""}function r(){var n=t.get("authorizationData");n&&(s.isAutorized=!0,s.userName=n.userName)}var u={login:o,logout:i,fetchAuthentificationData:r,authentification:s},s={isAutorized:!1,userName:""};return u}angular.module("landingApp").factory("loginService",n),n.$inject=["$http","localStorageService","loginUrl","$q"]}();