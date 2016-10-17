var app = angular.module('app', ['ngRoute']);

app.config(function ($routeProvider) {
    $routeProvider
    .when('/', {
        templateUrl: '../View/Pages/Notes.html',
        controller: 'NotesController'
    })
    .when('/Notes', {
        templateUrl: '../View/Pages/Notes.html',
        controller: 'NotesController'
    })
    .when('/LogIn', {
        templateUrl: '../View/Pages/LogIn.html',
        controller: 'LogInController'
    })
    .when('/Registration', {
        templateUrl: '../View/Pages/Registration.html',
        controller: 'RegistrationController'
    })
});

app.controller('NotesController', function ($scope, $http, $log) {
    // create a message to display in our view
    $scope.message = 'Everyone come and see how good I look!';
    $scope.keyword = '';
    //Вывод ВСЕХ записей
    $scope.GetAllData = function () {
        $http.get('http://localhost:10001/api/notes')
        .success(function (data, status, headers) {
            $scope.Details = data;
        })
        .error(function (data, status, header) {
            $scope.ResponseDetails = "Data: " + data +
                "<br />status: " + status +
                "<br />headers: " + jsonFilter(header);

            $log.error(data);
        });
    };
    //Вывод записи по конкретному тегу
    $scope.SearchData = function () {


        var address = 'http://localhost:10001/api/notes/bytag?tagName=' + $scope.keyword;

        $http.get(address)
        .success(function (data, status, headers) {
            $scope.Details = data;
        })
        .error(function (data, status, header) {
            $scope.ResponseDetails = "Data: " + data +
                "<hr />status: " + status +
                "<hr />headers: " + header;

            $log.error(data);

        });
    };
    //Добавление новой записи
    $scope.SendData = function () {

        //Передает элементы Name, Text, Tags
        $http.post('http://localhost:10001/api/notes/add', {
            Name: $scope.Name,
            Text: $scope.Text,
            Tags: $scope.Tags,
        }).success(function (data, status, headers) {
            $scope.PostDataResponse = data;
        })
    .error(function (data, status, header) {
        $scope.ResponseDetails = "Data: " + data +
            "<hr />status: " + status +
            "<hr />headers: " + header;
    });


    };
});

app.controller('LogInController', function ($scope, $log, $http, $httpParamSerializerJQLike, $window) {
    $scope.message = 'Look! I am an about page.';
    $scope.submitLogin = function () {
        var loginData = {
            grant_type: 'password',
            Username: $scope.Email,
            Password: $scope.Password
        };

        $http.post("http://localhost:10001/Token", $httpParamSerializerJQLike(loginData), {
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8'
            }
        })
        .success(function (data) {
            console.log(data);
            $scope.auth_string = "Bearer " + data.access_token;
            $http.defaults.headers.common['Authorization'] = $scope.auth_string;
        })
        .error(function (data) {
            console.log("fail1");
        });
    };
});

app.controller('RegistrationController', function ($scope, $log, $http) {
    $scope.message = 'Contact us! JK. This is just a demo.';
    $scope.submitRegistrarion = function () {
        var data = {
            Email: $scope.Email,
            Password: $scope.Password,
            ConfirmPassword: $scope.ConfirmPassword
        };

        $http.post('http://localhost:10001/api/Account/Register', data).success(function (data) {
            $log.info("Registration success");
        }).error(function (Data) {
            $log.error("Registration error")
        });
    };
});




//var app = angular.module("selfmessageApp", ["restangular", "ui.router"]);
//'use strict';

//app.config(function ($stateProvider, $urlRouterProvider) {
//    $stateProvider.state('notes', {
//        url: "/notes",
//        templateUrl: "View/Notes.html"
//    })
//    .state('login', {
//        url: "/login",
//        templateUrl: "View/LogIn.html"
//    })
//    .state('registration', {
//        url: "/registration",
//        templateUrl: "View/Registration.html"
//    });

//    $urlRouterProvider.otherwise('/notes');
//});



//app.controller('NotesController', function ($scope) {
//    $scope.name = 'NotesController';
//});

//app.controller('LogInController', function ($scope) {
//    $scope.name = 'LogInController';
//});

//app.controller('RegistrationController', function ($scope) {
//    $scope.name = 'RegistrationController';
//});










//app.config(function ($stateProvider, $urlRouterProvider) {
//    $routeProvider

//        .when('/', {
//            templateUrl: 'View/Notes.html',
//            controller: 'NotesController'
//        })

//        .when('/login', {
//            templateUrl: 'View/LogIn.html',
//            controller: 'LogInController'
//        })

//        .when('/Registration', {
//            templateUrl: 'View/Registration.html',
//            controller: 'RegistrationController'
//        })
//});


