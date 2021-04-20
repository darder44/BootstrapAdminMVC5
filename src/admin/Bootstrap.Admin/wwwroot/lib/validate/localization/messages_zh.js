(function( factory ) {
	if ( typeof define === "function" && define.amd ) {
		define( ["jquery", "../jquery.validate"], factory );
	} else if (typeof module === "object" && module.exports) {
		module.exports = factory( require( "jquery" ) );
	} else {
		factory( jQuery );
	}
}(function( $ ) {

/*
 * Translated default messages for the jQuery validation plugin.
 * Locale: ZH (Chinese, 中文 (Zhōngwén), 汉语, 漢語)
 */
$.extend( $.validator.messages, {
	required: "这是必填字段",
	remote: "请修正此字段",
	email: "请輸入有效的电子邮件地址",
	url: "请輸入有效的網址",
	date: "请輸入有效的日期",
	dateISO: "请輸入有效的日期 (YYYY-MM-DD)",
	number: "请輸入有效的数字",
	digits: "只能輸入数字",
	creditcard: "请輸入有效的信用卡号碼",
	equalTo: "你的輸入不相同",
	extension: "请輸入有效的後缀",
	maxlength: $.validator.format( "最多可以輸入 {0} 個字符" ),
	minlength: $.validator.format( "最少要輸入 {0} 個字符" ),
	rangelength: $.validator.format( "请輸入长度在 {0} 到 {1} 之间的字符串" ),
	range: $.validator.format( "请輸入范围在 {0} 到 {1} 之间的数值" ),
	step: $.validator.format( "请輸入 {0} 的整数倍值" ),
	max: $.validator.format( "请輸入不大于 {0} 的数值" ),
	min: $.validator.format( "请輸入不小于 {0} 的数值" )
} );
return $;
}));