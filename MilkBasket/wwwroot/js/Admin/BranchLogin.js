
$(document).ready(function () {

    var company = $("#company").val();
    
        $("#company").trigger('change');
      
    
});


function RegionBind(id) {

    $.ajax({
        url: localStorage.getItem("Url") +'/Home/RegionBind',
        type: 'get',
        data: { Company: $("#company").val() },
        success: function (data) {
            var data = JSON.parse(data);

            var dropdown = $('#RegionID');
            dropdown.empty();

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
    var data = { Company: $("#company").val(), Region: $("#RegionID").val() };
    divid = $("#branch")
    BindDropdownsingle(url, data, "", "", "", divid, "")
}