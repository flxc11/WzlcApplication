/* ******************************************
*	初始化SWFUpload上传控件
* ****************************************** */
function InitImagesUpload(PlaceHolderID, UploadUrl, UploadSize) {
    var SendUrl = UploadUrl;
    var swfu = new SWFUpload({
        upload_url: SendUrl + "?Action=UploadImages&ElementID=" + PlaceHolderID,
        file_post_name: "",
        post_params: { "ASPSESSID": "NONE" },

        file_size_limit: UploadSize,
        file_types: "*.jpg;*.jpge;*.png;*.gif",
        file_types_description: "图片文件",
        file_upload_limit: "0",
        file_queue_error_handler: fileQueueError,
        file_dialog_complete_handler: fileDialogComplete,
        upload_progress_handler: uploadProgress,
        upload_error_handler: uploadError,
        upload_success_handler: uploadSuccess,
        upload_complete_handler: uploadComplete,

        // Button Settings
        button_placeholder_id: 'Upload'+PlaceHolderID,
        button_width: 75,
        button_height: 28,
        button_text: '',
        button_window_mode: SWFUpload.WINDOW_MODE.TRANSPARENT,
        button_cursor: SWFUpload.CURSOR.HAND,

        // Flash Settings
        flash_url: "../lib/swfupload/swfupload.swf",

        custom_settings: {
            upload_target: "show"
        },
        // Debug Settings
        debug: false
    });
}

/* ******************************************
*	返回上传的状态
* ****************************************** */
function uploadSuccess(file, serverData) {
    try {
        var progress = new FileProgress(file, this.customSettings.upload_target);
        var jsonstr = eval('(' + serverData + ')');
        if (jsonstr.msg == '1') {
            var imgArr = jsonstr.msbox.split(",");
            if (imgArr.length == 2) {
                addImage(imgArr[0], imgArr[1], jsonstr.elementid);
            } else {
                addImage(jsonstr.msbox, jsonstr.msbox, jsonstr.elementid);
            }
            progress.setStatus("缩略图创建成功.");
            progress.toggleCancel(false);
        }
    } catch (ex) {
        this.debug(ex);
    }
}

/* ******************************************
*	添加图片列表
* ****************************************** */
function addImage(bigSrc, smallSrc, elementid) {
    var focus_photo = $('#Txt' + elementid).val();
    var newLi = $('<li></li>');
    var claname = '';
    if (focus_photo == '') {
        $('#Txt' + elementid).val(bigSrc);
        claname = 'current';
    }
    var hidValue = $('<input type="hidden" name="Hid' + elementid + '" value="0|' + bigSrc + '|' + smallSrc + '" />').appendTo(newLi);
    var input = $('<div style="margion:0px auto;height:25px"><b>说明：</b><input type="text" name="HText' + elementid + '" value="" class="l-text" style="width:65px"/></div>').appendTo(newLi);
    var newImg = $('<img src="' + smallSrc + '" bigsrc="' + bigSrc + '" elementid="' + elementid + '" onclick="focus_img(this);" class="' + claname + '" />').appendTo(newLi);
    var br = $('<br />').appendTo(newLi);
    var forea = $('<a href="' + bigSrc + '" target="_blank">浏览</a> ').appendTo(newLi);
    var downa = $('<a href="javascript:;" onclick="del_img(this);">删除</a> ').appendTo(newLi);
    var up = $('<a href="javascript:;" onclick="SortUp(this);"><img src="../Lib/Images/Up.png" /></a>').appendTo(newLi);
    var down = $('<a href="javascript:;" onclick="SortDown(this);"><img src="../Lib/Images/Down.png" /></a>').appendTo(newLi);
    newLi.appendTo("#Show" + elementid + " ul");
}
/* ******************************************
*	排序向上
* ****************************************** */
function SortUp(obj) {
    var prevAll = $(obj).parent().prevAll();
    if (prevAll.length == 0) {
        return;
    }
    $(obj).parent().prevAll().eq(0).before($(obj).parent());
}
/* ******************************************
*	排序向下
* ****************************************** */
function SortDown(obj) {
    var nextAll = $(obj).parent().nextAll();
    if (nextAll.length == 0) {
        return;
    }
    $(obj).parent().nextAll().eq(0).after($(obj).parent());
}
/* ******************************************
*	删除LI元素
* ****************************************** */
function del_img(obj) {
    var elementid = $(obj).prevAll("img").eq(0).attr("elementid");
    var focusphoto = $("#Txt" + elementid);
    var smallimg = $(obj).prevAll("img").eq(0).attr("bigsrc");
    $("#Del" + elementid).attr("value", $("#Del" + elementid).val() + "|" + smallimg);
    var node = $(obj).parent(); //要删除的LI节点
    node.remove(); //删除DOM元素
    //检查是否为封面
    if (focusphoto.val() == smallimg) {
        focusphoto.val("");
        var firtimg = $("#Show" + elementid + " ul li img").eq(0);
        firtimg.addClass("current"); //取第一张做为封面
        focusphoto.val(firtimg.attr("bigsrc")); //重新给封面的隐藏域赋值
    }
}

/* ******************************************
*	设置相册封面
* ****************************************** */
function focus_img(obj) {
    $("#Txt" + $(obj).attr("elementid")).val($(obj).attr("bigsrc"));
    $("#Show" + $(obj).attr("elementid") + " ul li img").removeClass("current");
    $(obj).addClass("current");
}

function fileQueueError(file, errorCode, message) {
    try {
        var progress = new FileProgress(file, this.customSettings.progressTarget);
        progress.setError();
        progress.toggleCancel(false);
        if (errorCode === SWFUpload.errorCode_QUEUE_LIMIT_EXCEEDED) {
            errorName = "您选择的文件太多.";
        }
        if (errorName !== "") {
            alert(errorName);
            return;
        }

        switch (errorCode) {
            case SWFUpload.QUEUE_ERROR.ZERO_BYTE_FILE:
                progress.setStatus(file.name + "文件太小");
                progress.toggleCancel(false, this);
                break;
            case SWFUpload.QUEUE_ERROR.FILE_EXCEEDS_SIZE_LIMIT:
                progress.setStatus(file.name + "文件太大");
                progress.toggleCancel(false, this);
                break;
            case SWFUpload.QUEUE_ERROR.INVALID_FILETYPE:
                progress.setStatus(file.name + "文件类型出错");
                progress.toggleCancel(false, this);
                break;
            default:
                if (file !== null) {
                    progress.setStatus("未知错误");
                    progress.toggleCancel(false, this);
                }
                break;
        }

    } catch (ex) {
        this.debug(ex);
    }

}

function fileDialogComplete(numFilesSelected, numFilesQueued) {
    try {
        if (numFilesQueued > 0) {
            this.startUpload();
        }
    } catch (ex) {
        this.debug(ex);
    }
}

function uploadProgress(file, bytesLoaded) {

    try {
        var percent = Math.ceil((bytesLoaded / file.size) * 100);

        var progress = new FileProgress(file, this.customSettings.upload_target);
        progress.setProgress(percent);
        if (percent === 100) {
            //progress.setStatus("创建缩略图...");
            progress.toggleCancel(false, this);
        } else {
            //progress.setStatus("上传...");
            progress.toggleCancel(true, this);
        }
    } catch (ex) {
        this.debug(ex);
    }
}

function uploadComplete(file) {
    try {
        /*  I want the next upload to continue automatically so I'll call startUpload here */
        if (this.getStats().files_queued > 0) {
            this.startUpload();
        } else {
            var progress = new FileProgress(file, this.customSettings.upload_target);
            progress.setComplete();
            //progress.setStatus("上传完成.");
            progress.toggleCancel(false);
        }
    } catch (ex) {
        this.debug(ex);
    }
}

function uploadError(file, errorCode, message) {
    var imageName = "error.gif";
    var progress;
    try {
        switch (errorCode) {
            case SWFUpload.UPLOAD_ERROR.FILE_CANCELLED:
                try {
                    progress = new FileProgress(file, this.customSettings.upload_target);
                    progress.setCancelled();
                    progress.setStatus("Cancelled");
                    progress.toggleCancel(false);
                }
                catch (ex1) {
                    this.debug(ex1);
                }
                break;
            case SWFUpload.UPLOAD_ERROR.UPLOAD_STOPPED:
                try {
                    progress = new FileProgress(file, this.customSettings.upload_target);
                    progress.setCancelled();
                    progress.setStatus("Stopped");
                    progress.toggleCancel(true);
                }
                catch (ex2) {
                    this.debug(ex2);
                }
            case SWFUpload.UPLOAD_ERROR.UPLOAD_LIMIT_EXCEEDED:
                imageName = "uploadlimit.gif";
                break;
            default:
                alert(message);
                break;
        }
        addImage("images/" + imageName);
    } catch (ex3) {
        this.debug(ex3);
    }
}

function fadeIn(element, opacity) {
    var reduceOpacityBy = 5;
    var rate = 30; // 15 fps


    if (opacity < 100) {
        opacity += reduceOpacityBy;
        if (opacity > 100) {
            opacity = 100;
        }

        if (element.filters) {
            try {
                element.filters.item("DXImageTransform.Microsoft.Alpha").opacity = opacity;
            } catch (e) {
                // If it is not set initially, the browser will throw an error.  This will set it if it is not set yet.
                element.style.filter = 'progid:DXImageTransform.Microsoft.Alpha(opacity=' + opacity + ')';
            }
        } else {
            element.style.opacity = opacity / 100;
        }
    }

    if (opacity < 100) {
        setTimeout(function () {
            fadeIn(element, opacity);
        }, rate);
    }
}

/* ******************************************
*	FileProgress Object
*	Control object for displaying file info
* ****************************************** */

function FileProgress(file, targetID) {
    this.fileProgressID = "divFileProgress";

    this.fileProgressWrapper = document.getElementById(this.fileProgressID);
    if (!this.fileProgressWrapper) {
        this.fileProgressWrapper = document.createElement("div");
        this.fileProgressWrapper.className = "progressWrapper";
        this.fileProgressWrapper.id = this.fileProgressID;

        this.fileProgressElement = document.createElement("div");
        this.fileProgressElement.className = "progressContainer";

        var progressCancel = document.createElement("a");
        progressCancel.className = "progressCancel";
        progressCancel.href = "#";
        progressCancel.style.visibility = "hidden";
        progressCancel.appendChild(document.createTextNode(" "));

        var progressText = document.createElement("div");
        progressText.className = "progressName";
        progressText.appendChild(document.createTextNode(file.name));

        var progressBar = document.createElement("div");
        progressBar.className = "progressBarInProgress";

        var progressStatus = document.createElement("div");
        progressStatus.className = "progressBarStatus";
        progressStatus.innerHTML = "&nbsp;";

        this.fileProgressElement.appendChild(progressCancel);
        this.fileProgressElement.appendChild(progressText);
        this.fileProgressElement.appendChild(progressStatus);
        this.fileProgressElement.appendChild(progressBar);

        this.fileProgressWrapper.appendChild(this.fileProgressElement);

        document.getElementById(targetID).appendChild(this.fileProgressWrapper);
        fadeIn(this.fileProgressWrapper, 0);

    } else {
        this.fileProgressElement = this.fileProgressWrapper.firstChild;
        this.fileProgressElement.childNodes[1].firstChild.nodeValue = file.name;
    }

    this.height = this.fileProgressWrapper.offsetHeight;

}
FileProgress.prototype.setProgress = function (percentage) {
    this.fileProgressElement.className = "progressContainer green";
    this.fileProgressElement.childNodes[3].className = "progressBarInProgress";
    this.fileProgressElement.childNodes[3].style.width = percentage + "%";
};
FileProgress.prototype.setComplete = function () {
    this.fileProgressElement.className = "progressContainer blue";
    this.fileProgressElement.childNodes[3].className = "progressBarComplete";
    this.fileProgressElement.childNodes[3].style.width = "";

};
FileProgress.prototype.setError = function () {
    this.fileProgressElement.className = "progressContainer red";
    this.fileProgressElement.childNodes[3].className = "progressBarError";
    this.fileProgressElement.childNodes[3].style.width = "";

};
FileProgress.prototype.setCancelled = function () {
    this.fileProgressElement.className = "progressContainer";
    this.fileProgressElement.childNodes[3].className = "progressBarError";
    this.fileProgressElement.childNodes[3].style.width = "";

};
FileProgress.prototype.setStatus = function (status) {
    this.fileProgressElement.childNodes[2].innerHTML = status;
};

FileProgress.prototype.toggleCancel = function (show, swfuploadInstance) {
    this.fileProgressElement.childNodes[0].style.visibility = show ? "visible" : "hidden";
    if (swfuploadInstance) {
        var fileID = this.fileProgressID;
        this.fileProgressElement.childNodes[0].onclick = function () {
            swfuploadInstance.cancelUpload(fileID);
            return false;
        };
    }
};

copy2Clipboard = function (txt) {
    if (window.clipboardData) {
        window.clipboardData.clearData();
        window.clipboardData.setData("Text", txt);
    }
    else if (navigator.userAgent.indexOf("Opera") != -1) {
        window.location = txt;
    }
    else if (window.netscape) {
        try {
            netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
        }
        catch (e) {
            alert("您的firefox安全限制限制您进行剪贴板操作，请打开’about:config’将signed.applets.codebase_principal_support’设置为true’之后重试，相对路径为firefox根目录/greprefs/all.js");
            return false;
        }
        var clip = Components.classes['@mozilla.org/widget/clipboard;1'].createInstance(Components.interfaces.nsIClipboard);
        if (!clip) return;
        var trans = Components.classes['@mozilla.org/widget/transferable;1'].createInstance(Components.interfaces.nsITransferable);
        if (!trans) return;
        trans.addDataFlavor('text/unicode');
        var str = new Object();
        var len = new Object();
        var str = Components.classes["@mozilla.org/supports-string;1"].createInstance(Components.interfaces.nsISupportsString);
        var copytext = txt; str.data = copytext;
        trans.setTransferData("text/unicode", str, copytext.length * 2);
        var clipid = Components.interfaces.nsIClipboard;
        if (!clip) return false;
        clip.setData(trans, null, clipid.kGlobalClipboard);
    }
}