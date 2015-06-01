'use strict';

/**
 * @ngdoc function
 * @name app.config:uiRouter
 * @description
 * # Config
 * Config for the router
 */
angular.module('app')
  .run(
    [           '$rootScope', '$state', '$stateParams','AuthService',
      function ( $rootScope,   $state,   $stateParams ,AuthService) {
          $rootScope.$state = $state;
          $rootScope.$stateParams = $stateParams;

          $rootScope.$on('$stateChangeStart', function (event,next) {
              if(next.name==='access.signin' || next.name==='access.signup' || next.name==='access.forgot-password'){
                  return;
              }
              if(!AuthService.IsAuthenticated()){
                  event.preventDefault();
                  $state.go('access.signin');
              }
              if(next.access !== undefined){
                  if(!AuthService.IsAuthorized(next.access)){
                    event.preventDefault();
                    //TODO:跳转到权限提示页
                  }

              }
          });
      }
    ]
  )
  .config(
    ['$stateProvider', '$urlRouterProvider', 'MODULE_CONFIG',
      function ($stateProvider, $urlRouterProvider, MODULE_CONFIG) {
        $urlRouterProvider
          .otherwise('/app/dashboard');
        $stateProvider
          .state('app', {
            abstract: true,
            url: '/app',
            views: {
              '': {
                templateUrl: 'views/layout.html'
              },
              'aside': {
                templateUrl: 'views/aside.html'
              },
                'content': {
                    templateUrl: 'views/content.html'
                }
            }
          })
            .state('app.dashboard',{
                url:'/dashboard',
                templateUrl:'views/pages/dashboard.html',
                resolve: load(['scripts/controllers/chart.js','scripts/controllers/vectormap.js'])
            })
            .state('app.product',{
                url:'/product',
                abstract: true,
                template:'<div ui-view></div>',
                resolve:load('scripts/controllers/product.js')
            })
              .state('app.product.index',{
                url:'/index',
                templateUrl:'views/pages/product/index.html'
            })
              .state('app.product.detail',{
                url:'/detail?id',
                templateUrl:'views/pages/product/detail.html'
            })
            .state('app.product.edit',{
                url:'/edit?id',
                templateUrl:'views/pages/product/edit.html'
            })
              .state('app.product.create',{
                url:'/create',
                templateUrl:'views/pages/product/create.html'
            })

            .state('app.order',{
                url:'/order',
                abstract: true,
                template:'<div ui-view></div>',
                resolve:load('scripts/controllers/order.js')
            })
            .state('app.order.index',{
                url:'/index',
                templateUrl:'views/pages/order/index.html'
            })
            .state('app.order.detail',{
                url:'/detail?id',
                templateUrl:'views/pages/order/detail.html'
            })
            .state('app.order.edit',{
                url:'/edit?id',
                templateUrl:'views/pages/order/edit.html'
            })
            .state('app.order.create',{
                url:'/create',
                templateUrl:'views/pages/order/create.html'
            })

            .state('app.content',{
                url:'/content',
                abstract: true,
                template:'<div ui-view></div>',
                resolve:load('scripts/controllers/content.js')
            })
            .state('app.content.index',{
                url:'/index',
                templateUrl:'views/pages/content/index.html'
            })
            .state('app.content.detail',{
                url:'/detail?id',
                templateUrl:'views/pages/content/detail.html'
            })
            .state('app.content.edit',{
                url:'/edit?id',
                templateUrl:'views/pages/content/edit.html'
            })
            .state('app.content.create',{
                url:'/create',
                templateUrl:'views/pages/content/create.html'
            })

            .state('app.category',{
                url:'/category',
                abstract: true,
                template:'<div ui-view></div>',
                resolve:load('scripts/controllers/category.js')
            })
            .state('app.category.index',{
                url:'/index',
                templateUrl:'views/pages/category/index.html'
            })
            .state('app.category.detail',{
                url:'/detail?id',
                templateUrl:'views/pages/category/detail.html'
            })
            .state('app.category.edit',{
                url:'/edit?id',
                templateUrl:'views/pages/category/edit.html'
            })
            .state('app.category.create',{
                url:'/create',
                templateUrl:'views/pages/category/create.html'
            })

            .state('app.property',{
                url:'/property',
                abstract: true,
                template:'<div ui-view></div>',
                resolve:load('scripts/controllers/property.js')
            })
            .state('app.property.index',{
                url:'/index',
                templateUrl:'views/pages/property/index.html'
            })
            .state('app.property.detail',{
                url:'/detail?id',
                templateUrl:'views/pages/property/detail.html'
            })
            .state('app.property.edit',{
                url:'/edit?id',
                templateUrl:'views/pages/property/edit.html'
            })
            .state('app.property.create',{
                url:'/create',
                templateUrl:'views/pages/property/create.html'
            })

            .state('app.channel',{
                url:'/channel',
                abstract: true,
                template:'<div ui-view></div>',
                resolve:load('scripts/controllers/channel.js')
            })
            .state('app.channel.index',{
                url:'/index',
                templateUrl:'views/pages/channel/index.html'
            })
            .state('app.channel.detail',{
                url:'/detail?id',
                templateUrl:'views/pages/channel/detail.html'
            })
            .state('app.channel.edit',{
                url:'/edit?id',
                templateUrl:'views/pages/channel/edit.html'
            })
            .state('app.channel.create',{
                url:'/create',
                templateUrl:'views/pages/channel/create.html'
            })

            .state('app.coupon',{
                url:'/coupon',
                abstract: true,
                template:'<div ui-view></div>',
                resolve:load('scripts/controllers/coupon.js')
            })
            .state('app.coupon.index',{
                url:'/index',
                templateUrl:'views/pages/coupon/index.html'
            })
            .state('app.coupon.detail',{
                url:'/detail?id',
                templateUrl:'views/pages/coupon/detail.html'
            })
            .state('app.coupon.edit',{
                url:'/edit?id',
                templateUrl:'views/pages/coupon/edit.html'
            })
            .state('app.coupon.create',{
                url:'/create',
                templateUrl:'views/pages/coupon/create.html'
            })

            .state('access', {
                url: '/access',
                template: '<div class="indigo bg-big"><div ui-view class="fade-in-down smooth"></div></div>'
            })
            .state('access.signin', {
                url: '/signin',
                templateUrl: 'views/pages/signin.html',
                controller: 'LoginControl',
                resolve: load('scripts/controllers/UC/Login.js')
            })
            .state('access.signup', {
                url: '/signup',
                templateUrl: 'views/pages/signup.html'
            })
            .state('access.forgot-password', {
                url: '/forgot-password',
                templateUrl: 'views/pages/forgot-password.html'
            })
            .state('access.lockme', {
                url: '/lockme',
                templateUrl: 'views/pages/lockme.html',
                controller: 'LogoutControl',
                resolve: load('scripts/controllers/UC/Logout.js')
            })

            //-----------------------end-------------------

          function load(srcs, callback) {
            return {
                deps: ['$ocLazyLoad', '$q',
                    function ($ocLazyLoad, $q) {
                    var deferred = $q.defer();
                        var promise = false;
                    srcs = angular.isArray(srcs) ? srcs : srcs.split(/\s+/);
                        if (!promise) {
                      promise = deferred.promise;
                    }
                        angular.forEach(srcs, function (src) {
                            promise = promise.then(function () {
                                angular.forEach(MODULE_CONFIG, function (module) {
                                    if (module.name == src) {
                                        if (!module.module) {
                              name = module.files;
                                        } else {
                              name = module.name;
                            }
                                    } else {
                            name = src;
                          }
                        });
                        return $ocLazyLoad.load(name);
                            });
                    });
                    deferred.resolve();
                        return callback ? promise.then(function () { return callback(); }) : promise;
                }]
            }
          }
      }
    ]
  )
