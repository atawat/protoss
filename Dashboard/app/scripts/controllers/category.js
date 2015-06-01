/**
 * Created by Yunjoy on 2015/6/1.
 */
app.controller('categoryIndexController',['$http','$state','$scope','$modal',function($http,$state,$scope,$modal){

    var getTagList = function() {
        $http.get(SETTING.ApiUrl+'/category/get',{
            'withCredentials':true
        }).success(function(data){
            $scope.list = data;
        });

    };
    $scope.getList = getTagList;
    getTagList();

    $scope.del = function (id) {
        $scope.selectedId = id;
        var modalInstance = $modal.open({
            templateUrl: 'myModalContent.html',
            controller:'ModalInstanceCtrl',
            resolve: {
                msg:function(){return "你确定要删除吗？";}
            }
        });
        modalInstance.result.then(function(){
            $http.get(SETTING.ApiUrl + '/category/Delete',{
                    params:{
                        id:$scope.selectedId
                    },
                    'withCredentials':true
                }
            ).success(function(data) {
                    if (data) {
                        getTagList();
                    }
                });
        })
    };

    $scope.gotoNew = function(){
        $state.go("app.category.create")
    }
}])
app.controller('categoryCreateController',['$http','$state','$scope',function($http,$state,$scope){
    $scope.Model = {
        Id:'', //int
        Name:'' //string
        //Parent:''
    };
    $scope.alerts =[];
    $scope.Create = function(){
        $http.post(SETTING.ApiUrl + '/category/Post',$scope.Model,{
            'withCredentials':true
        }).success(function(data){
            $scope.alerts =[];
            if(data){
                $state.go("app.category.index");
            }
            else{
                //$scope.Message=data.Msg;
                $scope.alerts=[{type:'danger',msg:'新建出错'}];
            }
        }).error(function(data){
            $scope.alerts=[{type:'danger',msg:'出错啦请检查网络或再次尝试'}];
        });
    }
}])
app.controller('categoryEditController',['$http','$state','$scope','$stateParams',function($http,$state,$scope,$stateParams){
    $http.get(SETTING.ApiUrl + '/category/get/' + $stateParams.id,{
        'withCredentials':true
    }).success(function(data){
        $scope.Model =data;
    });
    $scope.alerts =[];
    $scope.Save = function(){
        $http.post(SETTING.ApiUrl + '/category/put',$scope.Model,{
            'withCredentials':true
        }).success(function(data){
            $scope.alerts =[];
            if(data){
                $state.go("app.category.index");
            }
            else{
                $scope.alerts=[{type:'danger',msg:"保存出错"}];
            }
        }).error(function(data){
            $scope.alerts=[{type:'danger',msg:'出错啦请检查网络或再次尝试'}];
        });
    }
}])
app.controller('categoryDetailController',['$http','$state','$scope',function($http,$state,$scope){
    $http.get(SETTING.ApiUrl + '/category/get/' + $stateParams.id,{
        'withCredentials':true
    }).success(function(data){
        $scope.Model =data;
    });
}])