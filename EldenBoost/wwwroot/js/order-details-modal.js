
document.addEventListener('DOMContentLoaded', function () {
    const modalElement = document.getElementById('orderDetailsModal');
    const modal = new bootstrap.Modal(modalElement);
    const orderLinks = document.querySelectorAll('.order-link');

    orderLinks.forEach(link => {
        link.addEventListener('click', function (event) {
            event.preventDefault();

            const orderId = this.getAttribute('data-order-id');

            fetch(`/Admin/Order/GetOrderDetails?orderId=${orderId}`)
                .then(response => response.text())
                .then(order => {
                    document.getElementById('orderDetailsContent').innerHTML = order;
                    modal.show();
                })
                .catch(error => console.error('Error fetching order details:', error));
        });
    });
});