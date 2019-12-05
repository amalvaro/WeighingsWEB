import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ReportManager } from '../../data/reportManager';

@Component({
    selector: 'app-export-data',
    templateUrl: './export.component.html'
})
export class ExportDataComponent {

    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
        /* http.get<WeatherForecast[]>(baseUrl + 'weatherforecast').subscribe(result => {
            this.forecasts = result;
        }, error => console.error(error)); */
    }

    ExportListData() {

        let firstDateText = $("#firstDate").val();
        let secondDateText = $("#secondDate").val();

        if (firstDateText == null || secondDateText == null || firstDateText == "" || secondDateText == "") {
            alert("Укажите диапазон дат, между которыми необходимо выбрать данные");
            return;
        }

        let firstDate = new Date(firstDateText.toString());
        let secondDate = new Date(secondDateText.toString());


        let firstString =
            `${firstDate.getDate() < 10 ? "0" + firstDate.getDate() : firstDate.getDate()}-` +
            `${(firstDate.getMonth() + 1) < 10 ? "0" + firstDate.getMonth() + 1 : firstDate.getMonth() + 1}-` +
            `${firstDate.getFullYear()} ` +

            `${firstDate.getHours() < 10 ? "0" + firstDate.getHours() : firstDate.getHours()}:` +
            `${firstDate.getMinutes() < 10 ? "0" + firstDate.getMinutes() : firstDate.getMinutes()}`;

        let secondString =
            `${secondDate.getDate() < 10 ? "0" + secondDate.getDate() : secondDate.getDate()}-` +
            `${(secondDate.getMonth() + 1) < 10 ? "0" + secondDate.getMonth() + 1 : secondDate.getMonth() + 1}-` +
            `${secondDate.getFullYear()} ` +

            `${secondDate.getHours() < 10 ? "0" + secondDate.getHours() : secondDate.getHours()}:` +
            `${secondDate.getMinutes() < 10 ? "0" + secondDate.getMinutes() : secondDate.getMinutes()}`;

        $("#ExportButton").html("Подождите..");

        var reportManager: ReportManager = new ReportManager(this.http, this.baseUrl);
        reportManager.ExportTemplateData((state: number) => {

            if (state == 2) {
                alert("Произошла ошибка при выгрузке");
            }

            $("#ExportButton").html("Выгрузить");

        }, 'WeighingList', 'from,to', firstString + ',' + secondString);



    }


}
