
app.controller('RegistrationController', function ($scope, $log, $http) {
    $scope.message = 'Contact us! JK. This is just a demo.';
    $scope.submitRegistrarion = function () {
        var data = {
            Email: $scope.user.email,
            Password: $scope.user.password,
            ConfirmPassword: $scope.registration.user.confirmPassword
        };

        $http.post('http://localhost:10001/api/Account/Register', data).success(function (data) {
            $log.info("Registration success");
        }).error(function (Data) {
            $log.error("Registration error")
        });
    };
});
// ConfirmPassword
function confirm() {
    if (frm.confirmPassword.value != frm.password.value) {
        alert("Пароли не совпадают");
        return false
    }
    return true;
}