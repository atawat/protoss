// Ionic Starter App

// angular.module is a global place for creating, registering and retrieving Angular modules
// 'starter' is the name of this angular module example (also set in a <body> attribute in index.html)
// the 2nd parameter is an array of 'requires'
// 'starter.services' is found in services.js
// 'starter.controllers' is found in controllers.js
var app = angular.module('starter', ['ionic', 'ngCordova', 'ngStorage']);
var SETTING = {
    BaseUrl: 'http://www.iyookee.cn/',
    ApiUrl: 'http://localhost:22572/api',
    ImgUrl: 'http://img.iyookee.cn/',
    eventApiUrl: 'http://www.iyookee.cn/API'
};
app.run(function ($ionicPlatform, $ionicHistory, $ionicLoading) {
    $ionicPlatform.ready(function () {
        // Hide the accessory bar by default (remove this to show the accessory bar above the keyboard
        // for form inputs)
        if (window.cordova && window.cordova.plugins && window.cordova.plugins.Keyboard) {
            cordova.plugins.Keyboard.hideKeyboardAccessoryBar(true);
            cordova.plugins.Keyboard.disableScroll(true);

        }
        if (window.StatusBar) {
            // org.apache.cordova.statusbar required
            StatusBar.styleLightContent();
            //StatusBar.hide();
        }

    });
    $ionicPlatform.registerBackButtonAction(function (event) {
        event.preventDefault();

        if ($ionicHistory.currentStateName() === 'page.home') {
            window.close();
            ionic.Platform.exitApp();
        } else {
            $ionicHistory.goBack();
        }
        return false;
    }, 101);

});

app.config(function ($stateProvider, $urlRouterProvider) {

    // Ionic uses AngularUI Router which uses the concept of states
    // Learn more here: https://github.com/angular-ui/ui-router
    // Set up the various states which the app can be in.
    // Each state's controller can be found in controllers.js
    $stateProvider

        // setup an abstract state for the tabs directive
        .state('page', {
            url: '/page',
            abstract: true,
            templateUrl: 'page/tabs.html'
        })

        // Each tab has its own nav history stack:

        .state('page.home', {
            url: '/home',
            views: {
                'page-home': {
                    templateUrl: 'page/home/home.html',
                    controller:'home'
                }
            }
        })
        .state('page.product-detail', {
            url: '/product/product-detail?id',
            views: {
                'page-home': {
                    templateUrl: 'page/product/product-detail.html',
                    controller:'detailCtrl'
                }
            }
        })
        .state('page.login', {
            url: '/user/login',
            views: {
                'page-user': {
                    templateUrl: 'page/user/login.html',
                    controller:'login'
                }
            }
        })
        .state('page.register', {
            url: '/user/register',
            views: {
                'page-user': {
                    templateUrl: 'page/user/register.html',
                    controller:'register'
                }
            }
        })
        .state('page.changePW', {
            url: '/user/changePW',
            views: {
                'page-user': {
                    templateUrl: 'page/user/changePW.html'
                }
            }
        })
        .state('page.coupon', {
            url: '/user/coupon',
            views: {
                'page-user': {
                    templateUrl: 'page/user/coupon.html'
                }
            }
        })
        .state('page.about', {
            url: '/user/about',
            views: {
                'page-user': {
                    templateUrl: 'page/user/about.html'
                }
            }
        })
        .state('page.userCenter', {
            url: '/user/userCenter',
            views: {
                'page-user': {
                    templateUrl: 'page/user/userCenter.html'
                }
            }
        })
        .state('page.cart', {
            url: '/cart',
            views: {
                'page-cart': {
                    templateUrl: 'page/order/cart.html',
                    controller:'cartCtrl'

                }
            }
        })
        .state('page.orderList', {
            url: '/order/orderList',
            views: {
                'page-order': {
                    templateUrl: 'page/order/orderList.html',
                    controller:'orderList'
                }
            }
        })
        .state('page.orderCertain', {
            url: '/order/orderCertain',
            views: {
                'page-order': {
                    templateUrl: 'page/order/orderCertain.html'
                }
            }
        });
    // if none of the above states are matched, use this as the fallback
    $urlRouterProvider.otherwise('/page/home');

});


