import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
    selector: 'log-component',
    templateUrl: './log.component.html'
})

export class WeighingLogComponent {
    public logs: WeighingLog[];

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        http.get<WeighingLog[]>(baseUrl + 'weighinglog').subscribe(result => {
            this.logs = result;
        }, error => console.error(error));
    }
}

/* вынести в отдельный файл  */

interface VehicleDataRecords {
    owner: string
}

interface WeighingLog {
    id: number,
    vehicleId: number,
    vehiclePlate: string,
    vehiclePlateStencilId: number,
    trailerId: number,
    trailerPlate: string
    trailerPlateStencilId: number,
    timeStamp: string,
    scalesId:string
    operator:string,
    weight : number,
    previousWeighingId: number,
    type: number,
    flags: number,
    axlesWeighingFlags:number,
    isDeleted:boolean
    deletedById:number,
    deletedOn:string,
    deletionReason: string,
    previousWeighing: WeighingLog,
    vehicle: VehicleDataRecords
}
