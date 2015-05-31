/**
 * Created by ATA-GAME on 2015/5/31.
 */
app.controller('couponIndexController',['$http','$state','$scope',function($http,$state,$scope){
    $scope.searchCondition = {
        Page:'', //int
        PageCount:'', //int
        IsDescending:'', //bool
        Ids:'', //int
        Guid:'', //Guid
        Type:'', //EnumCouponType
        DisCountBegin:'', //decimal
        DisCountEnd:'', //decimal
        Status:'', //EnumCouponStatus
        Owner:'', //UserBase
        OrderBy:'' //EnumCouponSearchOrderBy
    };

    var getTagList = function() {
        $http.get(SETTING.ApiUrl+'/coupon/GetByCondition',{
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
            $http.get(SETTING.ApiUrl + '/coupon/Delete',{
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
app.controller('couponCreateController',['$http','$state','$scope',function($http,$state,$scope){
    $scope.Model = {
        Id:'', //int
        Guid:'', //Guid
        Type:'', //EnumCouponType
        DisCount:'', //decimal
        Product:'', //ProductEntity
        ExpireTime:'', //DateTime
        Status:'', //EnumCouponStatus
        Adduser:'', //UserBase
        Addtime:'', //DateTime
        Upduser:'', //UserBase
        Updtime:'', //DateTime
        Owner:''
    };

    $scope.Create = function(){
        $http.post(SETTING.ApiUrl + '/coupon/Post',$scope.Model,{
            'withCredentials':true
        }).success(function(data){
            if(data.Status){
                $state.go("app.coupon.index");
            }
            else{
                //$scope.Message=data.Msg;
                $scope.alerts=[{type:'danger',msg:data.Msg}];
            }
        });
    }
}])
app.controller('couponEditController',['$http','$state','$scope',function($http,$state,$scope){
    $http.get(SETTING.ApiUrl + '/coupon/get/' + $stateParams.id,{
        'withCredentials':true
    }).success(function(data){
        $scope.Model =data;
    });

    $scope.Save = function(){
        $http.post(SETTING.ApiUrl + '/coupon/put',$scope.Model,{
            'withCredentials':true
        }).success(function(data){
            if(data.Status){
                $state.go("app.coupon.index");
            }
            else{
                $scope.alerts=[{type:'danger',msg:data.Msg}];
            }
        });
    }
}])
app.controller('couponDetailController',['$http','$state','$scope',function($http,$state,$scope){
    $http.get(SETTING.ApiUrl + '/coupon/get/' + $stateParams.id,{
        'withCredentials':true
    }).success(function(data){
        $scope.Model =data;
    });
}])