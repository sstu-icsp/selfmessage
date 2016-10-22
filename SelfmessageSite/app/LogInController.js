app.controller("LogInController",
    function ($scope, $log, $http, $httpParamSerializerJQLike, $cookieStore, $rootScope) {
        $scope.showMessage = false;

        $scope.submitLogin = function () {
            //условие проверки прошли ли все элементы формы валидациию
            if (window.login_form.checkValidity()) {

                var loginData = {
                    grant_type: "password",
                    Username: $scope.Email,
                    Password: $scope.Password
                };

                $http.post("http://localhost:10001/Token",
                    //функциия сериализации требовалась только здесь
                        $httpParamSerializerJQLike(loginData),
                        {
                            headers: {
                                'Content-Type': "application/x-www-form-urlencoded; charset=UTF-8"
                            }
                        })
                    .success(function(data, status, headers, config) {
                        //При успешной авторизации в заголовки запросов по умолчанию записывается токен для доступа
                        console.log("Bearer" + data.access_token);
                        $http.defaults.headers.common.Authorization = "Bearer " + data.access_token;

                        //используем куки для хранения токена
                        $cookieStore.put("token", "Bearer " + data.access_token);


                        //такая же функция как в authblock
                        $http.get("http://localhost:10001/api/account/userinfo")
                            .success(function(data1) {
                                $log.info(data1.Name);
                                $rootScope.username = data1.Email;
                                $rootScope.isAuth = true;
                                window.location = "#/Notes";
                                $log.info("Авторизация пройдена");

                                $log.info("window.location = " + window.location);
                            })
                            .error(function(data1, status1) {

                                if (status === 401) {
                                    $rootScope.isAuth = false;
                                    window.location = "#/LogIn";
                                }

                            });


                    })
                    .error(function (data, status, headers, config) {
                        //показываем сообщение о том что выход выполнен не удачно
                        $scope.message = data.error_description;
                        $scope.showMessage = true;
                    });
            }


        };
    });