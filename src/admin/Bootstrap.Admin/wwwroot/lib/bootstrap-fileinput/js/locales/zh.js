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
        filePlural: '个文件',
        browseLabel: '选择 &hellip;',
        removeLabel: '移除',
        removeTitle: '清除选中文件',
        cancelLabel: '取消',
        cancelTitle: '取消进行中的上傳',
        uploadLabel: '上傳',
        uploadTitle: '上傳选中文件',
        msgNo: '没有',
        msgNoFilesSelected: '未选择文件',
        msgCancelled: '取消',
        msgPlaceholder: '选择 {files}...',
        msgZoomModalHeading: '详细預覽',
        msgFileRequired: '必须选择一个文件上傳.',
        msgSizeTooSmall: '文件 "{name}" (<b>{size} KB</b>) 必须大于限定大小 <b>{minSize} KB</b>.',
        msgSizeTooLarge: '文件 "{name}" (<b>{size} KB</b>) 超过了允许大小 <b>{maxSize} KB</b>.',
        msgFilesTooLess: '你必须选择最少 <b>{n}</b> {files} 来上傳. ',
        msgFilesTooMany: '选择的上傳文件个数 <b>({n})</b> 超出最大文件的限制个数 <b>{m}</b>.',
        msgFileNotFound: '文件 "{name}" 未找到!',
        msgFileSecured: '安全限制，为了防止读取文件 "{name}".',
        msgFileNotReadable: '文件 "{name}" 不可读.',
        msgFilePreviewAborted: '取消 "{name}" 的預覽.',
        msgFilePreviewError: '读取 "{name}" 時出现了一个錯誤.',
        msgInvalidFileName: '文件名 "{name}" 包含非法字符.',
        msgInvalidFileType: '不正确的类型 "{name}". 只支持 "{types}" 类型的文件.',
        msgInvalidFileExtension: '不正确的文件擴展名 "{name}". 只支持 "{extensions}" 的文件擴展名.',
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
        msgUploadAborted: '該文件上傳被中止',
        msgUploadThreshold: '处理中...',
        msgUploadBegin: '正在初始化...',
        msgUploadEnd: '完成',
        msgUploadEmpty: '無效的文件上傳.',
        msgUploadError: '上傳出錯',
        msgValidationError: '验证錯誤',
        msgLoading: '加載第 {index} 文件 共 {files} &hellip;',
        msgProgress: '加載第 {index} 文件 共 {files} - {name} - {percent}% 完成.',
        msgSelected: '{n} {files} 选中',
        msgFoldersNotAllowed: '只支持拖拽文件! 跳过 {n} 拖拽的文件夹.',
        msgImageWidthSmall: '圖像文件的"{name}"的宽度必须是至少{size}像素.',
        msgImageHeightSmall: '圖像文件的"{name}"的高度必须至少为{size}像素.',
        msgImageWidthLarge: '圖像文件"{name}"的宽度不能超过{size}像素.',
        msgImageHeightLarge: '圖像文件"{name}"的高度不能超过{size}像素.',
        msgImageResizeError: '無法获取的圖像尺寸调整。',
        msgImageResizeException: '调整圖像大小時發生錯誤。<pre>{errors}</pre>',
        msgAjaxError: '{operation} 發生錯誤. 請重試!',
        msgAjaxProgressError: '{operation} 失败',
        ajaxOperations: {
            deleteThumb: '删除文件',
            uploadThumb: '上傳文件',
            uploadBatch: '批量上傳',
            uploadExtra: '表單資料上傳'
        },
        dropZoneTitle: '拖拽文件到这里 &hellip;<br>支持多文件同時上傳',
        dropZoneClickTitle: '<br>(或点击{files}按钮选择文件)',
        fileActionSettings: {
            removeTitle: '删除文件',
            uploadTitle: '上傳文件',
            uploadRetryTitle: '重試',
            zoomTitle: '查看详情',
            dragTitle: '移动 / 重置',
            indicatorNewTitle: '没有上傳',
            indicatorSuccessTitle: '上傳',
            indicatorErrorTitle: '上傳錯誤',
            indicatorLoadingTitle: '上傳 ...'
        },
        previewZoomButtonTitles: {
            prev: '預覽上一个文件',
            next: '預覽下一个文件',
            toggleheader: '縮放',
            fullscreen: '全屏',
            borderless: '無邊界模式',
            close: '關閉當前預覽'
        }
    };
})(window.jQuery);
