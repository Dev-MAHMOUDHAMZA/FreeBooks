$(document).ready(function () {
    $('#tableRole').DataTable({
        "autoWidth": false,
        "responsive": true
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
            window.location.href = `/Admin/Accounts/DeleteRole?Id=${id}`;
            Swal.fire(
                lbTitleDeletedOk,
                lbMsgDeletedOkRole,
                lbSuccess
            )
        }
    })
}

Edit = (id, name) => {
    document.getElementById("title").innerHTML = lbTitleEdit;
    document.getElementById("btnSave").value = lbEdit;
    document.getElementById("roleId").value = id;
    document.getElementById("roleName").value = name;

}

Rest = () => {
    document.getElementById("title").innerHTML = lbAddNewRole;
    document.getElementById("btnSave").value = lbbtnSave;
    document.getElementById("roleId").value = "";
    document.getElementById("roleName").value = "";
}