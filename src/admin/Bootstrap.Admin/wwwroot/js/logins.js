// 登入日誌
$(function () {
    var apiUrl = "api/Login";
    var $table = $('.card-body table');
    $table.smartTable({
        url: apiUrl,
        showToggle: false,
        showRefresh: false,
        showColumns: false,
        sortName: 'LoginTime',
        sortOrder: "desc",
        toolbar: false,
        search: false,
        queryParams: function (params) { return $.extend(params, { startTime: $("#txt_operate_start").val(), endTime: $("#txt_operate_end").val(), loginIp: $('#txt_ip').val() }); },
        columns: [
            {
                title: "序号", formatter: function (value, row, index) {
                    var options = $table.bootstrapTable('getOptions');
                    return options.pageSize * (options.pageNumber - 1) + index + 1;
                }
            },
            { title: "登入名稱", field: "UserName" },
            { title: "登入時間", field: "LoginTime" },
            { title: "主机", field: "Ip" },
            { title: "登入地点", field: "City" },
            { title: "浏览器", field: "Browser" },
            { title: "操作系統", field: "OS" },
            {
                title: "登入结果", field: "Result", formatter: function (value, row, index) {
                    var css = value === "登入成功" ? "success" : "danger";
                    var icon = css === "success" ? "check" : "remove";
                    return $.format('<span class="badge badge-md badge-{0}"><i class="fa fa-{2}"></i>{1}</span>', css, value, icon);
                }
            }
        ]
    });
});