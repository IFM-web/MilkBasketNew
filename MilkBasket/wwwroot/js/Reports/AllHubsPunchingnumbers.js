

$(document).ready(() => {  
    showLoader()
    ShowAllHubsPunchingnumbers()
    DownloadFormat("ClusterExcelfrommat");
})
function ShowAllHubsPunchingnumbers() {
    showLoader();
    var url = localStorage.getItem("Url") + "/Report/ShowAllHubsPunchingnumbers";

    data = {
      
        Date: $("#Date").val(),
        
    }
    $.ajax({
        url: url,
        type: 'get',
        data: data,
        success: (data) => {
            var data = JSON.parse(data);
            hideLoader();
            console.log(data);
            $("#tbodydata").empty();
           
          var tr = "";
         

            for (let e in data) {
             

                tr += `
    <tr>
      <td>${parseInt(e) + 1}</td>
      <td>${data[e].CompanyDesc || ''}</td>
      <td>${data[e].HrLocationDesc || ''}</td>
      <td>${data[e].LocationDesc || ''}</td>
      <td>${data[e].Date || ''}</td>
      <td>${data[e].TotalEmp || ''}</td>
      <td>${data[e].TotalPresent || ''}</td>
    
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

    var fulname = name+'-'+datef;
    exportexcel(fulname)
}

