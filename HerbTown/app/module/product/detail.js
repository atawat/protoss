/**
 * Created by gaofengming on 2015/7/8.
 */
app.controller('detailController',['$scope','$http','$stateParams',function($scope,$http,$stateParams){
    $http.get(SETTING.ApiUrl+'/ProductDetail/ProductDetailModel?Id='+$stateParams.Id,{'withCredentials':true}).success(function(data){
        $scope.detail=data;
    })

    //���빺�ﳵ�¼�
    $scope.addCart=function(){

    }
}])