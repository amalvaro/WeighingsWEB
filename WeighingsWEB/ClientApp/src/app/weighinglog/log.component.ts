import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { WeighingLog } from "src/data/structure/WeighingLog";

import { WeighingImages } from '../../data/structure/WeghingImages';
import { VirtualTable } from '../../data/structure/VirtualTable';
import { Dictionary } from '../../data/structure/DictionaryData';
import { Cookie } from '../util/cookie';

import { ReportManager } from '../../data/reportManager';

import { Router } from '@angular/router';

/* Contains filter parameters. */
var filterContainer = null;


@Component({
    selector: 'log-component',
    templateUrl: './log.component.html',
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
    public dictionaryData = {};

    constructor(private route: ActivatedRoute, private http: HttpClient, @Inject('BASE_URL') private  baseUrl: string) {

        this.UpdateContent(
            route.snapshot.paramMap.get("page")
        );
        this.LoadVirtualTables();
        

        /* Загрузка фильтра столбцов */
        let val = Cookie.getCookieObject<boolean[]>("filterState");
        this.filterState = !val ? this.filterState : val;

        /* Загрузка фильтра данных */
        /* let searchParams = Cookie.getCookieObject<any>("searchParams");
        this.filterData = !searchParams ? this.filterData : searchParams; */

    }

    ExportActData(obj: any, Id: number) {
        var target = obj.currentTarget;
        var reportManager: ReportManager = new ReportManager(this.http, this.baseUrl);

        target.innerHTML = 'Подождите...';

        reportManager.ExportTemplateData((state: number) => {

            if (state == 2) {
                alert("Произошла ошибка при выгрузке");
            }

            target.innerHTML = 'Экспорт';
        }, 'WeighingAct', 'wId', Id.toString());
    }

    GetFieldsDataForTable(Id: number) {
        
        let result: Dictionary[] = [];


        for(let i = 0; i < this.searchTables[Id].fields.length; i++) {
            let dict = this.dictionaryData[this.searchTables[Id].fields[i].field.fieldCode.value];
            if(dict != null) {
                for(let j = 0; j < dict.length; j++) {
                    result.push(dict[j]);
                }
            }
        } 

        return result;

    }

    LoadDictionaryData() {
        for(let i = 0; i < this.searchTables.length; i++) {
            for(let j = 0; j < this.searchTables[i].fields.length; j++) {
                let fieldId = this.searchTables[i].fields[j].field.fieldCode.value;
                this.http.get<Dictionary[]>(this.baseUrl + 'dictionary/?fieldId='+fieldId).subscribe(result => {
                    this.dictionaryData[fieldId]=result;
                });
            } 
        }
    }

    public searchTables: VirtualTable[] ;
    public dividedTables = [];

    LoadVirtualTables() {
        this.http.get<VirtualTable[]>(this.baseUrl + 'interface').subscribe(result => {
            this.searchTables = result;
            
            if(this.searchTables.length > 0) {
                this.filterData.directory.selection.length = this.searchTables.length;
                this.filterData.directory.selection.fill(null);
    
                this.dividedTables = [];
                this.LoadDictionaryData();

                for(let i = 0; i < this.searchTables.length; i++) {
                    this.dividedTables.push({
                        firstTable: this.searchTables[i],
                        secondTable: this.searchTables[i + 1] != null ? this.searchTables[i + 1] : null
                    });
                    i = i + 1;  
                } 
            }


        });
        
    }

    

    dropDownCheckedState(id:number) {
        this.filterState[id] = !this.filterState[id];
        Cookie.setObject("filterState", this.filterState);
    }

    openDropdown(id:string) {

        var element: Element    = document.getElementById(id);
        var btn: Element        = element.getElementsByClassName("btn")[0];
        var menu: Element       = element.getElementsByClassName("dropdown-menu")[0];


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
        date:           { enable: false, from: null, to: null },
        typeAndStatus:  { firstSelection: null, secondSelection: null },
        carNumber:      { carNumber: null, fullContain: false },
        directory:      { selection: [null] },
        values:         { value: null, selection: null, fullContain: false }
    };

    ApplyDateMask(Id:number) {

        var firstDate   = new Date();
        var secondDate  = new Date();

        let days = {0 : 1, 1 : 3, 2 : 7, 3 : 30};
        firstDate.setDate(firstDate.getDate() - days[Id]);

        firstDate.setHours(0, 0, 0);
        secondDate.setHours(23, 59, 0);

        let firstString     = 
             `${firstDate.getFullYear()}-` + 
             `${(firstDate.getMonth() + 1) < 10 ? "0" + firstDate.getMonth() + 1 : firstDate.getMonth() + 1 }-` +
             `${firstDate.getDate() < 10 ? "0" + firstDate.getDate() : firstDate.getDate()}T` + 
             `${firstDate.getHours() < 10 ? "0" + firstDate.getHours() : firstDate.getHours()}:` + 
             `${firstDate.getMinutes() < 10 ? "0" + firstDate.getMinutes() : firstDate.getMinutes()}`;

        let secondString     = 
            `${secondDate.getFullYear()}-` + 
             `${(secondDate.getMonth() + 1) < 10 ? "0" + secondDate.getMonth() + 1 : secondDate.getMonth() + 1 }-` +
             `${secondDate.getDate() < 10 ? "0" + secondDate.getDate() : secondDate.getDate()}T` + 
             `${secondDate.getHours() < 10 ? "0" + secondDate.getHours() : secondDate.getHours()}:` +
             `${secondDate.getMinutes() < 10 ? "0" + secondDate.getMinutes() : secondDate.getMinutes()}`;

        this.filterData.date.from = firstString;
        this.filterData.date.to = secondString;
    }



    CheckDateInput() {
        if(this.filterData.date.enable != false) {
            $("#date_from").attr('disabled','disabled');
            $("#date_to").attr('disabled','disabled');
        } else {
            $("#date_from").removeAttr('disabled');
            $("#date_to").removeAttr('disabled');
        }
    }

    AcceptSearchParameters() {


        if(this.filterData.date.enable == true) {
            if($("#date_from").val() == "" || $("#date_to").val() == "") {

                alert("Укажите диапазон дат.");

                return;
            }
        }

        filterContainer = encodeURIComponent(JSON.stringify(this.filterData));
        this.UpdateContent();

    }

    ClearSearchParameters() {
        this.filterData = {
            date: { enable: false, from: null, to: null },
            typeAndStatus: { firstSelection: null, secondSelection: null },
            carNumber: { carNumber: null, fullContain: false },
            directory: { selection: [null] },
            values: { value: null, selection: null, fullContain: false }
        };

        this.filterData.directory.selection.length = this.searchTables.length;
        this.filterData.directory.selection.fill(null);

        filterContainer = null;
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

        if(page < 1 || page > this.maxPages)
            page = 1;

        this.data = null;
        this.http.get<WeighingLogResponse>(this.baseUrl + 'weighinglog/?page=' + page + "&stringSearchParams=" + filterContainer, { withCredentials: true }).subscribe(result => {


            this.data = result;

            /* if(result.response == null) {
                alert("data is null");
            } */

            this.slideState = [];
            this.slideState.length = this.data.response.length;
            this.slideState = this.slideState.fill(0);

            this.maxPages = Math.ceil(this.data.count / this.MAX_ROWS_COUNT);

            /* if (page > this.maxPages) {
                this.UpdateContent(this.maxPages);
            } */

            // 18.11.2019
            /* if (page < 1) {
                this.UpdateContent(1);
            } */

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
            
            if(this.maxPages == i) {
                this.isThisFinishPage = true;
                break;
            }

            if (this.currentPageArrayNumbers.length >= this.MAX_PAGE_COUNT) {
                this.isThisFinishPage = false;
                break;
            }
            this.isThisFinishPage = true;
            
            // console.log("c = index = " + i);
            // console.log("c = max = " + this.maxPages);

        }
    }
    UpdatePrevPageNumber(currentPage: number = 1): void {
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



