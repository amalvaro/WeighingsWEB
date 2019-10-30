import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { WeighingLog } from "src/data/structure/WeighingLog";
import { WeighingImages } from '../../data/structure/WeghingImages';

@Component({
  selector: 'app-home',
    templateUrl: './home.component.html',

})
export class HomeComponent {

    public currentModalPicture: WeighingImages;

    public data: WeighingLog[];
    public slideHomeState: number[] = [];
    public currentDialogData: WeighingLog;

    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
        this.http.get<WeighingLog[]>(this.baseUrl + 'home').subscribe(result => {
            this.data = result;
            this.slideHomeState = [];
            this.slideHomeState.length = this.data.length;
            this.slideHomeState = this.slideHomeState.fill(0);

        }, error => console.error(error));
    }

    ChangeHomeSlidePicture(elementId: number, state: boolean) {
        var countPictures: number = this.data[elementId].weighingImages.length;
        this.slideHomeState[elementId] = state == true ?
            ((this.slideHomeState[elementId] > countPictures - 2) ? 0 : this.slideHomeState[elementId] + 1) :
            ((this.slideHomeState[elementId] - 1 < 0) ? countPictures - 1 : this.slideHomeState[elementId] - 1);
    }

    SetModalPicture(wId: number, pId:number) {
        this.currentModalPicture = this.data[wId].weighingImages[pId];
    }

    SetCurrentDialogData(Id: number) {
        this.currentDialogData = this.data[Id];
    }
}
