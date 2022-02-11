$(document).ready(function () {
    $('#tableRole').DataTable();
});
function Delete(id) {
    Swal.fire({
        title: 'هل انتا متأكد ؟',
        text: 'لن تتمكن من التراجع عن هذا!!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = `/Admin/Accounts/DeleteUser?userId=${id}`;
            Swal.fire(
                'تم الحذف!',
                'تم حذف مجموعة المستخدم.',
                'success'
            )
        }
    })
}

Edit = (id, name, email, image, role, active) => {
    document.getElementById("title").innerHTML = "تعديل مجموعة المستخدم";
    document.getElementById("btnSave").value = "تعديل";
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
    document.getElementById("image").src = "/Images/Users/" + image; 
    document.getElementById("imgeHide").value = image;
}

Rest = () => {
    document.getElementById("title").innerHTML = "اضف مجموعة جديدة";
    document.getElementById("btnSave").value = "حفظ";
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