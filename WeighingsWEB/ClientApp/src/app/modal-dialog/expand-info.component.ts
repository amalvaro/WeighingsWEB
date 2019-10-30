import { Component, Input, Output } from '@angular/core';
import { WeighingLog } from '../../data/structure/WeighingLog';

@Component({
    selector: 'modal-info',
    templateUrl: './expand-info.component.html'
})

export class ExpandInfoComponent {

    @Input('dialog-name') public dialogName: string;
    @Input('title') public title: string;
    /* @Input('api-request') public api_request: boolean;
    @Input('api-request-id') public api_request_id: number; */
    @Input('data') public data: WeighingLog;

    constructor() {
        
    }
}
