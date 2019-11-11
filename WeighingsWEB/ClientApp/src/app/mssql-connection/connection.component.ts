
import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { VerifyManager, VerifyType } from '../util/verify_manager';
import { BooleanResponse } from '../../data/structure/BooleanResponse';

@Component({
    selector: 'connection',
    templateUrl: './connection.component.html'
  })

  
export class MSSQLConnection {

    constructor(private route: ActivatedRoute, private http: HttpClient, @Inject('BASE_URL') private  baseUrl: string) {
        //   
    }

    public responseLoading:boolean = false;

    public connectionString:string = "";
    SendSQLConnectionString() { 
        this.responseLoading=true;
        $("#connectionString").removeClass("is-valid is-invalid");
        if(this.Verify()) {
            $("#connectionString").addClass("is-valid");

            this.http.get<BooleanResponse>(this.baseUrl + 'connection/create?connectionString='+ encodeURIComponent(this.connectionString)).subscribe(result => {
                this.responseLoading = false;
                if(result.response == true) {
                    document.location.href=document.location.href;
                } else {
                    $("#connectionStringNotify").html("Данная строка подключения некорректна.");
                    $("#connectionString").addClass("is-invalid");
                }


            }, error => console.log(error));
        

        }
        else {
            $("#connectionString").addClass("is-invalid");
            this.responseLoading = false;
        }
    }

    Verify() : boolean {

        let verifyArray:VerifyType[] = [
            new VerifyType(
                "#connectionString", /^(.{10,350})$/, 
                "#connectionStringNotify", 
                "Введите строку подключения к MSSQL-серверу", 
                "Строка должна содержать от 10 до 350 символов."
            )
        ];

        return VerifyManager.Play(verifyArray);
    }




}