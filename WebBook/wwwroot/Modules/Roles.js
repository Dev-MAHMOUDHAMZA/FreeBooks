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
            window.location.href = `/Admin/Accounts/DeleteRole?Id=${id}`;
            Swal.fire(
                'تم الحذف!',
                'تم حذف مجموعة المستخدم.',
                'success'
            )
        }
    })
}

Edit = (id, name) => {
    document.getElementById("title").innerHTML = "تعديل مجموعة المستخدم";
    document.getElementById("btnSave").value = "تعديل";
    document.getElementById("roleId").value = id;
    document.getElementById("roleName").value = name;

}

Rest = () => {
    document.getElementById("title").innerHTML = "اضف مجموعة جديدة";
    document.getElementById("btnSave").value = "حفظ";
    document.getElementById("roleId").value = "";
    document.getElementById("roleName").value = "";
}