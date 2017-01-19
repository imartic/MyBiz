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