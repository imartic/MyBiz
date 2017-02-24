
var objProposal = {};
var editId = 0;

var deletedItems = [];

var cosLoaded = false; //if companies load for autocomplete -> for companyName input


$(document).ready(function () {
    $('.company-section').html("<i class='material-icons sectionIcon'>expand_less</i><span class='sectionTitle'>Company data</span>")
    $('.client-section').html("<i class='material-icons sectionIcon'>expand_less</i><span class='sectionTitle'>Client data</span>")
    $('.items-section').html("<i class='material-icons sectionIcon'>expand_less</i><span class='sectionTitle'>Items</span>")
    $('.summary-section').html("<i class='material-icons sectionIcon'>expand_less</i><span class='sectionTitle'>Summary</span>")

    getParam('id');
    if (editId > 0) {
        loadProposal();
    }
});


function getParam(name) {
    var id = (location.search.split(name + '=')[1] || '').split('&')[0];
    editId = (id != "") ? parseInt(id) : 0; 
}


function loadProposal() {
    $.ajax({
        type: "POST",
        url: "NewEditProposal.aspx/LoadProposal",
        data: "{id:" + editId + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var proposal = jQuery.parseJSON(data.d);

            fillEdit(proposal);

            loadItems();
        },
        error: function (res) {
            alert(res.responseText);
        }
    });
}

function loadItems() {
    $.ajax({
        type: "POST",
        url: "NewEditProposal.aspx/LoadProposalItems",
        data: "{id:" + editId + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var items = jQuery.parseJSON(data.d);
            for (var i = 0; i < items.length; i++) {
                if (i > 0) {
                    addItem();
                }
                fillEditItems(items[i], i);

                $('#btnExport').prop('disabled', false);
            }
        },
        error: function (res) {
            alert(res.responseText);
        }
    });
}

function fillEdit(proposal) {
    $('#proposalTitle').text(proposal.ProposalName);
    $('#proposalName').val(proposal.ProposalName).parent().addClass('is-dirty');
    if (proposal.CompanyName != "" && proposal.CompanyName != null) ((cosLoaded) ? $('#company').val(proposal.CompanyName).parent().parent().addClass('is-dirty') : $('#company').val(proposal.CompanyName).parent().addClass('is-dirty'));
    if (proposal.CompanyAddress != "" && proposal.CompanyAddress != null) $('#companyAddress').val(proposal.CompanyAddress).parent().addClass('is-dirty');
    if (proposal.CompanyCity != "" && proposal.CompanyCity != null) $('#companyCity').val(proposal.CompanyCity).parent().addClass('is-dirty');
    if (proposal.CompanyPhone != "" && proposal.CompanyPhone != null) $('#companyPhone').val(proposal.CompanyPhone).parent().addClass('is-dirty');
    if (proposal.CompanyFax != "" && proposal.CompanyFax != null) $('#companyFax').val(proposal.CompanyFax).parent().addClass('is-dirty');
    if (proposal.CompanyEmail != "" && proposal.CompanyEmail != null) $('#companyEmail').val(proposal.CompanyEmail).parent().addClass('is-dirty');
    if (proposal.CompanyPIN != "" && proposal.CompanyPIN != null) $('#companyPIN').val(proposal.CompanyPIN).parent().addClass('is-dirty');
    if (proposal.CompanyIBAN != "" && proposal.CompanyIBAN != null) $('#companyIBAN').val(proposal.CompanyIBAN).parent().addClass('is-dirty');
    if (proposal.ClientName != "" && proposal.ClientName != null) $('#client').val(proposal.ClientName).parent().addClass('is-dirty');
    if (proposal.ClientAddress != "" && proposal.ClientAddress != null) $('#clientAddress').val(proposal.ClientAddress).parent().addClass('is-dirty');
    if (proposal.ClientCity != "" && proposal.ClientCity != null) $('#clientCity').val(proposal.ClientCity).parent().addClass('is-dirty');
    if (proposal.ClientPhone != "" && proposal.ClientPhone != null) $('#clientPhone').val(proposal.ClientPhone).parent().addClass('is-dirty');
    if (proposal.ClientEmail != "" && proposal.ClientEmail != null) $('#clientEmail').val(proposal.ClientEmail).parent().addClass('is-dirty');
    if (proposal.ClientPIN != "" && proposal.ClientPIN != null) $('#clientPIN').val(proposal.ClientPIN).parent().addClass('is-dirty');
    if (proposal.ItemsTitle != "" && proposal.ItemsTitle != null) $('#itemsTitle').val(proposal.ItemsTitle).parent().addClass('is-dirty');
    $('#amount').text(proposal.Amount);
    if (proposal.Tax != 0) {
        $('#tax').text(proposal.Tax);
        $('#cbTax').prop('checked', true);
    }
    else {
        $('#tax').hide();
        $('#cbTax').prop('checked', false);
    }
    $('#total').text(proposal.Total);
    if (proposal.Note != "" && proposal.Note != null) $('#note').val(proposal.Note).parent().addClass('is-dirty');
    if (proposal.Signature != "" && proposal.Signature != null) $('#signature').val(proposal.Signature).parent().addClass('is-dirty');
}

function fillEditItems(item, i) {
    $('#item' + i).data('id', item.ID);
    $('#itemNumber' + i).val(item.ItemNumber).parent().addClass('is-dirty');
    $('#itemText' + i).val(item.ItemText).parent().addClass('is-dirty');
    $('#itemUnit' + i).val(item.Unit).parent().addClass('is-dirty');
    $('#itemQuantity' + i).val(parseFloat(item.Quantity).toFixed(2)).parent().addClass('is-dirty');
    $('#itemPrice' + i).val(parseFloat(item.UnitPrice).toFixed(2)).parent().addClass('is-dirty');
    $('#itemTotalPrice' + i).val(parseFloat(item.TotalPrice).toFixed(2)).parent().addClass('is-dirty');
    //$('#btnDeleteItem' + i).data('id', item.ID);
    $('#btnDeleteItem' + i).attr('i-id', item.ID);
}


//--------- toggle sections ------------
$('.company-section').click(function () {
    toggleContent($(this), "Company data");
});

$('.client-section').click(function () {
    //$('.content-one').slideToggle('slow');
    toggleContent($(this), "Client data");
});

$('.items-section').click(function () {
    toggleContent($(this), "Items");
});

$('.summary-section').click(function () {
    toggleContent($(this), "Summary");
});

function toggleContent(section, title){
    $header = section;
    //getting the next element
    $content = $header.next();
    //open up the content needed - toggle the slide- if visible, slide up, if not slidedown.
    $content.slideToggle(100, function () {
        //execute this after slideToggle is done
        //change text of header based on visibility of content div
        $header.html(function () {
            //change text based on condition
            return $content.is(":visible")
                ? "<i class='material-icons sectionIcon'>expand_less</i><span class='sectionTitle'>" + title + "</span>"
                : "<i class='material-icons sectionIcon'>expand_more</i><span class='sectionTitle'>" + title + "</span>";
        });
    });
}
//--------------------------------------


function addItem() {
    //http://stackoverflow.com/questions/32013816/processing-dynamically-added-elements-material-design-lite
    // get the last div which id starts with ^= "item"
    var $div = $('div[id^="item"]:last');

    // read the number from that div's id (i.e: 3 from "item3")
    var num = parseInt($div.prop("id").match(/\d+/g), 10);

    // add that number to all elements and increment it by 1
    var clone = $('#item' + num).clone().prop('id', 'item' + (num + 1));

    $.each(clone.find('.mdl-textfield'), function () {
        var id = $(this).attr('id');
        $(this).attr('id', id.replace(/\d+/g, (num + 1))).val('');
    });
    $.each(clone.find('input'), function () {
        var id = $(this).attr('id');
        $(this).attr('id', id.replace(/\d+/g, (num + 1))).val('');
    });
    $.each(clone.find('label'), function () {
        var forId = $(this).attr('for');
        $(this).attr('for', forId.replace(/\d+/g, (num + 1)));
    });
    $.each(clone.find('button'), function () {
        var id = $(this).attr('id');
        $(this).attr('id', id.replace(/\d+/g, (num + 1)));
    });
    $.each(clone.find('.mdl-tooltip'), function () {
        var forId = $(this).attr('for');
        $(this).attr('for', forId.replace(/\d+/g, (num + 1)));
    });

    clone.find(':not([data-upgraded=""])').attr('data-upgraded', '');

    $('.items').append(clone);
    clone.show(200, function () {
        componentHandler.upgradeAllRegistered();
    });

    $('#itemNumber' + (num + 1)).val(num + 2).parent().addClass('is-dirty');
}

//dodavanje stavki...
//ipak bez tablice, prekomplicirano za spremanje i izvlaćenje podataka...
//ovako se klonira div za unos stavki i svakom novom elementu se povećava broj u id-u
//kada dodajemo stavku, uzme se broj iz id-a zadnjeg diva i dodaje mu se 1, tako i za svaki element unutar tog diva
//npr. kada se klonira element sa id="itemText2", novi element će imati id="itemText3".
$('#btnAddItem').click(function () {
    addItem();
});

function deleteItem(btn) {
    var itemId = $(btn).attr('i-id');
    var id = parseInt(btn.id.match(/\d+/g), 10);

    if ($('.item').length > 1) {
        if (confirm('Are you sure you want to delete this item?')) {
            $('#item' + id).remove();
            deletedItems.push(parseInt(itemId));
        }
    }    
}


//======= SAVE ========
$('#btnSave').click(function () {
    var proposal = getData();
    var items = getItems();

    //provjere
    if (proposal.ProposalName == "" || proposal.ProposalName == null) {
        alert("Proposal name cannot be empty!");
        $("#proposalName").focus();
        return;
    }
    if (proposal.CompanyName == "" || proposal.CompanyName == null) {
        alert("Company name cannot be empty!");
        $("#company").focus();
        return;
    }
    if (proposal.ClientName == "" || proposal.ClientName == null) {
        alert("Client name cannot be empty!");
        $("#client").focus();
        return;
    }

    var delItems = JSON.stringify(deletedItems);

    $.ajax({
        type: "POST",
        url: "NewEditProposal.aspx/SaveProposal",
        data: "{proposal:" + JSON.stringify(proposal) + ", items:" + JSON.stringify(items) + ", deletedItems:" + delItems + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d.substring(0, 2) == "OK") {
                alert("Proposal saved!", data);
                //window.location = "Proposals.aspx";

                var id = parseInt(data.d.replace(/[^0-9\.]/g, ''), 10);
                editId = id;
                $('#btnExport').prop('disabled', false);
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

function getData() {
    var proposal = {
        ID: editId,
        ProposalName: $("#proposalName").val(),
        CompanyName: $("#company").val(),
        CompanyAddress: $("#companyAddress").val(),
        CompanyCity: $("#companyCity").val(),
        CompanyPIN: $("#companyPIN").val(),
        CompanyPhone: $("#companyPhone").val(),
        CompanyFax: $("#companyFax").val(),
        CompanyEmail: $("#companyEmail").val(),
        CompanyIBAN: $("#companyIBAN").val(),
        ClientName: $("#client").val(),
        ClientAddress: $("#clientAddress").val(),
        ClientCity: $("#clientCity").val(),
        ClientPhone: $("#clientPhone").val(),
        ClientEmail: $("#clientEmail").val(),
        ClientPIN: $("#clientPIN").val(),
        ItemsTitle: $("#itemsTitle").val(),
        Amount: $('#amount').text(),
        Tax: ($('#cbTax').is(':checked')) ? $('#tax').text() : 0,
        Total: $('#total').text(),
        Note: $('#note').val(),
        Signature: $('#signature').val()
    };
    return proposal;
}

function getItems() {
    var items = [];
    $.each($(".item"), function () {
        var item = {
            ID: $(this).data('id'),
            ProposalID: editId,
            ItemNumber: $(this).children().find("input[id^='itemNumber']").val(),
            ItemText: $(this).children().find("input[id^='itemText']").val(),
            Unit: $(this).children().find("input[id^='itemUnit']").val(),
            Quantity: $(this).children().find("input[id^='itemQuantity']").val(),
            UnitPrice: $(this).children().find("input[id^='itemPrice']").val(),
            TotalPrice: $(this).children().find("input[id^='itemTotalPrice']").val()
        };
        items.push(item);
    });
    
    return items;
}
//=======================

//========= EXPORT ==========
$('#btnExport').click(function () {
    //exportProposal();

    if (editId > 0) {
        //window.location = 'Download.aspx?id=' + eventId + '?type=' + type;
        var win = window.open('Download.aspx?id=' + editId, '_blank');
        win.focus();
    }
});

function exportProposal() {
    $.ajax({
        type: "POST",
        url: "NewEditProposal.aspx/ExportProposal",
        data: "{proposalId:" + editId + "}",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d[0] == "exported") {
                alert("Excel file succesfully exported as " + data.d[1]);
            }
            else if (data.d[0] == "excel not installed") {
                alert("Excel not installed!");
            }
            else if (data.d[0] == "") {
                alert("Error on saving proposal!", data);
            }
            else if (data.d[0] == "proposal not saved") {
                alert("Proposal is not saved!");
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
//====================


function onQuantityChange(input) {
    $(input).val(parseFloat($(input).val()).toFixed(2));
    var id = parseInt(input.id.match(/\d+/g), 10);

    if($('#itemPrice'+id).val() != ''){
        var total = $(input).val() * $('#itemPrice' + id).val();
        $('#itemTotalPrice' + id).val(total.toFixed(2)).change().parent().addClass('is-dirty');
    }    
}

function onPriceChange(input) {
    $(input).val(parseFloat($(input).val()).toFixed(2));
    var id = parseInt(input.id.match(/\d+/g), 10);

    if($('#itemQuantity'+id).val() != ''){
        var total = $(input).val() * $('#itemQuantity' + id).val();
        $('#itemTotalPrice' + id).val(total.toFixed(2)).change().parent().addClass('is-dirty');
    }
}

function onTotalPriceChange(ctrl) {
    var sum = 0;
    $('.itemTotalPrice').each(function () {
        sum += Number($(this).val());
    });

    $('#amount').text(sum.toFixed(2));
    calcTaxAndTotal();
}

$('#cbTax').click(function () {
    if ($(this).is(':checked')) {
        $('#tax').show();
        calcTaxAndTotal();
    } else {
        $('#tax').hide();
        $('#total').text(parseFloat($('#amount').text()));
    }
});

function calcTaxAndTotal() {
    $('#tax').text((($('#amount').text()) * 0.25).toFixed(2));
    $('#total').text(((parseFloat($('#amount').text())) + (parseFloat($('#tax').text()))).toFixed(2));
}



/******* autocomplete *******/
$.ajax({
    type: "POST",
    url: "Companies.aspx/LoadCompanies",
    data: "{}",
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    success: function (data) {
        var companies = jQuery.parseJSON(data.d);

        if (companies != null && companies.length > 0) {
            var options = {
                data: companies,
                getValue: "CompanyName",
                list: {
                    maxNumberOfElements: 20,
                    match: {
                        enabled: true
                    },

                    onSelectItemEvent: function () {
                        var address = $("#company").getSelectedItemData().CompanyAddress;
                        var city = $("#company").getSelectedItemData().CompanyCity;
                        var phone = $("#company").getSelectedItemData().CompanyPhone;
                        var fax = $("#company").getSelectedItemData().CompanyFax;
                        var email = $("#company").getSelectedItemData().CompanyEmail;
                        var pin = $("#company").getSelectedItemData().CompanyPIN;
                        var iban = $("#company").getSelectedItemData().CompanyIBAN;

                        (address != null && address != '') ? $("#companyAddress").val(address).parent().addClass('is-dirty') : $("#companyAddress").val('').parent().removeClass('is-dirty');
                        (city != null && city != '') ? $("#companyCity").val(city).parent().addClass('is-dirty') : $("#companyCity").val('').parent().removeClass('is-dirty');
                        (phone != null && phone != '') ? $("#companyPhone").val(phone).parent().addClass('is-dirty') : $("#companyPhone").val('').parent().removeClass('is-dirty');
                        (fax != null && fax != '') ? $("#companyFax").val(fax).parent().addClass('is-dirty') : $("#companyFax").val('').parent().removeClass('is-dirty');
                        (email != null && email != '') ? $("#companyEmail").val(email).parent().addClass('is-dirty') : $("#companyEmail").val('').parent().removeClass('is-dirty');
                        (pin != null && pin != '') ? $("#companyPIN").val(pin).parent().addClass('is-dirty') : $("#companyPIN").val('').parent().removeClass('is-dirty');
                        (iban != null && iban != '') ? $("#companyIBAN").val(iban).parent().addClass('is-dirty') : $("#companyIBAN").val('').parent().removeClass('is-dirty');
                    }
                }
            };

            cosLoaded = true;

            $("#company").easyAutocomplete(options);
        }
    },
    error: function (res) {
        alert(res.responseText);
    }
});
