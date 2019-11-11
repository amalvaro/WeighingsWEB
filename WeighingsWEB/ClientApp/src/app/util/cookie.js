"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Cookie = /** @class */ (function () {
    function Cookie() {
    }
    Cookie.setCookie = function (name, value) {
        document.cookie = encodeURIComponent(name) + '=' + encodeURIComponent(value);
    };
    Cookie.setObject = function (name, value) {
        document.cookie = encodeURIComponent(name) + '=' + encodeURIComponent(JSON.stringify(value));
    };
    Cookie.getCookie = function (name) {
        var matches = document.cookie.match(new RegExp("(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"));
        return matches ? decodeURIComponent(matches[1]) : null;
    };
    Cookie.getCookieObject = function (name) {
        try {
            var matches = document.cookie.match(new RegExp("(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"));
            return JSON.parse(matches ? decodeURIComponent(matches[1]) : null);
        }
        catch (_a) {
            return null;
        }
    };
    Cookie.deleteCookie = function (name) {
        if (this.getCookie(name) != null) {
            document.cookie = name + '=; expires=Thu, 01 Jan 1970 00:00:01 GMT;';
        }
    };
    return Cookie;
}());
exports.Cookie = Cookie;
//# sourceMappingURL=cookie.js.map