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
        })
        .when("/Tasks",
        {
            templateUrl: "../Pages/Tasks.html",
            controller: "TasksController"
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
                if (window.location != "#/Registration") {
                    window.location = "#/";
                }
    });



    $rootScope.logout = function () {
        $log.debug("logout has been click");
        $rootScope.isAuthorize = false;
        $rootScope.Email = "";


        $cookieStore.put("token", "");

        window.location = "#/";
    }

    $log.debug("App.run end");
});

