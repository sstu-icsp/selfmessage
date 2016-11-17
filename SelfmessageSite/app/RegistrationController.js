
app.controller("RegistrationController",
    function($scope, $log, $http) {

        $scope.user = { Email: "", Password: "", ConfirmPassword: "" };

        $scope.registrtionError = false;
        $scope.passwordNotMatch = false;

        $scope.submitRegistrarion = function() {
            if (window.registrtion_form.checkValidity()) {
                if (!($scope.user.Password === $scope.user.ConfirmPassword)) {
                    $scope.passwordNotMatch = true;
                } else {
                    $http.post("http://localhost:10001/api/Account/Register", $scope.user)
                        .success(function(data) {
                            $log.info("Registration success");
                            window.location = "#/LogIn";
                        })
                        .error(function(data) {
                            $log.error("Registration error");
                            $scope.passwordNotMatch = true;
                        });
                }
            }
        };
    });
