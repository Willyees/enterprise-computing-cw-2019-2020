// Crockford's supplant method
if (!String.prototype.supplant) {
    String.prototype.supplant = function (o) {
        return this.replace(/{([^{}]*)}/g,
            function (a, b) {
                var r = o[b];
                return typeof r === 'string' || typeof r === 'number' ? r : a;
            }
        );
    };
}
var headers = {};
var token = sessionStorage.getItem("accessToken");
if (token) {
    headers.Authorization = 'Bearer ' + token;
}
var proxy = $.connection.notificationHub; // the generated client-side hub proxy
proxy.client.updateAnnouncement = function (announcement) { alert(announcement) };
