

$(document).ready(() => {
    $("#Date").val(new Date().toISOString().split('T')[0]);
    showLoader();
    ShowEmployeeAttendence()
    DownloadFormat("ClusterExcelfrommat");
})
function ShowEmployeeAttendence() {
    showLoader();
    var url = localStorage.getItem("Url") + "/Report/ShowFaceRegister";

    data = {
      
       
        EmployeeNumber: $("#EmpNO").val(),
      
    }
    $.ajax({
        url: url,
        type: 'get',
        data: data,
        success: (data) => {
            var data = JSON.parse(data);
            $("#employeeTableBody").empty();
            hideLoader();
            console.log(data);
            $("#facepending").text(data.Dt2[0].TotalPending);
            $("#faceregistered").text(data.Dt2[0].TotalRegister);
            $("#totalemployees").text(data.Dt2[0].TotalEmp);
        
          var tr = "";
         

            for (let e in data.Dt) {
                const row = data.Dt[e];

                tr += `
    <tr>
      <td><i class="fas fa-trash onclick=Deletebyid('${row.EmpID})"></i></td>
      <td>${parseInt(e) + 1}</td>
      <td>${row.EmpID || ''}</td>
      <td>${row.EmpName || ''}</td>
     
    </tr>
  `;
            }

            $("#employeeTableBody").append(tr);

        },
        error: () =>{

        }

    })

  

}




function Delete(EmpId) {
    var i = confirm("Are you sure you want to delete this record?");
    if (i) {
        $.ajax({
            url: localStorage.getItem("Url") + "/Upload/DeleteFaceRegister",
            type: "get",
            data: {
                EmpId: EmpId


            },
            success: function (data) {


                alert(data)
                window.location.reload();



                console.log(result);
            },
            error: function (xhr) {
                console.log("Error:", xhr);
            }
        });
    }

}

