/**
 * Created by ATA-GAME on 2015/5/24.
 */
app.run(
    ['$rootScope', '$state', '$stateParams', 'AuthService',
        function ($rootScope, $state, $stateParams, AuthService) {
            $rootScope.$state = $state;
            $rootScope.$stateParams = $stateParams;
            $rootScope.$on('$stateChangeStart', function (event,next) {
                if(next.name ==='detail'){
                    event.preventDefault();
                    return;
                }
                //if(next.name==='access.signin' || next.name==='access.signup' || next.name==='access.forgot-password'){
                //    return;
                //}
                //if(!AuthService.IsAuthenticated()){
                //    event.preventDefault();
                //    $state.go('access.signin');
                //}
                //if(next.access !== undefined){
                //    if(!AuthService.IsAuthorized(next.access)){
                //        event.preventDefault();
                //        //TODO:跳转到权限提示页
                //    }
                //
                //}
            });
        }
    ]
).config(['$stateProvider', '$urlRouterProvider',function($stateProvider,$urlRouterProvider){
        $urlRouterProvider
            .otherwise('/home/index');
        $stateProvider
            .state('app',{
                url:'/app',
                abstract:true,
                views:{
                    '':{
                        templateUrl:'view/layout.html'
                    },
                    header:{
                        templateUrl:'view/header.html'
                    },
                    footer:{
                        templateUrl:'view/footer.html'
                    }
                }
            })
            .state('home',{
                abstract:true,
                url:'/home',
                views:{
                    '':{
                        templateUrl:'view/layout.html'
                    },
                    header:{
                        templateUrl:'module/home/header.html'
                    },
                    footer:{
                        templateUrl:'view/footer.html'
                    }
                }
            })
            .state('user',{
                abstract:true,
                url:'/user',
                views:{
                    '':{
                        templateUrl:'view/layout.html'
                    },
                    header:{
                        templateUrl:'view/header.html'
                    }
                }
            })
            .state('order',{
                abstract:true,
                url:'/order',
                views:{
                    '':{
                        templateUrl:'view/layout.html'
                    },
                    header:{
                        templateUrl:'view/header.html'
                    }
                }
            })
            .state('home.index',{
                url:'/index',
                templateUrl:'module/home/index.html'
            })
            .state('app.about',{
                url:'/about',
                templateUrl:'module/about/about.html'
            })
            .state('app.detail',{
                url:'/detail?id',
                templateUrl:'module/product/detail.html'
            })
            .state('user.login',{
                url:'/login',
                templateUrl:'module/user/login.html'
            })
            .state('user.signin',{
                url:'/signin',
                templateUrl:'module/user/signin.html'
            })
            .state('user.index',{
                url:'/index',
                templateUrl:'module/user/index.html'
            })
            .state('order.cart',{
                url:'/cart',
                templateUrl:'module/order/cart.html'
            })
            .state('order.preOrder',{
                url:'/preOrder',
                templateUrl:'module/order/preOrder.html'
            })
    }]);