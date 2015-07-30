/**
 * Created by ATA-GAME on 2015/7/8.
 */
app.controller('cartController','$scope','cartService',function($scope,carService){
    $scope.items=[{
        imgUrl:'',
        name:'',
        price:'',
        num:''
    }]
    //从本地获取购物车数据cartService

    for(i in $scope.items){
        if($scope.items[i].num>0){
            $scope.total+=$scope.items[i].price*$scope.items[i].num
        }
    }

    $scope.change=function(index,num){
        if ($scope.items[index].num==0 && num==-1){
            alert('已经没有了!请添加商品后再回来')
        }else{
            $scope.items[index].num+=num;
            $scope.total+=$scope.items[i].price*$scope.items[i].num
        }
    }



})
