/**
 * Created by Yunjoy on 2015/9/23.
 */


app.controller('home',['$scope','$http','cartservice',function($scope,$http,cartservice){
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
        oldprice: null,
        parameterValue:[]
    };
    $scope.AddCart = function (data) {
        $scope.cartinfo.id = data.row.Id;
        $scope.cartinfo.name = data.row.Name;
        $scope.cartinfo.mainimg = data.row.MainImg;
        $scope.cartinfo.price = data.row.Price;
        $scope.cartinfo.oldprice = data.row.OldPrice;
        $scope.cartinfo.count = 1;
        cartservice.add($scope.cartinfo);
    }

}])

