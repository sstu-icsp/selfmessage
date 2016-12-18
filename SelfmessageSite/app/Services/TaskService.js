app.factory("TaskService",
    function ($q, $log, $http, $httpParamSerializerJQLike) {

        var REST_SERVICE_URI = "http://localhost:9343/";


        var factory = {
            getAllData: getAllData,
            sendData: sendData
            //deleteNote: deleteNote,
            //searchData: searchData
        };

        return factory;


        //Вывод ВСЕХ записей
        function getAllData() {
            var defered = $q.defer();
            $http.get(REST_SERVICE_URI + 'api/tasks')
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


        //Уделанеи записи по id
        //api/notes/{id}
       /* function deleteNote(id) {
            var defered = $q.defer();
            $http.delete(REST_SERVICE_URI + 'api/notes/' + id)
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

        */
        //Добавление новой записи
        function sendData(sendData) {
            var defered = $q.defer();
            $http.post(REST_SERVICE_URI + "api/tasks", sendData)
             .then(
                        function (response) {
                            $log.debug(response);
                            defered.resolve(response.data);
                        },
                        function (errorResponse) {
                            $log.error("TaskService sendData error");
                            $log.error(errorResponse);
                            defered.reject(errorResponse);
                        });
            return defered.promise;
        };




    });