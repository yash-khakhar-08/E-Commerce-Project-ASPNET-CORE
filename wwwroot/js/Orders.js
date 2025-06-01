var dataTable;

var statusType = document.getElementById("status-type").value;

$(document).ready(function () {

    if (statusType === "Pending") {
        loadPendingOrders();
    } else  {
        loadCompletedOrders();
    }

});

function loadPendingOrders() {

    dataTable = $('#pendingOrdersTable').DataTable({
        "ajax": {
            "url": '/Admin/Home/GetOrders',
            "type": "GET",
            "data": {
                statusType: statusType
            },
            "datatype": "json"
        },
        "columns": [
            { data: 'id',width:'1%' },
            { data: 'userId' },
            { data: 'productId',width:'1%' },
            {
                data: 'dateTime',
                render: function (data) {
                    if (!data) return "";
                    let date = new Date(data);
                    let year = date.getFullYear();
                    let month = String(date.getMonth() + 1).padStart(2, '0');
                    let day = String(date.getDate()).padStart(2, '0');
                    return `${year}-${month}-${day}`;
                }
            },
            { data: 'total' },
            { data: 'paymentMode', width: '1%' },
            {
                data: 'status',
                render: function (data,type,row) {
                    return `
                        <select class="form-select" id="status-${row.id}" ${data !== 'Pending' ? 'disabled' : ''}>

                            <option value="Pending" ${data === 'Pending' ? 'selected' : ''}>Pending</option>
                            <option value="Approved" ${data === 'Approved' ? 'selected' : ''}>Approved</option>
                            <option value="Rejected" ${data === 'Rejected' ? 'selected' : ''}>Rejected</option>
                        </select>
                    `;
                }
            },
            {
                data: 'id',
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
						<a onClick="editStatus(${data})" type="button" class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i>Edit Status</a>
                        <a href="/Admin/Home/OrdersDetails/${data}" type="button" class="btn btn-primary">View Details</a>
                        </div>
                    `;
                }
            }
        ]
    });
}

function editStatus(id) {

    var orderStatus = document.getElementById('status-' + id).value;

    Swal.fire({
        title: "Are you sure, You want change status of Order?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, change it!"
    }).then((result) => {
        if (result.isConfirmed) {

            $.ajax({
                url: '/Admin/Home/EditOrderStatus/',
                type: 'POST',
                data: {
                    id: id,
                    status: orderStatus
                },
                success: function (response) {
                    if (response.success) {

                        Swal.fire({
                            title: "Order Status Changed!",
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

function loadCompletedOrders() {

    dataTable = $('#pendingOrdersTable').DataTable({
        "ajax": {
            "url": '/Admin/Home/GetOrders',
            "type": "GET",
            "data": {
                statusType: statusType
            },
            "datatype": "json"
        },
        "columns": [
            { data: 'id', width: '1%' },
            { data: 'userId' },
            { data: 'productId', width: '1%' },
            { data: 'dateTime' },
            { data: 'total' },
            { data: 'paymentMode', width: '1%' },
            { data: 'status' },
            {
                data: 'statusChangeDate',
                render: function (data) {
                    if (!data) return "";
                    let date = new Date(data);
                    let year = date.getFullYear();
                    let month = String(date.getMonth() + 1).padStart(2, '0');
                    let day = String(date.getDate()).padStart(2, '0');
                    return `${year}-${month}-${day}`;
                }
            },
            {
                data: 'id',
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                        <a href="/Admin/Home/OrdersDetails/${data}" type="button" class="btn btn-primary">View Details</a>
                        </div>
                    `;
                }
            }
        ]
    });
}

