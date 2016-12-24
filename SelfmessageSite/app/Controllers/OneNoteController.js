app.controller("OneNoteController", function($scope, $log, $http, NoteService, $routeParams) {
    /*
    * Контроллер для страници просмотра одной записи
    */

    $scope.note = {
        Id: "",
        DateAdded: "",
        Name: "",
        Text: "",
        Tags: []
    }

    $scope.imageUrls = [];

    $scope.currentImageUrl = "";

    $scope.imageForAdd = {
        image: ""
    };

    NoteService.getNote($routeParams.noteId)
        .then(
            function(response) {
                $scope.note = response;
                $log.debug("Response in call getNote function of noteService in OneNoteController");
                $log.debug(response);
                $log.debug("Note after function getNote in OneNoteController");
                $log.debug($scope.note);
            },
            function(errorResponse) {
                alert("Request error" + errorResponse);
            });

    NoteService.getImagesOfNote($routeParams.noteId)
        .then(
            function(response) {
                $log.debug("Response in call getImagesOfNote function of noteServiec in oneNoteController");
                $log.debug(response);

                $scope.imageUrls = response;
            });


    //Функция используемая для отображения картинки в большом размере
    //Записывает адресс картинки(миниаютюры) на которую нажал пользователь
    //Отображение делается на самой html странице
    $scope.setCurrentUrl = function(imageUrl) {
        $scope.currentImageUrl = imageUrl;
        $log.debug("Current url: " + imageUrl);
    }

    $scope.deleteNote = function(noteId) {
        NoteService.deleteNote(noteId)
            .then(
                function() {
                    window.location = "#/Notes";
                });
    };
})