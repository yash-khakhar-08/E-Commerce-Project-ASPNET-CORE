$(document).ready(function () {
    getCartCount();

});

function getCartCount() {

    $.ajax({

        url: '/Customer/Home/GetCartCount',
        type: 'GET',
        success: function (response) {
            if (response.success) {
                
                document.getElementById("cart-count").innerText = response.count;

            }
        }

    });

}