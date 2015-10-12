/**
 * Created by Yunjoy on 2015/10/8.
 */
/**
 * Created by 10138 on 2015/10/6.
 */
//订单列表
app.controller('orderList',['$http','$scope',function($http,$scope){
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