/**
 * Created by Yunjoy on 2015/6/20.
 */
app.service("cartservice",['$rootScope',function($rootScope){
    $rootScope.cartNum = 0;
    var successData;
    this.setSuccessData = function(data){
        successData =data;
    };
    this.getSuccessData = function(){
        return successData;
    };
}]);