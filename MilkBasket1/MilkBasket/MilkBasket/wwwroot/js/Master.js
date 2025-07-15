
function Hidegrid() {
    $("#griddiv").hide()
    $("#hidshowhead").show()
}
function showgrid() {
    $("#hidshowhead").hide()
    $("#griddiv").show()

}

function formatDate(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2)
        month = '0' + month;
    if (day.length < 2)
        day = '0' + day;

    return [year, month, day].join('-');
}

function Validation() {
    var msg = "";
    var charregex = /^[a-zA-Z\s]+$/;
    var intregex = /^[0-9]+$/;
    var charintregex = /^[a-zA-Z0-9\s]+$/;


    $(".mandatory").each(function () {
        if ($(this).val() == "" || $(this).val() == "0") {
            var name = $(this).attr('name')
            console.log($(this).val());
            msg += "" + name + "  Required !!\n";
        }
    });

    $(".checktaxes").each(function () {
        if ($(this).val() == "") {
            var name = $(this).attr('name')
            console.log($(this).val());
            msg += "" + name + "  Required !!\n";
        }
    });
    $(".chkint").each(function () {
        if (!intregex.test($(this).val())) {
            var name = $(this).attr('name')
            msg += "Enter Valid " + name + "!!\n";
        }
    });
    $(".chkcharint").each(function () {
        if (!charintregex.test($(this).val())) {
            var name = $(this).attr('name')
            msg += "Enter Valid " + name + "!!\n";
        }
    });
    $(".chkchar").each(function () {
        if (!charregex.test($(this).val())) {
            var name = $(this).attr('name')
            msg += "Enter Valid " + name + "!!\n";
        }
    });
    return msg;
}
function saveWithFile(var_url, var_data, var_type, var_ct, var_dt) {
    if (var_type == "")
        var_type = "POST";

    if (var_ct == "")
        var_ct = "application/json;charset=utf-8";

    if (var_dt == "")
        var_dt = "json";

    $.ajax({
        url: var_url,
        type: var_type,
        contentType: false,
        processData: false,
        data: var_data,
        success: function (data) {
            console.log(data);
            if (data.Message != "") {
                swal("Message", data.Message, data.Message == "Success" ? "success" : "error");
                clear();
            }
            if (data.Message == "Success") {
                clear();
            }
            if (data.Data != undefined) {
                CreateTableFromArray(data.Data, "Printdiv");
                dtable();
            }
        },
        error: function (err) {
            alert(err.statusText);
        }
    });
}


function CommonAjax(var_url, var_data, var_type, var_ct, var_dt, UserData, divid) {

    if (var_type == "")
        var_type = "POST";

    if (var_ct == "")
        var_ct = "application/json;charset=utf-8";

    if (var_dt == "")
        var_dt = "json";

    $.ajax({
        url: var_url,
        data: { jsonData: var_data, UserData: UserData },
        type: var_type,
        //contentType: var_ct,
        dataType: var_dt,
        success: function (data) {

         
            if (data.Message != "" && data.Message != " " && data.Message !="Show Data") {
                swal("Message", data.Message, data.Status == "Success" ? "success" : "error").then(() => {
                    if (data.Status != "Failed") {
                        showgrid(); 
                        window.location.reload();
                    }
                     
                }) 
               
            }
            if (data.Message == "Success") {
               
                clear();
            }
            $('#' + divid).empty();
            if (data.Message == " " && data1 == null) {
                swal("Message", "No Data Available..!", "error");
            }
            console.log(data.Message);
            if (data.Data != undefined && data.Data != '[]') {
                if (divid == "printdivReport") {
                    $("#loader").addClass("d-none");
                }
                CreateTableFromArray(data.Data, divid);
               // dtable();
            }
            if (divid == "printdivReport") {
                if (data.Data == undefined && data.Message == "") {
                    var Arr = [{ "Message":"No Data Available..!"}]
                    CreateTableFromArray(Arr, divid);
                    $("#loader").addClass("d-none");
                }
            }

            
            if (divid == 'grid') {
                if (data.Data != undefined) {

                
                    for (var e of data.Data) {

                        let geturl = localStorage.getItem("Url");
                        var div = `

                <a href="${geturl}${e.UrlLink}" style="text-decoration: none; ">
                                                            <div class="dashboard-box" >
                                                                <i class="${e.IconClass}" style="font-size:40px !important; color: #fff;"></i>
                                                                <p style='color: #fff; font-size: 18px;' id="${e.CountId}" style="color: #fff;">0</p>
                                                                <p style="color: #fff; font-size: 18px; margin-top: 10px;">${e.TitleName} </p>
                                                            </div>
                                                        </a>
                
                
                `
                        $(".dashboard").append(div);

                    }
                }
                else {
                    $(".dashboard").css("display","block")
                    $(".dashboard").append('<div class="alert alert-danger text-center" role="alert">You Have No Rights</div >');
                }

            }
            //if (data.Data == undefined && data.Message != "Success") {

            //    $("#" + divid).append('<table class="table "><tr><td colspan="3" style="text-align:center; color:red; font-size:20px;">You Have No Rights</td></tr><table>');

            //}

        },
        error: function (data) {
            $("#loader").addClass("d-none");
            console.log(data);
            var data = {
                status: "Error",
                msg: "Error on server.",
                data: [],
            }

        },
    });
}

function Bindtrntable(var_url, var_data, var_type, var_ct, var_dt, UserData, divid) {

    if (var_type == "")
        var_type = "POST";

    if (var_ct == "")
        var_ct = "application/json;charset=utf-8";

    if (var_dt == "")
        var_dt = "json";

    $.ajax({
        url: var_url,
        data: { jsonData: var_data, UserData: UserData },
        type: var_type,
        //contentType: var_ct,
        dataType: var_dt,
        success: function (data) {
            console.log(data);
            console.log(data.Data);
            //var data1 = JSON.parse(data[0].Data);
         
             if (divid == 1) {
                if (data.Status == "error") {
                    swal({
                        icon: "error",
                        title: data.Message,
                        text: "error",
                        timer: 2000
                    });

                }


             
                 var array = data.Data;
                 if (array.length > 0) {
                     $(".Availablebody").empty();
                     var row = ""
                     for (var i = 0; i < array.length; i++) {

                         row += "<tr id='row" + parseInt(i + 1) + "' onclick='addselectedpost(this)' style=' background - color: #f2f2f2;cursor: pointer; ' class='active' ><td style='display:none;'><span class='Hid_beatid'>" + array[i].Hid_beatid + "</span></td><td><span class='PostName'>" + array[i].PostName + "</span></td></tr > ";

                     }
                     $(".Availablebody").prepend(row);

                 }

                else {
                    $(".Availablebody").empty();
                }

            }
            else if (divid == 2) {
                if (data.Status == "error") {
                    swal({
                        icon: "error",
                        title: data.Message,
                        text: "error",
                        timer: 2000
                    });

                }

              
                var array = data.Data;
                if (array != undefined) {
                    $("#Availablebody").empty();
                    var row = ""
                    for (let i in array) {

                        row += "<tr id='row" + parseInt(i + 1) + "' onclick='addroute(this)' style=' background - color: #f2f2f2;cursor: pointer; ' ><td><span class='Routename'>" + array[i].Routename + "</span></td><td class='d-none'><span  id='Routecode'>" + array[i].routecode + "</span> <span  class='RouteId'>" + array[i].RouteId + "</span></td></tr > ";

                    }
                    $("#Availablebody").prepend(row);

                }
                else {
                    $("#Availablebody").empty();
                }

            }
            else if (divid == '3') {
                if (data.Status == "error") {
                    swal({
                        icon: "error",
                        title: data.Message,
                        text: "error",
                        timer: 2000
                    });

                }

                var array = data.Data;
                if (array != undefined) {
                    $("#Selectedbody").empty();
                    var row = ""
                    for (let i in array) {

                        row += "<tr id='row" + parseInt(i + 1) + "' onclick='addshift(this)' style=' background - color: #f2f2f2;cursor: pointer; ' class='active' ><td style='display:none;'><span id='shiftid'>" + array[i].shift_id + "</span></td><td><span class='shift'>" + array[i].shift + "</span></td></tr > ";

                    }
                    $("#Selectedbody").append(row);

                }
                else {
                    $("#Selectedbody").empty();
                }

            }

             else if (divid == '4') {
                 if (data.Status == "error") {
                     swal({
                         icon: "error",
                         title: data.Message,
                         text: "error",
                         timer: 2000
                     });

                 }

                 var array = data.Data;
                 if (array != undefined) {
                     $("#Selectedbody").empty();
                     var row = ""
                     for (let i in array) {

                         row += "<tr id='row" + parseInt(i + 1) + "' onclick='addshift(this)' style=' background - color: #f2f2f2;cursor: pointer; ' class='active' ><td style='display:none;'><span id='beatid'>" + array[i].BeatShift_Id + "</span></td><td><span class='Beat'>" + array[i].Beat + "</span></td></tr > ";

                     }
                     $("#Selectedbody").append(row);

                 }
                 else {
                     $("#Selectedbody").empty();
                 }

             }

        },

        error: function (data) {
            var data = {
                status: "Error",
                msg: "Error on server.",
                data: [],
            }

        },

    });

}


function CommonAjaxBom(var_url, var_data, var_type, var_ct, var_dt, encrp, divid) {

    if (var_type == "")
        var_type = "POST";

    if (var_ct == "")
        var_ct = "application/json;charset=utf-8";

    if (var_dt == "")
        var_dt = "json";

    $.ajax({
        url: var_url,
        data: { jsonData: var_data, UrlEncript: encrp },
        type: var_type,
        //contentType: var_ct,
        dataType: var_dt,
        success: function (data) {
            console.log(data);
            if (data.Message != "") {
                swal("Message", data.Message, data.Message == "Success" ? "success" : "error");
            }
            if (data.Message == "Success") {
                clear();
            }
            if (data.Data != undefined) {
                CreateTableFromArray(data.Data, divid);
                //dtable();
            }
        },
        error: function (data) {
            var data = {
                status: "Error",
                msg: "Error on server.",
                data: [],
            }

        },
    });
}


function Common(var_url, var_data, var_type, var_ct, var_dt, type) {

    if (var_type == "")
        var_type = "POST";

    if (var_ct == "")
        var_ct = "application/json;charset=utf-8";

    if (var_dt == "")
        var_dt = "json";

    $.ajax({
        url: var_url,
        data: var_data,
        type: var_type,
        //contentType: var_ct,
        dataType: var_dt,
        success: function (data) {
            //console.log(data);
            CalculateTaxAmount(data, type);
        },
        error: function (data) {
            var data = {
                status: "Error",
                msg: "Error on server.",
                data: [],
            }

        },
    });
}

function GetData(var_url, var_data, var_type, var_ct, var_dt, UserData, divid) {

    if (var_type == "")
        var_type = "POST";

    if (var_ct == "")
        var_ct = "application/json;charset=utf-8";

    if (var_dt == "")
        var_dt = "json";

    $.ajax({
        url: var_url,
        data: { jsonData: var_data, UserData: UserData },
        type: var_type,
        //contentType: var_ct,
        dataType: var_dt,
        success: function (data) {
            console.log(data);
            if (data[0].Message != "" && data[0].Message != " ") {
                swal("Message", data[0].Message, data[0].Status == "Success" ? "success" : "error");
            }
            if (data[0].Message == "Success") {
                clear();
            }
            console.log(data[0].Data);
            FillTextBoxes(data[0].Data);
            //if (data.Data != undefined) {
            //    CreateTableFromArray(data.Data, divid);
            //    //dtable();
            //}
        },
        error: function (data) {
            var data = {
                status: "Error",
                msg: "Error on server.",
                data: [],
            }

        },
    });
}



function BindDropdownsingle(var_url, var_data, var_type, var_ct, var_dt, var_id,optname) {
    if (var_type == "")
        var_type = "POST";

    if (var_ct == "")
        var_ct = "application/json;charset=utf-8";

    if (var_dt == "")
        var_dt = "json";

    $.ajax({
        type: var_type,
        url: var_url,
        data: var_data,
        async: false,
        success: function (json, result) {
            $(var_id).empty();
            json = JSON.parse(json);
            json = json || {};

            if (optname !== "") {
                $(var_id).append('<option value="0">' + optname + '</option>');
            }
           
            for (var i = 0; i < json.length; i++) {
                $(var_id).append('<option value="' + json[i].Id + '">' + json[i].Name + '</option>');
            }

        },
        error: function () {
            alert("Data Not Found");
        }
    });
}

function populateDropdown(dropdownId, dataArray) {
    var $dropdown = $(dropdownId);
    $dropdown.empty(); // Clear existing options
    $dropdown.append('<option value="0">Select</option>');
    $.each(dataArray, function (index, item) {
        $dropdown.append($('<option></option>').attr('value', item.ID).text(item.NAME));
    });
}

function isValidInput(input) {
    const regex = /^[^\s]+(?:[^\s]*\s*)?$/;
    return regex.test(input);
}

function validateInput(element) {
 
      element.value = element.value.replace(/\s+/g, '');
      
    
}

function CreateTableFromArray(arrItems, divid) {
    let col = [];
    for (let i = 0; i < arrItems.length; i++) {
        for (let key in arrItems[i]) {
            if (col.indexOf(key) === -1) {
                col.push(key);
            }
        }
    }

    // Create a Table.
    let table = document.createElement("table");
    table.setAttribute('id', 'data-table');
    table.setAttribute('class', 'table table-bordered');
    // Create table header row.

    let tr = table.insertRow(-1)	// table row.

    for (let i = 0; i < col.length; i++) {
        let th = document.createElement('th');  // table header.
        //console.log(col[i]);
        th.innerHTML = col[i];
        var result = col[i].includes("Hid_") == true ? 'none' : '';
        tr.appendChild(th);
        th.setAttribute('style',
            'font:18px Calibri;border: solid 1px #DDD;' +
            'border-collapse: collapse; font-weight:bold;' +
            'padding: 2px 3px; text-align: center;' +
            'display:' + result + ';'
        );
    }

    // Add JSON to the table as rows.
    for (let z = 0; z < arrItems.length; z++) {
        tr = table.insertRow(-1);

        for (let j = 0; j < col.length; j++) {
            let tabCell = tr.insertCell(-1);
            var result = col[j].includes("Hid_") == true ? 'none' : '';
            tabCell.innerHTML = arrItems[z][col[j]];
            tabCell.setAttribute('style',
                'font:18px Calibri;border: solid 1px #DDD;' +
                'border-collapse: collapse; ' +
                'padding: 2px 3px; text-align: center;' +
                'display:' + result + ';'
            );
            tabCell.setAttribute('class', "" + col[j].replace(" ", "").replace(" ","") + "");
            tabCell.setAttribute('id', "" + col[j].replace(" ", "").replace(" ", "") + "" + z + "");

        }
    }

    // Show the table.
    let container = document.getElementById('' + divid + '');
    if (container != null) {
        container.appendChild(table);
    }
   

    $('#filterInput').on('keyup', function () {
        let filter = $(this).val().toUpperCase();


        $('#data-table tbody tr').each(function (index) {
            if (index === 0) return;

            let row = $(this);
            let match = false;

            // Check each cell in the row
            row.find('td').each(function () {
                if ($(this).text().toUpperCase().indexOf(filter) > -1) {
                    match = true;
                    return false;
                }
                else {
                    row.add('<tr><td>Not Found</td></tr>')
                }
            });


            row.toggle(match);
        });
    });

}

function getFileUrl(fileId, folder) {
    var currentdate = new Date();
    var currdate = currentdate.getDate() + "" + currentdate.getMonth() + "" + currentdate.getFullYear() + "" + currentdate.getHours() + "" + currentdate.getMinutes() + "" + currentdate.getSeconds() + "" + currentdate.getMilliseconds();
    var filename = $(fileId).val();
    var name = filename.substr(0, filename.lastIndexOf('.'));
    var dataimg = name.split("\\");
    var extension = filename.replace(/^.*\./, '');
    var url_add = window.location.href;
    var data = url_add.split("://")
    var protocol = data[0];
    data = data[1].split("/");
    var domain = data[0];
    //var urlimd = protocol + "://" + domain + "/" + folder + "/" + dataimg[2] + currdate + "." + extension;
    var urlimd = dataimg[2] + currdate + "." + extension;
    //console.log(urlimd);
    return { url: urlimd, fname: dataimg[2] + currdate + "." + extension };
}

function fnLoadVotesForm() {

    var STATE_code = $("#trntype").val();;


    var URL = '/Transcation/_UploadPartialView?id=' + STATE_code + '';
    $.ajax({
        type: "GET",
        //contentType: "application/json; charset=utf-8",
        url: URL,
        data: "{}",
        dataType: 'html',
        success: function (data) {
            //console.log(data);
            $("#Printdivfile").html(data)
        },
        error: function (result) {

        }
    });



}

function getfile() {
    url_add = window.location.href;
    var data = url_add.split("://");
    data = data[1].split("/");
    var menuname = data[1] + "/" + data[2];
    var url_add = window.location.protocol + "//" + window.location.host + "/";
    var url = url_add + 'api/ApiServices/SaveTransactions';
    var Hid_Con = $("#cid").val() + "##" + $("#UserId").val() + "##" + menuname + "##" + $("#flgmode").val();
    //if ($("#txtsonumber").val() == "") {
    //	alert("Enter SO Number !!");
    //	$("#ddlitem").val(0);
    //}
    //else {
    var Data = {

        trntype: $("#trntype").val(),
        trnid: $("#trnid").val(),
        type: 121
    }

    CommonAjaxBom(url, JSON.stringify(Data), "", "", "", "", "PrintdivModal");
    //}
}
function exportexcel(fileName) {
    const table = document.getElementById("data-table").cloneNode(true);
    const rows = table.querySelectorAll("tr");
    const columnsToRemove = new Set();

    // Identify columns to remove (e.g., starts with Hid_ or Action)
    rows[0].querySelectorAll("th, td").forEach((cell, index) => {
        const text = cell.textContent.trim();
        if (text.startsWith("Hid_") || text.startsWith("Action")) {
            columnsToRemove.add(index);
        }
    });

    // Remove marked columns
    rows.forEach(row => {
        Array.from(row.children).forEach((cell, idx) => {
            if (columnsToRemove.has(idx)) cell.remove();
        });
    });

    // Convert cleaned table to worksheet
    const wb = XLSX.utils.book_new();
    const ws = XLSX.utils.table_to_sheet(table);

    // Style the header row (only works with xlsx-style)
    const range = XLSX.utils.decode_range(ws['!ref']);
    for (let C = range.s.c; C <= range.e.c; ++C) {
        const addr = XLSX.utils.encode_cell({ c: C, r: 0 });
        if (ws[addr]) {
            ws[addr].s = {
                fill: {
                    fgColor: { rgb: "4F81BD" },
                    patternType: "solid"
                },
                font: {
                    bold: true,
                    color: { rgb: "FFFFFF" }
                },
                alignment: {
                    horizontal: "center"
                },
                border: {
                    top: { style: "thin", color: { rgb: "000000" } },
                    bottom: { style: "thin", color: { rgb: "000000" } },
                    left: { style: "thin", color: { rgb: "000000" } },
                    right: { style: "thin", color: { rgb: "000000" } }
                }
            };
        }
    }

    // Auto column width
    const colWidths = [];
    for (let C = range.s.c; C <= range.e.c; ++C) {
        let maxLen = 10;
        for (let R = range.s.r; R <= range.e.r; ++R) {
            const addr = XLSX.utils.encode_cell({ c: C, r: R });
            const cell = ws[addr];
            if (cell && cell.v) {
                const len = cell.v.toString().length;
                if (len > maxLen) maxLen = len;
            }
        }
        colWidths.push({ wch: maxLen + 2 });
    }
    ws['!cols'] = colWidths;

    XLSX.utils.book_append_sheet(wb, ws, "Sheet1");
    XLSX.writeFile(wb, fileName + ".xlsx");
}


function showLoader() {
    $("#loaderOverlay").removeClass("d-none");
}

function hideLoader() {
    $("#loaderOverlay").addClass("d-none");
}

