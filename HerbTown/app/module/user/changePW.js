/**
 * Created by gaofengming on 2015/7/31.
 */
app.controller('changePWController',['$scope','$http','$state',function($scope,$http,$state){
    $scope.password={
        oldPassword:'',
        newPassword:''
    }
    $scope.save = function(){
        $http.post(SETTING.APIURL+'/user/ChangePassword',$scope.password,{'withCredentials':true}).success(function(data){
            if(data.Status==false){
                $scope.changeError=data.Msg;
            }
            else{
                $state.go('user.index')
            }
        })
    }
}])

//验证两次密码输入是否一致
function check()
{
    var pass1 = document.getElementById("NewPassword1");
    var pass2 = document.getElementById("NewPassword2");
    var tips= document.getElementById("errorTip");
    if(pass1.value!=pass2.value)
    {
        tips.innerHTML="两次密码输入不一致，请重新输入！";
    }else{
        tips.innerHTML="";
    }
    if(pass1.value==""||pass2.value=="")
    {
        tips.innerHTML="";
    }
}
