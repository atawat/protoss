'use strict';

// Declare app level module which depends on views, and components
var app = angular.module('app', [
     'ui.router'
    //,'ngCookies'
    ,'angularLocalStorage'
    ,'oc.lazyLoad'
]);

var SETTING = {
    APIURL:'http://www.ynwebapp.cn/api'
};


app.controller('appController',["$scope",function($scope){
    $scope.tittle = "黑舞堂";
}]);

app.config(
    [        '$controllerProvider', '$compileProvider', '$filterProvider', '$provide',
        function ($controllerProvider,   $compileProvider,   $filterProvider,   $provide) {

            // lazy controller, directive and service
            app.controller = $controllerProvider.register;
            app.directive  = $compileProvider.directive;
            app.filter     = $filterProvider.register;
            app.factory    = $provide.factory;
            app.service    = $provide.service;
            app.constant   = $provide.constant;
            app.value      = $provide.value;
        }
    ])
    .constant('MAIN_CONFIG',[
        {
            name:'app',
            module:false,
            files:['Common/scripts/appCtrl.js']
        }])
    .config(['$ocLazyLoadProvider', 'MAIN_CONFIG', function($ocLazyLoadProvider, MAIN_CONFIG) {
        $ocLazyLoadProvider.config({
            debug: false,
            events: false,
            modules: MAIN_CONFIG
        });
    }]);
