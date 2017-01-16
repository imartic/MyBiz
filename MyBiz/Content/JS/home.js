$(document).ready(function () {
    loadProposals();
});

function loadProposals() {
    $.ajax({
        type: "POST",
        url: "Home.aspx/LoadHomeProposals",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            console.log(data.d + '|' + data);
            var proposals = jQuery.parseJSON(data.d);

            if (proposals) {
                //$(".homeProposals").append("<div class='mdl-spinner mdl-js-spinner is-active' style='margin:10px'></div>");
                var len = proposals.length;
                var txt = "";
                if (len > 0) {
                    $(".mdl-spinner").remove();
                    //prikazuje samo prva 3 reda iz tablice ponuda, privremeno.. kasnije uzeti top 3 upitom!!
                    for (var i = 0; i < ((len < 3) ? len : 3); i++) {
                        console.log(proposals[i].ProposalName + "|" + proposals[i].DateSaved + "|" + proposals[i].ID)
                        txt += "<tbody><tr><td class='mdl-data-table__cell--non-numeric'>" +
                            proposals[i].ProposalName +
                            "</td><td>" +
                            ToJavaScriptDate(proposals[i].DateSaved) +
                            "</td></tr></tbody>";
                    }
                    if (txt != "") {
                        console.log("table: " + $(".homeProposalsTbl tbody"));
                        $(".homeProposalsTbl").show();
                        $(".homeProposals .mdl-button--raised").show();
                        $(".homeProposalsTbl").append(txt);
                    }
                }
                else {
                    //$(".mdl-spinner").remove();
                    $(".homeProposalsTbl").hide();
                    $(".homeProposals .mdl-button--raised").hide();
                    $(".homeProposals").append("<p style = 'margin: 10px'><b>No saved proposals.</b></p>");
                }
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status);
            console.log(xhr.responseText);
            console.log(thrownError);
        }
    });
};


function ToJavaScriptDate(value) {
    var pattern = /Date\(([^)]+)\)/;
    var results = pattern.exec(value);
    var dt = new Date(parseFloat(results[1]));

    //var h = dt.getHours();
    //h = (h < 10) ? ("0" + h) : h;

    //var m = dt.getMinutes();
    //m = (m < 10) ? ("0" + m) : m;

    return (dt.getDate() + "." + (dt.getMonth() + 1) + "." + dt.getFullYear() /*+ " - " + h + ":" + m*/);
}

