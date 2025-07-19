$(document).ready(() => {
    ShowSociety()
})
function ShowSociety() {
   
    var url = localStorage.getItem("Url") + "/Master/ShowSociety";
       
            data = {
              
            }


    NewCommon(url, data, "", "", "", "PrintDiv")
    
}

function DeleteById(Id) {
    var i = confirm("Are you sure you want to delete this record?");
    if (i) {
        $.ajax({
            url: localStorage.getItem("Url") + "/Master/DeleteSociety",
            type: "get",
            data: {
                Id: Id


            },
            success: function (data) {
                var data = JSON.parse(data);
                if (data[0].Massage != "") {
                    alert(data[0].Massage)
                }
               
                window.location.reload();



                console.log(result);
            },
            error: function (xhr) {
                console.log("Error:", xhr);
            }
        });
    }

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
            rest = Number(response)
            if (rest > 0) {
                alert("Upload successful!")
                window.location.reload();
            } else {
                alert("Invalid data format in Excel")
            }


            console.log(rest);
        },
        error: function (xhr) {
            alert("Invalid data format in Excel.")

            console.error(xhr);
        }
    });
});