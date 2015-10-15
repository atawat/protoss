/**
 * Created by Yunjoy on 2015/10/14.
 */
app.controller('userCenter',['$scope','$state','AuthService',function($scope,$state,AuthService){

    $scope.currentuser= AuthService.CurrentUser(); //����service��������ȡ��ǰ��½��Ϣ
    if( $scope.currentuser==undefined ||  $scope.currentuser=="")
    {
        $state.go("page.login");//������¼ҳ��
    }


    function clearCookie(){
        var keys=document.cookie.match(/[^ =;]+(?=\=)/g);
        if (keys) {
            for (var i = keys.length; i--;)
                document.cookie=keys[i]+'=0;expires=' + new Date( 0).toUTCString()
        }
    }
    clearCookie();
}]);