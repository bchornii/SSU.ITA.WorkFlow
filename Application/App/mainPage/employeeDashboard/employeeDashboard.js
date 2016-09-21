var calendarDemoApp = angular.module('calendarDemoApp', ['ui.calendar', 'ui.bootstrap']);

calendarDemoApp.controller('CalendarCtrl',
   function($scope, uiCalendarConfig) {
    var date = new Date();
    var d = date.getDate();
    var m = date.getMonth();
    var y = date.getFullYear();

    $scope.events = [
      {title: 'Report1',start: new Date(y, m, 9)},
      {title: 'Report2',start: new Date(y, m, d + 2, 8, 0),end: new Date(y, m, d + 2, 17, 0),allDay: false},
      {title: 'Click for Google',start: new Date(y, m, 10),end: new Date(y, m, 10),url: 'http://google.com/'}
    ];

    $scope.addEvent = function() {
      $scope.events.push({
        title: 'New Report',
        start: new Date(y, m, d),
        end: new Date(y, m, d),
        className: ['newReport']
      });
    };
    
    $scope.remove = function(index) {
      $scope.events.splice(index,1);
    };
    
    $scope.changeView = function(view,calendar) {
      uiCalendarConfig.calendars[calendar].fullCalendar('changeView',view);
    };
    
    $scope.uiConfig = {
      calendar:{
        height: 450,
        editable: true,
        header:{
          left: 'title',
          center: '',
          right: 'today prev,next'
        }
      }
    };

    $scope.eventSources = [$scope.events];
});