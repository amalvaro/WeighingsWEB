import { Component, Input, Output } from '@angular/core';

@Component({
  selector: 'modal-picture',
  templateUrl: './modal.component.html',
})
export class ModalPictureComponent {

    @Input('dialog-name') public dialogName: string;
    @Input('picture-link') public picture: string;
    @Input('title') public title: string;

    constructor() { }
    

}
