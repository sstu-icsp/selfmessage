app.controller('TasksController',
    function($scope, $http, $log, $rootScope,TaskService) {

        if ($rootScope.isAuthorize !== true) {
            window.location = "#/";
        }
        TaskService.getAllData()
              .then(
                  function (data) {
                      $log.info(data);
                      $scope.Details = data.reverse();
                  },
                  function (errorData) {
                      $scope.errorMessage = errorData.data.error_description;
                  });


        $scope.getAllData = function () {
            TaskService.getAllData()
            .then(
                function (data) {
                    $log.info(data);
                    $scope.Details = data.reverse();
                },
                function (errorData) {
                    $scope.errorMessage = errorData.data.error_description;
                });
        }

        $scope.taskData = {
            Name: "",
            Importance: "",
            About: "",
            StartTime: "",
            EndTime: "",
            

        }
        $scope.sendData = function () {
            TaskService.sendData($scope.noteData)
            .then(
                function (data) {
                    $log.info(data);
                    $scope.PostDaraResponse = data;
                    $scope.noteData = {};

                    TaskService.getAllData()
                        .then(
                            function (data) {
                                $log.info(data);
                                $scope.Details = data.reverse();;
                            },
                            function (errorData) {
                                $scope.errorMessage = errorData.data.error_description;
                            });

                },
                function (errorData) {
                    $scope.errorMessage = errorData.data.error_description;
                }
            );
        }
    });