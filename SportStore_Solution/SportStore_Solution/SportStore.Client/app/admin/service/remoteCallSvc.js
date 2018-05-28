angular.module("sportsStoreAdmin")
.factory("remoteCallSvc", function ($http) {
    return {
        get: function (url, headers, data) {
            return $http({
                url: url,
                method: "GET",
                headers: headers,
                params: data
            });
        },
        post: function (url, headers, data) {
            return $http({
                url: url,
                method: "POST",
                headers: headers,
                data: data
            });
        },
        put: function (url, headers, data) {
            return $http({
                url: url,
                method: "PUT",
                headers: headers,
                data: data
            });
        },
        remove: function (url, headers) {
            return $http({
                url: url,
                method: "DELETE",
                headers: headers
            });
        }
    }
});