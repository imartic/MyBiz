

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
            var proposals = jQuery.parseJSON(data.d);
            
            if (proposals) {
                var len = proposals.length;
                var txt = "";
                if (len > 0) {
                    for (var i = 0; i < len; i++) {
                        txt += "<tr>" +
                            "<td class='mdl-data-table__cell--non-numeric'>" + proposals[i].ProposalName + "</td>" +
                            "<td class='mdl-data-table__cell--non-numeric'>" + proposals[i].ClientName + "</td>" +
                            "<td class='mdl-data-table__cell--non-numeric'>" + ToJavaScriptDate(proposals[i].DateSaved) + "</td>" +
                            "<td>" + setIconTooltips(proposals[i].ID) + "</td>" +
                            "</tr>";
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

function setIconTooltips(proposalId) {
    var onclick_link = "NewEditProposal.aspx";
    var html_EditDeleteIcons = "<button class='mdl-button mdl-js-button mdl-button--icon mdl-color-text--primary-dark'" +
                                            "id='editProposal" + proposalId + "'" +
                                            "style='margin-left:auto;'" +
                                            "onclick=" + "'" + "window.location.href=" + "\"" + onclick_link + "?id=" + proposalId + "\"" + "; return false;" + "'>" +
                                        "<i class='material-icons'>create</i>" +
                                    "</button>" +
                                    "<div class='mdl-tooltip' for='editProposal" + proposalId + "'>Edit Proposal</div>" +

                                    "<button class='mdl-button mdl-js-button mdl-button--icon mdl-color-text--red'" +
                                            "id='deleteProposal" + proposalId + "'" +
                                            "style='margin-left:auto;'" +
                                            "onclick='window.location.href=''; return false;'>" +
                                        "<i class='material-icons'>delete</i>" +
                                    "</button>" +
                                    "<div class='mdl-tooltip' for='deleteProposal" + proposalId + "'>Delete Proposal</div>";

    return html_EditDeleteIcons;
}

