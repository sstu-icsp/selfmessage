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

    $scope.deleteNote = function(noteId) {
        NoteService.deleteNote(noteId)
            .then(
                function() {
                    window.location = "#/Notes"
                });
    };
})