var dataTable;

$(document).ready(function () {

    loadDataTable();
});

function loadDataTable() {

    dataTable = $('#categoryTable').DataTable({
        "ajax": {
            "url": '/Admin/Product/GetAll',
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            {
                data: 'imageUrl',
                "render": function (data) {
                    return `<img src="${data}" style="height:10%;width: 100%;" />`;
                }
            },
            { data: 'name' },
            { data: 'price' },
            { data: 'quantity' },
            { data: 'description' },
            { data: 'color' },
            {
                data: 'id',
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                        <a href="/Admin/Product/Edit/${data}" class="btn btn-primary mx-1"><i class="bi bi-pencil-square"></i> Edit</a>
						<a onClick="deleteProduct(${data})" class="btn btn-danger mx-1"><i class="bi bi-trash-fill"></i> Delete</a>
                        </div>
                    `;
                }
            }
        ]
    });
}

function deleteProduct(id) {
    Swal.fire({
        title: "Are you sure, You want to delete the Product?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {

            $.ajax({
                url: '/Admin/Product/Delete/' + id,
                type: 'DELETE',
                success: function (response) {
                    if (response.success) {

                        Swal.fire({
                            title: "Deleted!",
                            text: "Your file has been deleted.",
                            icon: "success"
                        });
                        dataTable.ajax.reload();

                    } else {
                        Swal.fire({
                            title: "Server Error!",
                            text: "Something went wrong on server, Please Try later!",
                            icon: "error"
                        });
                    }
                }

            });

        }
    });
}