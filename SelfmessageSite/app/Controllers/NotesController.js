app.controller('NotesController', function ($scope, $http, $log, $rootScope) {
    
    if ($rootScope.isAuthorize !== true) {
        window.location = "#/";
    }


    //Вывод ВСЕХ записей
    $scope.GetAllData = function () {
        $http.get('http://localhost:10001/api/notes')
        .success(function (data, status, headers) {
            $scope.Details = data;
        })
        .error(function (data, status, header) {
            //Закоментировал вызывает ошибку
            /*$scope.ResponseDetails = "Data: " + data +
                "<br />status: " + status +
                "<br />headers: " + jsonFilter(header);

            $log.error(data);*/
        });
    };

    $scope.SearchNoteByName = function () {


        var address = 'http://localhost:10001/api/notes/byname?noteName=' + $scope.keyword;

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