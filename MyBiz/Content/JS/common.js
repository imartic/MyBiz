//Logout
$("#btnLogout").click(function () {
    $.ajax({
        type: "POST",
        url: "Home.aspx/Logout",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            window.location = data.d;
        },
        error: function (res) {
            alert(res.responseText);
        }
    });
});

function ToJavaScriptDate(value) {
    var pattern = /Date\(([^)]+)\)/;
    var results = pattern.exec(value);
    var dt = new Date(parseFloat(results[1]));

    var h = dt.getHours();
    h = (h < 10) ? ("0" + h) : h;

    var m = dt.getMinutes();
    m = (m < 10) ? ("0" + m) : m;

    return (dt.getDate() + "." + (dt.getMonth() + 1) + "." + dt.getFullYear() + " - " + h + ":" + m);
}