$(function () {
    var url = 'api/Logs';
    var $data = $('#requestData');
    var $dialog = $('#dialogRequestData');

    $('.card-body table').smartTable({
        url: url,
        sortName: 'LogTime',
        sortOrder: 'desc',
        toolbar: false,
        search: false,
        queryParams: function (params) { return $.extend(params, { operateType: $("#txt_operate_type").val(), OperateTimeStart: $("#txt_operate_start").val(), OperateTimeEnd: $("#txt_operate_end").val() }); },
        columns: [
            { title: "操作類型", field: "CRUD", sortable: true },
            { title: "用户名稱", field: "UserName", sortable: true },
            { title: "操作時間", field: "LogTime", sortable: true },
            { title: "登入主機", field: "Ip", sortable: true },
            { title: "操作地点", field: "City" },
            { title: "瀏覽器", field: "Browser" },
            { title: "操作系統", field: "OS" },
            { title: "操作頁面", field: "RequestUrl", sortable: true },
            {
                title: "請求資料", field: "RequestData", formatter: function (value, row, index) {
                    return '<button class="detail btn btn-info"><i class="fa fa-info"></i><span>明詳</span></button>';
                },
                events: {
                    'click .detail': function (e, value, row, index) {
                        $data.html($.syntaxHighlight($.safeHtml(row.RequestData)));
                        $dialog.modal('show');
                    }
                }
            }
        ],
        exportOptions: {
            fileName: "操作日誌資料",
            ignoreColumn: [8]
        }
    });
});