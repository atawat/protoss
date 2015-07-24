/**
 * Created by Yunjoy on 2015/6/20.
 */
app.service("cartservice",['$rootScope','storage','$http',function($rootScope,$sessionStorage){
    $rootScope.cartNum = 0;
    var successData;


    this.setSuccessData = function(){

        successData =data;
    };
    this.getSuccessData = function(){
        return successData;
    };
}]);

