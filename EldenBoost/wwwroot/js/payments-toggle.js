
const filterBtn = document.querySelector('#show-paid');
let isShowingPaid = false; 

filterBtn.addEventListener('click', togglePayments);

async function togglePayments() {
    const tableBody = document.querySelector('table tbody');

    let url = 'https://localhost:7277/api/payment/';
    url += isShowingPaid ? 'getPending' : 'getPaid';

    try {
        const response = await fetch(url);
        if (!response.ok) {
            throw new Error(`Error fetching payments: ${response.statusText}`);
        }
        const data = await response.json();

        // Clear the existing table content
        const antiForgeryToken = document.querySelector('input[name="__RequestVerificationToken"]').value;
        tableBody.innerHTML = '';

        // Populate the table with the fetched payments
        data.forEach(payment => {
            const row = document.createElement('tr');

            // Create the orders cell dynamically
            const ordersCell = document.createElement('td');
            payment.orders.forEach((orderId, index) => {
                const orderLink = document.createElement('a');
                orderLink.href = '#';
                orderLink.className = 'order-link';
                orderLink.setAttribute('data-order-id', orderId);
                orderLink.textContent = orderId;

                ordersCell.appendChild(orderLink);

                // Add a separator unless it's the last order
                if (index !== payment.orders.length - 1) {
                    const separator = document.createElement('span');
                    separator.textContent = ' | ';
                    ordersCell.appendChild(separator);
                }
            });

            // Add other table cells
            row.innerHTML = `
                <td><label>${payment.boosterName}</label></td>
                <td></td>
                <td><label>$${payment.amount.toFixed(2)}</label></td>
                <td><label>${payment.issueDate}</label></td>
                <td>${payment.isPaid ? payment.completionDate : ''}</td>
                <td>
                    ${!payment.isPaid
                ? `<form action="/Admin/Payment/Pay/${payment.id}" method="post" style="display: inline">
                     <input name="__RequestVerificationToken" type="hidden" value="${antiForgeryToken}">
                               <button type="submit" class="btn btn-outline-light">Pay</button>
                           </form>`
                    : ''}
                </td>
            `;

            // Replace the placeholder cell with the orders cell
            row.children[1].replaceWith(ordersCell);

            // Append the row to the table body
            tableBody.appendChild(row);
        });

        // Rebind event listeners for dynamically added .order-link elements
        bindOrderLinkEvents();

        // Toggle the state and button text
        isShowingPaid = !isShowingPaid;
        filterBtn.textContent = isShowingPaid ? 'Show Pending' : 'Show Paid';
    } catch (error) {
        console.error(error);
    }
}

// Bind event listeners to .order-link elements
function bindOrderLinkEvents() {
    const orderLinks = document.querySelectorAll('.order-link');
    const modalElement = document.getElementById('orderDetailsModal');
    const modal = new bootstrap.Modal(modalElement);

    orderLinks.forEach(link => {
        link.addEventListener('click', async event => {
            event.preventDefault();

            const orderId = link.getAttribute('data-order-id');

            try {
                const response = await fetch(`/Admin/Order/GetOrderDetails?orderId=${orderId}`);
                if (!response.ok) {
                    throw new Error(`Error fetching order details: ${response.statusText}`);
                }
                const html = await response.text();
                document.getElementById('orderDetailsContent').innerHTML = html;

                // Show the modal
                modal.show();
            } catch (error) {
                console.error(error);
            }
        });
    });
}

// Initial binding for order links in the existing table
bindOrderLinkEvents();


