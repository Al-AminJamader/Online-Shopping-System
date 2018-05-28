angular.module("sportStore")
.factory("cart", function () {
    var cartItems = [];
    return {
        add: function (id, name, price) {
            var alreadyHere = false;
            for (var i = 0; i < cartItems.length; i++) {
                if (cartItems[i].id == id) {
                    alreadyHere = true;
                    cartItems[i].qty++;
                    break;
                }
            }
            if (!alreadyHere) {
                cartItems.push({ id: id, name: name, price: price, qty: 1 });
            }
        },
        remove: function (id) {
            for (var i = 0; i < cartItems.length; i++) {
                if (cartItems[i].id == id) {
                    cartItems.splice(i, 1);
                    break;
                }
            }
        },
        products: function () {
            return cartItems;
        },
        clear: function () {
            cartItems.length = 0;
        },
        get: function (id) {
            var item = null;
            for (var i = 0; i < cartItems.length; i++) {
                if (cartItems[i].id == id) {
                    item = cartItems[i];
                }
            }
            return item;
        }
    }
});