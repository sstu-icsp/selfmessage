//Блок отвечающий за авторизацию и все что с ней связано
app.controller("AuthBlock",
    function ($http, $cookieStore, $log, $scope, $rootScope) {

        //Заполнение заголовков авторизации
        $http.defaults.headers.common.Authorization = $cookieStore.get("token");

        //флаг показывающий выполнена ли авторизация
        $rootScope.isAuth = false;

        //выполнения запроса получающего информацию о пользоавтеле
        $http.get("http://localhost:10001/api/account/userinfo")
            .success(function (data) {
                //При успешном выполнение заполняем флаги и имя пользователя перенапралвяем на страницу записей
                $log.info(data.Email);
                $rootScope.username = data.Email;
                $rootScope.isAuth = true;

                window.location = "#/Notes";
            })
            .error(function(data, status) {
                //при не удаче проверяем что именно ошибка авторизации
                if (status === 401) {
                    //устанавливаем флага авторизации
                    $rootScope.isAuth = false;
                    //если текущая страница не является странице й регистрации перенаправляем на страницу входа
                    if (window.location != "http://localhost:5000/#/Registration") {
                        $log.info(window.location + " change");
                        window.location = "#/LogIn";
                    }
                }

            });

        //функциия выходи из программы вызывается со странницы индекс
        $scope.logout = function() {
            $http.post("http://localhost:10001/api/Account/Logout")
                .success(function(data) {

                    //Костыльный метод. Возможно когда-нибудь перепишу.
                    //Почему-то не выходит автоматически при вызове апишки, поэтому просто переписываю хеадеры
                    $log.info(data);
                    $rootScope.isAuth = false;
                    $rootScope.username = "";
                    window.location = "#/LogIn";
                    $cookieStore.put("token", "");
                    $http.defaults.headers.common.Authorization = $cookieStore.get("token");
                })
                .error(function (data, status) {
                    //вывод статуса ошибки и данных
                    $log.error(status);
                    $log.error(data);
                });
        }

        $scope.isLoginPage = function() {
            if (window.location == "http://localhost:5000/#/LogIn") {
                return true;
            } else {
                return false;
            }
        }

        $scope.isRegistrationPage = function() {
            if (window.location == "http://localhost:5000/#/Registration") {
                return true;
            } else {
                return false;
            }
        }
    });