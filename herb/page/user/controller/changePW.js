/**
 * Created by 10138 on 2015/10/6.
 */
//修改密码
app.controller('changePW',['$http','$scope','$state','$ionicLoading','$timeout','AuthService','$ionicHistory',function($http,$scope,$state,$ionicLoading,$timeout,AuthService,$ionicHistory){
    //清除页面堆栈
    $ionicHistory.clearHistory();
    $scope.currentuser= AuthService.CurrentUser(); //调用service服务来获取当前登陆信息
    if( $scope.currentuser==undefined ||  $scope.currentuser=="")
    {
        $state.go("page.login");//调到登录页面
    }
    $scope.pwd ={
        oldPassword:'',
        newPassword:'',
        secondPassword:''

    }
    $scope.change = function() {
        $http.post(SETTING.ApiUrl + '/User/ChangePassword', $scope.pwd, {'withCredentials': true}).success(function (data) {
            if (data.Status == false) {
                $ionicLoading.show({
                    template: data.Msg,
                    noBackdrop: false
                });
                $timeout(function () {
                    $ionicLoading.hide();
                }, 3000);
            }
            else {

                $ionicLoading.show({
                    template: "修改密码成功，请重新登陆"
                });
                $timeout(function () {
                    $state.go("page.login");
                    $ionicLoading.hide();
                }, 1000);

            }

        })
    }}])