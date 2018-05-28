angular.module("sportsStoreAdmin")
.controller("stockinCtrl", function ($scope, remoteCallSvc) {
    var fileSelect, fileSelectEdit;
    $scope.productToDel = null;
    $scope.model.current = null;
    $scope.model.tempProduct = null;
    remoteCallSvc.post("http://localhost:4049/odata/Products/AcProducts", { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
         .then(function (result) {
             $scope.model.products = result.data.value;


         }, function (response) {
             console.log(response);
         });
    remoteCallSvc.get("http://localhost:4049/odata/StockIns?$expand=Product", { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
         .then(function (result) {
             $scope.model.stockins = result.data.value;


         }, function (response) {
             console.log(response);
         });
    //Add new
    $scope.addNew = function () {
        $scope.model.current = null;
        $scope.model.temp = null;
        $("#insertModal").modal("show");
    }
    //Chack Stock Level
    $scope.checkStockLevel = function () {
        remoteCallSvc.get("http://localhost:4049/odata/StockIns?$expand=Product", { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
        .then(function (result) {
            $scope.model.stockins = result.data.value;


        }, function (response) {
            console.log(response);
        });

    }
    //Save new
    $scope.saveProduct = function () {
        console.log("Product")
        console.log($scope.findProduct(1))
        //$scope.model.currentProduct.Stocklevel = 0;
        console.log($scope.model.current);
        remoteCallSvc.post("http://localhost:4049/odata/StockIns?$expand=Product", { "Authorization": "Bearer " + $scope.auth.accesstoken }, $scope.model.current)
         .then(function (result) {
             console.log(result.data);
             var d = result.data;
             d.Product = $scope.findProduct(d.ProductId);
             $scope.model.stockins.push(d);
             $("#insertModal").modal("hide");
             //console.log($scope.model.products);
         }, function (response) {
             console.log(response);
         });
    }
    //
    $scope.confirmDelstockin = function (item) {
        //console.log(item);
        //$scope.current = item;
        $scope.productToDel = item;
        //$scope.productToDel.Id = 500;
        $("#confirmDialog").modal("show");
    }
    $scope.delProduct = function () {
        remoteCallSvc.remove("http://localhost:4049/odata/StockIns(" + $scope.productToDel.StockInId + ")", { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
        .then(function (result) {
            //console.log(result);
            var i = $scope.model.stockins.indexOf($scope.productToDel);
            $scope.model.stockins.splice(i, 1);
            $scope.productToDel = null;
            $("#confirmDialog").modal("hide");
            //console.log(i);
        }, function (response) {

            console.log(response.statusText);
        });
    }
    $scope.editstockin = function (stockin) {
        $scope.model.temp = stockin;
        $scope.model.current = { StockInId: stockin.StockInId, Date: stockin.Date, quantity: stockin.quantity, ProductId: stockin.ProductId };
        
        $("#editModal").modal("show");
    }

    //cancel

    $scope.cancel = function () {

        $scope.model.current = null;
        $scope.model.temp = null;
        $("#editModal").modal("hide");
        $("#confirmDialog").modal("hide");
        $("#insertModal").modal("hide");
    }
    $scope.editProduct = function () {
        remoteCallSvc.put("http://localhost:4049/odata/StockIns(" + $scope.model.current.StockInId + ")", { "Authorization": "Bearer " + $scope.auth.accesstoken },
            $scope.model.current)
        .then(function (result) {

            var i = $scope.model.stockins.indexOf($scope.model.temp);
            $scope.model.stockins[i] = $scope.model.current;
            
            $scope.model.current = null;
            $scope.model.temp = null;
            $("#editModal").modal("hide");
        }, function (response) {
            var err = response.data["odata.error"].innererror;
            console.log(err.message);

        });
    }
    $scope.findProduct = function (id) {
        //console.log($scope.model.products[0])
        for (var i = 0; i<$scope.model.products.length; i++) {
            if ($scope.model.products[i].Id == id) {
                return $scope.model.products[i];
            }
        }
    }
});