/**
 * Created by ATA-GAME on 2015/5/31.
 */
app.controller('productIndexController',['$http','$state','$scope',function($http,$state,$scope){
    $scope.searchCondition = {
        Page:'1', //int
        PageCount:'10', //int
        IsDescending:false, //bool
        Name:''//string
        //Spec:'', //string
        //PriceBegin:'', //decimal
        //PriceEnd:'', //decimal
        //CategoryId:'', //int
        //Status:'', //EnumProductStatus
        //OrderBy:'' //EnumProductSearchOrderBy
    };

    var getProductList = function() {
        $http.get(SETTING.ApiUrl+'/product/GetByCondition',{
            params:$scope.searchCondition,
            'withCredentials':true
        }).success(function(data){
            $scope.list = data;
        });

        $http.get(SETTING.ApiUrl+'/product/GetCount',{
            params:$scope.searchCondition,
            'withCredentials':true
        }).success(function(data){
            $scope.searchCondition.page = data.Condition.Page;
            $scope.searchCondition.pageSize = data.Condition.PageCount;
            $scope.totalCount = data.TotalCount;
        });
    };
    $scope.getList = getProductList;
    getProductList();

    $scope.del = function (id) {
        $scope.selectedId = id;
        var modalInstance = $modal.open({
            templateUrl: 'myModalContent.html',
            controller:'ModalInstanceCtrl',
            resolve: {
                msg:function(){return "你确定要删除吗";}
            }
        });
        modalInstance.result.then(function(){
            $http.get(SETTING.ApiUrl + '/product/Delete',{
                    params:{
                        tagId:$scope.selectedId
                    },
                    'withCredentials':true
                }
            ).success(function(data) {
                    if (data.Status) {
                        getProductList();
                    }
                });
        })
    }

    $scope.gotoNew = function(){
        $state.go('app.product.create')
    }
}])
app.controller('productCreateController',['$http','$state','$scope','FileUploader',function($http,$state,$scope,FileUploader){
    $scope.Model = {
        Name:'', //string
        Spec:'', //string
        Price:'', //decimal
        Unit:'', //string
        Category:{Id:'',CategoryName:''}, //CategoryEntity
        PropertyValues:[], //IList<ProductPropertyValueEntity>
        Detail:{Detail:'',ImgUrl1:'',ImgUrl2:'',ImgUrl3:'',ImgUrl4:'',ImgUrl5:''}
    };

    $http.get(SETTING.ApiUrl+'/Category/Get',{'withCredentials':true}).success(function(data){
        $scope.CategoryList = data;
    })

    $scope.Create = function(){
        $http.post(SETTING.ApiUrl + '/product/Post',$scope.Model,{
            'withCredentials':true
        }).success(function(data){
            if(data.Status){
                $state.go("app.product.index");
            }
            else{
                //$scope.Message=data.Msg;
                $scope.alerts=[{type:'danger',msg:data.Msg}];
            }
        }).error(function(data){
            $scope.alerts=[{type:'danger',msg:'出错啦请检查网络或再次尝试'}];
        });
    };

    var uploader = $scope.uploader = new FileUploader({
        url: SETTING.ApiUrl+'/Resource/Upload',
        'withCredentials':true
    });

    $scope.showThumbnail = false;
    uploader.onSuccessItem = function(fileItem, response, status, headers) {
        //console.info('onSuccessItem', fileItem, response, status, headers);
        $scope.Thumbnail=response.Msg;
        $scope.Model.Image = response.Msg;
    };

    $scope.getProperty = function(){
        $http.get(SETTING.ApiUrl +'/property/getByCategoryId?categoryId=' + $scope.Model.Category.Id,{'withCredentials':true}).success(function(data){
            for(var i= 0;i<data.length;i++){
                $scope.Model.PropertyValues.push({PropertyName:data[i].PropertyName,PropertyId:data[i].Id,PropertyValueId:0,PropertyValue:''});
            }
        });
    }
}]);
app.controller('productEditController',['$http','$state','$scope',function($http,$state,$scope){
    $http.get(SETTING.ApiUrl + '/product/get/' + $stateParams.id,{
        'withCredentials':true
    }).success(function(data){
        $scope.Model =data;
    });

    $scope.Save = function(){
        $http.post(SETTING.ApiUrl + '/product/put',$scope.Model,{
            'withCredentials':true
        }).success(function(data){
            if(data.Status){
                $state.go("app.product.index");
            }
            else{
                $scope.alerts=[{type:'danger',msg:data.Msg}];
            }
        });
    }
}])
app.controller('productDetailController',['$http','$state','$scope',function($http,$state,$scope){
    $http.get(SETTING.ApiUrl + '/product/get/' + $stateParams.id,{
        'withCredentials':true
    }).success(function(data){
        $scope.Model =data;
    });
}])