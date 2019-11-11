export class VerifyManager {

    static Play(types: VerifyType[]):boolean {
        let state: boolean = true;
        types.forEach(element => {
            state = state && element.play();
        });
        return state;
    }

}


export class VerifyType {
    constructor(protected jElement:string, 
        protected pattern:RegExp, 
        protected jNotifyPlacement?:string,
        protected defaultMessage?:string,
        protected errNotify?:string) { }

    play() : boolean {
        let data:string = $(this.jElement).val().toString();
        let result = data.match(this.pattern) != null;

        if(result == false) {
            if(this.jNotifyPlacement != null && this.errNotify != null) {
                $(this.jNotifyPlacement).html(this.errNotify);
            }
        } else {
            if(this.jNotifyPlacement != null && this.defaultMessage != null) {
                $(this.jNotifyPlacement).html(this.defaultMessage);
            }
        }

        return result;

    }
}


