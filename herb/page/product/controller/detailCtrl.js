/**
 * Created by Yunjoy on 2015/9/23.
 */


app.controller('detailCtrl',['$scope','$http','cartservice','$stateParams','$ionicSlideBoxDelegate','$state',function($scope,$http,cartservice,$stateParams,$ionicSlideBoxDelegate,$state) {

    $scope.Img=SETTING.ImgUrl;
    //region �ֲ�ͼ
    $scope.$on('$ionicView.enter', function () {
        $ionicSlideBoxDelegate.start();
    });
    //endregion
    //��ȡͼƬ����
    var load_detail = function () {
            $http.get(SETTING.ApiUrl + "/ProductDetail/productDetail?id=" + $stateParams.id, {
                'withCredentials': true
            }).success(function (data) {
                $scope.productDetail = data;
            });
    };
    load_detail();
    //��ȡ��Ʒ�۸������
    $scope.items = [];
    var getList = function () {
        $http.get(SETTING.ApiUrl + "/Product/Get?id=" + $stateParams.id,  {

            'withCredentials': true
        }).success(function (data) {
            if (data.List != "") {
                $scope.items = data.List;
            }
            console.log(data.List);
        });
    };
    getList();


    //region ���빺�ﳵ
    $scope.cartinfo = {
        id: null,
        name: null,
        count: null,
        price: null,
        image:null,
    };
    $scope.AddCart = function (data) {
        $scope.cartinfo.id = $scope.items.Id;
        $scope.cartinfo.name = $scope.items.Name;
        $scope.cartinfo.image = $scope.items.Image;
        $scope.cartinfo.price = $scope.items.Price;
        $scope.cartinfo.count = 1;
        cartservice.add($scope.cartinfo);
    }
    //endregion
    //region  ��������
    $scope.buy=function(){
        $state.go("page.cart",{productId: $scope.product.Id,count:$scope.numbers})
    }
    //endregion
   }])