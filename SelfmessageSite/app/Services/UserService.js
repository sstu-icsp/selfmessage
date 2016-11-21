app.factory("UserService", function ($q, $log, $http) {

    var REST_SERVICE_URI = "http://localhost:10001/";


    var factory = {
        getUserInfo: getUserInfo
    };

    return factory;

    function getUserInfo() {
        var defered = $q.defer();
        $http.get(REST_SERVICE_URI + "api/account/userinfo")
            .then(
            function (response) {
                $log.debug(responce)
                defered.resolve(response.data);
            },
            function (errorResponse) {
                $log.debug(errorResponse)
                defered.reject(errorResponse);
            }
        );
        return defered.promise;
    };

});