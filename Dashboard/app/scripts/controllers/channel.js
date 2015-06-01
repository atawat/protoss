/**
 * Created by ATA-GAME on 2015/5/31.
 */
app.controller('channelIndexController',['$http','$state','$scope','$modal',function($http,$state,$scope,$modal){
    $scope.searchCondition = {
        Page:'1', //int
        PageCount:'10', //int
        IsDescending:'false', //bool
        Name:'', //string
        Status:'', //EnumChannelStatus
        OrderBy:'OrderById' //EnumChannelSearchOrderBy
    };

    var getTagList = function() {
        $http.get(SETTING.ApiUrl+'/channel/GetByCondition',{
            params:$scope.searchCondition,
            'withCredentials':true
        }).success(function(data){
            $scope.list = data;
        });

        $http.get(SETTING.ApiUrl+'/channel/GetCount',{
            params:$scope.searchCondition,
            'withCredentials':true
        }).success(function(data){
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
                msg:function(){return "你确定要删除吗？";}
            }
        });
        modalInstance.result.then(function(){
            $http.get(SETTING.ApiUrl + '/channel/Delete',{
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
        $state.go("app.channel.create")
    }
}])
app.controller('channelCreateController',['$http','$state','$scope',function($http,$state,$scope){
    $scope.Model = {
        Id:'', //int
        Name:'', //string
        Status:'Normal' //EnumChannelStatus
        //Parent:''
    };
    $scope.alerts =[];
    $scope.Create = function(){
        $http.post(SETTING.ApiUrl + '/channel/Post',$scope.Model,{
            'withCredentials':true
        }).success(function(data){
            $scope.alerts =[];
            if(data){
                $state.go("app.channel.index");
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
app.controller('channelEditController',['$http','$state','$scope','$stateParams',function($http,$state,$scope,$stateParams){
    $http.get(SETTING.ApiUrl + '/channel/get/' + $stateParams.id,{
        'withCredentials':true
    }).success(function(data){
        $scope.Model =data;
    });
    $scope.alerts =[];
    $scope.Save = function(){
        $http.post(SETTING.ApiUrl + '/channel/put',$scope.Model,{
            'withCredentials':true
        }).success(function(data){
            $scope.alerts =[];
            if(data){
                $state.go("app.channel.index");
            }
            else{
                $scope.alerts=[{type:'danger',msg:"保存出错"}];
            }
        }).error(function(data){
            $scope.alerts=[{type:'danger',msg:'出错啦请检查网络或再次尝试'}];
        });
    }
}])
app.controller('channelDetailController',['$http','$state','$scope',function($http,$state,$scope){
    $http.get(SETTING.ApiUrl + '/channel/get/' + $stateParams.id,{
        'withCredentials':true
    }).success(function(data){
        $scope.Model =data;
    });
}])