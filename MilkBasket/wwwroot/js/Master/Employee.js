$(document).ready(() => {
    var EmpNo = $("#EmpNoHid").val();

    if (EmpNo != "") {
        bindEmp();
    }
})


$("#empNo,#empName").on("keyup", function (e) {
    if ($("#empNo").val() == "") {
        $("#savebtn,#restbtn").addClass("d-none");
    }
    if (e.key === "Enter" || e.keyCode === 9) {
        e.preventDefault(); 

        $.ajax({
            url: localStorage.getItem("Url") + "/Master/CheckEmployee",
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
    var val = Validation();
    if (val == "") {
        $.ajax({
            url: localStorage.getItem("Url") + "/Master/InsertEmployeeMB",
            type: "Post",
            data: {
                empNo: $("#empNo").val().trim(),
                empName: $("#empName").val().trim(),
                designation: $("#designation").val(),
                department: $("#DepartmentCode").val(),
                areaID: $("#AreaID").val(),
                contactNo: '',
                emailid: $("#email").val().trim(),
                EmpNoHid: $("#EmpNoHid").val()


            },
            success: function (data) {
                var result = JSON.parse(data);
                if (result[0].MessageID == "1") {


                    alert(result[0].MessageString)
                    if (result[0].MessageString == 'Employee Updated') {
                        window.location.href = localStorage.getItem("Url") + "/Master/EmployeeList";
                    }

                    clear()
                    $("#savebtn,#restbtn").addClass("d-none");
                }
                console.log(result);
            },
            error: function (xhr) {
                console.log("Error:", xhr);
            }
        });
    } else {
        alert(val);
    }
}
function clear() {
    $("#empNo").val('');
    $("#empName").val('');
    $("#designation").val(0);
              
    $("#email").val('');
}
function clear1() {
 
    $("#empName").val('');
    $("#designation").val(0);
              
    $("#email").val('');
}
function bindEmp() {
    var url = localStorage.getItem("Url") + "/Master/GetEmployee";
    var EmpNo = $("#EmpNoHid").val();
    var areaID = $("#AreaID").val(),
        data = {
            EmpNo: EmpNo,
            AreaId: areaID
        }
   

    NewCommon(url, data, "", "", "", "BindEmp")
}
