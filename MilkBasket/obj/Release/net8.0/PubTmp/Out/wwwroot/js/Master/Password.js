function Save() {
    var val = Validation();
    if (val == "") {
        $.ajax({
            url: localStorage.getItem("Url") + "/Master/UpdatePassword",
            type: "Post",
            data: {
                Password: $("#NewPassword").val().trim(),
                UserId: $("#SelectLogin").val(),
            },
            success: function (data) {
                var result = JSON.parse(data);
                if (result[0].MessageID == "1") {
                    alert("Password Updated Successfully");
                    $("#NewPassword").val('');
                    $("#SelectLogin").val(0);

                }
                console.log(result);
            },
            error: function (xhr) {
                console.log("Error:", xhr);
            }
        });
    } else {
        alert(val);
    }
}