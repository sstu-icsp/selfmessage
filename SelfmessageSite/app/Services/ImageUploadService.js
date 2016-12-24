app.service("ImageUploadService", function ($http) {

    var uploadUrl = "http://localhost:9343/";

    this.uploadFileToUrl = function(file, noteId) {

        var body = {
            image: new FormData()
        }
        body.image.append("file", file);

        $http.post(uploadUrl + "api/notes/" + noteId + "/images",
                body,
                {
                    transformRequest: angular.identity,
                    headers: { 'Content-Type': undefined }
                })
            .success(function() {
            })
            .error(function() {
            });
    }
});