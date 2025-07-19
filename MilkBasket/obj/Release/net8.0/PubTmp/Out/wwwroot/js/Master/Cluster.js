$(document).ready(() => {
    var EmpNo = $("#SocietyCodeHid").val();
    if (EmpNo != "") {
        bindSociety();
    }
})
function Save() {
    var val = $("#ClusterName").val();
    if (val != "") {


        $.ajax({
            url: localStorage.getItem("Url") + "/Master/InsertCluster",
            type: "Post",
            data: {
                ClusterName: val,
               
            },
            success: function (data) {
                var result = JSON.parse(data);
                if (result[0].MessageID == "1") {

                    
                        alert("Cluster Created !!")

                    
                    clear()
             
                }
                if (result[0].MessageID == "2") {
                    alert("Cluster Already Exists !!")
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
    var url = localStorage.getItem("Url") + "/Master/GetSociety";
    var SocietyCode = $("#SocietyCodeHid").val();

    data = {
        SocietyCode: SocietyCode,

    }


    NewCommon(url, data, "", "get", "", "BindSociety")
}


function clear() {
    $("#ClusterName").val('');
   

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