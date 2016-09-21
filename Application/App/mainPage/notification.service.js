(function () {
    'use strict';

    angular
        .module('mainPageApp')
        .factory('notificationService', notificationService);

    function notificationService() {

        var indexOutOfRangeValue = -1;
        var notificationObject = [];

        var notificationServiceFactory = {
            subscribe: _subscribe,                        
            fire: _fire,                               
        };

        return notificationServiceFactory;
        
        function _subscribe(eventName, callbackFunction) {
            if (angular.isString(eventName) && angular.isFunction(callbackFunction)) {
                var obj = _get(eventName) || { eventName: eventName, items: [] };                            
                obj.items.push(callbackFunction);
                _set(obj);                
            }            
        }                   
        
        function _fire(eventName, args, context) {
            var eventIndex = _indexOf(eventName);
            var userContext = context || null;            
            if (eventIndex != indexOutOfRangeValue) {
                notificationObject[eventIndex].items.forEach(function (callbackFunction) {
                    callbackFunction.call(userContext, args);
                });
            }
        }
        
        function _get(eventName) {
            var eventIndex = _indexOf(eventName);
            if (eventIndex != indexOutOfRangeValue) {
                return notificationObject[eventIndex];
            }
            return null;
        }
        
        function _set(eventObject) {
            if (_justAdded(eventObject.items)){
               notificationObject.push(eventObject);
            }            
        }
        
        function _indexOf(eventName) {
            var eventIndex = indexOutOfRangeValue;
            notificationObject.forEach(function (item, i) {
                if (strcmp(item.eventName, eventName) == 0) {
                    eventIndex = i;
                }
            });
            return eventIndex;
        }

        function _justAdded(items) {
            return items.length <= 1;
        }
        
        function strcmp(str1, str2) {
            return ((str1 === str2) ? 0 : ((str1 > str2) ? 1 : indexOutOfRangeValue));
        }        
    }

})();