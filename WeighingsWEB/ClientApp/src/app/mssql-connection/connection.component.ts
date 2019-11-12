
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
    }

    public responseLoading:boolean = false;

    public connectionString:string = "";
    SendSQLConnectionString() { 

        this.responseLoading=true;
        $("#ConnectionNotify").html("");

        if(this.Verify()) {

            let connectionUrl: string = "";

            if(this.bIsWindowsAuthentication) {
                connectionUrl = this.baseUrl + `connection/create?Server=${$("#DatabaseServerInput").val()}&Database=${$("#DatabaseNameInput").val()}`;
            } else {

            }
            this.http.get<BooleanResponse>(connectionUrl).subscribe(result => {
                this.responseLoading = false;
                if(result.response == true) {
                    document.location.href=document.location.href;
                } else {
                    $("#ConnectionNotify").html("Введенные данные некорректны");
                }
            }, error => console.log(error));

        } else {
            $("#ConnectionNotify").html("Проверьте правильность заполненных данных");
            this.responseLoading=false;
        }

        /* this.responseLoading=true;
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
        } */
    }

    Verify() : boolean {

        let verifyArray:VerifyType[];
        if(this.bIsWindowsAuthentication) {
            verifyArray = [
                new VerifyType("#DatabaseServerInput", /^(.{1,350})$/),
                new VerifyType("#DatabaseNameInput", /^(.{1,350})$/)
            ];
        } else {
            verifyArray = [
                new VerifyType("#DatabaseServerInput", /^(.{1,350})$/),
                new VerifyType("#DatabaseNameInput", /^(.{1,350})$/),
                new VerifyType("#DatabaseUserNameInput", /^(.{1,350})$/),
                new VerifyType("#DatabaseUserPasswordInput", /^(.{1,350})$/)
            ];
        }

        return VerifyManager.Play(verifyArray);
    }

    public bIsWindowsAuthentication:boolean = true;
    CheckLoginAndPasswordInputs() {
        if(this.bIsWindowsAuthentication == false) {
            $("#DatabaseUserNameInput").attr('disabled','disabled');
            $("#DatabaseUserPasswordInput").attr('disabled','disabled');
        } else {
            $("#DatabaseUserNameInput").removeAttr('disabled');
            $("#DatabaseUserPasswordInput").removeAttr('disabled');
        }
    }


}