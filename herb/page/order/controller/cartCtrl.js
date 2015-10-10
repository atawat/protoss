
app.controller('cartCtrl',['$scope','cartservice','$state',function($scope,cartservice,$state){

        //    从localStorage获取购物车信息
        var carlistcount=0;
        var getcar =function (){
            var storage=window.localStorage.ShoppingCart;
            if(storage!=undefined)
            {
                var jsonstr = JSON.parse(storage.substr(1, storage.length));
                $scope.productlist = jsonstr.productlist;
                carlistcount=$scope.productlist.length;
            }
        };
        getcar();
        //region 数量增加减
        $scope.adding=function(id){
            cartservice.addone(id);
            for(j=0;j<$scope.productlist.length;j++){
                if($scope.productlist[j].id==id){
                    $scope.productlist[j].count=  $scope.productlist[j].count+1;
                }
            }
            allprice();
        }
        $scope.decrease=function(id){

            for(j=0;j<$scope.productlist.length;j++){
                if($scope.productlist[j].id==id){
                    if(  $scope.productlist[j].count>1){
                        $scope.productlist[j].count=  $scope.productlist[j].count-1;
                        //cartservice.delete(id);
                    }
                }
            }
            allprice();
        }

        //endregion

        //region 计算总价
        var allprice=function(){
                $scope.total=0;
                for(j=0;j<$scope.productlist.length;j++){
                        $scope.total+= parseInt($scope.productlist[j].price * $scope.productlist[j].count);
                }
        }
        allprice();
        //endregion
        //region 提交订单
        $scope.submit=function(){
            $scope.productcount=[];
                for(j=0;j<$scope.productlist.length;j++){
                        $scope.productcount.push($scope.productlist[j])
                }
            $state.go("page.orderCertain",{productcount:$scope.productcount,total:$scope.total})
        }
        //endregion


    }]
)