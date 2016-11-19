app.factory('NoteService', ['$log', '$q', '$http', function ($log, $q, $http) {

    $log.debug("note service start work");

    var REST_SERVICE_NOTE_URI = 'http://localhost:10001/api/notes';


    var factory = {
        deleteUser: deleteUser
    };

    return factory;


    function deleteUser(id) {
        var deferred = $q.defer();

        $http.delete(REST_SERVICE_NOTE_URI + "?id=" + id)
            .then(
                function (response) {
                    deferred.resolve(response.data);
                },
                function (errorResponse) {
                    deferred.reject(errorResponse);
                }
            );

        return deferred.promise;
    }
}]);