var dataTable;

$(document).ready(function () {

    loadCustomerDetails();

});

function loadCustomerDetails() {

    dataTable = $('#customerDetails').DataTable({
        "ajax": {
            "url": '/Admin/Home/GetCustomerDetails',
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { data: 'userId'},
            { data: 'username' },
            { data: 'fullName' },
            { data: 'phoneNumber' },
            { data: 'roles' },
            {data : 'address'}
        ]
    });
}

