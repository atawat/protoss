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
    //�ӱ��ػ�ȡ���ﳵ����cartService

    for(i in $scope.items){
        if($scope.items[i].num>0){
            $scope.total+=$scope.items[i].price*$scope.items[i].num
        }
    }

    $scope.change=function(index,num){
        if ($scope.items[index].num==0 && num==-1){
            alert('�Ѿ�û����!�������Ʒ���ٻ���')
        }else{
            $scope.items[index].num+=num;
            $scope.total+=$scope.items[i].price*$scope.items[i].num
        }
    }



})
