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

  public bShowSQLConnectionAuth:boolean = false;
  constructor(private route: ActivatedRoute, private http: HttpClient, @Inject('BASE_URL') private  baseUrl: string) {  
    this.http.get<BooleanResponse>(this.baseUrl + 'connection').subscribe(result => {
          this.bShowSQLConnectionAuth=!result.response;
      }, error => this.bShowSQLConnectionAuth = true);
  }

  

}
