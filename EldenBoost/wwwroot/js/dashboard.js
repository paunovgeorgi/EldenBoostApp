
import { apiPort } from "./config.js"

document.addEventListener('DOMContentLoaded', function () {
    const apiBaseUrl = `https://localhost:${apiPort}/api`;

    // Function to update counts for orders, services, and users
    function updateDashboardCounts() {
        // Fetch and update orders data
        fetch(`${apiBaseUrl}/order/get-data`)
            .then(response => {
                if (!response.ok) {
                    throw new Error(`Failed to fetch orders data: ${response.status}`);
                }
                return response.json();
            })
            .then(orderData => {
                document.getElementById('ordersPending').textContent = orderData.pending;
                document.getElementById('ordersWorking').textContent = orderData.working;
                document.getElementById('ordersCompleted').textContent = orderData.completed;
                document.getElementById('ordersTotal').textContent = orderData.total;
            })
            .catch(error => console.error("Error fetching order data:", error));

        // Fetch and update services data
        fetch(`${apiBaseUrl}/service/get-data`)
            .then(response => {
                if (!response.ok) {
                    throw new Error(`Failed to fetch services data: ${response.status}`);
                }
                return response.json();
            })
            .then(serviceData => {
                document.getElementById('servicesStandard').textContent = serviceData.standard;
                document.getElementById('servicesSlider').textContent = serviceData.slider;
                document.getElementById('servicesOption').textContent = serviceData.option;
                document.getElementById('servicesTotal').textContent = serviceData.total;
            })
            .catch(error => console.error("Error fetching service data:", error));

        fetch(`${apiBaseUrl}/user/get-data`)
            .then(response => {
                if (!response.ok) {
                    throw new Error(`Failed to fetch users data: ${response.status}`)
                }
                return response.json();
            })
            .then(userData => {
                document.getElementById('usersClients').textContent = userData.clients;
                document.getElementById('usersBoosters').textContent = userData.boosters;
                document.getElementById('usersAuthors').textContent = userData.authors;
                document.getElementById('usersTotal').textContent = userData.total;
            })
            .catch(error => console.error("Error fetching user data:", error))

        fetch(`${apiBaseUrl}/application/get-data`)
            .then(response => {
                if (!response.ok) {
                    throw new Error(`Failed to fetch users data: ${response.status}`);
                }
                return response.json();
            })
            .then(applicationData => {
                document.getElementById('boosterPending').textContent = applicationData.boosterPending;
                document.getElementById('boosterApproved').textContent = applicationData.boosterApproved;
                document.getElementById('authorPending').textContent = applicationData.authorPending;
                document.getElementById('authorApproved').textContent = applicationData.authorApproved;
            })
            .catch(error => console.error("Error fetching user data:", error));

        fetch(`${apiBaseUrl}/article/get-data`)
            .then(response => {
                if (!response.ok) {
                    throw new Error(`Failed to fetch users data: ${response.status}`);
                }
                return response.json();
            })
            .then(articleData => {
                document.getElementById('articleNews').textContent = articleData.news;
                document.getElementById('articleGuides').textContent = articleData.guides;
            })
            .catch(error => console.error("Error fetching user data:", error));

        fetch(`${apiBaseUrl}/payment/get-data`)
            .then(response => {
                if (!response.ok) {
                    throw new Error(`Failed to fetch users data: ${response.status}`);
                }
                return response.json();
            })
            .then(paymentData => {
                document.getElementById('paymentPending').textContent = paymentData.pending;
                document.getElementById('paymentPaid').textContent = paymentData.paid;
            })
            .catch(error => console.error("Error fetching user data:", error));
    }

    // Update counts immediately and then refresh every 30 seconds
    updateDashboardCounts();
    setInterval(updateDashboardCounts, 30000); // 30 seconds
});
