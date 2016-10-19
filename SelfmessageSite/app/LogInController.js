app.controller("LogInController",
    function ($scope, $log, $http, $httpParamSerializerJQLike, $cookieStore) {
        $scope.message = "Look! I am an about page.";
        $scope.submitLogin = function() {
            var loginData = {
                grant_type: "password",
                Username: $scope.Email,
                Password: $scope.Password
            };

            $http.post("http://localhost:10001/Token",
                    $httpParamSerializerJQLike(loginData),
                    {
                        headers: {
                            'Content-Type': "application/x-www-form-urlencoded; charset=UTF-8"
                        }
                    })
                .success(function (data, status, headers, config) {
                    //При успешной авторизации в заголовки запросов по умолчанию записывается токен для доступа
                    console.log("Bearer" + data.access_token);
                    $http.defaults.headers.common.Authorization = "Bearer " + data.access_token;

                    $cookieStore.put("token", "Bearer " + data.access_token);

                    $log.info("Авторизация пройдена");
                })
                .error(function (data, status, headers, config) {

                });
        };
    });