'use strict';

// Declare app level module which depends on views, and components
var app = angular.module('app', [
     'ui.router'
    //,'ngCookies'
    ,'angularLocalStorage'
]);

var SETTING = {
    APIURL:''
};


app.controller('appController',["$scope",function($scope){
    $scope.tittle = "黑舞堂";
}]);