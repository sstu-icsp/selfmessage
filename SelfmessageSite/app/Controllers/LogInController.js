app.controller("LoginConroller",
    function($scope, $log, $http, $cookieStore, $rootScope, UserService) {


        if ($rootScope.isAuthorize === true) {
            window.location = "#/Notes";
        }

        $scope.loginData = {
            grant_type: "password",
            Username: "",
            Password: ""
        }

        $scope.isLoginError = false;

        $scope.submitLogin = function() {
            //условие проверки прошли ли все элементы формы валидациию
            if (window.login_form.checkValidity()) {

                UserService.login($scope.loginData)
                    .then(
                        function(data) {
                            $log.info(data);

                            console.log("Bearer" + data.access_token);
                            $http.defaults.headers.common.Authorization = "Bearer " + data.access_token;
                            $cookieStore.put("token", "Bearer " + data.access_token);

                            $rootScope.isAuthorize = true;
                            $rootScope.Email = $scope.loginData.Username;

                            window.location = "#/Notes";
                        },
                        function (errorData) {
                            $scope.isLoginError = true;
                            $scope.errorMessage = errorData.data.error_description;
                        });
            }
        }
    });
