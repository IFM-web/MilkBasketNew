
$(document).ready(() => {
    ShowMenu();
})
function ShowMenu() {
    let url = localStorage.getItem("Url") + '/Admin/GetMenu';
    data = { GroupId: $("#UserGroup").val() },
        Common(url, data, "", "", "", "printdiv");
}

function SaveMenu() {
    let arr = [];
    if ($("#UserGroup").val() == '0') {
        alert("Select User Group");
        return;
    }
    $("#printdiv tbody tr").each(function (index, row) {
        var row = $(row);
        var obj = {};
        MenuId = row.find('.Hid_MenuId').text();
        let GroupId = $("#UserGroup").val();
      
        if (MenuId != '') {


            obj.GroupId = GroupId;
            obj.MenuId = row.find('.Hid_MenuId').text();
            obj.View = row.find('.Viewflg').is(':checked') ? 1 : 0;
            obj.Add = row.find('.Addflg').is(':checked') ? 1 : 0;
            obj.Edit = row.find('.Editflg').is(':checked') ? 1 : 0;
            obj.Delete = row.find('.Deleteflg').is(':checked') ? 1 : 0;

            arr.push(obj);
        }
    });




    $.ajax({
        url: localStorage.getItem("Url") + '/Admin/SaveMenuRights',
        type: "POST",
        
        data: { data: JSON.stringify(arr) },
        dataType: 'json',
        success: (data) => {
            var data = JSON.parse(data);

            if (data[0].Status=="Success") {
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