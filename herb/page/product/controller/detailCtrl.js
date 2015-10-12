/**
 * Created by Yunjoy on 2015/9/23.
 */


app.controller('detailCtrl',['$scope','$http','cartservice','$stateParams','$ionicSlideBoxDelegate','$state','$ionicLoading','$timeout',function($scope,$http,cartservice,$stateParams,$ionicSlideBoxDelegate,$state,$ionicLoading,$timeout) {

    $scope.Img=SETTING.ImgUrl;
    //region 轮播图
    $scope.$on('$ionicView.enter', function () {
        $ionicSlideBoxDelegate.start();
    });
    //endregion
    //获取图片详情
    var load_detail = function () {
            $http.get(SETTING.ApiUrl + "/ProductDetail/productDetail?id=" + $stateParams.id, {
                'withCredentials': true
            }).success(function (data) {
                $scope.productDetail = data;
            });
    };
    load_detail();
    //获取商品价格等详情
    $scope.items = [];
    var getList = function () {
        $http.get(SETTING.ApiUrl + "/Product/Get?id=" + $stateParams.id,  {

            'withCredentials': true
        }).success(function (data) {
            if (data.List != "") {
                $scope.items = data.List;
            }
            console.log(data.List);
        });
    };
    getList();


    //region 加入购物车
    $scope.cartinfo = {
        id: null,
        name: null,
        count: null,
        price: null,
        image:null,
    };
    $scope.AddCart = function (data) {
        $scope.cartinfo.id = $scope.items.Id;
        $scope.cartinfo.name = $scope.items.Name;
        $scope.cartinfo.image = $scope.items.Image;
        $scope.cartinfo.price = $scope.items.Price;
        $scope.cartinfo.count = 1;
        cartservice.add($scope.cartinfo);

        $ionicLoading.show({
            template: "加入购物车成功..."
        });
        $timeout(function(){
            $ionicLoading.hide();
        },2000);
    }
    //endregion
    //region  立即购买
    $scope.buy=function(){
        $scope.cartinfo.id = $scope.items.Id;
        $scope.cartinfo.name = $scope.items.Name;
        $scope.cartinfo.image = $scope.items.Image;
        $scope.cartinfo.price = $scope.items.Price;
        $scope.cartinfo.count = 1;
        cartservice.add($scope.cartinfo);
        $state.go("page.cart");
    }
    //endregion
   }])