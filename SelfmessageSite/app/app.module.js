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

app.controller('NotesController', function ($scope) {
    // create a message to display in our view
    $scope.message = 'Everyone come and see how good I look!';
});

app.controller('LogInController', function ($scope) {
    $scope.message = 'Look! I am an about page.';
});

app.controller('RegistrationController', function ($scope) {
    $scope.message = 'Contact us! JK. This is just a demo.';
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


