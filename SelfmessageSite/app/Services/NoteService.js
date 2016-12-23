﻿app.factory("NoteService",
    function ($q, $log, $http, $httpParamSerializerJQLike) {

    var REST_SERVICE_URI = "http://localhost:9343/";


    var factory = {
        getAllData: getAllData,
        sendData: sendData,
        deleteNote: deleteNote,
        searchData: searchData,
        editNote: editNote,
        getNote: getNote,
        getImagesOfNote: getImagesOfNote,
        addImageToNote: addImageToNote
    };

    return factory; 

    function addImageToNote(image, noteId) {
            /*var defered = $q.defer();
            $http.post(REST_SERVICE_URI + "api/notes/" + noteId + "/images",
                    $httpParamSerializerJQLike(image),
                    {
                        headers: {
                            'Content-Type': "multipart/form-data"
                        }
                    })
                .then(
                function (response) {
                    $log.debug(response);
                    defered.resolve(response.data);
                },
                function (errorResponse) {
                    $log.error("UserService login error");
                    $log.error(errorResponse);
                    defered.reject(errorResponse);
                });
            return defered.promise;*/
        }

        function getImagesOfNote(noteId) {
            var defered = $q.defer();
            $http.get(REST_SERVICE_URI + "api/notes/" + noteId +"/images")
                .then(
                    function (response) {
                        $log.debug(response);
                        defered.resolve(response.data);
                    },
                    function (errorResponse) {
                        $log.debug(errorResponse);
                        defered.reject(errorResponse);
                    });
            return defered.promise;
        }

        function getNote(noteId) {
            var defered = $q.defer();
            $http.get(REST_SERVICE_URI + "api/notes/" + noteId)
                .then(
                    function(response) {
                        $log.debug(response);
                        defered.resolve(response.data);
                    },
                    function(errorResponse) {
                        $log.debug(errorResponse);
                        defered.reject(errorResponse);
                    });
            return defered.promise;
        }

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

    //url api/notes/{id}
    //тело: Name
    function editNote(id, sendData) {
        var defered = $q.defer();
        $http.put(REST_SERVICE_URI + 'api/notes/' + id, sendData)
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

    //Поиск записи по тегу
    //api/notes/byname?notename=name
    //name заменяется на название тэга
    function searchData(findData) {
        var defered = $q.defer();
        $http.get(REST_SERVICE_URI + 'api/notes/byname?notename=' + findData.Name)
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