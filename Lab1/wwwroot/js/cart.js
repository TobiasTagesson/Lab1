//function AddToCart(productId) {
//    fetch("https://localhost:44354/api/cart/addtocart?id=" + productId)
//        .then(response => {
//            console.log(response);
//            if (response.ok) {
//                return response.text();
//            }
//        }).then(data => {
//            console.log("data: ", data);
//            //var element = document.getElementById("cart-amount");
//            //element.innerHTML = data;
//        });
//}
function AddToCart(productId) {
    fetch("https://localhost:44354/products/addtocart?id=" + productId)
        .then(response => {
            console.log(response);
            if (response.ok) {
                return response.text();
            }
        }).then(data => {
            console.log("data: ", data);
            //var element = document.getElementById("cart-amount");
            //element.innerHTML = data;
        });
}