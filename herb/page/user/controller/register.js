/**
 * Created by Yunjoy on 2015/9/15.
 */

app.controller('register',['$http','$scope','$state','AuthService','$ionicLoading','$timeout','$ionicHistory',function($http,$scope,$state,AuthService,$ionicLoading,$timeout,$ionicHistory){
    //清除页面堆栈
    $ionicHistory.clearHistory();
    $scope.signer ={
        UserName:'',
        Password:'',
        SecondPassword:''
    }
    $scope.sign = function(){
        $http.post(SETTING.ApiUrl+'/User/SignUp',$scope.signer,{'withCredentials':true}).success(function(data){
            console.log(data);
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
                        template:"注册成功，登录ing..."
                    });
                    $timeout(function(){
                        $state.go("page.userCenter");
                        $ionicLoading.hide();
                    },1000);
                })
            }
        })
    }
}])
