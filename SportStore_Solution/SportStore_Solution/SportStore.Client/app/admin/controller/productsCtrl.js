angular.module("sportsStoreAdmin")
.controller("productsCtrl", function ($scope, remoteCallSvc) {
    var fileSelect, fileSelectEdit;
    $scope.productToDel = null;
    $scope.model.currentProduct = null;
    $scope.model.tempProduct = null;
    remoteCallSvc.post("http://localhost:4049/odata/Products/AcProducts", { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
         .then(function (result) {
             $scope.model.products = result.data.value;


         }, function (response) {
             console.log(response);
         });
    //Add new
    $scope.addNew = function () {
        $("#insertModal").modal("show");
    }

    //Save new
    $scope.saveProduct = function () {
        console.log("content")
        console.log($scope.model.currentProduct)
        //$scope.model.currentProduct.Stocklevel = 0;
        console.log($scope.model.currentProduct);
        remoteCallSvc.post("http://localhost:4049/odata/Products", { "Authorization": "Bearer " + $scope.auth.accesstoken }, $scope.model.currentProduct)
         .then(function (result) {
             console.log(result);
             $scope.model.products.push(result.data);
             $("#insertModal").modal("hide");
             //console.log($scope.model.products);
         }, function (response) {
             console.log(response);
         });
    }

    //Delete Product
    $scope.confirmDelProduct = function (item) {
        //console.log(item);
        $scope.productToDel = item;
        //$scope.productToDel.Id = 500;
        $("#confirmDialog").modal("show");
    }
    $scope.delProduct = function () {
        remoteCallSvc.remove("http://localhost:4049/odata/Products(" + $scope.productToDel.Id + ")", { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
        .then(function (result) {
            //console.log(result);
            var i = $scope.model.products.indexOf($scope.productToDel);
            $scope.model.products.splice(i, 1);
            $scope.productToDel = null;
            $("#confirmDialog").modal("hide");
            //console.log(i);
        }, function (response) {

            console.log(response.statusText);
        });
    }
    //edit product
    $scope.editProduct = function (product) {
        $scope.model.currentProduct = { Id: product.Id, Name: product.Name, Category: product.Category, Price: product.Price, Description: product.Description, Picture:product.Picture,Stocklevel:product.Stocklevel };
        $scope.model.tempProduct = product;
        $("#editModal").modal("show");
    }
    $scope.cancelEditProduct = function () {

        $scope.model.currentProduct = null;
        $scope.model.tempProduct = null;
        $("#editModal").modal("hide");
    }
    $scope.updateProduct = function () {
        remoteCallSvc.put("http://localhost:4049/odata/Products(" + $scope.model.currentProduct.Id + ")",
            { "Authorization": "Bearer " + $scope.auth.accesstoken },
            $scope.model.currentProduct)
        .then(function (result) {

            var i = $scope.model.products.indexOf($scope.model.tempProduct);
            $scope.model.products[i].Name = $scope.model.currentProduct.Name;
            $scope.model.products[i].Category = $scope.model.currentProduct.Category;
            $scope.model.products[i].Price = $scope.model.currentProduct.Price;
            $scope.model.products[i].Description = $scope.model.currentProduct.Description;
            $scope.model.products[i].Picture = $scope.model.currentProduct.Picture;
            $scope.model.products[i].Stocklevel = $scope.model.currentProduct.Stocklevel;
            $scope.model.currentProduct = null;
            $scope.model.tempProduct = null;
            $("#editModal").modal("hide");
        }, function (response) {
            var err = response.data["odata.error"].innererror;
            console.log(err.message);

        });
    }
    $scope.newProductPictureClick = function () {

        fileSelect = document.createElement('input'); //input it's not displayed in html, I want to trigger it form other elements
        fileSelect.type = 'file';

        if (fileSelect.disabled) { //check if browser support input type='file' and stop execution of controller
            return;
        }
        //console.log("new pic");
        if (fileSelect) { //activate function to begin input file on click
            fileSelect.click();
        }

        fileSelect.onchange = function () { //set callback to action after choosing file
            var f = fileSelect.files[0],
              r = new FileReader();

            r.onloadend = function (e) { //callback after files finish loading
                $scope.model.currentProduct.Picture = e.target.result;
                $scope.$apply();
                console.log($scope.model.currentProduct.Picture.replace(/^data:image\/(png|jpg);base64,/, "")); //replace regex if you want to rip off the base 64 "header"
                $("#newProductPrictrue").attr("src", $scope.model.currentProduct.Picture)
                //here you can send data over your server as desired
            }

            r.readAsDataURL(f); //once defined all callbacks, begin reading the file
        }
    }
    $scope.editPictureClick = function () {
        fileSelectEdit = document.createElement("input");// same as insert
        fileSelectEdit.type = 'file';
        if (fileSelectEdit.disabled) {
            return;
        }
        if (fileSelectEdit) {
            fileSelectEdit.click();
        }
        fileSelectEdit.onchange = function () { //set callback to action after choosing file
            var f = fileSelectEdit.files[0],
            r = new FileReader();

            r.onloadend = function (e) { //callback after files finish loading
                $scope.model.currentProduct.Picture = e.target.result;
                $scope.$apply();
                console.log($scope.model.currentProduct.Picture.replace(/^data:image\/(png|jpg);base64,/, "")); //replace regex if you want to rip off the base 64 "header"
                $("#newProductPrictrue").attr("src", $scope.model.currentProduct.Picture)
                //here you can send data over your server as desired
            }

            r.readAsDataURL(f); //once defined all callbacks, begin reading the file
        }
    }
    
});