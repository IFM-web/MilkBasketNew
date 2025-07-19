function InsUpt_Ajax(var_url, var_data, var_type, var_ct, var_dt) {

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
            swal("Message",data.status, data.status == "Saved Successfully" ? "success" :"error");
            if (data.data == "") { } else { $("#Printdiv").html(data.data); dtable(); }


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
function BindDiv(var_url, var_data, var_type, var_ct, var_dt) {
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

            $("#Printdiv").html(data)
            //loadOtherExpand();
            dtable();


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
function BindDropdown(var_url, var_data, var_type, var_ct, var_dt, var_id) {
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
            json = json || {};
            $(var_id).append('<option value="0">Select</option>');
            for (var i = 0; i < json.length; i++) {
                $(var_id).append('<option value="' + json[i].value + '">' + json[i].text + '</option>');
            }

        },
        error: function () {
            alert("Data Not Found");
        }
    });
}

function BindTextbox(var_url, var_data, var_type, var_ct, var_dt, var_id) {
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

            $(var_id).val(data[0].printOrder);


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

function MenuRightst_Ajax(var_url, var_data, var_type, var_ct, var_dt) {

    if (var_type == "")
        var_type = "POST";

    if (var_ct == "")
        var_ct = "application/json;charset=utf-8";

    if (var_dt == "")
        var_dt = "json";


    $.ajax({
        url: var_url,
        data: JSON.stringify(var_data),
        type: var_type,
        contentType: var_ct,
        dataType: var_dt,
        success: function (data) {
            //return data;
            swal("Message", data.status, data.status == "Saved Successfully" ? "success" : "error");
            if (msg.data == "") { } else { $("#Printdiv").html(msg.data) }


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
function Bindrpt(var_url, var_data, var_type, var_ct, var_dt) {
    if (var_type == "")
        var_type = "POST";

    if (var_ct == "")
        var_ct = "application/json;charset=utf-8";

    if (var_dt == "")
        var_dt = "json";


    $.ajax({
        url: var_url,
        data: { JsonData: var_data },
        type: var_type,
        //contentType: var_ct,
        dataType: var_dt,
        success: function (response) {
            if (response.length == 0)
                alert('Some error occured while downaload Format');
            else {
                $('#Printdiv').html(response);
                dtable();
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
function Bindrpt1(var_url, var_data, var_type, var_ct, var_dt) {
    if (var_type == "")
        var_type = "POST";

    if (var_ct == "")
        var_ct = "application/json;charset=utf-8";

    if (var_dt == "")
        var_dt = "json";


    $.ajax({
        url: var_url,
        data: { JsonData: var_data },
        type: var_type,
        //contentType: var_ct,
        dataType: var_dt,
        success: function (response) {
            //var response = JSON.parse(response.data);
            if (response.length == 0)
                alert('Some error occured while downaload Format');
            else {
                //var result = response[0]["ModifyDate"];
                //console.log(result);
                $('#Printdiv1').html(response);
                dtable();
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

function BindDivCommon1(var_url, var_data, var_type, var_ct, var_dt, var_divid,modalid) {
    if (var_type == "")
        var_type = "POST";

    if (var_ct == "")
        var_ct = "application/json;charset=utf-8";

    if (var_dt == "")
        var_dt = "json";


    $.ajax({
        url: var_url,
        data: {
            JsonData: var_data
        },
        type: var_type,
        //contentType: var_ct,
        dataType: var_dt,
        success: function (data) {

            $(var_divid).html(data)
            $(modalid).modal('show');
            //loadOtherExpand();
            //$('#data-table').DataTable();
            //dtable();


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

function DownLoad(var_url, var_data, var_type, var_ct, var_dt) {
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
        success: function (response) {
            if (response.length == 0)
                alert('Some error occured while downaload Format');
            else {
                $('#Printdiv').html(response);
                exportexcele('xlsx');
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

function Upload(var_url, var_data, var_type, var_ct, var_dt) {

    if (var_type == "")
        var_type = "POST";

    if (var_ct == "")
        var_ct = "application/json;charset=utf-8";

    if (var_dt == "")
        var_dt = "json";

    $.ajax({
        type: "POST",
        //data: { TypeValue: TypeValue, UploadTypename: TypeName, fdata: fdata},
        url: var_url,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: var_data,
        contentType: false,
        processData: false,
        success: function (response) {
            if (response.length == 0)
                alert('Some error occured while uploading');
            else {
                $('#Printdiv').html(response);
                $("#data-table TBODY tr").each(function () {
                    var col_val = $(this).find("td:eq(0)").text();
                    if (col_val == "Added And Modifyied Successfully") {
                        //$(this).addClass('Uploaded');
                        $(this).find("td:eq(0)").addClass('Uploaded');
                    } else {
                        $(this).addClass('NotUploaded');
                        //$(this).find("td:eq(0)").addClass('NotUploaded');
                    }
                });
                alert("Uploaded Successfully");
            }
        },
        error: function (e) {
            $('#Printdiv').html(e.responseText);
        }
    });
}

function CommonAjax(var_url, var_data, var_type, var_ct, var_dt) {

    if (var_type == "")
        var_type = "POST";

    if (var_ct == "")
        var_ct = "application/json;charset=utf-8";

    if (var_dt == "")
        var_dt = "json";

    //alert(var_data);

    $.ajax({
        url: var_url,
        data: {
            JsonData: var_data
        },
        type: var_type,
        //contentType: var_ct,
        dataType: var_dt,
        success: function (data) {
            //console.log(data)
            if (data.messa != "") {
                swal("Message", data.messa, data.messa=="Saved Successfully"?"success":"error");
            }
            clear();
            if (data.datagrid == "") { } else {
                $("#Printdiv").html(data.datagrid);
                //$('#data-table').DataTable();
                dtable();
                
            }
            $('#data-table').DataTable();


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

function BindDivCommon(var_url, var_data, var_type, var_ct, var_dt) {
    if (var_type == "")
        var_type = "POST";

    if (var_ct == "")
        var_ct = "application/json;charset=utf-8";

    if (var_dt == "")
        var_dt = "json";


    $.ajax({
        url: var_url,
        data: {
            JsonData: var_data
        },
        type: var_type,
        //contentType: var_ct,
        dataType: var_dt,
        success: function (data) {

            $("#Printdiv").html(data)
            //loadOtherExpand();
            //$('#data-table').DataTable();
            dtable();


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

function getDatagrid(var_url, var_data, var_type, var_ct, var_dt) {
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
        datatype: var_dt,
        success: function (data) {
            $("#Printdiv1").html(data.stddata);
            $("#Printdiv2").html(data.cdata);
            /*-------*/
           

        }, error: function (er) {
            alert(er);
        }
    });
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
            swal("Message", data.status, data.status == "Saved Successfully" ? "success" : "error");
            if (data.data == "") { } else { $("#Printdiv").html(data.data); dtable(); }
        },
        error: function (err) {
            alert(err.statusText);
        }
    });
}

function bindParams(var_url, var_data, var_type, var_ct, var_dt) {
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
        dataType: var_dt,
        success: function (data) {
            
            var Val = JSON.parse(data);

            
            if (Val[0].StartDate != "" && Val[0].StartDate != null) {
                $("#divd1").show();
                $("#dt1").html(Val[0].StartDate);
            }
            else {
                $("#divdate1").hide();
            }

            if (Val[0].EndDate != "" && Val[0].EndDate != null) {
                $("#divd2").show();
                $("#dt2").html(Val[0].EndDate);
            } else {
                $("#divdate2").hide();
            }
            if (Val[0].Course != "" && Val[0].Course != null) {
                $("#divcourse").show();
                //$("#st2").html(Val[0].State);
            } else {
                $("#divcourse").hide();
            }
            if (Val[0].Semester != "" && Val[0].Semester != null) {
                $("#divsemester").show();
                //$("#ac2").html(Val[0].Ac);
            } else {
                $("#divsemester").hide();
            }
            if (Val[0].Teacher != "" && Val[0].Teacher != null) {
                $("#divteacher").show();
                //"#party").html(Val[0].Party);
            } else {
                $("#divteacher").hide();
            }

            if (Val[0].Student != "" && Val[0].Student != null) {
                $("#divstudent").show();
                //$("#party").html(Val[0].Party);
            } else {
                $("#divstudent").hide();
            }

            //if (Val[0].Users != "" && Val[0].Users != null) {
            //    $("#divUser").show();
            //    $("#User").html(Val[0].Users);
            //} else {
            //    $("#divUser").hide();
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

function getFileUrl(fileId,folder) {
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
    var urlimd = protocol + "://" + domain + "/" + folder+ "/" + dataimg[2] + currdate + "." + extension;
    console.log(urlimd);
    return {url:urlimd,fname: dataimg[2] + currdate + "." + extension };
}

function getstudata(var_url, var_data, var_type, var_ct, var_dt) {

    if (var_type == "")
        var_type = "POST";

    if (var_ct == "")
        var_ct = "application/json;charset=utf-8";

    if (var_dt == "")
        var_dt = "json";

    //alert(var_data);

    $.ajax({
        url: var_url,
        data: {
            JsonData: var_data
        },
        type: var_type,
        //contentType: var_ct,
        dataType: var_dt,
        success: function (data) {
            //console.log(data)
            if (data != "") {

                $("#candiv").html(data);
            }
            else {
                alert("Data Not Available..!");
                $("#candiv").empty();
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


function appCommonAjax(var_url, var_data, var_type, var_ct, var_dt) {

    if (var_type == "")
        var_type = "POST";

    if (var_ct == "")
        var_ct = "application/json;charset=utf-8";

    if (var_dt == "")
        var_dt = "json";

    //alert(var_data);

    $.ajax({
        url: var_url,
        data: {
            JsonData: var_data
        },
        type: var_type,
        //contentType: var_ct,
        dataType: var_dt,
        success: function (data) {
            //console.log(data)
            if (data.messa != "") {
                alert(data.messa);
            }
            clear();
            if (data.datagrid == "") { } else {
                $("#Printdiv").html(data.datagrid);
                

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

function BindBarCodeStatus(var_url, var_data, var_type, var_ct, var_dt) {
    if (var_type == "")
        var_type = "POST";

    if (var_ct == "")
        var_ct = "application/json;charset=utf-8";

    if (var_dt == "")
        var_dt = "json";


    $.ajax({
        url: var_url,
        data: { JsonData: var_data },
        type: var_type,
        //contentType: var_ct,
        dataType: var_dt,
        success: function (response) {
            var data = response.Data;
            if (response.Status == "") {

                var GoodsInDate = "";
                var DispatchDate = "";

                if ((data[0]["Goods In Date"]) != null) {
                    GoodsInDate = data[0]["Goods In Date"].replace("T", " ")
                }
                if ((data[0]["Dispatch Date"]) != null) {
                    DispatchDate = data[0]["Dispatch Date"].replace("T", " ")
                }
                $("#itemheading").show();
                $("#itemname").html(data[0]["ItemName"])



                $("#tblproduction").show();
                $('#data-table1').html('<tr><td>' + (data[0]["Production No"] ? data[0]["Production No"] : '') + '</td><td>' + (data[0]["Production Date"] ? data[0]["Production Date"] : '') + '</td><td>' + (data[0]["Production User"] ? data[0]["Production User"] : '') + '</td></tr>')


                $("#tblpacking").show();
                $('#data-table4').html('<tr><td>' + (data[0]["Packing No"] ? data[0]["Packing No"] : '') + '</td><td>' + (data[0]["Packing Date"] ? data[0]["Packing Date"] : '') + '</td></tr>')



                $("#tblgoodsin").show();
                $('#data-table2').html('<tr><td>' + data[0]["Goods In User"] + '</td><td>' + GoodsInDate + '</td></tr>')

                $("#tbldispatch").show();
                $('#data-table3').html('<tr><td>' + data[0]["Dispatch No"] + '</td><td>' + DispatchDate + '</td><td>' + data[0]["Dispatch User"] + '</td><td>' + data[0]["Dispatched To Customer"] + '</td></tr>')
            }

            else {
                $("#itemname").html('');
                $("#itemheading").hide();
                $('#tblproduction').hide();
                $('#tblgoodsin').hide();
                $('#tbldispatch').hide();
                $("#tblpacking").hide();
                swal({
                    icon: response.Status == "error" ? "error" : "success",
                    title: "Message",
                    text: response.Message == "Invalid barcode" ? response.Message : "success",
                    timer: 2000
                });
            }
            clear();
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