/**
 * Created by ATA-GAME on 2015/5/31.
 */
app.controller('propertyIndexController',['$http','$state','$scope',function($http,$state,$scope){
    $scope.searchCondition = {
        Page:'', //int
        PageCount:'', //int
        IsDescending:'', //bool
        Ids:'', //int
        PropertyName:'', //string
        Addusers:'', //UserBase
        AddtimeBegin:'', //DateTime
        AddtimeEnd:'', //DateTime
        UpdUsers:'', //UserBase
        UpdTimeBegin:'', //DateTime
        UpdTimeEnd:'', //DateTime
        OrderBy:'' //EnumPropertySearchOrderBy
    };

    var getTagList = function() {
        $http.get(SETTING.ApiUrl+'/property/GetByCondition',{
            params:$scope.searchCondition,
            'withCredentials':true
        }).success(function(data){
            $scope.list = data.List;
            $scope.searchCondition.page = data.Condition.Page;
            $scope.searchCondition.pageSize = data.Condition.PageCount;
            $scope.totalCount = data.TotalCount;
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
                msg:function(){return "ÄãÈ·¶¨ÒªÉ¾³ýÂð£¿";}
            }
        });
        modalInstance.result.then(function(){
            $http.get(SETTING.ApiUrl + '/property/Delete',{
                    params:{
                        tagId:$scope.selectedId
                    },
                    'withCredentials':true
                }
            ).success(function(data) {
                    if (data.Status) {
                        getTagList();
                    }
                });
        })
    }
}])
app.controller('propertyCreateController',['$http','$state','$scope',function($http,$state,$scope){
    $scope.Model = {
        Id:'', //int
        PropertyName:'', //string
        Adduser:'', //UserBase
        Addtime:'', //DateTime
        UpdUser:'', //UserBase
        UpdTime:'', //DateTime
        Value:''
    };

    $scope.Create = function(){
        $http.post(SETTING.ApiUrl + '/property/Post',$scope.Model,{
            'withCredentials':true
        }).success(function(data){
            if(data.Status){
                $state.go("app.property.index");
            }
            else{
                //$scope.Message=data.Msg;
                $scope.alerts=[{type:'danger',msg:data.Msg}];
            }
        });
    }
}])
app.controller('propertyEditController',['$http','$state','$scope',function($http,$state,$scope){
    $http.get(SETTING.ApiUrl + '/property/get/' + $stateParams.id,{
        'withCredentials':true
    }).success(function(data){
        $scope.Model =data;
    });

    $scope.Save = function(){
        $http.post(SETTING.ApiUrl + '/property/put',$scope.Model,{
            'withCredentials':true
        }).success(function(data){
            if(data.Status){
                $state.go("app.property.index");
            }
            else{
                $scope.alerts=[{type:'danger',msg:data.Msg}];
            }
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