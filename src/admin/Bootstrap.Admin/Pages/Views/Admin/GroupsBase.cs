using Bootstrap.Admin.Pages.Components;
using Bootstrap.DataAccess;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bootstrap.Admin.Pages.Views.Admin.Components
{
    /// <summary>
    /// 部門維護組件
    /// </summary>
    public class GroupsBase : QueryPageBase<Group>
    {
        /// <summary>
        /// 查詢方法
        /// </summary>
        /// <param name="options"></param>
        protected override QueryData<Group> Query(QueryPageOptions options)
        {
            var data = GroupHelper.Retrieves();
            if (!string.IsNullOrEmpty(QueryModel.GroupName)) data = data.Where(d => d.GroupName.Contains(QueryModel.GroupName, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrEmpty(QueryModel.Description)) data = data.Where(d => d.Description != null && d.Description.Contains(QueryModel.Description, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrEmpty(options.SearchText)) data = data.Where(d => d.GroupName.Contains(options.SearchText, StringComparison.OrdinalIgnoreCase) || (d.Description ?? "").Contains(options.SearchText, StringComparison.OrdinalIgnoreCase));

            // sort
            data = options.SortName switch
            {
                nameof(Group.GroupName) => options.SortOrder == SortOrder.Asc ? data.OrderBy(d => d.GroupName) : data.OrderByDescending(d => d.GroupName),
                nameof(Group.GroupCode) => options.SortOrder == SortOrder.Asc ? data.OrderBy(d => d.GroupCode) : data.OrderByDescending(d => d.GroupCode),
                nameof(Group.Description) => options.SortOrder == SortOrder.Asc ? data.OrderBy(d => d.Description) : data.OrderByDescending(d => d.Description),
                _ => data
            };

            var totalCount = data.Count();
            var items = data.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems);
            return new QueryData<Group>() { Items = items, TotalCount = totalCount, PageIndex = options.PageIndex, PageItems = options.PageItems };
        }

        /// <summary>
        /// 保存方法
        /// </summary>
        protected override bool Save(Group group) => GroupHelper.Save(group);

        /// <summary>
        /// 刪除方法
        /// </summary>
        protected override bool Delete(IEnumerable<Group> groups) => GroupHelper.Delete(groups.Select(item => item.Id ?? ""));

        /// <summary>
        /// 重置查詢方法
        /// </summary>
        protected void ResetSearch()
        {
            QueryModel.GroupName = "";
            QueryModel.Description = "";
        }

        /// <summary>
        /// 獲得/設置 Modal 實例
        /// </summary>
        protected AssignModalBase<User>? AssignUserModal { get; set; }

        /// <summary>
        /// 彈窗分配角色方法
        /// </summary>
        protected void AssignUsers()
        {
            // 選單对角色授權操作
            if (EditPage != null)
            {
                if (EditPage.SelectedItems.Count() != 1)
                {
                    ShowMessage("用户授權", "請选择一個部門", ToastCategory.Information);
                }
                else
                {
                    var groupId = EditPage.SelectedItems.First().Id;
                    if (!string.IsNullOrEmpty(groupId))
                    {
                        var users = UserHelper.RetrievesByGroupId(groupId);
                        AssignUserModal?.Update(users);
                    }
                }
            }
        }

        /// <summary>
        /// 保存授權部門方法
        /// </summary>
        protected void SaveUsers(IEnumerable<User> users)
        {
            bool ret = false;
            if (EditPage != null && EditPage.SelectedItems.Any())
            {
                var groupId = EditPage.SelectedItems.First().Id;
                var userIds = users.Where(r => r.Checked == "checked").Select(r => r.Id ?? "");
                if (!string.IsNullOrEmpty(groupId)) ret = UserHelper.SaveByGroupId(groupId, userIds);
            }
            ShowMessage("用户授權", ret ? "保存成功" : "保存失败", ret ? ToastCategory.Success : ToastCategory.Error);
        }

        /// <summary>
        /// 选择框点击時調用此方法
        /// </summary>
        /// <param name="item"></param>
        /// <param name="check"></param>
        protected void OnUserClick(User item, bool check)
        {
            item.Checked = check ? "checked" : "";
        }

        /// <summary>
        /// 設置初始化值
        /// </summary>
        protected CheckBoxState SetUserCheck(User item) => item.Checked == "checked" ? CheckBoxState.Checked : CheckBoxState.UnChecked;

        /// <summary>
        /// IJSRuntime 接口實例
        /// </summary>
        [Inject]
        protected IJSRuntime? JSRuntime { get; set; }

        /// <summary>
        /// 顯示提示信息
        /// </summary>
        /// <param name="title"></param>
        /// <param name="text"></param>
        /// <param name="cate"></param>
        protected void ShowMessage(string title, string text, ToastCategory cate = ToastCategory.Success) => JSRuntime?.ShowToast(title, text, cate);

        /// <summary>
        /// 獲得/設置 Modal 實例
        /// </summary>
        protected AssignModalBase<Role>? AssignRoleModal { get; set; }

        /// <summary>
        /// 彈窗分配角色方法
        /// </summary>
        protected void AssignRoles()
        {
            // 選單对角色授權操作
            if (EditPage != null)
            {
                if (EditPage.SelectedItems.Count() != 1)
                {
                    ShowMessage("角色授權", "請选择一個部門", ToastCategory.Information);
                }
                else
                {
                    var groupId = EditPage.SelectedItems.First().Id;
                    if (!string.IsNullOrEmpty(groupId))
                    {
                        var roles = RoleHelper.RetrievesByGroupId(groupId);
                        AssignRoleModal?.Update(roles);
                    }
                }
            }
        }

        /// <summary>
        /// 保存授權角色方法
        /// </summary>
        protected void SaveRoles(IEnumerable<Role> roles)
        {
            bool ret = false;
            if (EditPage != null && EditPage.SelectedItems.Any())
            {
                var groupId = EditPage.SelectedItems.First().Id;
                var roleIds = roles.Where(r => r.Checked == "checked").Select(r => r.Id ?? "");
                if (!string.IsNullOrEmpty(groupId)) ret = RoleHelper.SaveByGroupId(groupId, roleIds);
            }
            ShowMessage("角色授權", ret ? "保存成功" : "保存失败", ret ? ToastCategory.Success : ToastCategory.Error);
        }

        /// <summary>
        /// 选择框点击時調用此方法
        /// </summary>
        /// <param name="item"></param>
        /// <param name="check"></param>
        protected void OnClick(Role item, bool check)
        {
            item.Checked = check ? "checked" : "";
        }

        /// <summary>
        /// 設置初始化值
        /// </summary>
        protected CheckBoxState SetCheck(Role item) => item.Checked == "checked" ? CheckBoxState.Checked : CheckBoxState.UnChecked;
    }
}
