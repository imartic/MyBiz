$(document).ready(function () {
    loadCompanies();
});

function loadCompanies() {
    $.ajax({
        type: "POST",
        url: "Companies.aspx/LoadCompanies",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var cos = jQuery.parseJSON(data.d);

            console.log(cos);

            if (cos) {
                var len = cos.length;
                var txt = "";

                console.log(len);
                if (len > 0) {
                    for (var i = 0; i < len; i++) {
                        txt += "<tr><td class='mdl-data-table__cell--non-numeric'>" + cos[i].CompanyName +
                            "<td class='buttons-cell'>" + setIconTooltips(cos[i].ID) + "</td>" +
                            "</td></tr>";
                    }
                    if (txt != "") {
                        $("#companiesTbl").show();
                        $("#companiesTbl").append(txt);
                    }
                }
                else {
                    $("#companiesTbl").hide();
                    $(".companies-card").append("<p style = 'margin: 10px'><b>No saved Companies.</b></p>");
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

//TODO: MODAL newEditCompany!!

function setIconTooltips(id) {
    var onclick_link = "";
    var html_editIcon = "<button class='mdl-button mdl-js-button mdl-button--icon mdl-color-text--primary-dark'" +
                                "id='editCompany" + id + "'" +
                                "style='margin-left:auto;'" +
                                "onclick=" + "'" + "window.location.href=" + "\"" + onclick_link + "?id=" + id + "\"" + "; return false;" + "'>" +
                            "<i class='material-icons'>create</i>" +
                        "</button>" +
                        "<div class='mdl-tooltip' for='editCompany" + id + "'>Edit Company</div>";

    var html_deleteIcon = "<button class='mdl-button mdl-js-button mdl-button--icon mdl-color-text--red'" +
                                   "id='deleteCompany" + id + "'" +
                                   "style='margin-left:auto;'" +
                                   "onclick='deleteCompany(this)'>" +
                               "<i class='material-icons'>delete</i>" +
                           "</button>" +
                           "<div class='mdl-tooltip' for='deleteCompany" + id + "'>Delete Company</div>";

    return html_editIcon + html_deleteIcon;
}


function deleteCompany(co) {
    console.log(co.id);
    var id = parseInt(co.id.match(/\d+/g), 10);
    console.log(id);

    if (confirm('Are you sure you want to delete this company?')) {
        $.ajax({
            type: "POST",
            url: "Companies.aspx/DeleteCompanies",
            data: "{id:" + id + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.d == "OK") {
                    $('table').find('tbody').empty();
                    loadCompanies();
                    //location.reload();
                }
                else {
                    alert(data.d);
                }
            },
            error: function (res) {
                alert(res.responseText);
            }
        });
    }
}