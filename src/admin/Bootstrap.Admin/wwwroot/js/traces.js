$(function () {
    var url = 'api/Traces';

    $('.card-body table').smartTable({
        url: url,
        sortName: 'LogTime',
        sortOrder: 'desc',
        toolbar: false,
        search: false,
        queryParams: function (params) { return $.extend(params, { OperateTimeStart: $("#txt_operate_start").val(), OperateTimeEnd: $("#txt_operate_end").val(), AccessIP: $('#txt_ip').val() }); },
        columns: [
            { title: "用户名稱", field: "UserName", sortable: true },
            { title: "操作時間", field: "LogTime", sortable: true },
            { title: "登入主機", field: "Ip", sortable: true },
            { title: "操作地点", field: "City", sortable: true },
            { title: "瀏覽器", field: "Browser", sortable: true },
            { title: "操作系統", field: "OS", sortable: true },
            { title: "操作頁面", field: "RequestUrl", sortable: true },
            { title: "Referer", field: "Referer", sortable: false }
        ],
        exportOptions: {
            fileName: "訪問日誌資料",
            ignoreColumn: [8]
        }
    });
});