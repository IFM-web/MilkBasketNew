


$("#empNo").on("keydown", function (e) {
    if (e.key === "Enter" || e.keyCode === 13) {
        e.preventDefault(); 

        $.ajax({
            url: "/Master/CheckEmployee",
            type: "get",
            data: { EmployeeNo: $("#empNo").val() },
            success: function (data) {
                var result = JSON.parse(data);
                if (result[0].MessageID == "1") {

                    $("#massage").empty();
                    $("#savebtn,#restbtn").removeClass("d-none");

                } else {
                    $("#savebtn,#restbtn").addClass("d-none");
                    var mes = "Employee Already Exists !!";
                    $("#massage").empty();
                    $("#massage").append(mes);
                }
                console.log(result); 
            },
            error: function (xhr) {
                console.log("Error:", xhr);
            }
        });
    }
});

function Save() {
    $.ajax({
        url: "/Master/InsertEmployeeMB",
        type: "Post",
        data: {
            empNo: $("#empNo").val(),
            empName: $("#empName").val(),
            designation: $("#designation").val(),
            department: $("#DepartmentCode").val(),
            areaID: $("#AreaID").val(),
            contactNo: '',
            emailid: $("#email").val(),



        },
        success: function (data) {
            var result = JSON.parse(data);
            if (result[0].MessageID == "1") {


                alert(result[0].MessageString)
                clear()
                $("#savebtn,#restbtn").addClass("d-none");
            } 
            console.log(result);
        },
        error: function (xhr) {
            console.log("Error:", xhr);
        }
    });
}
function clear() {
    $("#empNo").val('');
    $("#empName").val('');
 
              
    $("#email").val('');
}
