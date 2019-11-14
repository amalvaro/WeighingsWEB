import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { WeighingLog } from "src/data/structure/WeighingLog";

import { WeighingImages } from '../../data/structure/WeghingImages';
import { Cookie } from '../util/cookie';

import { Router } from '@angular/router';


/*import { style, state, animate, transition, trigger } from '@angular/animations'; */

@Component({
    selector: 'log-component',
    templateUrl: './log.component.html',
    /*animations: [
  trigger('fadeInOut', [
    transition(':enter', [   // :enter is alias to 'void => *'
      style({opacity:0}),
      animate(500, style({opacity:1})) 
    ]),
    transition(':leave', [   // :leave is alias to '* => void'
      animate(500, style({opacity:0})) 
    ])
  ])
] */
})

export class WeighingLogComponent {
    public data: WeighingLogResponse;
    public currentPageCounter: number = 1;

    public isThisFinishPage: boolean = false;

    public currentPageArrayNumbers: number[] = [];
    public maxPages: number;

    public MAX_PAGE_COUNT: number = 4;
    public MAX_ROWS_COUNT: number = 5;

    public slideState: number[] = [];
    public currentModalPicture: WeighingImages;
    public currentDialogData: WeighingLog;

    public filterState: boolean[] = [true, true, true, true, true, true, true, true, true];


    constructor(private route: ActivatedRoute, private http: HttpClient, @Inject('BASE_URL') private  baseUrl: string) {
        this.UpdateContent(
            route.snapshot.paramMap.get("page")
        );

        /* Загрузка фильтра столбцов */
        let val = Cookie.getCookieObject<boolean[]>("filterState");
        this.filterState = !val ? this.filterState : val;

        /* Загрузка фильтра данных */
        let searchParams = Cookie.getCookieObject<any>("searchParams");
        this.filterData = !searchParams ? this.filterData : searchParams;

    }

    dropDownCheckedState(id:number) {
        this.filterState[id] = !this.filterState[id];
        Cookie.setObject("filterState", this.filterState);
    }

    openDropdown(id:string) {

        

        var element: Element = document.getElementById(id);
        var btn: Element  = element.getElementsByClassName("btn")[0];
        var menu: Element = element.getElementsByClassName("dropdown-menu")[0];


        $(document.body).mouseup(function (e) {
            var container = $(element);
            if (container.has(e.target).length === 0) {
                menu.classList.remove("open", "show");
                btn.classList.add("btn-secondary");
                btn.classList.remove("btn-danger");
                $(document.body).unbind(e);
            } 
        });

        if (menu.classList.contains("show")) {

            menu.classList.remove("open", "show");
            btn.classList.add("btn-secondary");
            btn.classList.remove("btn-danger");

        } else {
            menu.classList.add("open", "show");
            btn.classList.remove("btn-secondary");
            btn.classList.add("btn-danger");
        }
    }


    public filterData: any = {
        date:           { from: null, to: null },
        typeAndStatus:  { firstSelection: null, secondSelection: null },
        carNumber:      { carNumber: null, fullContain: false },
        directory:      { firstSelection: null, secondSelection: null },
        values:         { value: null, selection: null, fullContain: null }
    };


    AcceptSearchParameters() {
        Cookie.setObject("searchParams", this.filterData);
        this.UpdateContent();
    }

    ClearSearchParameters() {
        this.filterData = {
            date: { from: null, to: null },
            typeAndStatus: { firstSelection: null, secondSelection: null },
            carNumber: { carNumber: null, fullContain: false },
            directory: { firstSelection: null, secondSelection: null },
            values: { value: null, selection: null, fullContain: null }
        };
        Cookie.deleteCookie("searchParams");

        this.UpdateContent();

    }

    SetCurrentDialogData(Id: number) {
        this.currentDialogData = this.data.response[Id];
    }

    closeDropdown() {
        var element: HTMLElement = document.getElementById("dropdown_filter");
        element.classList.remove("open");
        element.classList.remove("show");
    }

    get currentPage(): number {
        return !this.currentPageCounter ? 1 : this.currentPageCounter;
    }

    UpdateContent(page:any = 1): void {

        page = page == null ? 1 : page;

        this.data = null;
        this.http.get<WeighingLogResponse>(this.baseUrl + 'weighinglog/?page=' + page, { withCredentials: true }).subscribe(result => {


            this.data = result;

            this.slideState = [];
            this.slideState.length = this.data.response.length;
            this.slideState = this.slideState.fill(0);

            this.maxPages = Math.ceil(this.data.count / this.MAX_ROWS_COUNT);

            if (page > this.maxPages) {
                this.UpdateContent(this.maxPages);
            }

            if (page < 1) {
                this.UpdateContent(1);
            }

            this.UpdateNextPageNumber(page);
            this.currentPageCounter = page;

        }, error => console.error(error));
    }
    UpdateNextPageNumber(currentPage: number = 1): void {

        var startAt: number = (Math.ceil(currentPage / this.MAX_PAGE_COUNT) - 1 +
            (1 / this.MAX_PAGE_COUNT)) / (1 / this.MAX_PAGE_COUNT);

        this.currentPageArrayNumbers = [];
        for (let i = startAt; i <= this.maxPages; i++) {

            this.currentPageArrayNumbers.push(i);
            if (this.currentPageArrayNumbers.length >= this.MAX_PAGE_COUNT) {
                this.isThisFinishPage = false;
                break;
            }
            this.isThisFinishPage = true;

        }
    }
    UpdatePrevPageNumber(currentPage: number = 1): void {
        /* var startAt: number = (Math.ceil(currentPage / this.MAX_PAGE_COUNT) - 1 + (1 / this.MAX_PAGE_COUNT)) / (1 / this.MAX_PAGE_COUNT); */
        this.currentPageArrayNumbers = [];
        for (let i = currentPage; 1 <= i; i--) {
            this.currentPageArrayNumbers.push(i);
            if (this.currentPageArrayNumbers.length >= this.MAX_PAGE_COUNT) {
                this.isThisFinishPage = false;
                break;
            }
            this.isThisFinishPage = true;
        }
        this.currentPageArrayNumbers.reverse();
    }
    get isShowPrevButton(): boolean {
        return this.currentPageArrayNumbers[0] != 1;
    }
    get lastPageNumber(): number {
        return this.currentPageArrayNumbers[this.currentPageArrayNumbers.length - 1] + 1;
    }
    ChangePicture(elementId: number, state: boolean) {

      /* State argument is describe direction (left = false, right = true) */
        var countPictures: number = this.data.response[elementId].weighingImages.length;
        this.slideState[elementId] = state == true ?
            ((this.slideState[elementId] > countPictures - 2) ? 0 : this.slideState[elementId] + 1) :
            ((this.slideState[elementId] - 1 < 0) ? countPictures - 1 : this.slideState[elementId] - 1);

    }
    SetModalPicture(weighingId: number, pictureId: number) {
        this.currentModalPicture = this.data.response[weighingId].weighingImages[pictureId];
    }

}



interface WeighingLogResponse {

    /* Массив записей для текущей страницы */
    response: WeighingLog[],

    /* Количество записей в базе данных для постраничной навигации */
    count: number

}



