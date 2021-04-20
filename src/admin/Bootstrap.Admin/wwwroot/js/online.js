$(function () {
    var apiUrl = "api/OnlineUsers";
    var $table = $('table').smartTable({
        url: apiUrl,
        method: "get",
        sidePagination: "client",
        toolbar: false,
        search: false,
        showToggle: false,
        showRefresh: false,
        showColumns: false,
        showAdvancedSearchButton: false,
        columns: [
            {
                title: "序号", formatter: function (value, row, index) {
                    var options = $table.bootstrapTable('getOptions');
                    return options.pageSize * (options.pageNumber - 1) + index + 1;
                }
            },
            {
                title: "會话Id", field: "ConnectionId"
            },
            { title: "登入名稱", field: "UserName" },
            { title: "顯示名稱", field: "DisplayName" },
            { title: "登入時間", field: "FirstAccessTime" },
            { title: "訪問時間", field: "LastAccessTime" },
            { title: "请求方式", field: "Method" },
            { title: "主机", field: "Ip" },
            { title: "登入地点", field: "Location" },
            { title: "浏览器", field: "Browser" },
            { title: "操作系統", field: "OS" },
            { title: "訪問地址", field: "RequestUrl" },
            {
                title: "历史地址", field: "ConnectionId", formatter: function (value, row, index, field) {
                    return $.format('<button type="button" class="btn btn-info" data-id="{0}" data-toggle="popover" data-trigger="focus" data-html="true" data-title="訪問紀錄"><i class="fa fa-info"></i><span>明詳</span></button>', value);
                }
            }
        ]
    }).on('click', 'button[data-id]', function () {
        var $this = $(this);
        if (!$this.data($.fn.popover.Constructor.DATA_KEY)) {
            var id = $this.attr('data-id');
            var data = $table.bootstrapTable('getData');
            var row = data.filter(function (v) {
                return v.ConnectionId === id;
            });
            var content = row[0].RequestUrls.map(function (item) {
                return $.format("<tr><td>{0}</td><td>{1}</td></tr>", item.Key, item.Value);
            }).join('');
            content = content === '' ?
                '已断开' :
                $.format("<div class='bootstrap-table' style='margin: 4px 0;'><div class='fixed-table-container'><div class='fixed-table-body'><table class='table table-bordered table-hover'><thead><tr><th class='p-1'><b>訪問時間</b></th><th class='p-1'>訪問地址</th></tr></thead><tbody>{0}</tbody></table></div></div></div>", content);
            $this.popover({ content: content, sanitize: false, placement: $(window).width() < 768 ? 'top' : 'left' });
            $this.popover('show');
        }
    }).on('mouseup', 'button[data-id]', function () {
        $(this).focus();
    });

    $('#refreshUsers').tooltip().on('click', function () {
        $table.bootstrapTable('refresh');
    });
});