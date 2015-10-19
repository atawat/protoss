/**
 * Created by Yunjoy on 2015/10/8.
 */
/**
 * Created by 10138 on 2015/10/6.
 */
//订单列表
app.controller('orderList',['$http','$scope','AuthService','$state','$ionicHistory',function($http,$scope,AuthService,$state,$ionicHistory){
    //清除页面堆栈
    $ionicHistory.clearHistory();
    $scope.currentuser= AuthService.CurrentUser(); //调用service服务来获取当前登陆信息
    if( $scope.currentuser==undefined ||  $scope.currentuser=="")
    {
        $state.go("page.login");//调到登录页面
    }
     $scope.condition = {
         PageCount:10,
         Page:1,
         IsDescending:true
     };

     $http.get(SETTING.ApiUrl+"/Order/GetByCondition", {params:$scope.condition,
         'withCredentials': true
     }).success(function(data){
         $scope.orderList=data.List;
         console.log($scope.orderList);
     })
     //$http.get(SETTING.ApiUrl + '/OrderDetail/Get', {
     //    params: $scope.condition,
     //    'withCredentials': true
     //}).success(function(data){
     //    $scope.orderDetail=data.List;
     //    console.log( $scope.orderDetail);
     //})
}])