$(document).ready(() => {
    if ($("#Hid_Id").val() != "") {
        bindUser();
    }
})
function Save() {
    var val = Validation();
    if (val !== "") {
        alert(val)
        return
    }
    if ($("#ConfirmPassword").val().trim() !== $("#Password").val().trim()) {
        alert("Password Not Match to Comfirm Password")

        return;
    }
    $.ajax({
        url: localStorage.getItem("Url")+'/Admin/SaveUserCreation',
        type: "POST",
        
        data: {
            Id: $("#Hid_Id").val(),
            UserId: $("#UserId").val().trim(),
            UserName: $("#UserName").val().trim(),
            Password: $("#ConfirmPassword").val().trim(),
            GroupId: $("#UserGroup").val(),           
            status: $("#status").is(':checked') ? 1 : 0,
        },
        dataType: 'json',
        success: (data) => {
            var data = JSON.parse(data);
           
            alert(data[0].Message)
            if (data[0].Status == "Success") {
                clear1();
            }
           
            console.log(data);
        },
        error: (data) => {
            console.log(data);
        }
    });
     
}

function clear1() {
    $("#Hid_Id").val(0);
    $("#UserId").val('');
           $("#UserName").val('');
                 $("#ConfirmPassword").val('');
                 $("#Password").val('');
                     $("#UserGroup").val(0);
                     
}


function bindUser() {
    var url = localStorage.getItem("Url") + "/Admin/GetUserCreation";
    var UserId = $("#Hid_Id").val();

    data = {
        UserId: UserId,

    }


    NewCommon(url, data, "", "get", "", "BindUser")
}