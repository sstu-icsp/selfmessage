app.controller("RegistrationController",
    function($scope, $log, $http) {
        $scope.message = "Contact us! JK. This is just a demo.";
        $scope.submitRegistrarion = function() {
            var data = {
                Email: $scope.Email,
                Password: $scope.Password,
                ConfirmPassword: $scope.ConfirmPassword
            };

            $http.post('http://localhost:10001/api/Account/Register', data)
                .success(function(data) {
                    $log.info("Registration success");
                })
                .error(function(data) {
                    $log.error("Registration error");
                });
        };
    });

// ConfirmPassword
function confirm() {
    if (frm.confirmPassword.value != frm.password.value) {
        alert("Пароли не совпадают");
        return false
    }
    alert("Регистрация прошла успешно");
    return true;
    
}