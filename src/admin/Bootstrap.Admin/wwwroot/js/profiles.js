$(function () {
    var $headerIcon = $('.userinfo img, .nav-header img');
    var preIcon = $headerIcon.attr('src');
    var $file = $('#fileIcon');
    var defFileName = $file.attr('data-file');
    $file.fileinput({
        uploadUrl: $.formatUrl(Profiles.url),
        deleteUrl: $.formatUrl(Profiles.del),
        browseOnZoneClick: true,
        theme: 'fa',
        language: 'zh',
        maxFileSize: 5000,
        allowedFileExtensions: ['jpg', 'png', 'bmp', 'gif', 'jpeg'],
        initialPreview: [
            preIcon
        ],
        initialPreviewConfig: [
            { caption: "", size: $file.attr('data-init'), showZoom: true, showRemove: defFileName !== 'default.jpg', key: defFileName }
        ],
        initialPreviewAsData: true,
        overwriteInitial: true,
        dropZoneTitle: "請选择頭像",
        msgPlaceholder: "請选择頭像",
        fileActionSettings: { showUpload: false }
    }).on('fileuploaded', function (event, data, previewId, index) {
        var url = data.response.initialPreview[0];
        if (!!url === true) $headerIcon.attr('src', url);
    }).on('filebeforedelete', function (e, key) {
        if (key === "default.jpg") return true;
        return new Promise(function (resolve, reject) {
            var swalDeleteOptions = {
                title: "您確定要刪除吗？",
                html: '您確定要刪除选中的所有資料吗',
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: '#dc3545',
                cancelButtonColor: '#6c757d',
                confirmButtonText: "我要刪除",
                cancelButtonText: "取消"
            };
            swal(swalDeleteOptions).then(function (result) {
                if (result.value) {
                    resolve(false);
                    $file.fileinput('default');
                }
            });
        });
    }).on('filedeleted', function (event, key, jqXHR, data) {
        $headerIcon.attr('src', $.formatUrl('images/uploader/default.jpg'));
    });

    $.fn.fileinput.Constructor.prototype.default = function () {
        $.extend(this, {
            initialPreview: [
                $.formatUrl('images/uploader/default.jpg')
            ],
            initialPreviewConfig: [
                { caption: "", size: 7195, showZoom: true, showRemove: false, key: 'default.jpg' }
            ]
        });
        this._initPreviewCache();
        this._initPreview(true);
        this._initPreviewActions();
        this._initZoom();
    };

    $.footer();

    var dataBinder = new DataEntity({
        Password: "#currentPassword",
        NewPassword: "#newPassword",
        DisplayName: "#displayName",
        UserName: "#userName",
        Css: "#css",
        App: '#app'
    });

    $('button[data-method]').on('click', function (e) {
        var $this = $(this);
        if ($this.parent().attr("data-admin") === "True") return false;
        var data = dataBinder.get();
        switch ($this.attr('data-method')) {
            case 'password':
                data.UserStatus = 'ChangePassword';
                $.bc({ url: Profiles.url, method: "put", data: data, title: "更改密碼" });
                break;
            case 'user':
                data.UserStatus = 'ChangeDisplayName';
                $.bc({
                    url: Profiles.url, method: "put", data: data, title: "修改用户顯示名稱",
                    callback: function (result) {
                        if (result) {
                            $('.username').text(data.DisplayName);
                        }
                    }
                });
                break;
            case 'profileCss':
                data.UserStatus = 'ChangeTheme';
                $.bc({
                    url: Profiles.url, method: "put", data: data, title: "保存樣式", callback: function (result) {
                        if (result) {
                            window.setTimeout(function () { window.location.reload(true); }, 1000);
                        }
                    }
                });
                break;
            case 'app':
                data.UserStatus = 'SaveApp';
                $.bc({
                    url: Profiles.url, method: "put", data: data, title: "保存應用", callback: function (result) {
                        if (result) {
                            window.setTimeout(function () { window.location.reload(true); }, 1000);
                        }
                    }
                });
                break;
        }
    });
    if ($('[enctype="multipart/form-data"]').length === 0) {
        $('.card-img').removeClass('d-none');
    }
    $('#css').dropdown('val');
    $('#app').dropdown('val');
});