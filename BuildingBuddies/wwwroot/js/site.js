$(document).ready(function () {
    var pastElement = $(".active");
    pastElement.removeClass("active");

    var path = window.location.pathname;
    if (path == "/") path = "/Index";

    var clrPath = path.substr(1, path.length);
    var activeElem = $('a[href=' + clrPath + ']');
    activeElem.addClass("active");
});