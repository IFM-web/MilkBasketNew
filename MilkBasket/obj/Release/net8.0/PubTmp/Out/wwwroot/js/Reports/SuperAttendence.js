

$(document).ready(() => {
    $("#Date").val(new Date().toISOString().split('T')[0]);
    showLoader();
    ShowEmployeeAttendence()
    DownloadFormat("ClusterExcelfrommat");
})
function ShowEmployeeAttendence() {
    showLoader();
    var url = localStorage.getItem("Url") + "/Report/ShowSuperAttendence";

    data = {
      
        FromDate: $("#FromDate").val(),
        ToDate: $("#ToDate").val(),
        EmployeeNumber: $("#EmpNo").val(),
        Cluster: $("#Cluester").val()
    }
    $.ajax({
        url: url,
        type: 'get',
        data: data,
        success: (data) => {
            var data = JSON.parse(data);
            hideLoader();
            $("#tbodydata").empty();
            console.log(data);
            $("#TotalEmp").text(data.Dt2[0].TotalEmp);
            $("#presentEmp").text(data.Dt2[0].TotalPresent);
            $("#punchingEmp").text(data.Dt2[0].TotalLatepunch);
            $("#AbsentEmp").text(data.Dt2[0].TotalAbsent);
          var tr = "";
         

            for (let e in data.Dt) {
                const row = data.Dt[e];

                tr += `
    <tr>
      <td>${parseInt(e) + 1}</td>
      <td>${row.EmployeeCode || ''}</td>
      <td>${row.EmployeeName || ''}</td>
      <td>${row.Designation || ''}</td>
      <td>${row.EmailID || ''}</td>
      <td>${row.ShiftDetails || ''}</td>
      <td>${row.ClusterNo || ''}</td>
      <td>${row.Date || ''}</td>
      <td>${row.InTime || ''}</td>
      <td>${row.OutTime || ''}</td>
      <td>
        <span class="${row.LateReamrks === 'Late Punching' ? 'text-red-600' : 'text-green-600'}">
          ${row.LateReamrks || ''}
        </span>
      </td>
      <td>${row.MinuteDuty || 0}</td>
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
    let date = $("#ToDate").val();
    let datef = dateformat(date);

    var fulname = name + '-' + datef;
    exportexcel(fulname)
}
