import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { WeighingLogComponent } from './weighinglog/log.component';
import { ExportDataComponent } from './export/export.component';

import { ControlDataComponent } from './control/control.component';

import { ModalPictureComponent } from "./modal-dialog/modal.component";
import { ExpandInfoComponent } from "./modal-dialog/expand-info.component";
import { SliderComponent } from "./weighing-picture-slider/slider.component";
import { MSSQLConnection } from "./mssql-connection/connection.component";
// import { NgbModule } from 'bootstrap/js/src'

// import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
// import { BsDropdownModule } from 'ngx-bootstrap/dropdown'

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    WeighingLogComponent,
    ExportDataComponent,
    ModalPictureComponent,
    ExpandInfoComponent,
    SliderComponent,
    MSSQLConnection,
    ControlDataComponent
  ],
    imports: [
    //BsDropdownModule.forRoot(),
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
        { path: 'fetch-data', component: FetchDataComponent },
        { path: 'weighinglog-data', component: WeighingLogComponent },
        { path: 'weighinglog-data/:page', component: WeighingLogComponent }, /* export-data */
        { path: 'export-data', component: ExportDataComponent },
        { path: 'control-data', component: ControlDataComponent }
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
