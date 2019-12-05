"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var ReportManager = /** @class */ (function () {
    function ReportManager(http, baseUrl) {
        this.http = http;
        this.baseUrl = baseUrl;
    }
    ReportManager.prototype.base64ToArrayBuffer = function (data) {
        var binaryString = window.atob(data);
        var binaryLen = binaryString.length;
        var bytes = new Uint8Array(binaryLen);
        for (var i = 0; i < binaryLen; i++) {
            var ascii = binaryString.charCodeAt(i);
            bytes[i] = ascii;
        }
        return bytes;
    };
    ReportManager.prototype.ExportData = function (base64Data) {
        var arrBuffer = this.base64ToArrayBuffer(base64Data);
        // It is necessary to create a new blob object with mime-type explicitly set
        // otherwise only Chrome works like it should
        var newBlob = new Blob([arrBuffer], { type: "application/pdf" });
        // IE doesn't allow using a blob object directly as link href
        // instead it is necessary to use msSaveOrOpenBlob
        var name = "[" + new Date().toLocaleDateString() + "]\u041E\u0442\u0447\u0451\u0442.pdf";
        if (window.navigator && window.navigator.msSaveOrOpenBlob) {
            window.navigator.msSaveOrOpenBlob(newBlob, name);
            return;
        }
        // For other browsers: 
        // Create a link pointing to the ObjectURL containing the blob.
        var data = window.URL.createObjectURL(newBlob);
        var link = document.createElement('a');
        document.body.appendChild(link); //required in FF, optional for Chrome
        link.href = data;
        link.download = name;
        link.click();
        window.URL.revokeObjectURL(data);
        link.remove();
    };
    ReportManager.prototype.ExportTemplateData = function (callback, template, paramenterNames, parameterValues) {
        var _this = this;
        this.http.get(this.baseUrl + 'reportpdf/?templatePath=' + template + '&parameterNames=' + paramenterNames + '&parameterValues=' + parameterValues).subscribe(function (result) {
            if (result.length > 2) {
                _this.ExportData(result);
                callback.call(_this, 1);
            }
            else {
                callback.call(_this, 2);
            }
        }, function (error) { console.error(error); callback.call(_this, 2); });
    };
    return ReportManager;
}());
exports.ReportManager = ReportManager;
//# sourceMappingURL=reportManager.js.map