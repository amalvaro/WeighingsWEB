
import { Component,  Input } from '@angular/core';


@Component({
    selector: 'modal-content',
    templateUrl: './modal-content.component.html'
})

export class ModalContent {
  
    public display: string = 'block';

    @Input('dialog-name') public dialogName: string;
    @Input('title') public title: string;


}