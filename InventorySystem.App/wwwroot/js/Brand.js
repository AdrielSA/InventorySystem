// Start Brand

var datatable;

$(document).ready(function () {
    LoadDataTable();
});

function LoadDataTable() {
    datatable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Brand/GetAll"
        },
        "columns": [
            { "data": "name", "autoWidth": true },
            {
                "data": "status",
                "render": function (data) {
                    if (data == true) {
                        return "Activo";
                    }
                    else {
                        return "Inactivo";
                    }
                },
                "autoWidth": true
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/Admin/Brand/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                <i class="fas fa-edit"></i>
                            </a>
                            <a onclick=Delete("/Admin/Brand/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                <i class="fas fa-trash"></i>
                            </a>
                        </div>
                    `;
                },
                "autoWidth": true
            }
        ]
    });
}

function Delete(uri) {

    swal({
        title: "Está seguro que desea eliminar esta Marca?",
        text: "Este registro no se podrá recuperar.",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((remove) => {
        if (remove) {
            $.ajax({
                type: "Delete",
                url: uri,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        datatable.ajax.reload();
                    } else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}

// End Brand