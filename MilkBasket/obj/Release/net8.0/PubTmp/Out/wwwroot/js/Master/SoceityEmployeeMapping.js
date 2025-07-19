$(document).ready(() => {
    var EmpNo = $("#EmpCodeHid").val();
    if (EmpNo != "") {
        bindSociety();
    }
})
function Save() {
    var val = $("#EmployeeCode").val();
    
    if (val != "") {


        $.ajax({
            url: localStorage.getItem("Url") + "/Master/InsertSocietyMapToEmployee",
            type: "Post",
            data: {
                societyCode: $("#EmployeeCode").val().trim()
                , InsocietyCode: $("#INSociety").val()
                , OutsocietyCode: $("#OutSociety").val(),
                EmpCodeHid: $("#EmpCodeHid").val()
               
            },
            success: function (data) {
                var result = JSON.parse(data);
                if (result[0].Massage == "Emp Society Mapped") {


                    alert(result[0].Massage)


                    clear()

                }
                else if (result[0].Massage == "Emp Society Maping Updated !") {
                    alert(result[0].Massage)
                    window.location.href = localStorage.getItem("Url") + "/Master/SocietyMapToEmployeeList"
                }
                console.log(result);
            },
            error: function (xhr) {
                console.log("Error:", xhr);
            }

        })
    } else {
        alert("Cluster Name Required")
    }
}

function bindSociety() {
    var url = localStorage.getItem("Url") + "/Master/GetEmpSociety";
    var EmpCode = $("#EmpCodeHid").val();

    data = {
        EmpCode: EmpCode,

    }


    NewCommon(url, data, "", "get", "", "EmpSociety")
}


function clear() {
    $("#SocietyCode").val('');
    $("#SocietyCode").prop("disabled", false);
    $("#SocietyName").val('');
    $("#Latitude").val('');
    $("#Longitude").val('');

    $("#savebtn").text("Create")
}




$("#btnexcelsave").on("click", function (e) {
    e.preventDefault();

    var fileInput = $("#excelFile")[0];
    var file = fileInput.files[0];

    if (!file) {
        alert("Please select a file.");
        return;
    }

    var allowedExtensions = /(\.xlsx|\.xls)$/i;
    if (!allowedExtensions.exec(file.name)) {
        alert("Please upload only .xlsx or .xls files.");
        fileInput.value = ""; // clear file input
        return;
    }

    var formData = new FormData();
    formData.append("file", file);
    formData.append("Type", "SocietyUpload");

    $.ajax({
        url: localStorage.getItem("Url") + "/Upload/Import",
        type: "POST",
        data: formData,
        contentType: false,
        processData: false,
        success: function (response) {
            if (response == 1) {
                alert("Upload successful!")
            }


            console.log(response);
        },
        error: function (xhr) {
            alert("Invalid data format in Excel.")

            console.error(xhr);
        }
    });
});

$("#EmployeeCode").on("keyup", function (e) {
    if (e.key === "Enter" || e.keyCode === 9) {
        e.preventDefault();

        $.ajax({
            url: localStorage.getItem("Url") + "/Master/CheckEmployee",
            type: "get",
            data: { EmployeeNo: $("#EmployeeCode").val() },
            success: function (data) {
                var result = JSON.parse(data);
                if (result[0].MessageID != "1") {

                    $("#massage").empty();
                    $("#savebtn,#restbtn").removeClass("d-none");

                } else {
                    $("#savebtn,#restbtn").addClass("d-none");
                    var mes = "Employee Not Exists !!";
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