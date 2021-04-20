/*!
 * FileInput Chinese Translations
 *
 * This file must be loaded after 'fileinput.js'. Patterns in braces '{}', or
 * any HTML markup tags in the messages must not be converted or translated.
 *
 * @see http://github.com/kartik-v/bootstrap-fileinput
 * @author kangqf <kangqingfei@gmail.com>
 *
 * NOTE: this file must be saved in UTF-8 encoding.
 */
(function ($) {
    "use strict";

    $.fn.fileinputLocales['zh'] = {
        fileSingle: '文件',
        filePlural: '個文件',
        browseLabel: '选择 &hellip;',
        removeLabel: '移除',
        removeTitle: '清除选中文件',
        cancelLabel: '取消',
        cancelTitle: '取消進行中的上传',
        uploadLabel: '上传',
        uploadTitle: '上传选中文件',
        msgNo: '没有',
        msgNoFilesSelected: '未选择文件',
        msgCancelled: '取消',
        msgPlaceholder: '选择 {files}...',
        msgZoomModalHeading: '詳詳预览',
        msgFileRequired: '必须选择一個文件上传.',
        msgSizeTooSmall: '文件 "{name}" (<b>{size} KB</b>) 必须大於限定大小 <b>{minSize} KB</b>.',
        msgSizeTooLarge: '文件 "{name}" (<b>{size} KB</b>) 超過了允許大小 <b>{maxSize} KB</b>.',
        msgFilesTooLess: '你必须选择最少 <b>{n}</b> {files} 来上传. ',
        msgFilesTooMany: '选择的上传文件個数 <b>({n})</b> 超出最大文件的限制個数 <b>{m}</b>.',
        msgFileNotFound: '文件 "{name}" 未找到!',
        msgFileSecured: '安全限制，為了防止讀取文件 "{name}".',
        msgFileNotReadable: '文件 "{name}" 不可讀.',
        msgFilePreviewAborted: '取消 "{name}" 的预览.',
        msgFilePreviewError: '讀取 "{name}" 時出现了一個错误.',
        msgInvalidFileName: '文件名 "{name}" 包含非法字符.',
        msgInvalidFileType: '不正確的類型 "{name}". 只支持 "{types}" 類型的文件.',
        msgInvalidFileExtension: '不正確的文件扩展名 "{name}". 只支持 "{extensions}" 的文件扩展名.',
        msgFileTypes: {
            'image': 'image',
            'html': 'HTML',
            'text': 'text',
            'video': 'video',
            'audio': 'audio',
            'flash': 'flash',
            'pdf': 'PDF',
            'object': 'object'
        },
        msgUploadAborted: '该文件上传被中止',
        msgUploadThreshold: '处理中...',
        msgUploadBegin: '正在初始化...',
        msgUploadEnd: '完成',
        msgUploadEmpty: '无效的文件上传.',
        msgUploadError: '上传出错',
        msgValidationError: '驗證错误',
        msgLoading: '加载第 {index} 文件 共 {files} &hellip;',
        msgProgress: '加载第 {index} 文件 共 {files} - {name} - {percent}% 完成.',
        msgSelected: '{n} {files} 选中',
        msgFoldersNotAllowed: '只支持拖拽文件! 跳過 {n} 拖拽的文件夹.',
        msgImageWidthSmall: '圖像文件的"{name}"的宽度必须是至少{size}像素.',
        msgImageHeightSmall: '圖像文件的"{name}"的高度必须至少為{size}像素.',
        msgImageWidthLarge: '圖像文件"{name}"的宽度不能超過{size}像素.',
        msgImageHeightLarge: '圖像文件"{name}"的高度不能超過{size}像素.',
        msgImageResizeError: '无法獲取的圖像尺寸调整。',
        msgImageResizeException: '调整圖像大小時發生错误。<pre>{errors}</pre>',
        msgAjaxError: '{operation} 發生错误. 请重試!',
        msgAjaxProgressError: '{operation} 失败',
        ajaxOperations: {
            deleteThumb: '刪除文件',
            uploadThumb: '上传文件',
            uploadBatch: '批量上传',
            uploadExtra: '表單資料上传'
        },
        dropZoneTitle: '拖拽文件到这里 &hellip;<br>支持多文件同時上传',
        dropZoneClickTitle: '<br>(或点击{files}按鈕选择文件)',
        fileActionSettings: {
            removeTitle: '刪除文件',
            uploadTitle: '上传文件',
            uploadRetryTitle: '重試',
            zoomTitle: '查看詳情',
            dragTitle: '移动 / 重置',
            indicatorNewTitle: '没有上传',
            indicatorSuccessTitle: '上传',
            indicatorErrorTitle: '上传错误',
            indicatorLoadingTitle: '上传 ...'
        },
        previewZoomButtonTitles: {
            prev: '预览上一個文件',
            next: '预览下一個文件',
            toggleheader: '缩放',
            fullscreen: '全屏',
            borderless: '无邊界模式',
            close: '關闭當前预览'
        }
    };
})(window.jQuery);
