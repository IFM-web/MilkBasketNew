$(document).ready(() => {
  
    Show()
})

function Show() {
    showLoader();
    $.ajax({
        url: "/Master/ShowEmployee",
        type: "get",
        data: {
            EmpNo: $("#empNo").val(),
            

        },
        success: function (data) {
            hideLoader()
            var result = JSON.parse(data);
          
           var tr = "";
            var i=1
            for (var e of result) {
                tr += `
                <tr>
                <td><i class="fa fa-trash" onclick="Delete('${e.EmpID}')" ></i></td>
                <td>${i}</td>
                <td>${e.EmpID}</td>
                <td>${e.EmpName}</td>
                <td>${e.Designation}</td>
                <td>${e.EmailID ||''}</td>

                </tr>
                `;
                i++
            }
            $("#tablebody").append(tr);
     
            console.log(result);
        },
        error: function (xhr) {
            console.log("Error:", xhr);
        }
    });
}

function Delete(EmpId) {

        $.ajax({
            url: "/Master/DeleteEmployee",
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