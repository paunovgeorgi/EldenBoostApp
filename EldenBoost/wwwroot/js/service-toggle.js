
let filterBtn = document.querySelector('#show-deactivated');
let isShowingDeactivated = false;

filterBtn.addEventListener('click', toggleServices);

async function toggleServices() {
    const tableBody = document.querySelector('table tbody');

    let url = 'https://localhost:7277/api/service/';
    if (isShowingDeactivated) {
        url += 'getActive';
    } else {
        url += 'getDeactivated';
    }

    try {
        const response = await fetch(url);
        if (!response.ok) {
            throw new Error(`Error fetching orders: ${response.statusText}`);
        }
        const data = await response.json();


        const antiForgeryToken = document.querySelector('input[name="__RequestVerificationToken"]').value;
        tableBody.innerHTML = '';


        data.forEach(service => {
            const row = document.createElement('tr');

            row.innerHTML = `
                <td><img src="${service.imageURL}" style="width: 74px; object-fit: cover; aspect-ratio: 16/9;"/></td>
                <td><label>${service.title}</label></td>
                <td><label>${service.price}</label></td>
                <td><label>${service.purchaseCount}</label></td>
                <td>
    ${service.isActive
                    ? `<form method="post" action="/Admin/Service/Deactivate/${service.id}" style="display: inline">
               <input name="__RequestVerificationToken" type="hidden" value="${antiForgeryToken}">
               <button type="submit" class="btn btn-outline-danger">Deactivate</button>
           </form>`
                    : `<form method="post" action="/Admin/Service/Activate/${service.id}" style="display: inline">
               <input name="__RequestVerificationToken" type="hidden" value="${antiForgeryToken}">
               <button type="submit" class="btn btn-outline-success">Activate</button>
           </form>`}
</td>
                <td>
                    <a href="/Admin/Service/Edit/${service.id}?" class="btn btn-outline-light">Edit</a>
                </td>
            `;

            tableBody.appendChild(row);
        });


        isShowingDeactivated = !isShowingDeactivated;
        filterBtn.textContent = isShowingDeactivated ? 'Show Active' : 'Show Deactivated';
    } catch (error) {
        console.error(error);
    }
}

