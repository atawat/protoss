/**
 * Created by Yunjoy on 2015/10/8.
 */
/**
 * Created by 10138 on 2015/10/6.
 */
//¶©µ¥ÁÐ±í
app.controller('orderList',['$http','$scope',function($http,$scope){
     $scope.condition={
         PageCount:10,
         Page:1

     };
     $http.get(SETTING.ApiUrl+"/Order/GetByCondition", {params:$scope.condition,
         'withCredentials': true
     }).success(function(data){
         $scope.orderList=data;
         console.log(data);
     })
}])