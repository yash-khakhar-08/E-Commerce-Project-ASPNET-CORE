function addToCart(productId) {
    var qty = document.getElementById("qty").value;

    const button = document.getElementById(`add-cart-button-${productId}`);

    

    $.ajax({

        url: '/Customer/Home/AddToCart',
        type: 'POST',
        data: {
            'productId': productId,
            'qty': qty

        },
        success: function (response) {
            if (response.success) {

                button.textContent = "Go to Cart";
                button.onclick = () => window.location.href = '/Customer/Home/GoToCart';
                alert("Product added to cart");
                getCartCount();


            } else {
                
                window.location.href = "/Identity/Account/Login";
            }
        }

    });

}

