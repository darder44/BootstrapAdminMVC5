$(function () {
    var url = 'api/SQL';

    $('.card-body table').smartTable({
        url: url,
        sortName: 'LogTime',
        sortOrder: 'desc',
        toolbar: false,
        search: false,
        queryParams: function (params) { return $.extend(params, { UserName: $("#txt_username").val(), OperateTimeStart: $("#txt_operate_start").val(), OperateTimeEnd: $("#txt_operate_end").val() }); },
        columns: [
            { title: "所属用户", field: "UserName", sortable: true },
            { title: "执行時間", field: "LogTime", sortable: true },
            { title: "腳本内容", field: "SQL", sortable: false }
        ],
        exportOptions: {
            fileName: "SQL日誌資料"
        }
    });
});
