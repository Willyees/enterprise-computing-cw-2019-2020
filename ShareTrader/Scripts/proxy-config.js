jQuery.fn.flash = function (n, t) {
    var i = this.css("backgroundColor");
    this.animate({
        backgroundColor: "rgb(" + n + ")"
    }, t / 2).animate({
        backgroundColor: i
    }, t / 2)
};
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

//fucntions to notify user of change (alerts, no changements to specific tables) - always available
proxy.client.receiveAnnouncement = function (announcement) {Swal.fire("New Announcement", announcement) };

proxy.client.receiveStockMessage = function (stockmessage) { Swal.fire("Share interested", stockmessage) };