/**
 * Created by ATA-GAME on 2015/7/8.
 */
app.controller('homeIndexController',['$http','$scope',function($http,$scope){
    $scope.seachCondition={
        id:'',
        name:'',
        price:'',
        img:''
    }
    $http.get(SETTING.APIURL+'',{params:$scope.seachCondition,withCredential:true}).success(function(data){
        if(data.object!==''){
            $scope.seachCondition=data;
        }
        else{
            $scope.errorTip="���������ڵ��ԣ������ڴ���";
        }
    })
}])