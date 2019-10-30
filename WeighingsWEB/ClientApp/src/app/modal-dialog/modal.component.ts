import { Component, Input, Output } from '@angular/core';

import * as $ from "jquery";
(<any>window).jQuery = $

import "bootstrap";

@Component({
  selector: 'modal-picture',
  templateUrl: './modal.component.html'
})
export class ModalPictureComponent {

    public display: string = 'block';

    @Input('dialog-name') public dialogName: string;
    @Input('picture-link') public picture: string;
    @Input('title') public title: string;


    constructor() { }
    /*
    CloseDialog() {
        $("#" + this.dialogName).modal("hide");
    } */

}
