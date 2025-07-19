$(document).ready(() => {
    var EmpNo = $("#SocietyCodeHid").val();
    if (EmpNo != "") {
        bindSociety();
    }
})
function Save() {
    var val = Validation();
    if (val == "") {
        $.ajax({
            url: localStorage.getItem("Url") + "/Master/InsertSociety",
            type: "Post",
            data: {
                SocietyCode: $("#SocietyCode").val().trim(),
                SocietyName: $("#SocietyName").val().trim(),
                Lat: $("#Latitude").val().trim(),
                Lon: $("#Longitude").val().trim(),
                SocietyCodeHid: $("#SocietyCodeHid").val(),
            },
            success: function (data) {
                var result = JSON.parse(data);
                if (result[0].MessageID == "1") {

                    if ($("#SocietyCodeHid").val() != "") {
                        alert("Society Updated !!")
                        window.location.href = localStorage.getItem("Url") + "/Master/SocietyList";
                    } else {
                        alert("Society Created !!")
                       
                    }
                    clear()
                    $("#savebtn,#restbtn").addClass("d-none");
                }
                if (result[0].MessageID == "2") {
                    alert("Society Already Exists !!")
                }
                console.log(result);
            },
            error: function (xhr) {
                console.log("Error:", xhr);
            }
        
    })
}else {
    alert(val)
}
}

function bindSociety() {
    var url = localStorage.getItem("Url") + "/Master/GetSociety";
    var SocietyCode = $("#SocietyCodeHid").val();

    data = {
        SocietyCode: SocietyCode,
            
        }
    NewCommon(url, data, "", "get", "", "BindSociety")
}


function clear() {
    $("#SocietyCode").val('');
    $("#SocietyCode").prop("disabled", false);
    $("#SocietyName").val('');
    $("#Latitude").val('');
    $("#Longitude").val('');

    $("#savebtn").text("Create")
}



