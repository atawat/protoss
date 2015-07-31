/**
 * Created by gaofengming on 2015/7/8.
 */
app.controller('listController',['$scope','$http',function(){
    $scope.seachCondition={
        categoryId:'',
        priceBegin:'',
        page:'1',
        pageCount:'10'
    }
    $http.get(SETTING.APIURL+'/Product/List',{params: $scope.seachCondition,'withCredentials':true}).success(function(data){

        if(data!=""&&data!=null){
            $scope.list=data;
        }
        else{
            $scope.tips="¾´ÇëÆÚ´ý"
        }
    })
}])