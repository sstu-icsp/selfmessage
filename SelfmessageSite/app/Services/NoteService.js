﻿app.factory("NoteService",
    function ($q, $log, $http, $httpParamSerializerJQLike) {

    var REST_SERVICE_URI = "http://localhost:9343/";


    var factory = {
        getAllData: getAllData,
        sendData: sendData,
        deleteNote: deleteNote
    };

    return factory;


    //Вывод ВСЕХ записей
    function getAllData() {
        var defered = $q.defer();
        $http.get(REST_SERVICE_URI + 'api/notes')
            .then(
                function(response){
                    $log.debug(response);
                    defered.resolve(response.data);
                },
                function(errorResponse){
                    $log.debug(errorResponse);
                    defered.reject(errorResponse);
                }
            
            );
        return defered.promise;
    };

    
    //Уделанеи записи по id
    //api/notes/{id}
    function deleteNote(id) {
        var defered = $q.defer();
        $http.delete(REST_SERVICE_URI + 'api/notes/'+id)
            .then(
                function (response) {
                    $log.debug(response);
                    defered.resolve(response.data);
                },
                function (errorResponse) {
                    $log.debug(errorResponse);
                    defered.reject(errorResponse);
                }

            );
        return defered.promise;
    };
   

    //Добавление новой записи
    function sendData(sendData) {
        var defered = $q.defer();
        $http.post(REST_SERVICE_URI + "api/notes", sendData)
         .then(
                    function (response) {
                        $log.debug(response);
                        defered.resolve(response.data);
                    },
                    function (errorResponse) {
                        $log.error("NoteService sendData error");
                        $log.error(errorResponse);
                        defered.reject(errorResponse);
                    });
        return defered.promise;
    };

    

        
});