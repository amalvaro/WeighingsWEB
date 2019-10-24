import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
    selector: 'app-export-data',
    templateUrl: './export.component.html'
})
export class ExportDataComponent {

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        /* http.get<WeatherForecast[]>(baseUrl + 'weatherforecast').subscribe(result => {
            this.forecasts = result;
        }, error => console.error(error)); */
    }
}
