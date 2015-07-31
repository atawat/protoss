/**
 * Created by Yunjoy on 2015/7/9.
 */
//注册
app.controller('signInController',['$http','$scope','$state','AuthService',function($http,$scope,$state,AuthService){
    $scope.signer ={
        userName:'',
        FPassword:'',
        SPassword:''
    }
    $scope.sign = function(){
        $http.post(SETTING.APIURL+'/user/SignUp',$scope.signer,{'withCredentials':true}).success(function(data){
            if(data.Status==false){
                $scope.errorTip=data.Msg;
            }
            else{
                AuthService.doLogin($scope.signer.userName,$scope.FPassword,function(){
                    $state.go('user.index');
                })
            }
        })
    }
}])

//两次密码输入验证
function check()
{
    var pass1 = document.getElementById("FPassword");
    var pass2 = document.getElementById("SPassword");
    var tips= document.getElementById("errorTip");
    if(pass1.value!=pass2.value)
    {
        tips.innerHTML="两次密码输入不一致，请重新输入！";
    }else{
        tips.innerHTML="";
    }
    if(pass1.value==""||pass2.value=="")
    {
        tips.innerHTML="请输入密码";
    }
}