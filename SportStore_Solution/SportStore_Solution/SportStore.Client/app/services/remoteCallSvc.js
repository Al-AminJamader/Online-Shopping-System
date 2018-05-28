angular.module("sportStore")
.factory("remoteCallSvc", function ($http) {
    return {
        get: function (url, headers, data) {
            return $http({
                url: url,
                method: "GET",
                headers: headers,
                params:data
            });
        },
        post: function (url, headers, data) {
            return $http({
                url: url,
                method: "POST",
                headers: headers,
                data: data
            });
        }
    }
});