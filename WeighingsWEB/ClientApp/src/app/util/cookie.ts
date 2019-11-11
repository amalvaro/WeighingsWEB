
export class Cookie {

    public static setCookie(name: string, value: string): void {
        document.cookie = encodeURIComponent(name) + '=' + encodeURIComponent(value);
    }
    public static setObject(name: string, value: any): void {
       
        document.cookie = encodeURIComponent(name) + '=' + encodeURIComponent(JSON.stringify(value));
    }
    public static getCookie(name: string): string {
        let matches = document.cookie.match(new RegExp(
            "(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"
        ));
        return matches ? decodeURIComponent(matches[1]) : null;
    }
    public static getCookieObject<T>(name: string): T {
        try {
            let matches = document.cookie.match(new RegExp(
                "(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"
            ));
            return JSON.parse(matches ? decodeURIComponent(matches[1]) : null);
        }
        catch {
            return null;
        }
    }


    public static deleteCookie(name: string) {
        if (this.getCookie(name) != null) {
            document.cookie = name + '=; expires=Thu, 01 Jan 1970 00:00:01 GMT;';
        }
    }

}
