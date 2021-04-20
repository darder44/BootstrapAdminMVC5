using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Bootstrap.Admin.Pages.Components
{
    /// <summary>
    /// 
    /// </summary>
    public class RequiredValidator : ValidatorComponentBase
    {
        /// <summary>
        /// 
        /// </summary>
        public RequiredValidator()
        {
            ErrorMessage = "这是必填字串";
        }

        /// <summary>
        /// 獲得/設置 是否允許空字符串 預設 false 不允許
        /// </summary>
        [Parameter]
        public bool AllowEmptyString { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyValue"></param>
        /// <param name="context"></param>
        /// <param name="results"></param>
        public override void Validate(object? propertyValue, ValidationContext context, List<ValidationResult> results)
        {
            var val = propertyValue?.ToString() ?? "";
            if (!AllowEmptyString && val == string.Empty)
            {
                results.Add(new ValidationResult(ErrorMessage, string.IsNullOrEmpty(context.MemberName) ? Enumerable.Empty<string>() : new string[] { context.MemberName }));
            }
        }
    }
}
