$(document).ready(() => {

    ShowCluster();
})


function RegionBind() {

    $.ajax({
        url: localStorage.getItem("Url") + '/Home/RegionBind',
        type: 'get',
        data: { Company: $("#Company").val() },
        success: function (data) {
            var data = JSON.parse(data);

            var dropdown = $('#Region');
            dropdown.empty();
            dropdown.append("<option value='0'>Select</option>");
            for (var i = 0; i < data.length; i++) {

                dropdown.append($('<option></option>').attr('value', data[i].Id).text(data[i].Name));
            }
            bindbarnch();
        },
        error: function (error) {
            alert(error.massage);
        }
    })
}




function bindbarnch() {

    var url = localStorage.getItem("Url") + "/Home/BindBranchbind";
    var data = { Company: $("#Company").val(), Region: $("#Region").val() };
    divid = $("#Branch")
    BindDropdownsingle(url, data, "", "", "", divid, "Select")
}

function ShowCluster() {
    let url = localStorage.getItem("Url") + '/Master/ShowCopyClusterData';
    data = { },
        Common(url, data, "", "", "", "printdiv");
}

function SaveCluster() {
    let arr = [];
    if ($("#Branch").val() == '0') {
        alert("Select Branch ");
        return;
    }
    $("#printdiv tbody tr").each(function (index, row) {
        var row = $(row);
        var obj = {};
 
        let Branch = $("#Branch").val();

       let clusetName= row.find('.ClstName').text()
        if (clusetName != '') {
            
            obj.ClusterName = clusetName;
            

            arr.push(obj);
        }
    });




    $.ajax({
        url: localStorage.getItem("Url") + '/Master/SaveCopyClusterData',
        type: "POST",

        data: { data: JSON.stringify(arr), LocationAutoId: $("#Branch").val() },
        dataType: 'json',
        success: (data) => {
            var data = JSON.parse(data);

            if (data[0].Status == "Success") {
                alert(data[0].Message)
                window.location.reload();
            }
            console.log(data);
        },
        error: (data) => {
            console.log(data);
        }
    });

}