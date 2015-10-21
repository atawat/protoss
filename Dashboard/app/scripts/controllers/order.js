/**
 * Created by ATA-GAME on 2015/5/31.
 */
app.controller('orderIndexController',['$http','$state','$scope',function($http,$state,$scope){
    $scope.searchCondition = {
        Page:1, //int
        PageCount:10, //int
        OrderNum:'', //string
        Status:'', //EnumOrderStatus
        IsPrint:'', //bool
        PhoneNumber:'', //string
        Type:'', //EnumOrderType
        PayType:'', //EnumPayType
        AddTimeBegin:'', //DateTime
        AddTimeEnd:'' //DateTime
    };

    var getOrderList = function() {
        $http.get(SETTING.ApiUrl+'/order/GetByCondition',{
            params:$scope.searchCondition,
            'withCredentials':true
        }).success(function(data){
            $scope.list = data.List;
        });

        $http.get(SETTING.ApiUrl+'/order/GetCount',{
            params:$scope.searchCondition,
            'withCredentials':true
        }).success(function(data){
            $scope.searchCondition.page = data.Condition.Page;
            $scope.searchCondition.pageSize = data.Condition.PageCount;
            $scope.totalCount = data.TotalCount;
        });
    };
    $scope.getList = getOrderList;
    getOrderList();

    $scope.del = function (id) {
        $scope.selectedId = id;
        var modalInstance = $modal.open({
            templateUrl: 'myModalContent.html',
            controller:'ModalInstanceCtrl',
            resolve: {
                msg:function(){return "您确定要删除订单吗";}
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
                        getOrderList();
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
app.controller('orderEditController',['$http','$state','$scope','$stateParams',function($http,$state,$scope,$stateParams){
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