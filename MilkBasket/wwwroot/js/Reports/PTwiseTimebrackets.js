

$(document).ready(() => {
   
    showLoader();
    ShowAPTwiseTimebrackets()
    
})
function ShowAPTwiseTimebrackets() {
    showLoader();
    var url = localStorage.getItem("Url") + "/Report/ShowAPTwiseTimebrackets";

    data = {
      
        Date: $("#Date").val(),
       
    }
    $.ajax({
        url: url,
        type: 'get',
        data: data,
        success: (data) => {
            $("#tbodydata").empty();
            var data = JSON.parse(data);
            hideLoader();
            console.log(data);
           
          var tr = "";
         

            for (let e in data) {
                const row = data;

                tr += `
    <tr>
      <td>${parseInt(e) + 1}</td>
      <td>${row[e].CompanyDesc || ''}</td>
      <td>${row[e].HrLocationDesc || ''}</td>
      <td>${row[e].LocationDesc || ''}</td>
      <td>${row[e].EmployeeNumber || ''}</td>
      <td>${row[e].EmpName || ''}</td>
      <td>${row[e].Designation || ''}</td>
      <td>${row[e].ClusterNo || ''}</td>
      <td>${row[e].Date || ''}</td>
      <td>${row[e].INTime || ''}</td>
      <td>${row[e].OutTime || ''}</td>
      <td>${row[e].Slot1 || ''}</td>
      <td>${row[e].Slot2 || ''}</td>
      <td>${row[e].Slot3 || ''}</td>
      <td>${row[e].Slot4 || ''}</td>
      <td>${row[e].Slot5 || ''}</td>
    
    </tr>
  `;
            }

            $("#tbodydata").append(tr);

        },
        error: () =>{

        }

    })

  

}

function exportexcel1(name) {
    let date = $("#Date").val();
    let datef = dateformat(date);

    var fulname = name + '-' + datef;
    exportexcel(fulname)
}