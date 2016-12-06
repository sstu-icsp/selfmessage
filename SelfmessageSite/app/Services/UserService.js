app.factory("UserService",
    function ($q, $log, $http, $httpParamSerializerJQLike) {

        var REST_SERVICE_URI = "http://localhost:9343/";


        var factory = {
            getUserInfo: getUserInfo,
            login: login,
            registration: registration
        };

        return factory;

        function getUserInfo() {
            var defered = $q.defer();
            $http.get(REST_SERVICE_URI + "api/account/userinfo")
                .then(
                    function(response) {
                        $log.debug(response);
                        defered.resolve(response.data);
                    },
                    function(errorResponse) {
                        $log.debug(errorResponse);
                        defered.reject(errorResponse);
                    }
                );
            return defered.promise;
        };

        function login(loginData) {
            var defered = $q.defer();
            $http.post(REST_SERVICE_URI + "Token",
                    $httpParamSerializerJQLike(loginData),
                    {
                        headers: {
                            'Content-Type': "application/x-www-form-urlencoded; charset=UTF-8"
                        }
                    })
                .then(
                function (response) {
                    $log.debug(response);
                    defered.resolve(response.data);
                },
                function (errorResponse) {
                    $log.error("UserService login error");
                    $log.error(errorResponse);
                    defered.reject(errorResponse);
                });
            return defered.promise;
        };

        function registration(registrationData) {
            var defered = $q.defer();
            $http.post(REST_SERVICE_URI + "api/Account/Register", registrationData)
                .then(
                    function (response) {
                        $log.debug(response);
                        defered.resolve(response.data);
                    },
                    function (errorResponse) {
                        $log.error("UserService registrtion error");
                        $log.error(errorResponse);
                        defered.reject(errorResponse);
                    });
            return defered.promise;
        };
    });