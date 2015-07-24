/**
 * Created by ATA-GAME on 2015/7/8.
 */



function NumControl(){
    var t = $("#num");
    $("#add").click(function(){
        t.val(parseInt(t.val())+1)
        setTotal();
    })
    $("#reduce").click(function(){
        t.val(parseInt(t.val())-1)
        setTotal();
    })
    function setTotal(){
        $("#total").html((parseInt(t.val())*300).toFixed(2));
    }
    setTotal();
}