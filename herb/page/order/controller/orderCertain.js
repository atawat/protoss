/**
 * Created by Yunjoy on 2015/10/8.
 */
app.controller('orderCertain',['$http','$scope','$stateParams','$ionicLoading','$timeout','$state','$ionicPopup','AuthService','$ionicHistory',function($http,$scope,$stateParams,$ionicLoading,$timeout,$state,$ionicPopup,AuthService,$ionicHistory){
    //清除页面堆栈
    $ionicHistory.clearHistory();
    $scope.currentuser= AuthService.CurrentUser(); //调用service服务来获取当前登陆信息
    if( $scope.currentuser==undefined ||  $scope.currentuser=="")
    {
        $state.go("page.login");//调到登录页面
    }
//获取购物车信息
    var storage=window.localStorage.ShoppingCart;
    var jsonstr = JSON.parse(storage.substr(1, storage.length));
    $scope.productlist = jsonstr.productlist;
    $scope.Details=[];
    $scope.TotalPrice=$stateParams.TotalPrice;
    //遍历购物车
    for(i=0;i<$scope.productlist.length;i++){
        $scope.DetailModel={
            ProductId:'',
            Count:'',
            ProductName:'',
            Remark:''
        };
        $scope.DetailModel.ProductId=$scope.productlist[i].id;
        $scope.DetailModel.Count=$scope.productlist[i].count
        //$scope.Details.join($scope.DetailModel)
        $scope.Details.push($scope.DetailModel);
    }
    //留言板
    $scope.showPopup = function() {
        $scope.data = {}

        // 调用$ionicPopup弹出定制弹出框
        $ionicPopup.show({
            template: "<input type='text' ng-model=''>",
            title: "留言板",
            scope: $scope,
            buttons: [
                { text: "取消" },
                {
                    text: "<b>保存</b>",
                    type: "button-positive",
                    onTap: function(e) {
                        return $scope.data.wifi;
                    }
                }
            ]
        })
            .then(function(res) {
                $scope.status = ["Wi-Fi密码到手了",":",res].join(" ");
            });
    };


    //地址
    $scope.town=[
        {
            id:'1',
            name:"盘龙区"
        },
        {
            id:'2',
            name:"五华区"
        },
        {
            id:'3',
            name:"西山区"
        },
        {
            id:'4',
            name:"官渡区"
        }
    ]

    $scope.select=function (choice) {
        $scope.Htown=choice;
        $scope.DeliveryAddress="云南省"+"昆明市"+$scope.Htown
    };


    //创建订单
    $scope.orderModel = {
        DeliveryAddress:'',
        Type:1,
        TotalPrice:'',
        PhoneNumber:'',
        CounponNum:'0',
        LocationY:'',
        LocationX:'',
        Discount:1,
        Details:$scope.Details
    };
    $scope.HouseAddress={
        HouseAddress:''
    };
    $scope.createOrder=function(){
        $scope.orderModel.DeliveryAddress = $scope.DeliveryAddress+$scope.HouseAddress.HouseAddress;
        if($scope.HouseAddress.HouseAddress==""||$scope.HouseAddress.HouseAddress==undefined){
            $ionicLoading.show({
                template: "请填写收货地址"
            });
            $timeout(function(){
                $ionicLoading.hide();
                return;
            },2000);
        }
        else{
            $http.post(SETTING.ApiUrl + '/Order/CreateOrder',$scope.orderModel ,{
                'withCredentials': true
            }).success(function(data){
                $scope.order = data;
                $ionicLoading.show({
                    template: "订单提交成功，请付款..."
                });
                $timeout(function(){
                    $ionicLoading.hide();
                    $state.go("page.orderList")
                },2000);
            })
        }

    }
}])