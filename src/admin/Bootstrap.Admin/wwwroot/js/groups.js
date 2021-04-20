$(function () {
    var $dialogUser = $("#dialogUser");
    var $dialogUserHeader = $('#myUserModalLabel');
    var $dialogUserForm = $('#userForm');
    var $dialogRole = $('#dialogRole');
    var $dialogRoleHeader = $('#myRoleModalLabel');
    var $dialogRoleForm = $('#roleForm');

    $('table').lgbTable({
        url: Group.url,
        dataBinder: {
            map: {
                Id: "#groupID",
                GroupCode: "#groupCode",
                GroupName: "#groupName",
                Description: "#groupDesc"
            },
            events: {
                '#btn_assignRole': function (row) {
                    $.bc({
                        id: row.Id, url: Role.url, query: { type: "group" }, method: "post", htmlTemplate: CheckboxHtmlTemplate,
                        callback: function (result) {
                            var htmlTemplate = this.htmlTemplate;
                            var html = $.map(result, function (element, index) {
                                return $.format(htmlTemplate, element.Id, element.RoleName, element.Checked, element.Description);
                            }).join('');
                            $dialogRoleHeader.text($.format('{0}-角色授權窗口', row.GroupName));
                            $dialogRoleForm.html(html).find('[data-toggle="tooltip"]').each(function (index, label) {
                                if (label.title === "") label.title = "未設置";
                            }).tooltip();
                            $dialogRoleForm.find('.form-checkbox').lgbCheckbox();
                            $dialogRole.modal('show');
                        }
                    });
                },
                '#btn_assignUser': function (row) {
                    $.bc({
                        id: row.Id, url: User.url, query: { type: "group" }, method: "post", htmlTemplate: CheckboxHtmlTemplate,
                        callback: function (result) {
                            var htmlTemplate = this.htmlTemplate;
                            var html = $.map(result, function (element, index) {
                                return $.format(htmlTemplate, element.Id, element.DisplayName, element.Checked, element.UserName);
                            }).join('');
                            $dialogUserHeader.text($.format('{0}-用户授權窗口', row.GroupName));
                            $dialogUserForm.html(html).find('[data-toggle="tooltip"]').each(function (index, label) {
                                if (label.title === "") label.title = "未設置";
                            }).tooltip();
                            $dialogUserForm.find('.form-checkbox').lgbCheckbox();
                            $dialogUser.modal('show');
                        }
                    });
                },
                '#btnSubmitRole': function (row) {
                    var groupId = row.Id;
                    var roleIds = $dialogRole.find('input:checked').map(function (index, element) {
                        return $(element).val();
                    }).toArray();
                    $.bc({ id: groupId, url: Group.url, method: "put", data: roleIds, query: { type: "role" }, title: Role.title, modal: '#dialogRole' });
                },
                '#btnSubmitUser': function (row) {
                    var groupId = row.Id;
                    var userIds = $dialogUser.find(':checked').map(function (index, element) {
                        return $(element).val();
                    }).toArray();
                    $.bc({ id: groupId, url: Group.url, method: "put", data: userIds, query: { type: "user" }, title: User.title, modal: '#dialogUser' });
                }
            }
        },
        smartTable: {
            sortName: 'GroupName',
            queryParams: function (params) { return $.extend(params, { groupName: $("#txt_search_name").val(), description: $("#txt_group_desc").val() }); },           //傳递参数（*）
            columns: [
                { title: "部門編碼", field: "GroupCode", sortable: true },
                { title: "部門名稱", field: "GroupName", sortable: true },
                { title: "部門描述", field: "Description", sortable: false }
            ],
            exportOptions: {
                fileName: "部門資料",
                ignoreColumn: [0, 3]
            }
        }
    });
});