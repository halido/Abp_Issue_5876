var $VideoFile = $('#VideoFile');
var uploadVideo = function (file, callbackFn) {
    var formData = new FormData();
    formData.append('file', file);

    $.ajax({
        type: 'POST',
        url: '/api/files/upload',
        data: formData,
        contentType: false,
        processData: false,
        success: function (response) {
            callbackFn(response);
        },
        xhr: function(){
//upload Progress
            var xhr = $.ajaxSettings.xhr();
            if (xhr.upload) {
                xhr.upload.addEventListener('progress', function(event) {
                    var percent = 0;
                    var position = event.loaded || event.position;
                    var total = event.total;
                    if (event.lengthComputable)
                    {
                        percent = Math.ceil(position / total * 100);
                    }
//update progressbar
                    $("#fileProgress").css("width", + percent +"%");
                }, true);
            }
            return xhr;
        },
        error:function (xhr, ajaxOptions, thrownError) {
            abp.notify.error( xhr.responseJSON.error.message);

        }


    });
};
var setVideoSrc = function (file) {
    // Do nothing , this is for test app :) 
};

$VideoFile.change(function () {
    if (!$VideoFile.prop('files').length) {
        return;
    }
    var file = $VideoFile.prop('files')[0];
    uploadVideo(file,setVideoSrc);
});
