import { Component, Input } from '@angular/core';
import { WeighingImages } from '../../data/structure/WeghingImages';

@Component({
  selector: 'weighing-slider',
    templateUrl: './slider.component.html',

})
export class SliderComponent {

    public currentModalPicture: WeighingImages;
    public currentPicture: number = 0;

    @Input("weighing-picture-array") public weighingImagesArray: WeighingImages[];
    @Input("picture-size") public picture_size: string;
    @Input("slider-name") public slider_name: string; /* required due to bootstrap name conflict */
    @Input('disable-size-picture') public disable_size: boolean;
    /* @Input("multi-nesting") public multi_nesting: boolean = false; */

    constructor() {
       
    }

    ChangePicture(Id: number, direction: boolean) {
        // console.log("Slider initialized : " + this.slider_name);
        var countPictures: number = this.weighingImagesArray.length;
        this.currentPicture = direction == true ?
            ((this.currentPicture > countPictures - 2) ? 0 : this.currentPicture + 1) :
            ((this.currentPicture - 1 < 0) ? countPictures - 1 : this.currentPicture - 1);
    }
    ApplyDialogPicture(Id:number) {
        this.currentModalPicture = this.weighingImagesArray[Id];
    }

    TransfromToId(text:string) {
        return "#" + text;
    }

    /*
    RandomSequenceSalt():string {
        // Math.random().toString(36).substring(7);
        return Math.random().toString(36).substring(10);
    } */

    /* SetModalPicture(weighingId: number, pictureId: number) {
        this.currentModalPicture = this.weighingImages[pictureId];
    } */

    /* ChangeHomeSlidePicture(elementId: number, state: boolean) {
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
    } */
}
