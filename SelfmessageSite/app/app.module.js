var app = angular.module('app', ['ngRoute',
    "ngCookies", ]);

app.config(function ($routeProvider) {
    $routeProvider
    .when('/', {
        templateUrl: '../Pages/Notes.html',
        controller: 'NotesController'
    })
    .when('/Notes', {
        templateUrl: '../Pages/Notes.html',
        controller: 'NotesController'
    })
    .when('/LogIn', {
        templateUrl: '../Pages/LogIn.html',
        controller: 'LogInController'
    })
    .when('/Registration', {
        templateUrl: '../Pages/Registration.html',
        controller: 'RegistrationController'
    })
});

{
//app.controller('NotesController', function ($scope, $http, $log) {
//    // create a message to display in our view
//    $scope.message = 'Everyone come and see how good I look!';
//    $scope.keyword = '';
//    //Вывод ВСЕХ записей
//    $scope.GetAllData = function () {
//        $http.get('http://localhost:10001/api/notes')
//        .success(function (data, status, headers) {
//            $scope.Details = data;
//        })
//        .error(function (data, status, header) {
//            $scope.ResponseDetails = "Data: " + data +
//                "<br />status: " + status +
//                "<br />headers: " + jsonFilter(header);

//            $log.error(data);
//        });
//    };
//    //Вывод записи по конкретному тегу
//    $scope.SearchData = function () {


//        var address = 'http://localhost:10001/api/notes/bytag?tagName=' + $scope.keyword;

//        $http.get(address)
//        .success(function (data, status, headers) {
//            $scope.Details = data;
//        })
//        .error(function (data, status, header) {
//            $scope.ResponseDetails = "Data: " + data +
//                "<hr />status: " + status +
//                "<hr />headers: " + header;

//            $log.error(data);

//        });
//    };
//    //Добавление новой записи
//    $scope.SendData = function () {

//        //Передает элементы Name, Text, Tags
//        $http.post('http://localhost:10001/api/notes/add', {
//            Name: $scope.Name,
//            Text: $scope.Text,
//            Tags: $scope.Tags,
//        }).success(function (data, status, headers) {
//            $scope.PostDataResponse = data;
//        })
//    .error(function (data, status, header) {
//        $scope.ResponseDetails = "Data: " + data +
//            "<hr />status: " + status +
//            "<hr />headers: " + header;
//    });


//    };
//});
}

{
    //app.controller('LogInController', function ($scope, $log, $http, $httpParamSerializerJQLike, $window) {
    //    $scope.message = 'Look! I am an about page.';
    //    $scope.submitLogin = function () {
    //        var loginData = {
    //            grant_type: 'password',
    //            Username: $scope.Email,
    //            Password: $scope.Password
    //        };

    //        $http.post("http://localhost:10001/Token", $httpParamSerializerJQLike(loginData), {
    //            headers: {
    //                'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8'
    //            }
    //        })
    //        .success(function (data) {
    //            console.log(data);
    //            $scope.auth_string = "Bearer " + data.access_token;
    //            $http.defaults.headers.common['Authorization'] = $scope.auth_string;
    //        })
    //        .error(function (data) {
    //            console.log("fail1");
    //        });
    //    };
    //});
}

{
    //app.controller('RegistrationController', function ($scope, $log, $http) {
    //    $scope.message = 'Contact us! JK. This is just a demo.';
    //    $scope.submitRegistrarion = function () {
    //        var data = {
    //            Email: $scope.Email,
    //            Password: $scope.Password,
    //            ConfirmPassword: $scope.ConfirmPassword
    //        };

    //        $http.post('http://localhost:10001/api/Account/Register', data).success(function (data) {
    //            $log.info("Registration success");
    //        }).error(function (Data) {
    //            $log.error("Registration error")
    //        });
    //    };
    //});
}
