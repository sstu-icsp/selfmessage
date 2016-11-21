var app = angular.module("app",
[
    "ngRoute",
    "ngCookies",
]);

app.config(function($routeProvider) {
    $routeProvider
        .when("/",
        {
            templateUrl: "../Pages/Login.html",
            controller: "LoginConroller"
        })
        .when("/Registration",
        {
            templateUrl: "../Pages/Registration.html",
            controller: "RegistrationController"
        })
        .when("/Notes",
        {
            templateUrl: "../Pages/Notes.html",
            controller: "NotesController"
        });
});

app.run(function ($http, $cookieStore, $log, UserService, $rootScope) {
    $log.debug("App.run start");

    $http.defaults.headers.common.Authorization = $cookieStore.get("token");

    $rootScope.isAuthorize = false;
    $rootScope.Email = "";


    UserService.getUserInfo()
        .then(
            function(data) {
                $rootScope.isAuthorize = true;
                $rootScope.Email = data.Email;

                window.location = "#/Notes";
            },
            function () {
                $rootScope.isAuthorize = false;
                window.location = "#/";
    });

    $log.debug("App.run end");
});

