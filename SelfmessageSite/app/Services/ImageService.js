app.factory("ImageService",
    function($http, $log, $q) {

        var REST_SERVICE_URI = "http://localhost:9343/";
        var IMAGE_API_URI = REST_SERVICE_URI + "api/images/";

        var factory = {
            deleteImage: deleteImage
        };

        return factory;

        function deleteImage(imageUrl) {
            $log.debug("deleteImage ImageService");

            var defered = $q.defer();

            $http.delete(imageUrl)
                .then(
                function (response) {
                    defered.resolve(response.data);
                },
                function (response) {
                    return defered.reject(response);
                });
            return defered.promise;
        }
    });