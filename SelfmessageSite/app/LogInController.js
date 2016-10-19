app.controller("LogInController",
    function($scope, $log, $http, $httpParamSerializerJQLike, $cookieStore) {
        $scope.showMessage = false;

        $scope.submitLogin = function () {
            if (window.login_form.checkValidity()) {

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

                        window.location = "#/Notes";

                        $log.info("Авторизация пройдена");
                    })
                    .error(function (data, status, headers, config) {
                        $scope.message = data.error_description;
                        $scope.showMessage = true;
                    });
            }

            
        };
    });