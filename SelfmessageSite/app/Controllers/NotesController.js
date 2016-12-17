app.controller('NotesController',
    function ($scope, $http, $log, $rootScope, NoteService) {
    
    if ($rootScope.isAuthorize !== true) {
        window.location = "#/";
    }

    NoteService.getAllData()
        .then(
            function (data) {
                $log.info(data);
                $scope.Details = data.reverse();
            },
            function (errorData) {
                $scope.errorMessage = errorData.data.error_description;
            });


    $scope.getAllData = function () {
        NoteService.getAllData()
        .then(
            function (data) {
                $log.info(data);
                $scope.Details = data.reverse();
            },
            function (errorData) {
                $scope.errorMessage = errorData.data.error_description;
            });
    }
    
    $scope.deleteNote = function (id) {
        NoteService.deleteNote(id)
        .then(
            function (data) {
                $log.info(data);
                NoteService.getAllData()
                    .then(
                        function (data) {
                            $log.info(data);
                            $scope.Details = data.reverse();
                        },
                        function (errorData) {
                            $scope.errorMessage = errorData.data.error_description;
                        });
            },
            function (errorData) {
                $scope.errorMessage = errorData.data.error_description;
            });
    }

    

    $scope.findData = {
        Name: ""
    }

    $scope.noContent = false;

    $scope.searchData = function () {
        NoteService.searchData($scope.findData)
        .then(
            function (data) {
                
                    if (data.length == 0) {
                        $scope.noContent = true;
                    }
                    else $scope.noContent = false;
                    $log.info(data);
                    $scope.PostDaraResponse = data;
                    $scope.Details = data.reverse();
                    $scope.Details = data.reverse();
                

            },
            function (errorData) {
                $scope.errorMessage = errorData.data.error_description;
            }
        );
    }
   

    $scope.clearSearch = function () {
        NoteService.getAllData()
        .then(
            function (data) {
                $log.info(data);
                $scope.findData = {};
                $scope.Details = data.reverse();
            },
            function (errorData) {
                $scope.errorMessage = errorData.data.error_description;
            });
    }



    $scope.noteData = {
        Name: "",
        Tags: "",
        Text: ""
    }

    $scope.sendData = function () {
        NoteService.sendData($scope.noteData)
        .then(
            function (data) {
                $log.info(data);
                $scope.PostDaraResponse = data;
                $scope.noteData = {};

                NoteService.getAllData()
                    .then(
                        function (data) {
                            $log.info(data);
                            $scope.Details = data.reverse();
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

    $scope.editData = {
        Name: "",
        Tags: "",
        Text: ""
    }

    $scope.editNote = function (id) {
        NoteService.editNote(id, $scope.editData)
        .then(
            function (data) {
                $log.info(data);
                $scope.editData = {};
                NoteService.getAllData()
                    .then(
                        function (data) {
                            $log.info(data);
                            $scope.Details = data.reverse();
                        },
                        function (errorData) {
                            $scope.errorMessage = errorData.data.error_description;
                        });
            },
            function (errorData) {
                $scope.errorMessage = errorData.data.error_description;
            });
    }

    


});