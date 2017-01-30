
var objProposal = {};
var editId = 0;


$(document).ready(function () {
    $('.company-section').html("<i class='material-icons sectionIcon'>expand_less</i><span class='sectionTitle'>Company data</span>")
    $('.client-section').html("<i class='material-icons sectionIcon'>expand_less</i><span class='sectionTitle'>Client data</span>")
    $('.items-section').html("<i class='material-icons sectionIcon'>expand_less</i><span class='sectionTitle'>Items</span>")

    getParam('id');
    if (editId > 0) loadProposal();

    //todo: loadItems...
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
        },
        error: function (res) {
            alert(res.responseText);
        }
    });
}

function fillEdit(proposal) {
    $('#proposalTitle').text(proposal.ProposalName);
    $('#proposalName').val(proposal.ProposalName);
    $('#company').val(proposal.CompanyName);
    $('#companyAddress').val(proposal.CompanyAddress);
    $('#companyCity').val(proposal.CompanyCity);
    $('#companyPhone').val(proposal.CompanyPhone);
    $('#companyFax').val(proposal.CompanyFax);
    $('#companyEmail').val(proposal.CompanyEmail);
    $('#companyPIN').val(proposal.CompanyPIN);
    $('#companyIBAN').val(proposal.CompanyIBAN);
    $('#client').val(proposal.ClientName);
    $('#clientAddress').val(proposal.ClientAddress);
    $('#clientCity').val(proposal.ClientCity);
    $('#clientPhone').val(proposal.ClientPhone);
    $('#clientEmail').val(proposal.ClientEmail);
    $('#clientPIN').val(proposal.ClientPIN);
    $('#itemsTitle').val(proposal.ItemsTitle);
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


$('.exportProposal').click(function () {
    //saveProposal();
    exportProposal();
});

function exportProposal() {
    //var resultToExport = "";

    //$.each(objProposal, function (key, value) {
    //    console.log("KEY: " + key + " | " + "VALUE: " + value);

    //    if (resultToExport.length > 0) resultToExport += "|";
    //    resultToExport += key + ":" + value;
    //    //console.log('i = ' + counter + ' | obj = ' + Object.keys(objQandA).length)
    //    //if (counter == Object.keys(objQandA).length) {
    //    //    //console.log("QuestionID: " + key + " | EmployeeID: " + value);

    //    //    //spremanje u bazu ...
    //    //    readyToSave = true;
    //    //}
    //});

    $.ajax({
        type: "POST",
        url: "NewEditProposal.aspx/ExportProposal",
        //data: "{objQandA:" + JSON.stringify(objQandA) + "}",
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


function loadItems() {
    //todo: DbItems.. učitavanje podataka
}

//dodavanje stavki...
//ipak bez tablice, prekomplicirano za spremanje i izvlaćenje podataka...
//ovako se klonira div za unos stavki i svakom novom elementu se povećava broj u id-u
//kada dodajemo stavku, uzme se broj iz id-a zadnjeg diva i dodaje mu se 1, tako i za svaki element unutar tog diva
//npr. kada se klonira element sa id="itemText2", novi element će imati id="itemText3".
$('#btnAddItem').click(function () {
    //http://stackoverflow.com/questions/32013816/processing-dynamically-added-elements-material-design-lite
    // get the last div which id starts with ^= "item"
    var $div = $('div[id^="item"]:last');

    // read the number from that div's id (i.e: 3 from "item3")
    var num = parseInt($div.prop("id").match(/\d+/g), 10);

    // add that number to all elements and increment it by 1
    var clone = $('#item' + num).clone().prop('id', 'item' + (num + 1));

    $.each(clone.find('input'), function () {
        var id = $(this).attr('id');
        $(this).attr('id', id.replace(/\d+/g, (num + 1))).val('');
    });
    $.each(clone.find('label'), function () {
        var forId = $(this).attr('for');
        $(this).attr('for', forId.replace(/\d+/g, (num + 1)));
    });

    clone.find(':not([data-upgraded=""])').attr('data-upgraded', '');

    $('.items').append(clone);
    clone.show(200, function () {
        componentHandler.upgradeAllRegistered();
    });
});


//spremanje
$('.saveProposal').click(function () {
    var proposal = getData();
    var items = getItems();

    console.log(proposal, items);
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

    $.ajax({
        type: "POST",
        url: "NewEditProposal.aspx/SaveProposal",
        data: "{proposal:" + JSON.stringify(proposal) + ", items:" + JSON.stringify(items) + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d == "OK") {
                alert("Proposal saved!");
                window.location = "Proposals.aspx";
                //loadData(lastQuery, 0, 0);
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
        ItemsTitle: $("#itemsTitle").val()
    };
    return proposal;
}

function getItems() {
    var items = [];
    $.each($(".item"), function () {
        var item = {
            ID: 0,
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
