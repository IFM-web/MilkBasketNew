$(document).ready(() => {
  
    ShowSociety();
})


function RegionBind() {

    $.ajax({
        url: localStorage.getItem("Url") + '/Home/RegionBind',
        type: 'get',
        data: { Company: $("#Company").val() },
        success: function (data) {
            hideLoader();
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

function ShowSociety() {
    let url = localStorage.getItem("Url") + '/Master/ShowCopySocietyData';
    data = {},
        Common(url, data, "", "", "", "printdiv");
}

function SaveSociety() {
    let arr = [];
    if ($("#Branch").val() == '0') {
        alert("Select Branch ");
        return;
    }
    $("#printdiv tbody tr").each(function (index, row) {
        var row = $(row);
        var obj = {};

        let Branch = $("#Branch").val();

        let SocietyCode = row.find('.SocietyCode').text()
        let SocietyName = row.find('.SocietyName').text()
        let Latitude = row.find('.Latitude').text()
        let Longitude = row.find('.Longitude').text()
        if (SocietyCode != '') {

            obj.SocietyCode = SocietyCode;
            obj.SocietyName = SocietyName;
            obj.Latitude = Latitude;
            obj.Longitude = Longitude
                ;
                            arr.push(obj);
        }
    });




    $.ajax({
        url: localStorage.getItem("Url") + '/Master/SaveCopySocietyData',
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