$(function () {
    var apiUrl = "api/New";
    var $table = $('table').smartTable({
        url: apiUrl,
        sidePagination: "client",
        toolbar: false,
        search: false,
        toolbar: false,
        showToggle: false,
        showRefresh: false,
        showColumns: false,
        showAdvancedSearchButton: false,
        columns: [
            { title: "登入名稱", field: "UserName" },
            { title: "顯示名稱", field: "DisplayName" },
            { title: "說明信息", field: "Description" },
            { title: "注册時間", field: "RegisterTime" },
            {
                title: "操作", field: "Id", formatter: function (value, row, index, field) {
                    return $.format('<div class="btn-group"><button class="btn btn-success" data-toggle="tooltip" data-id="{0}" data-result="ApproveUser" title="同意授權"><i class="fa fa-check"></i><span>同意</span></button><button class="btn btn-danger" data-toggle="tooltip" data-id="{0}" data-result="RejectUser" title="拒绝授權"><i class="fa fa-remove"></i><span>拒绝</span></button></div>', value);
                }
            }
        ]
    }).on('click', 'button[data-id]', function () {
        var $this = $(this);
        var id = $this.attr('data-id');
        var result = $this.attr('data-result');
        $.bc({
            url: apiUrl, method: "put", data: { Id: id, UserStatus: result }, title: result === "ApproveUser" ? "授權用户" : "拒绝用户",
            callback: function (result) {
                if (!result) return;
                $table.bootstrapTable('refresh');
                $('.header .nav').reloadWidget();
            }
        });
    });

    $('#refreshUsers').tooltip().on('click', function (e) {
        e.preventDefault();
        $table.bootstrapTable('refresh');
    });
});