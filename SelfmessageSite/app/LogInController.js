app.controller("LogInController", function ($scope, $log, $http, $httpParamSerializerJQLike, $window) {
    $scope.message = "Look! I am an about page.";
    $scope.submitLogin = function () {
        var loginData = {
            grant_type: "password",
            Username: $scope.Email,
            Password: $scope.Password
        };

        $http.post("http://localhost:10001/Token", $httpParamSerializerJQLike(loginData), {
            headers: {
                'Content-Type': "application/x-www-form-urlencoded; charset=UTF-8"
            }
        })
        .success(function (data) {
            console.log(data);
            $scope.auth_string = "Bearer " + data.access_token;
            $http.defaults.headers.common["Authorization"] = $scope.auth_string;
        })
        .error(function (data) {
            console.log("fail1");
        });
    };
});