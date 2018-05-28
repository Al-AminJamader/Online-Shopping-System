angular.module("sportsStoreAdmin")
.controller("orderCtrl", function ($scope, remoteCallSvc) {
    $scope.model = {};
    $scope.model.current = null;
    $scope.model.temp = null;

    //panding
    $scope.pandingOrder = function ()
    {
        $scope.model.orders = null;
        remoteCallSvc.get("http://localhost:4049/odata/Orders?$filter=ShippedDate eq null", { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
        .then(function (result) {
            $("#headline").html("Pending Order List");
            $scope.model.orders = result.data.value;

            console.log($scope.model.orders);
        }, function (response) {
            console.log(response);
        });
    }
    //Shipped
    $scope.shippedOrder = function () {
        $scope.model.orders = null;
        remoteCallSvc.get("http://localhost:4049/odata/Orders?$filter=ShippedDate ne null", { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
        .then(function (result) {
            $("#headline").html("Shipped Order List");
            $scope.model.orders = result.data.value;

            console.log($scope.model.orders);
        }, function (response) {
            console.log(response);
        });
    }

    $scope.editOrder = function (order) {
        $scope.model.current = { OrderId: order.OrderId, TransactionId: order.TransactionId, OrderDate: order.OrderDate, ShippedDate: order.ShippedDate, CustomerName: order.CustomerName, ShippingAddress: order.ShippingAddress, Phone: order.Phone, Email: order.Email };
        $scope.model.temp = order;
        $("#editOrderModal").modal("show");
    }
    $scope.cancelOrder = function () {

        $scope.model.current = null;
        $scope.model.temp = null;
        $("#editOrderModal").modal("hide");
    }
    $scope.updateOrder = function () {
        remoteCallSvc.put("http://localhost:4049/odata/Orders(" + $scope.model.current.OrderId + ")",
            { "Authorization": "Bearer " + $scope.auth.accesstoken },
            $scope.model.current)
        .then(function (result) {

            var i = $scope.model.orders.indexOf($scope.model.temp);
            $scope.model.orders[i].OrderDate = $scope.model.current.OrderDate;
            $scope.model.orders[i].ShippedDate = $scope.model.current.ShippedDate;
            $scope.model.orders[i].CustomerName = $scope.model.current.CustomerName;
            $scope.model.orders[i].ShippingAddress = $scope.model.current.ShippingAddress;
            $scope.model.orders[i].Phone = $scope.model.current.Phone;
            $scope.model.orders[i].Email = $scope.model.current.Email;

            $scope.model.current = null;
            $scope.model.temp = null;
            console.log("success");
            $scope.shippedOrder();
            $("#editOrderModal").modal("hide");
        }, function (response) {
           

        });
    }
    //Delete
    $scope.confirmDelete = function (item) {
        //console.log(item);
        $scope.current = item;
        //$scope.productToDel.Id = 500;
        $("#confirmDialog").modal("show");
    }

    $scope.deleteOrder = function () {
        remoteCallSvc.remove("http://localhost:4049/odata/Orders(" + $scope.current.OrderId + ")", { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
        .then(function (result) {
            //console.log(result);
            var i = $scope.model.orders.indexOf($scope.model.current);
            $scope.model.orders.splice(i, 1);
            $scope.current = null;
            $scope.shippedOrder();
            $("#confirmDialog").modal("hide");
            //console.log(i);
        }, function (response) {

            console.log(response.statusText);
        });
    }

});