angular.module("sportStore")
.controller("cartCtrl", function ($scope, cart, remoteCallSvc) {
    $scope.model.cartItems = cart.products();
    $scope.total = function () {
        var s = 0;
        for (var i = 0; i < $scope.model.cartItems.length; i++) {
            s += $scope.model.cartItems[i].qty * $scope.model.cartItems[i].price;
        }
        return s;
        
    }
    $scope.chekStock = function (item) {
        remoteCallSvc.get("http://localhost:4049/odata/Products(" + item.id + ")", null, null)
         .then(function (result) {
            var product= result.data;
            if(item.qty >= product.Stocklevel){
                item.qty = product.Stocklevel;
                alert("No more products.");
            }
            console.log(result.data);
         }, function (response) {
             console.log(response);
         });
    }
    $scope.remove = function (id) {
        //console.log(cart.products());
        cart.remove(id);
    }
    $scope.clearCart= function(){
        cart.clear();
    }
    
});