angular.module("sportStore")
.controller("productsCtrl", function ($scope, remoteCallSvc
       ) {
    remoteCallSvc.get("http://localhost:4049/odata/Products", null, null)
         .then(function (result) {
             $scope.model.products = result.data.value;
            console.log($scope.model.products);
         }, function (response) {
             console.log(response);
         });

});