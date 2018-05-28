angular.module("sportStore")
.controller("checkoutCtrl", function ($scope, cart, remoteCallSvc, $location) {
    $scope.order = null;
    $scope.placeOrder = function () {
        console.log($scope.shippingDetail.CustomerName);
        $scope.order = {
            OrderDate: new Date(),
            CustomerName: $scope.shippingDetail.CustomerName,
            ShippingAddress: $scope.shippingDetail.ShippingAddress,
            Phone: $scope.shippingDetail.Phone,
            Email: $scope.shippingDetail.Email
        };
        
        $scope.order.OrderDetails = [];
        var items = cart.products();
        for (var i = 0; i < items.length; i++) {
            $scope.order.OrderDetails.push({ ProductId: items[i].id, Quantity: items[i].qty });
        }

        console.log($scope.order);
        $("#payMentModal").modal("show");
        //$location.path("/creditCard");
        //remoteCallSvc.post("http://localhost:4049/odata/Orders", null, order)
        //.then(function (result) {
        //    console.log(result);
        //    cart.clear();
        //    $scope.orderCreated = result.data.OrderId;
        //    $location.path("/creditCard");
        //    //$location.path("/thanks");
        //}, function (response) {
        //    console.log(response);
        //});
        console.log($scope.model.cartItems.price);
        
    }
    //Pay in Credit Card
    $scope.pay = function () {
        console.log($scope.order);
        console.log("Pay Function");
        console.log($scope.model.cartItems.price);
        console.log($scope.rate);
        console.log($scope.card);
        var expiration = $scope.card.exp_month.toString() + $scope.card.exp_year.toString();
        console.log(expiration);
        var r = {
            "createTransactionRequest": {
                "merchantAuthentication": {
                    "name": "6UhGj85R",
                    "transactionKey": "6gXU7BrVVr5a628Y"
                    
                },
                "refId": "123456",
                "transactionRequest": {
                    "transactionType": "authCaptureTransaction",
                    "amount": 20,
                    "payment": {
                        "creditCard": {
                            "cardNumber": $scope.card.number,
                            "expirationDate": expiration,
                            "cardCode": $scope.card.cvc
                        }
                    },
                    "lineItems": {
                        "lineItem": {
                            "itemId": "1",
                            "name": $scope.order.CustomerName,
                            "description": "Cannes logo",
                            "quantity": "1",
                            "unitPrice": 20
                        }
                    },
                    "tax": {
                        "amount": "4.26",
                        "name": "level2 tax name",
                        "description": "level2 tax"
                    },
                    "duty": {
                        "amount": "8.55",
                        "name": "duty name",
                        "description": "duty description"
                    },
                    "shipping": {
                        "amount": "4.26",
                        "name": "level2 tax name",
                        "description": "level2 tax"
                    },
                    "poNumber": "456654",
                    "customer": {
                        "id": "99999456654"
                    },
                    "billTo": {
                        "firstName": $scope.order.CustomerName,
                        "lastName": "",
                        "company": "Self Ltd.",
                        "address": "Mohammadpur",
                        "city": "Dhaka",
                        "state": "",
                        "zip": "1207",
                        "country": "Bangladesh"
                    },
                    "shipTo": {
                        "firstName": $scope.order.CustomerName,
                        "lastName": "Bayles",
                        "company": "Thyme for Tea",
                        "address": "12 Main Street",
                        "city": "Pecan Springs",
                        "state": "TX",
                        "zip": "44628",
                        "country": "USA"
                    },
                    "customerIP": "192.168.1.1",
                    "transactionSettings": {
                        "setting": {
                            "settingName": "testRequest",
                            "settingValue": "false"
                        }
                    },
                    "userFields": {
                        "userField": [
                            {
                                "name": "MerchantDefinedFieldName1",
                                "value": "MerchantDefinedFieldValue1"
                            },
                            {
                                "name": "favorite_color",
                                "value": "blue"
                            }
                        ]
                    }
                }
            }
        };
        console.log(r);
        remoteCallSvc.post("https://apitest.authorize.net/xml/v1/request.api", null, r)
              .then(function (data) {
                  console.log(data);
                  $scope.order.TransactionId = data.data.transactionResponse.transId;
                  console.log(data.data.transactionResponse.transId);
                  $scope.model.transactionId = data.data.transactionResponse.transId;
                  console.log($scope.transactionId);
                  console.log($scope.order);
                  remoteCallSvc.post("http://localhost:4049/odata/Orders", null, $scope.order)
                  .then(function (result) {
                      $scope.transactionId = result.data.TransactionId;
                      console.log($scope.transactionId);
                      console.log(result);
                      cart.clear();
                      $scope.orderCreated = result.data.OrderId;
                      $("#payMentModal").modal("hide");
                      $scope.order = null;
                      $("#payMentModal").modal("hide");
                      $('body').removeClass('modal-open');
                      $('.modal-backdrop').remove();
                      $location.path("/thanks");
                  }, function (response) {
                      console.log(response);
                  });
              }, function (res) {
                  console.log(res);
              });

    }

});