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
            var proposals = jQuery.parseJSON(data.d);

            if (proposals) {
                //$(".homeProposals").append("<div class='mdl-spinner mdl-js-spinner is-active' style='margin:10px'></div>");
                var len = proposals.length;
                var txt = "";
                if (len > 0) {
                    //$(".mdl-spinner").remove();
                    for (var i = 0; i < len; i++) {
                        txt += "<tr><td class='mdl-data-table__cell--non-numeric' style='width:60%'>" +
                            proposals[i].ProposalName +
                            "</td><td  style='width:40%'>" +
                            ToJavaScriptDate(proposals[i].DateSaved) +
                            "</td></tr>";
                    }
                    if (txt != "") {
                        $(".homeProposalsTbl").show();
                        $(".homeProposals .mdl-button--raised").show();
                        $(".homeProposalsTbl").append(txt);
                    }
                }
                else {
                    //$(".mdl-spinner").remove();
                    $(".homeProposalsTbl").hide();
                    $(".homeProposals .mdl-button--raised").hide();
                    $(".homeProposals tbody").append("<p style = 'margin: 10px'><b>No saved proposals.</b></p>");
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

