angular.module("sportStore")
.filter("unique", function () {
    return function (input, prop) {
        if (angular.isArray(input) && angular.isString(prop)) {
            var keys = [];
            var data = [];
            angular.forEach(input, function (item) {
                var key = item[prop];
                if (keys.indexOf(key) == -1) {
                    keys.push(key);
                    data.push(item);
                }
            })
            return data;
        }
        else
            return input;
    }
})
.filter("range", function ($filter) {
    return function (data, page, size) {
        if (angular.isArray(data) && angular.isNumber(page) && angular.isNumber(size)) {
            var start_index = (page - 1) * size;
            if (data.length < start_index) {
                return [];
            } else {
                return $filter("limitTo")(data.splice(start_index), size);
            }
        } else {
            return data;
        }
    }
})
.filter("pageCount", function () {
    return function (data, size) {
        if (angular.isArray(data)) {
            var result = [];
            for (var i = 0; i < Math.ceil(data.length / size) ; i++) {
                result.push(i);
            }
            return result;
        } else {
            return data;
        }
    }
});