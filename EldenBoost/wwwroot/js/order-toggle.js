

let filterBtn = document.querySelector('#show-archived');
let isShowingArchived = false; 

filterBtn.addEventListener('click', toggleOrders);

async function toggleOrders() {
    const tableBody = document.querySelector('table tbody');

    let url = 'https://localhost:7277/api/order/';
    if (isShowingArchived) {
        url += 'getActive';
    } else {
        url += 'getArchived';
    }

    try {
        const response = await fetch(url);
        if (!response.ok) {
            throw new Error(`Error fetching orders: ${response.statusText}`);
        }
        const data = await response.json();


        tableBody.innerHTML = '';

  
        data.forEach(order => {
            const row = document.createElement('tr');

            row.innerHTML = `
                <td><label>${order.id}</label></td>
                <td><label>${order.timeOfPurchase}</label></td>
                <td><label>${order.serviceName}</label></td>
                <td><label>${order.platformName}</label></td>
                <td><label>${order.status}</label></td>
                <td><label>$${order.boosterPay.toFixed(2)}</label></td>
                <td><label>${order.assignedTo}</label></td>
                <td>
                   ${order.status === 'Completed' && !order.isArchived ? `<form method="post" action="/Order/Archive/${order.id}" style="display: inline">
                        <button type="submit" class="btn btn-outline-info">Archive</button>
                    </form>` : ''}
                </td>
                <td>
                    <a href="/Chat/OrderChat/${order.id}?area=" class="btn btn-outline-light">Order Chat</a>
                </td>
            `;

            tableBody.appendChild(row);
        });

 
        isShowingArchived = !isShowingArchived;
        filterBtn.textContent = isShowingArchived ? 'Show Active' : 'Show Archived';
    } catch (error) {
        console.error(error);
    }
}

