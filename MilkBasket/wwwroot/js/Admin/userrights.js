function RegionBind() {

    $.ajax({
        url: localStorage.getItem("Url") + '/Home/Region',
        type: 'get',
        data: { Company: $("#Company").val() },
        success: function (data) {
            var data = JSON.parse(data);

            var dropdown = $('#Region');
            dropdown.empty();
            dropdown.append('<option value="0">Select</option>');
            for (var i = 0; i < data.length; i++) {

                dropdown.append($('<option></option>').attr('value', data[i].Id).text(data[i].Name));
            }
            bindbarnch();
        },
        error: function (error) {
            alert(error.massage);
        }
    })
}

$("#User").on('change', () => {
    $("#customSelect").val('');
    $("#customSelectName").val('');
})


function bindbarnch() {

    var url = localStorage.getItem("Url") + "/Home/BindBranch";
    var data = { Company: $("#Company").val(), Region: $("#Region").val() };
    divid = $("#Branch")
    BindDropdownsingle(url, data, "", "", "", divid, "Select")
}


function bindbarnch() {

    $("#customSelect").val('');
    $("#customSelectName").val('');
    let url = localStorage.getItem("Url") + '/Home/BindBranchUserrights';
    $.ajax({
        url: url,
        type: 'post',
        data: { Company: $("#Company").val(), Region: $("#Region").val(), UserId: $("#User").val() },
        success: (data) => {
            var data = JSON.parse(data);
            var i = 0;
            var container = document.getElementById("checkboxContainer");
            container.innerHTML = '';

            for (var e of data) {
                const dd = `
      <div class="form-check ms-2">
         
        <input class="form-check-input chkItem" type="checkbox" value="${e.Id}" id="chk${i}">
        <label class="form-check-label" for="chk${i}">${e.Name}</label>
      </div>
    `;
                container.innerHTML += dd;
                ++i;
            }
        },
        error: (data) => {
            console.log(data);
        }


    });
}

    function Save() {
        var val = Validation();
        if (val !== "") {
            alert(val);
            return;
        }
        url = localStorage.getItem("Url") + "/Admin/SaveUserRights";
        let arr = [];
      
        let Branch = $("#customSelect").val().split(',');
        console.log(Branch)
        for (let e of Branch) {
           let data = {
                Company: $("#Company").val(), Branch: e, Region: $("#Region").val(), UserId: $("#User").val()
            }
            arr.push(data);
        }
        console.log(arr)
        let data = { data: JSON.stringify(arr) }
        Common(url, data, '', '', '', 'UserRight');
    }

    function clear1() {
        $("#Company").val(0); $("#Branch").val(0); $("#Region").val(0); $("#User").val(0)
    }



const displayBox = document.getElementById('customSelect');
const displayBoxName = document.getElementById('customSelectName');
const menu = document.getElementById('checkboxContainer');
const wrapper = document.querySelector('.custom-multiselect');


displayBoxName.addEventListener('click', function () {
    menu.style.display = menu.style.display === 'block' ? 'none' : 'block';
});


document.addEventListener('click', function (event) {
    if (!wrapper.contains(event.target)) {
        menu.style.display = 'none';
    }
});


menu.addEventListener('change', function () {
    const selectedCheckboxes = Array.from(menu.querySelectorAll(".chkItem:checked"));

    
    const selectedValues = selectedCheckboxes.map(cb => cb.value);
    displayBox.value = selectedValues.join(",");

  
    const selectedTexts = selectedCheckboxes.map(cb => {
        const label = cb.nextElementSibling;
        return label ? label.innerText.trim() : "";
    });
    displayBoxName.value = selectedTexts.join(",");
});

//---------------------Region DDL---------------------------------------

//const displayBox2 = document.getElementById('customSelect2');
//const menu2 = document.getElementById('checkboxContainer2');
//const wrapper2 = document.querySelector('.custom-multiselect2');

//// Open/close toggle
//displayBox2.addEventListener('click', function () {
//    menu2.style.display = menu2.style.display === 'block' ? 'none' : 'block';
//});

//// Close when clicking outside
//document.addEventListener('click', function (event) {
//    if (!wrapper2.contains(event.target)) {
//        menu2.style.display = 'none';
//    }
//});


//menu2.addEventListener('change', function () {
//    const selected = Array.from(menu2.querySelectorAll(".chkItem2:checked"))
//        .map(cb => cb.value);
//    displayBox2.value = selected.join(",");
//});