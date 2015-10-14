/**
 * Created by 10138 on 2015/10/6.
 */
//修改密码
app.controller('changePW',['$http','$scope','$state','$ionicLoading','$timeout','AuthService',function($http,$scope,$state,$ionicLoading,$timeout,AuthService){
    $scope.currentuser= AuthService.CurrentUser(); //调用service服务来获取当前登陆信息
    if( $scope.currentuser==undefined ||  $scope.currentuser=="")
    {
        $state.go("page.login");//调到登录页面
    }
    $scope.pw ={
        oldPassword:'',
        newPassword:'',
        secondPassword:''

    }
    $scope.change = function(){
        $http.post(SETTING.ApiUrl+'/User/ChangePassword',$scope.pw,{'withCredentials':true}).success(function(data){
            if(data.Status==false){
                $ionicLoading.show({
                    template:data.Msg,
                    noBackdrop:true
                });
                $timeout(function(){
                    $ionicLoading.hide();
                },3000);
            }
            else{
                AuthService.doLogin($scope.signer.UserName,$scope.signer.Password,function(){
                    $ionicLoading.show({
                        template:"修改密码成功，请重新登陆"
                    });
                    $timeout(function(){
                        $state.go("page.login");
                        $ionicLoading.hide();
                    },1000);
                })
            }
        })
    }
}])