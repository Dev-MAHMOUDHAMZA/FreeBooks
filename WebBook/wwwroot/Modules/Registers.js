$(document).ready(function () {
    $('#tableRole').DataTable({
        "autoWidth": false,
        "responsive":true
    });
});

function Delete(id) {
    Swal.fire({
        title: lbTitleMsgDelete,
        text: lbTextMsgDelete,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: lbconfirmButtonText,
        cancelButtonText: lbcancelButtonText
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = `/Admin/Accounts/DeleteUser?userId=${id}`;
            Swal.fire(
                lbTitleDeletedOk,
                lbMsgDeletedOkRegister,
                lbSuccess
            )
        }
    })
}

Edit = (id, name, email, image, role, active) => {
    document.getElementById("title").innerHTML = lbTitleEdit;
    document.getElementById("btnSave").value = lbEdit;
    document.getElementById("userId").value = id;
    document.getElementById("userName").value = name;
    document.getElementById("userEmail").value = email;
    document.getElementById("ddluserRole").value = role;
    var Active = document.getElementById("userActive");
    if (active == "True")
        Active.checked = true;
    else
        Active.checked = false;
    $('#grPassword').hide();
    $('#grcomPassword').hide();
    document.getElementById("userPassword").value = "$$$$$$";
    document.getElementById("userCompare").value = "$$$$$$";
    document.getElementById("image").hidden = false;
    document.getElementById("image").src = PathImageuser + image; 
    document.getElementById("imgeHide").value = image;
}

Rest = () => {
    document.getElementById("title").innerHTML = lbAddNewRole;
    document.getElementById("btnSave").value = lbbtnSave;
    document.getElementById("userId").value = "";
    document.getElementById("userName").value = "";
    document.getElementById("userEmail").value = "";
    //document.getElementById("userImage").value = "";
    document.getElementById("ddluserRole").value = "";
    document.getElementById("userActive").checked = false;
    $('#grPassword').show();
    $('#grcomPassword').show();
    document.getElementById("userPassword").value = "";
    document.getElementById("userCompare").value = "";
    document.getElementById("image").hidden = true;
    document.getElementById("imgeHide").value = "";


}

function ChangePassword(id) {

    document.getElementById('userPassId').value = id;
}