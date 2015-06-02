/**
 * Created by ATA-GAME on 2015/5/31.
 */
app.controller('propertyIndexController',['$http','$state','$scope','$modal',function($http,$state,$scope,$modal){
    $scope.searchCondition = {
        Page:'1', //int
        PageCount:'10', //int
        //IsDescending:'', //bool
        //Ids:'', //int
        PropertyName:'' //string
        //Addusers:'', //UserBase
        //AddtimeBegin:'', //DateTime
        //AddtimeEnd:'', //DateTime
        //UpdUsers:'', //UserBase
        //UpdTimeBegin:'', //DateTime
        //UpdTimeEnd:'', //DateTime
        //OrderBy:'' //EnumPropertySearchOrderBy
    };

    var getPropertyList = function() {
        $http.get(SETTING.ApiUrl+'/property/GetByCondition',{
            params:$scope.searchCondition,
            'withCredentials':true
        }).success(function(data){
            $scope.list = data;
        });

        $http.get(SETTING.ApiUrl+'/property/GetCount',{
            params:$scope.searchCondition,
            'withCredentials':true
        }).success(function(data){
            $scope.searchCondition.page = data.Condition.Page;
            $scope.searchCondition.pageSize = data.Condition.PageCount;
            $scope.totalCount = data.TotalCount;
        });
    };
    $scope.getList = getPropertyList;
    getPropertyList();

    $scope.del = function (id) {
        $scope.selectedId = id;
        var modalInstance = $modal.open({
            templateUrl: 'myModalContent.html',
            controller:'ModalInstanceCtrl',
            resolve: {
                msg:function(){return "确定要删除吗";}
            }
        });
        modalInstance.result.then(function(){
            $http.get(SETTING.ApiUrl + '/property/Delete',{
                    params:{
                        id:$scope.selectedId
                    },
                    'withCredentials':true
                }
            ).success(function(data) {
                    if (data) {
                        getPropertyList();
                    }
                });
        })
    }

    $scope.gotoNew = function(){
        $state.go("app.property.create")
    }
}])
app.controller('propertyCreateController',['$http','$state','$scope',function($http,$state,$scope){
    $scope.Model = {
        Id:'', //int
        PropertyName:'', //string
        Category:{}
    };

    $scope.Create = function(){
        $http.post(SETTING.ApiUrl + '/property/Post',$scope.Model,{
            'withCredentials':true
        }).success(function(data){
            if(data){
                $state.go("app.property.index");
            }
            else{
                //$scope.Message=data.Msg;
                $scope.alerts=[{type:'danger',msg:'新建出错'}];
            }
        }).error(function(data){
            $scope.alerts=[{type:'danger',msg:'出错啦请检查网络或再次尝试'}];
        });
    }

    $http.get(SETTING.ApiUrl+'/Category/Get',{'withCredentials':true}).success(function(data){
        $scope.CategoryList = data;
    }).error(function(data){
        $scope.alerts=[{type:'danger',msg:'获取分类信息出错'}];
    });
}])
app.controller('propertyEditController',['$http','$state','$scope','$stateParams',function($http,$state,$scope,$stateParams){
    $http.get(SETTING.ApiUrl + '/property/get/' + $stateParams.id,{
        'withCredentials':true
    }).success(function(data){
        $scope.Model =data;
    }).error(function(data){
        $scope.alerts=[{type:'danger',msg:'出错啦请检查网络或再次尝试'}];
    });

    $http.get(SETTING.ApiUrl+'/Category/Get',{'withCredentials':true}).success(function(data){
        $scope.CategoryList = data;
    }).error(function(data){
        $scope.alerts=[{type:'danger',msg:'获取分类信息出错'}];
    });

    $scope.Save = function(){
        $http.post(SETTING.ApiUrl + '/property/put',$scope.Model,{
            'withCredentials':true
        }).success(function(data){
            if(data){
                $state.go("app.property.index");
            }
            else{
                $scope.alerts=[{type:'danger',msg:'修改出错'}];
            }
        }).error(function(data){
            $scope.alerts=[{type:'danger',msg:'出错啦请检查网络或再次尝试'}];
        });
    }
}])
app.controller('propertyDetailController',['$http','$state','$scope',function($http,$state,$scope){
    $http.get(SETTING.ApiUrl + '/property/get/' + $stateParams.id,{
        'withCredentials':true
    }).success(function(data){
        $scope.Model =data;
    });
}])