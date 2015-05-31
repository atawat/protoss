/**
 * Created by ATA-GAME on 2015/5/31.
 */
app.controller('contentIndexController',['$http','$state','$scope',function($http,$state,$scope){
    $scope.searchCondition = {
        Page:'', //int
        PageCount:'', //int
        IsDescending:'', //bool
        Ids:'', //int
        Content:'', //string
        Title:'', //string
        Addusers:'', //UserBase
        AddtimeBegin:'', //DateTime
        AddtimeEnd:'', //DateTime
        Updusers:'', //UserBase
        UpdtimeBegin:'', //DateTime
        UpdtimeEnd:'', //DateTime
        Status:'', //EnumContentStatus
        Praises:'', //int
        Unpraises:'', //int
        Viewcounts:'', //int
        Tagss:'', //IList<TagEntity>
        Channelss:'', //IList<ChannelEntity>
        OrderBy:'' //EnumContentSearchOrderBy
    };

    var getTagList = function() {
        $http.get(SETTING.ApiUrl+'/content/GetByCondition',{
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
            $http.get(SETTING.ApiUrl + '/content/Delete',{
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
app.controller('contentCreateController',['$http','$state','$scope',function($http,$state,$scope){
    $scope.Model = {
        Id:'', //int
        Content:'', //string
        Title:'', //string
        Adduser:'', //UserBase
        Addtime:'', //DateTime
        Upduser:'', //UserBase
        Updtime:'', //DateTime
        Status:'', //EnumContentStatus
        Praise:'', //int
        Unpraise:'', //int
        Viewcount:'', //int
        ContentTags:'', //IList<ContentTag>
        Channels:''
    };

    $scope.Create = function(){
        $http.post(SETTING.ApiUrl + '/content/Post',$scope.Model,{
            'withCredentials':true
        }).success(function(data){
            if(data.Status){
                $state.go("app.content.index");
            }
            else{
                //$scope.Message=data.Msg;
                $scope.alerts=[{type:'danger',msg:data.Msg}];
            }
        });
    }
}])
app.controller('contentEditController',['$http','$state','$scope',function($http,$state,$scope){
    $http.get(SETTING.ApiUrl + '/content/get/' + $stateParams.id,{
        'withCredentials':true
    }).success(function(data){
        $scope.Model =data;
    });

    $scope.Save = function(){
        $http.post(SETTING.ApiUrl + '/content/put',$scope.Model,{
            'withCredentials':true
        }).success(function(data){
            if(data.Status){
                $state.go("app.content.index");
            }
            else{
                $scope.alerts=[{type:'danger',msg:data.Msg}];
            }
        });
    }
}])
app.controller('contentDetailController',['$http','$state','$scope',function($http,$state,$scope){
    $http.get(SETTING.ApiUrl + '/content/get/' + $stateParams.id,{
        'withCredentials':true
    }).success(function(data){
        $scope.Model =data;
    });
}])