/**
 * Created by Yunjoy on 2015/9/23.
 */


app.controller('detailCtrl',['$scope','$http','cartservice','$stateParams','$ionicSlideBoxDelegate',function($scope,$http,cartservice,$stateParams,$ionicSlideBoxDelegate) {

    $scope.Img=SETTING.ImgUrl;
    //region 轮播图
    $scope.$on('$ionicView.enter', function () {
        $ionicSlideBoxDelegate.start();
    });
    var load_detail = function () {
            $http.get(SETTING.ApiUrl + "/ProductDetail/productDetail?id=" + $stateParams.id, {
                'withCredentials': true
            }).success(function (data) {
                $scope.productDetail = data;
            });
    };
    load_detail();
    //endregion
    //region 加入购物车
    $scope.cartinfo = {
        id: null,
        name: null,
        count: null,
        mainimg:null,
        price:null,
        oldprice:null,
        //parameterValue:[]
    };
    $scope.changIng=false;
    $scope.AddCart=function(){
        $scope.changIng=true;
        $scope.cartinfo.id = $scope.product.Id;
        $scope.cartinfo.name = $scope.product.Name;
        $scope.cartinfo.mainimg=$scope.product.MainImg;
        $scope.cartinfo.price=$scope.product.Price;
        $scope.cartinfo.oldprice=$scope.product.OldPrice;
        //$scope.cartinfo.parameterValue=$scope.product.ParameterValue;
        $scope.cartinfo.count = 1;
        cartservice.add($scope.cartinfo);

    }
    //endregion
    //region  立即购买
    $scope.buy=function(){

        $state.go("page.order",{productId: $scope.product.Id,count:$scope.numbers})
    }
   }])