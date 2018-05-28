angular.module("sportStore")
.directive("cartSummary", function (cart) {
    return {
        restrict: 'E',
        templateUrl: '/app/widgets/templates/summary.html',
        controller: function ($scope) {
            var items = cart.products();
            $scope.totalItems = function () {
                var q = 0;
                for (var i = 0; i < items.length; i++) {
                    q += items[i].qty;
                }
                return q;
            }
            $scope.totalAmount = function () {
                var s = 0;
                for (var i = 0; i < items.length; i++) {    
                    s += items[i].qty*items[i].price;
                }
                return s;
            }
        }
    }
});