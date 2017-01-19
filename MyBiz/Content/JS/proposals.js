$(document).ready(function () {
    loadProposals();
});

function loadProposals() {    

    $.ajax({
        type: "POST",
        url: "Proposals.aspx/LoadProposals",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            console.log(data.d + '|' + data);
            var proposals = jQuery.parseJSON(data.d);
            
            if (proposals) {
                var len = proposals.length;
                var txt = "";
                if (len > 0) {
                    for (var i = 0; i < len; i++) {
                        console.log(proposals[i].ProposalName + "|" + proposals[i].DateSaved + "|" + proposals[i].ID);
                        console.log(setIconTooltips(proposals[i].ID));
                        txt += "<tr><td class='mdl-data-table__cell--non-numeric'>" +
                            proposals[i].ProposalName +
                            "</td><td>" +
                            ToJavaScriptDate(proposals[i].DateSaved) +
                            "</td>" + setIconTooltips(proposals[i].ID) + "</tr>";
                    }
                    if (txt != "") {
                        $("table").show();
                        $("tbody").append(txt);
                    }
                }
                else {
                    $("table").hide();
                    $(".contentProposals").append("<p><b>No saved proposals.</b></p>");
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

    var h = dt.getHours();
    h = (h < 10) ? ("0" + h) : h;

    var m = dt.getMinutes();
    m = (m < 10) ? ("0" + m) : m;

    return (dt.getDate() + "." + (dt.getMonth() + 1) + "." + dt.getFullYear() + " - " + h + ":" + m);
}

function setIconTooltips(proposalId) {
    var onclick_link = "NewEditProposal.aspx";
    var html_EditDeleteIcons = "<td>" +
                                    "<button class='mdl-button mdl-js-button mdl-button--icon mdl-color-text--primary-dark'" +
                                            "id='editProposal" + proposalId + "'" +
                                            "style='margin-left:auto;'" +
                                            "onclick="+"'"+"window.location.href="+"\""+ onclick_link +"\"" +"; return false;"+"'>" +
                                        "<i class='material-icons'>create</i>" +
                                    "</button>" +
                                    "<div class='mdl-tooltip' for='editProposal" + proposalId + "'>Edit Proposal</div>" +
                                "</td>" +
                                "<td>" +
                                    "<button class='mdl-button mdl-js-button mdl-button--icon mdl-color-text--red'" +
                                            "id='deleteProposal" + proposalId + "'" +
                                            "style='margin-left:auto;'" +
                                            "onclick='window.location.href=''; return false;'>" +
                                        "<i class='material-icons'>delete</i>" +
                                    "</button>" +
                                    "<div class='mdl-tooltip' for='deleteProposal" + proposalId + "'>Delete Proposal</div>" +
                                "</td>";

    return html_EditDeleteIcons;
}

