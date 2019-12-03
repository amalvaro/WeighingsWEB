
import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { VerifyManager, VerifyType } from '../util/verify_manager';
import { BooleanResponse } from '../../data/structure/BooleanResponse';

@Component({
    selector: 'authorization',
    templateUrl: './authorization.component.html'
  })

  
export class Authorization {
    constructor(private route: ActivatedRoute, private http: HttpClient, @Inject('BASE_URL') private  baseUrl: string) {

    }


    public bShowAuthorizationProgress : boolean;
    
    SendAuthorizationRequest() {
        $("#ConnectionNotify").html("");
        this.bShowAuthorizationProgress = true;
        this.http.get<BooleanResponse>(this.baseUrl + `user/?usrName=${$("#inputEmail").val()}&usrPassword=${$("#inputPassword").val()}`).subscribe(result => {
            this.bShowAuthorizationProgress = false;
            if(result.response == false) {
                $("#ConnectionNotify").html("Пароль или логин был введен неверно");
            } else {
                document.location.href=document.location.href;
            }

        }, error => console.log(error));
    }

}