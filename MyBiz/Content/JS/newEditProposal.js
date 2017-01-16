﻿
var objProposal = {};


$(document).ready(function () {
    $('.company-section').html("<i class='material-icons sectionIcon'>expand_less</i><span class='sectionTitle'>Company data</span>")
    $('.client-section').html("<i class='material-icons sectionIcon'>expand_less</i><span class='sectionTitle'>Client data</span>")
});


$('.company-section').click(function () {
    toggleContent($(this), "Company data");
});

$('.client-section').click(function () {
    //$('.content-one').slideToggle('slow');
    toggleContent($(this), "Client data");
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


//btn for going one page back
//$("#proposal_arrowBack").click(function (e) {
//    e.preventDefault();
//    history.back(1);
//})


$('.saveProposal').click(function () {   
    saveProposal();    
});

$('.exportProposal').click(function () {
    //saveProposal();
    exportProposal();
});

function saveProposal() {
    if (!$('#proposalName').val()) {
        alert("Proposal name cannot be empty!");
        $('#proposalName').focus();
        return;
    }
    if (!$('#company').val()) {
        alert("Company name cannot be empty!");
        $('#company').focus();
        return;
    }


    $('.proposalInput :input').each(function () {
        var input = $(this);
        objProposal[input.attr('id')] = input.val();
    })


    $.ajax({
        type: "POST",
        url: "NewEditProposal.aspx/SaveProposal",
        //data: "{objQandA:" + JSON.stringify(objQandA) + "}",
        data: "{proposal:" + "'" + JSON.stringify(objProposal) + "'" + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            alert("Proposal successfully saved!");
            //if (data.d == "OK") {
            //    //alert("Rezultat uspješno poslan.");
            //    window.location = 'ThankYou.aspx?lang=' + lang;
            //}
            //else if (data.d == "IP address exists") {
            //    if (lang == "en") alert("You have already filled out the questionnaire!");
            //    else alert("Anketu je moguće ispuniti samo jednom!");
            //}
            //else if (data.d == "Error saving result") {
            //    if (lang == "en") alert("Error on sending data!");
            //    else alert("Greška prilikom slanja rezultata!");
            //}
            //else if (data.d == "Error saving IP") {
            //    if (lang == "en") alert("Possible connection error!");
            //    else alert("Moguća greška sa vezom!");
            //}
            //else {
            //    alert(data.d);
            //}
        },
        error: function (res) {
            alert(res.responseText);
        }
    });
}

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
            console.log(data, data.d, data.d[0], data.d[1]);
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


