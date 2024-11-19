let filterBtn = document.querySelector('#show-approved');
let isShowingApproved = false;


filterBtn.addEventListener('click', toggleApplications);

async function toggleApplications() {
    const tableBody = document.querySelector('table tbody');

    let url = 'https://localhost:7277/api/application/';
    if (isShowingApproved) {
        url += 'get-pending-author';
    } else {
        url += 'get-approved-author';
    }


    try {
        const response = await fetch(url);
        if (!response.ok) {
            throw new Error(`Error fetching orders: ${response.statusText}`);
        }
        const data = await response.json();

        const antiForgeryToken = document.querySelector('input[name="__RequestVerificationToken"]').value;

        tableBody.innerHTML = '';

        data.forEach(app => {
            const row = document.createElement('tr');

            row.innerHTML = `
                <td><label>${app.nickname}</label></td>
                <td><label>${app.email}</label></td>
                <td><label>${app.country}</label></td>
                <td><label>${app.experience}</label></td>
                <td><label>${app.availability}</label></td>
                ${!app.isRejected && !app.isApproved ? `
             <td>
                 <form method="post" action="/Admin/Application/ApproveAuthor/${app.Id}" style="display: inline;">
                <input name="__RequestVerificationToken" type="hidden" value="${antiForgeryToken}">
                <button type="submit" class="btn btn-outline-success">Approve</button>
                 </form>
             </td>
             <td>
            <form method="post" action="/Admin/Application/Reject/${app.Id}" style="display: inline;">
                <input name="__RequestVerificationToken" type="hidden" value="${antiForgeryToken}">
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