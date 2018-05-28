angular.module("sportsStoreAdmin")
.controller("userCtrl", function ($scope, $http, $location, remoteCallSvc, authStore, loginSvc) {
    $scope.userError = null;
    $scope.userInfo = null;
    $scope.confirm;
    $scope.allUser = [];
    remoteCallSvc.get("http://localhost:4049/api/Account/UserInfo", { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
    .then(function (result) {
        $scope.allUser = result.data;
        console.log(result);
        console.log($scope.allUser);
    }, function (err) {
        $scope.userError = "Error encountered";

    });
    $scope.register = function () {
        loginSvc.register($scope.userInfo.Email, $scope.userInfo.Password, $scope.userInfo.ConfirmPassword)
        .then(function () {
            $scope.userInfo = null;
            $("#ok").css("display", "initial");
            $scope.regform.$setPristine();
            $scope.regform.$setValidity();
            $scope.regform.$setUntouched();
            $scope.confirm = true;
        }, function (err) {
            $("#cancel").css("display", "initial");
            console.log(err);
        });
    }
});