var app = angular.module('app',
[
    'ngRoute',
    "ngCookies",
]);

app.config(function($routeProvider) {
    $routeProvider
        .when('/',
        {
            templateUrl: '../Pages/Login.html',
            controller: 'LoginConroller'
        });
});

app.run(function ($http, $cookieStore, $log, UserService) {
    $log.debug("App.run start");

    $http.defaults.headers.common.Authorization = $cookieStore.get("token");

    UserService.getUserInfo();

    $log.debug("App.run end");
});

