$(document).ready(function () {

    loadCategory();
});

function loadCategory() {

    $.ajax({

        url: "/Customer/Home/GetCategory",
        type: 'GET',
        success: function (response) {
            if (response.success) {
                $('#categories').empty();

                $.each(response.data, function (index, item) {
                    $('#categories').append(`<a  class="dropdown-item" href="#category_${item.id}">${item.name} - ${item.sectionName}</a>`);
                });

            } else {
                $('#categories').empty();

                $('#categories').append('<span class="dropdown-item text-dark">No Categories Available</span>');
            }
        }

    });

}