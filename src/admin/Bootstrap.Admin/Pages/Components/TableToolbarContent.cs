using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Linq;

namespace Bootstrap.Admin.Pages.Components
{
    /// <summary>
    /// Table Toolbar 按鈕呈现組件
    /// </summary>
    public class TableToolbarContent : ComponentBase
    {
        /// <summary>
        /// 獲得/設置 Table Toolbar 實例
        /// </summary>
        [CascadingParameter]
        protected TableToolbarBase? Toolbar { get; set; }

        /// <summary>
        /// 渲染組件方法
        /// </summary>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            // 渲染正常按鈕
            if (Toolbar != null && Toolbar.Buttons.Any())
            {
                // 渲染 Toolbar 按鈕
                //<div class="toolbar btn-group">
                //  <button type="button" class="btn btn-success"><i class="fa fa-plus" aria-hidden="true"></i><span>新增</span></button>
                //  <button type="button" class="btn btn-danger"><i class="fa fa-remove" aria-hidden="true"></i><span>刪除</span></button>
                //  <button type="button" class="btn btn-primary"><i class="fa fa-pencil" aria-hidden="true"></i><span>編輯</span></button>
                //</div>
                var index = 0;
                builder.OpenElement(index++, "div");
                builder.AddAttribute(index++, "class", "toolbar btn-group");
                foreach (var button in Toolbar.Buttons)
                {
                    builder.OpenElement(index++, "button");
                    builder.AddAttribute(index++, "type", "button");
                    builder.AddMultipleAttributes(index++, button.AdditionalAttributes);
                    builder.AddAttribute(index++, "onclick", button.OnClick);

                    // icon
                    builder.OpenElement(index++, "i");

                    // class="fa fa-plus" aria-hidden="true"
                    builder.AddAttribute(index++, "class", button.Icon);
                    builder.AddAttribute(index++, "aria-hidden", "true");
                    builder.CloseElement();

                    // span
                    builder.OpenElement(index++, "span");
                    builder.AddContent(index++, button.Title);
                    builder.CloseElement();
                    builder.CloseElement();
                }
                builder.CloseElement();

                // 渲染移動版按鈕
                //<div class="gear btn-group">
                //  <button class="btn btn-secondary dropdown-toggle" data-toggle="dropdown" type="button"><i class="fa fa-gear"></i></button>
                //  <div class="dropdown-menu">
                //      <div class="dropdown-item" title="新增" @onclick="Add" asp-auth="add"><i class="fa fa-plus"></i></div>
                //      <div class="dropdown-item" title="刪除" @onclick="Delete" asp-auth="del"><i class="fa fa-remove"></i></div>
                //      <div class="dropdown-item" title="編輯" @onclick="Edit" asp-auth="edit"><i class="fa fa-pencil"></i></div>
                //  </div>
                //</div>
                builder.OpenElement(index++, "div");
                builder.AddAttribute(index++, "class", "gear btn-group");

                builder.OpenElement(index++, "button");
                builder.AddAttribute(index++, "class", "btn btn-secondary dropdown-toggle");
                builder.AddAttribute(index++, "data-toggle", "dropdown");
                builder.AddAttribute(index++, "type", "button");

                // i
                builder.OpenElement(index++, "i");
                builder.AddAttribute(index++, "class", "fa fa-gear");
                builder.CloseElement();
                builder.CloseElement(); // end button

                // div dropdown-menu
                builder.OpenElement(index++, "div");
                builder.AddAttribute(index++, "class", "dropdown-menu");

                foreach (var button in Toolbar.Buttons)
                {
                    builder.OpenElement(index++, "div");
                    builder.AddAttribute(index++, "class", "dropdown-item");
                    builder.AddAttribute(index++, "title", button.Title);
                    builder.AddAttribute(index++, "onclick", EventCallback.Factory.Create(button, button.OnClick));

                    // icon
                    builder.OpenElement(index++, "i");

                    // class="fa fa-plus" aria-hidden="true"
                    builder.AddAttribute(index++, "class", button.Icon);
                    builder.AddAttribute(index++, "aria-hidden", "true");
                    builder.CloseElement(); // end i

                    builder.CloseElement(); // end div
                }
                builder.CloseElement(); // end dropdown-menu
                builder.CloseElement(); // end div
            }
        }
    }
}
