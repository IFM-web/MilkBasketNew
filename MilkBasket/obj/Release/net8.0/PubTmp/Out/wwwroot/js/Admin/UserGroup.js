function Save() {
      

    var vali = Validation();
    if (vali !== '') {
        alert(vali)
        return 
    }



    $.ajax({
        url: localStorage.getItem("Url")+'/Admin/SaveUserGroup',
        type: "POST",
        
        data: {
            Id: $("#Hid_Id").val(),
            Groupcode: $("#UserGroupCode").val().trim(),
            Groupname: $("#UserGroupName").val().trim(),
            status: $("#status").is(':checked') ? 1 : 0,
        },
        dataType: 'json',
        success: (data) => {
            var data = JSON.parse(data);
           
            alert(data[0].Message)
            if (data[0].Status == "Success") {
                $("#Hid_Id").val(0)
                $("#UserGroupCode").val('')
                   $("#UserGroupName").val('')
            }
            console.log(data);
        },
        error: (data) => {
            console.log(data);
        }
    });
     
}