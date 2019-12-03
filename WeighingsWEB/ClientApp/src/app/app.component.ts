import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';

import { BooleanResponse } from '../data/structure/BooleanResponse';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';

  public bShowNavbar: boolean = false;
  public bShowContent: boolean = false;
  public bShowSQLConnectionAuth:boolean = false;
  public bShowAuthorization:boolean = false;

  constructor(private route: ActivatedRoute, private http: HttpClient, @Inject('BASE_URL') private  baseUrl: string) {  
    this.http.get<BooleanResponse>(this.baseUrl + 'connection').subscribe(result => {
        this.bShowSQLConnectionAuth=!result.response;
        this.bShowNavbar=result.response;
        this.bShowContent=result.response;

        if(!this.bShowSQLConnectionAuth) {

          this.http.get<BooleanResponse>(this.baseUrl + 'user/checkAuthorization').subscribe(result => {
            if(result.response == false) {
              this.bShowNavbar  = false;
              this.bShowContent = false;
              this.bShowAuthorization = true;
            }
          }, error => this.bShowAuthorization = true);
        } 

      }, error => this.bShowSQLConnectionAuth = true);
  }


  

}
