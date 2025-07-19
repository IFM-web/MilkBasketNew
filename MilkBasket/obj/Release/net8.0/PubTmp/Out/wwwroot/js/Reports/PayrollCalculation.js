

$(document).ready(() => {
    $("#Date").val(new Date().toISOString().split('T')[0]);
    let monthNumber = new Date().getMonth() + 1;
    $("#month").val(monthNumber);
    showLoader();
    ShowPayrollCalculation()
    DownloadFormat("ClusterExcelfrommat");
})
function ShowPayrollCalculation() {
    showLoader();
    var url = localStorage.getItem("Url") + "/Report/ShowPayrollCalculation";

    data = {
      
        Month: $("#month").val(),
        Year: $("#year").val(),
        
    }
    $.ajax({
        url: url,
        type: 'get',
        data: data,
        success: (data) => {
            var data = JSON.parse(data);
            hideLoader();
            console.log(data);
            var tr = "";
            $("#databody").empty();
            for (let e of data) {
                tr += `<tr>
    <td>${e.SNo || ''}</td>
    <td>${e.EmpCode || ''}</td>
    <td>${e.EmpName || ''}</td>
    <td>${e.Designation || ''}</td>`;

                for (let i = 1; i <= 31; i++) {
                    if (i <= 9) {
                       i= '0'+i
                    }
                    const dayValue = e[i] || '';
                    const colorClass = dayValue === 'P' ? 'text-green-600 font-semibold' : dayValue === 'A'? 'text-red-600 font-semibold':'';
                    tr += `<td class="${colorClass}">${dayValue}</td>`
                }

                tr += `<td>${e.DutyDays || ''}</td></tr>`;
            }

            $("#databody").append(tr);

        },
        error: () =>{

        }

    })

  

}

function exportexcel1(name) {
    let Year = $("#year").val();
    let Month = $("#month option:selected").text();


    var fulname = name + '-' + Month + '-' + Year;
    exportexcel(fulname)
}

