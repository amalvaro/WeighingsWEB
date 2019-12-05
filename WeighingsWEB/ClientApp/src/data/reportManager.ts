import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

export class ReportManager {

    constructor(private http: HttpClient, private baseUrl: string) {

    }

    private base64ToArrayBuffer(data: any) {
        var binaryString = window.atob(data);
        var binaryLen = binaryString.length;
        var bytes = new Uint8Array(binaryLen);
        for (var i = 0; i < binaryLen; i++) {
            var ascii = binaryString.charCodeAt(i);
            bytes[i] = ascii;
        }
        return bytes;
    }

    private ExportData(base64Data: any) {

        var arrBuffer = this.base64ToArrayBuffer(base64Data);

        // It is necessary to create a new blob object with mime-type explicitly set
        // otherwise only Chrome works like it should
        var newBlob = new Blob([arrBuffer], { type: "application/pdf" });

        // IE doesn't allow using a blob object directly as link href
        // instead it is necessary to use msSaveOrOpenBlob

        var name = `[${new Date().toLocaleDateString()}]Отчёт.pdf`;

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
    }




    ExportTemplateData(callback: Function, template: string, paramenterNames: string, parameterValues: string) {
        this.http.get<string>(this.baseUrl + 'reportpdf/?templatePath=' + template + '&parameterNames=' + paramenterNames + '&parameterValues=' + parameterValues).subscribe(result => {
            if (result.length > 2) {
                this.ExportData(result);
                callback.call(this, 1);
            } else {
                callback.call(this, 2)
            }
        }, error => { console.error(error); callback.call(this, 2); });

    }

}
