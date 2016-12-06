app.controller("RegistrationController",
    function($scope, $log, $http, UserService, $rootScope) {
       
        if ($rootScope.isAuthorize === true) {
            window.location = "#/Notes";
        }


        $scope.isRegistrationError = false;
        $scope.isRegistrationSuccess = false;

        $scope.registrationData = {
            Email: "",
            Password: "",
            ConfirmPassword: ""
        }

        $scope.submitRegistration = function () {
            if (window.registration_form.checkValidity()) {
                if ($scope.registrationData.Password === $scope.registrationData.ConfirmPassword) {

                    UserService.registration($scope.registrationData)
                        .then(
                            function(data) {
                                $log.info(data);
                                $scope.isRegistrationError = false;
                                $scope.isRegistrationSuccess = true;
                                $scope.successMessage = "Вы успешно зарегистрировались в системе."
                            },
                            function(errorData) {
                                $scope.isRegistrationError = true;
                                $scope.errorMessage = "Имя пользователя " + $scope.registrationData.Email + " уже используется";
                            });
                } else {
                    $scope.isRegistrationError = true;
                    $scope.errorMessage = "Пароль и его подтверждение не совпадают."
                }
            }
        };
    });