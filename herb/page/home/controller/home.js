/**
 * Created by Yunjoy on 2015/9/23.
 */


app.controller('home',['$scope','$http','cartservice','$ionicLoading','$timeout',function($scope,$http,cartservice,$ionicLoading,$timeout){
    //获取商品
    $scope.items = [];
    $scope.searchCondition = {
        Page: 1,
        PageCount: 10
    };
    var getList = function () {
        $http.get(SETTING.ApiUrl + '/Product/GetByCondition', {
            params: $scope.searchCondition,
            'withCredentials': true
        }).success(function (data) {
            if (data.List != "") {
                $scope.items = data.List;
            }
            console.log(data.List);
        });
    };
    getList();

//加入购物车
    $scope.cartinfo = {
        id: null,
        name: null,
        count: null,
        price: null,
        image:null,
    };
    $scope.AddCart = function (data) {
        $scope.cartinfo.id = data.row.Id;
        $scope.cartinfo.name = data.row.Name;
        $scope.cartinfo.image = data.row.Image;
        $scope.cartinfo.price = data.row.Price;
        $scope.cartinfo.count = 1;
        cartservice.add($scope.cartinfo);

        $ionicLoading.show({
            template: "加入购物车成功..."
        });
        $timeout(function(){
            $ionicLoading.hide();
        },2000);
    }

}])

