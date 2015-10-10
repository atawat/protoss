/**
 * Created by Yunjoy on 2015/10/8.
 */
app.controller('orderCertain',['$http','$scope','$stateParams',function($http,$scope,$stateParams){
//获取购物车信息
    var storage=window.localStorage.ShoppingCart;
    var jsonstr = JSON.parse(storage.substr(1, storage.length));
    $scope.productlist = jsonstr.productlist;
    $scope.Details=[];
    $scope.DetailModel={
        ProductId:'',
        Count:'',
        ProductName:'阿嘎',
        Remark:'飞洒地方'
    }
    //遍历购物车
    for(i=0;i<$scope.productlist.length;i++){
        $scope.DetailModel.ProductId=$scope.productlist[i].id;
        $scope.DetailModel.Count=$scope.productlist[i].count
        //$scope.Details.join($scope.DetailModel)
        $scope.Details.push($scope.DetailModel);

    }

    $scope.orderModel = {
        DeliveryAddress:'aSas',
        Type:1,
        TotalPrice:56,
        PhoneNumber:'18388026186',
        CounponNum:'56',
        LocationY:'34',
        LocationX:'35',
        Discount:0.8,
        //Details:[
        //{
        //    ProductId:10,
        //    Count:'2',
        //    ProductName:'阿嘎',
        //    Remark:'飞洒地方'
        //}
        //]
        Details:$scope.Details
    };



   // console.log($scope.productlist);
    console.log($scope.Details);





    //$scope.town=[
    //    {
    //        id:'1',
    //        name:"盘龙区"
    //    },
    // {
    //     id:'2',
    //     name:"五华区"
    // },
    //    {
    //        id:'3',
    //        name:"西山区"
    //    },
    //    {
    //        id:'4',
    //        name:"官渡区"
    //    }
    //]
    //
    //$scope.select=function () {
    //    $scope.choice='五华区';
    //    alert($scope.choice)
    //}
    $http.post(SETTING.ApiUrl + '/Order/CreateOrder',$scope.orderModel ,{

        'withCredentials': true
    }).success(function(data){
            console.log("sucees");
    })


}])