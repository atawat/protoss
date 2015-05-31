/**
 * Created by ATA-GAME on 2015/5/31.
 */
app.controller('orderIndexController',['$http','$state','$scope',function($http,$state,$scope){
    $scope.searchCondition = {
        Page:'', //int
        PageCount:'', //int
        IsDescending:'', //bool
        Ids:'', //int
        OrderNum:'', //string
        Status:'', //EnumOrderStatus
        DeliveryAddress:'', //string
        IsPrint:'', //bool
        PhoneNumber:'', //string
        Type:'', //EnumOrderType
        PayType:'', //EnumPayType
        LocationX:'', //decimal
        LocationY:'', //decimal
        AddTimeBegin:'', //DateTime
        AddTimeEnd:'', //DateTime
        OrderBy:'' //EnumOrderSearchOrderBy
    };

    var getTagList = function() {
        $http.get(SETTING.ApiUrl+'/order/GetByCondition',{
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
            $http.get(SETTING.ApiUrl + '/order/Delete',{
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
app.controller('orderCreateController',['$http','$state','$scope',function($http,$state,$scope){
    $scope.Model = {
        Id:'', //int
        OrderNum:'', //string
        TotalPrice:'', //decimal
        TransCost:'', //decimal
        ProductCost:'', //decimal
        Discount:'', //decimal
        Status:'', //EnumOrderStatus
        DeliveryAddress:'', //string
        IsPrint:'', //bool
        PhoneNumber:'', //string
        Adduser:'', //UserBase
        Addtime:'', //DateTime
        Upduser:'', //UserBase
        Updtime:'', //DateTime
        Details:'', //IList<OrderDetailEntity>
        Coupon:'', //IList<CouponEntity>
        Type:'', //EnumOrderType
        PayType:'', //EnumPayType
        LocationX:'', //decimal
        LocationY:''
    };

    $scope.Create = function(){
        $http.post(SETTING.ApiUrl + '/order/Post',$scope.Model,{
            'withCredentials':true
        }).success(function(data){
            if(data.Status){
                $state.go("app.order.index");
            }
            else{
                //$scope.Message=data.Msg;
                $scope.alerts=[{type:'danger',msg:data.Msg}];
            }
        });
    }
}])
app.controller('orderEditController',['$http','$state','$scope',function($http,$state,$scope){
    $http.get(SETTING.ApiUrl + '/order/get/' + $stateParams.id,{
        'withCredentials':true
    }).success(function(data){
        $scope.Model =data;
    });

    $scope.Save = function(){
        $http.post(SETTING.ApiUrl + '/order/put',$scope.Model,{
            'withCredentials':true
        }).success(function(data){
            if(data.Status){
                $state.go("app.order.index");
            }
            else{
                $scope.alerts=[{type:'danger',msg:data.Msg}];
            }
        });
    }
}])
app.controller('orderDetailController',['$http','$state','$scope',function($http,$state,$scope){
    $http.get(SETTING.ApiUrl + '/order/get/' + $stateParams.id,{
        'withCredentials':true
    }).success(function(data){
        $scope.Model =data;
    });
}])