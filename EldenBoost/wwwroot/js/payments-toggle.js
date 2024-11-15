

let filterBtn = document.querySelector('#show-paid');
let isShowingPaid = false; // Initial state is not showing archived payments

filterBtn.addEventListener('click', toggleApplications);

async function toggleApplications() {
    const tableBody = document.querySelector('table tbody');

    let url = 'https://localhost:7277/api/payment/';
    if (isShowingPaid) {
        url += 'getPending';
    } else {
        url += 'getPaid';
    }

    try {
        const response = await fetch(url);
        if (!response.ok) {
            throw new Error(`Error fetching orders: ${response.statusText}`);
        }
        const data = await response.json();

        // Clear the existing table content
        tableBody.innerHTML = '';

        // Populate the table with the fetched payments
        data.forEach(payment => {
            const row = document.createElement('tr');

            row.innerHTML = `
                <td><label>${payment.boosterName}</label></td>
                <td><label>${payment.orders.join(", ")}</label></td>
                <td><label>$${payment.amount.toFixed(2)}</label></td>
                <td><label>$${payment.issueDate}</label></td>
            `;

            tableBody.appendChild(row);
        });

        // Toggle the state and button text
        isShowingPaid = !isShowingPaid;
        filterBtn.textContent = isShowingPaid ? 'Show Pending' : 'Show Paid';
    } catch (error) {
        console.error(error);
    }
}

