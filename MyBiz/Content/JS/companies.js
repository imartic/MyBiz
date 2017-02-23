
var editId = 0;

var emptyTbl = 

$(document).ready(function () {
    $('.co-input').prop('disabled', true);
    $('.co-input').addClass("mdl-color-text--primary-dark");

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

            if (cos) {
                var len = cos.length;
                var txt = "";

                if (len > 0) {
                    for (var i = 0; i < len; i++) {
                        txt += "<tr onclick='loadCompanyData(" + cos[i].ID + ", false); highlightRow(this)'><td class='mdl-data-table__cell--non-numeric'>" + cos[i].CompanyName +
                            "<td class='buttons-cell'>" + setIconTooltips(cos[i].ID) + "</td>" +
                            "</td></tr>";
                    }
                    if (txt != "") {
                        $(".companies-card p").remove();
                        $("#companiesTbl").show();
                        $("#companiesTbl").append(txt);
                    }
                }
                else {
                    $("#companiesTbl").hide();
                    $(".companies-card").append("<p class='emptyTbl' style = 'margin: 10px'><b>No saved Companies.</b></p>");
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


function setIconTooltips(id) {
    var onclick_link = "";
    var html_editIcon = "<button class='mdl-button mdl-js-button mdl-button--icon mdl-color-text--primary-dark show-modal'" +
                                "id='editCompany" + id + "'" +
                                "style='margin-left:auto;'" +
                                "onclick='showDialog(this)'>" +
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
            url: "Companies.aspx/DeleteCompany",
            data: "{id:" + id + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.d == "OK") {
                    $('#companiesTbl tr').remove();
                    loadCompanies();
                    $('.co-input').val('').parent().removeClass('is-dirty');
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


/****** MODAL ******/
var dialog = document.querySelector('dialog');
var showModalButton1 = document.querySelector('.show-modal');
var showModalButton2 = document.querySelector('#newCompany');
if (!dialog.showModal) {
    dialogPolyfill.registerDialog(dialog);
}
showModalButton1.addEventListener('click', function () {
    editId = 0;
    $('.dlg-input').val('').parent().removeClass('is-dirty');
    dialog.showModal();
});
showModalButton2.addEventListener('click', function () {
    editId = 0;
    $('.dlg-input').val('').parent().removeClass('is-dirty');
    dialog.showModal();
});
dialog.querySelector('.close').addEventListener('click', function () {
    dialog.close();
    editId = 0;
    $('dlg-input').val('').parent().removeClass('is-dirty');
});


function showDialog(btn) {
    var id = parseInt(btn.id.match(/\d+/g), 10);

    loadCompanyData(id, true);
}

function loadCompanyData(id, dlg) {
    $.ajax({
        type: "POST",
        url: "Companies.aspx/LoadCompany",
        data: "{coId:" + id + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var co = jQuery.parseJSON(data.d);

            if (co != null) {
                if (dlg) {
                    var dialog = document.querySelector('dialog');

                    if (!dialog.showModal) {
                        dialogPolyfill.registerDialog(dialog);
                    }

                    dialog.showModal();

                    $('.dlg-input').val('').parent().removeClass('is-dirty');
                    fillDlgData(co);
                }
                else {
                    $('.co-input').val('').parent().removeClass('is-dirty');
                    fillCoData(co);
                }
            }           
        },
        error: function (res) {
            alert(res.responseText);
        }
    });
}

function fillDlgData(co) {
    editId = co.ID;
    if (co.CompanyName != "" && co.CompanyName != null) $('#name').val(co.CompanyName).parent().addClass('is-dirty');
    if (co.CompanyAddress != "" && co.CompanyAddress != null) $('#address').val(co.CompanyAddress).parent().addClass('is-dirty');
    if (co.CompanyCity != "" && co.CompanyCity != null) $('#city').val(co.CompanyCity).parent().addClass('is-dirty');
    if (co.CompanyPhone != "" && co.CompanyPhone != null) $('#phone').val(co.CompanyPhone).parent().addClass('is-dirty');
    if (co.CompanyFax != "" && co.CompanyFax != null) $('#fax').val(co.CompanyFax).parent().addClass('is-dirty');
    if (co.CompanyEmail != "" && co.CompanyEmail != null) $('#email').val(co.CompanyEmail).parent().addClass('is-dirty');
    if (co.CompanyPIN != "" && co.CompanyPIN != null) $('#pin').val(co.CompanyPIN).parent().addClass('is-dirty');
    if (co.CompanyIBAN != "" && co.CompanyIBAN != null) $('#iban').val(co.CompanyIBAN).parent().addClass('is-dirty');
}


dialog.querySelector('.save').addEventListener('click', function () {
    var co = getFromEdit();

    if (co.CompanyName == "" || co.CompanyName == null) {
        alert("Company name cannot be empty!");
        $("#name").focus();
        return;
    }

    $.ajax({
        type: "POST",
        url: "Companies.aspx/SaveCompany",
        data: "{co: " + JSON.stringify(co) + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d == "OK") {
                //alert("Company saved!");
                dialog.close();
                editId = 0;
                $('.dlg-input').val('').parent().removeClass('is-dirty');

                $('#companiesTbl tr').remove();
                loadCompanies();

                $('.co-input').val('').parent().removeClass('is-dirty');
            }
            else {
                alert(data.d);
            }
        },
        error: function (res) {
            alert(res.responseText);
        }
    });
});

function getFromEdit() {
    var co = {
        ID: editId,
        CompanyName: $("#name").val(),
        CompanyAddress: $("#address").val(),
        CompanyCity: $("#city").val(),
        CompanyPhone: $('#phone').val(),
        CompanyFax: $('#fax').val(),
        CompanyEmail: $('#email').val(),
        CompanyPIN: $('#pin').val(),
        CompanyIBAN: $('#iban').val()
    };
    return co;
}


function fillCoData(co) {
    if (co.CompanyName != "" && co.CompanyName != null) $('#co-name').val(co.CompanyName).parent().addClass('is-dirty');
    if (co.CompanyAddress != "" && co.CompanyAddress != null) $('#co-address').val(co.CompanyAddress).parent().addClass('is-dirty');
    if (co.CompanyCity != "" && co.CompanyCity != null) $('#co-city').val(co.CompanyCity).parent().addClass('is-dirty');
    if (co.CompanyPhone != "" && co.CompanyPhone != null) $('#co-phone').val(co.CompanyPhone).parent().addClass('is-dirty');
    if (co.CompanyFax != "" && co.CompanyFax != null) $('#co-fax').val(co.CompanyFax).parent().addClass('is-dirty');
    if (co.CompanyEmail != "" && co.CompanyEmail != null) $('#co-email').val(co.CompanyEmail).parent().addClass('is-dirty');
    if (co.CompanyPIN != "" && co.CompanyPIN != null) $('#co-pin').val(co.CompanyPIN).parent().addClass('is-dirty');
    if (co.CompanyIBAN != "" && co.CompanyIBAN != null) $('#co-iban').val(co.CompanyIBAN).parent().addClass('is-dirty');
}


function highlightRow(row) {
    $(row).addClass("selected-row").siblings().removeClass("selected-row");
}
