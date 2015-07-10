/**
 * Created by Yunjoy on 2015/7/9.
 */
app.controller('loginController',['$scope','AuthService','$state',function($scope,AuthService,$state){
    $scope.user={
        userName:'',
        password:''
    }
    $scope.login = function(){
        AuthService.doLogin($scope.user.userName,$scope.user.password,function(data){
            console.log(data);
            $state.go('');
        },function(data){
            $scope.errorTip=data.Msg;
        })
    }
}])