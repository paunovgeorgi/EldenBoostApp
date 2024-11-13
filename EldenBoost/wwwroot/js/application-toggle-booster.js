let filterBtn = document.querySelector('#show-approved');
let isShowingApproved = false;

filterBtn.addEventListener('click', toggleApplications);

async function toggleApplications() {
    const tableBody = document.querySelector('table tbody');

    let url = 'https://localhost:7277/api/application/';
    if (isShowingApproved) {
        url += 'get-pending';
    } else {
        url += 'get-approved';
    }

    try {
        const response = await fetch(url);
        if (!response.ok) {
            throw new Error(`Error fetching orders: ${response.statusText}`);
        }
        const data = await response.json();
        console.log(data);

        tableBody.innerHTML = '';

        data.forEach(app => {
            const row = document.createElement('tr');

            row.innerHTML = `
                <td><label>${app.nickname}</label></td>
                <td><label>${app.email}</label></td>
                <td><label>${app.country}</label></td>
                <td><label>${app.experience}</label></td>
                <td><label>${app.availability}</label></td>
                <td><label>${app.platforms}</label></td>
                ${!app.isRejected && !app.isApproved ? `
        <td>
            <form method="post" action="/Application/ApproveBooster/${app.Id}" style="display: inline;">
                <button type="submit" class="btn btn-outline-success">Approve</button>
            </form>
        </td>
        <td>
            <form method="post" action="/Application/Reject/${app.Id}" style="display: inline;">
                <button type="submit" class="btn btn-outline-danger">Reject</button>
            </form>
        </td>` : ''}
            `;

            tableBody.appendChild(row);
        });

        isShowingApproved = !isShowingApproved;
        filterBtn.textContent = isShowingApproved ? 'Show Pending' : 'Show Approved';
    } catch (error) {
        console.error(error);
    }
}