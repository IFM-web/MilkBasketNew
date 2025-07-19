

$(document).ready(() => {
   
    showLoader();
    ShowEmployeeAttendence()
    DownloadFormat("ClusterExcelfrommat");
})
function ShowEmployeeAttendence() {
    showLoader();
    var url = localStorage.getItem("Url") + "/Report/ShowEmployeeAttendence";

    data = {
        date: $("#Date").val()
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
                const row = data[e];
                tr += `
        <tr>
            <td>${parseInt(e) + 1}</td>
            <td>${row.EmployeeNumber}</td>
            <td>${row.EmployeeName}</td>
            <td>${row.ShiftDetails}</td>
            <td>${row.ClusterNo}</td>
            <td>${row.BranchCode || ''}</td>
            <td>${row.Status}</td>
            <td>${row.Date}</td>
            <td>${row.Time}</td>
            <td>${row.Latitude}</td>
            <td>${row.Longitude}</td>
            <td>${row.LocationName}</td>
            <td>
                <img  data-bs-toggle="modal" data-bs-target="#myModal" onclick="image('data:image/jpeg;base64,${row.EmployeeImage}')" src="data:image/jpeg;base64,${row.EmployeeImage}" alt="Employee Image" class="w-[150px] object-cover rounded" />
            </td>
        </tr>
    `;
            }

            $("#tbodydata").append(tr);

        },
        error: (data) =>{
            console.log(data);
            alert("Please try Again......")
            hideLoader();

        }

    })

  

}
function exportexcel1(name) {
    let date = $("#Date").val();
    let datef = dateformat(date);

    var fulname = name + '-' + datef;
    exportexcel(fulname)
}

function image(id) {
  
    $("#img01").attr("src", id);
    //$(".modal-body").css("backgroundImage", "url(" + id + ")"); 


}

var angle = 0;

function rotateImage() {
    angle += 90;
    var image = document.getElementById('img01');
    image.style.transform = 'rotate(' + angle + 'deg)';
}
function resetImageRotation() {
    angle = 0;
    var image = document.getElementById('img01');
    image.style.transform = 'rotate(0deg)';
}

