/**
 * Created by Yunjoy on 2015/9/23.
 */
app.controller('cartCtrl',['$scope','cartservice',function($scope,cartservice){

        //    ��localStorage��ȡ���ﳵ��Ϣ
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



        ////�����Ӽ�
        //$scope.numbers=1;
        //$scope.addNumbers=function(){
        //    $scope.numbers=$scope.numbers+1;
        //};
        //$scope.deNumbers=function(){
        //    if($scope.numbers>=2)
        //        $scope.numbers-=1;
        //    else{
        //        $scope.numbers=1;
        //    }
        //}

        //region �������Ӽ�

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
                        cartservice.delete(id);
                    }
                }

            }
            allprice();

        }

        //endregion

        //region �����ܼ�


        var allprice=function(){

                for(j=0;j<$scope.productlist.length;j++){
                        $scope.total+= parseInt($scope.productlist[j].price * $scope.productlist[j].count);
                }


        }
        //endregion

        //region ����
        $scope.jiesuan=function(){
            $scope.productcount=[];
            for(i=0;i< $scope.choseArr.length;i++)
            {
                for(j=0;j<$scope.productlist.length;j++){
                    if($scope.choseArr[i]==$scope.productlist[j].id){
                        $scope.productcount.push($scope.productlist[j])
                    }
                }
            }
            $state.go("page.order",{productcount:$scope.productcount,pricecount:$scope.dprice})
        }

        //endregion


    }]
)