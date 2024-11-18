

document.addEventListener("DOMContentLoaded", function () {
    const modalElement = document.getElementById("cartModal");
    const modal = new bootstrap.Modal(modalElement);

    // Event listener for the "View Cart" button
    const viewCartButton = document.getElementById("viewCartButton");
    viewCartButton.addEventListener("click", function (event) {
        event.preventDefault();

        // Fetch the cart details using AJAX
        fetch("/Cart/ShowCart")
            .then(response => response.text())
            .then(html => {
                // Insert the cart content into the modal
                document.querySelector("#cartDetailsContent").innerHTML = html;

                // Attach event listeners for the cart buttons
                attachEventListeners();

                // Show the modal
                modal.show();
            })
            .catch(error => console.error("Error fetching cart details:", error));
    });

    // Attach event listeners for "Remove Item" and "Clear Cart" buttons
    function attachEventListeners() {
        attachRemoveItemListeners();
        attachClearCartListener();
    }

    // Function to attach event listeners for "Remove Item" buttons
    function attachRemoveItemListeners() {
        const removeButtons = document.querySelectorAll(".remove-item");
        removeButtons.forEach(button => {
            button.addEventListener("click", function (event) {
                event.preventDefault(); // Prevent any default action

                const itemId = button.getAttribute("data-item-id");
                const token = document.querySelector('input[name="__RequestVerificationToken"]').value;

                // Send the delete request
                fetch(`/Cart/RemoveItem`, {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/x-www-form-urlencoded", // Form data
                    },
                    body: `__RequestVerificationToken=${encodeURIComponent(token)}&cartItemId=${encodeURIComponent(itemId)}`, // Include token in the body
                })
                    .then(response => {
                        if (response.ok) {
                            return response.text(); // Updated modal HTML
                        } else {
                            return response.text().then(errorMessage => {
                                throw new Error(errorMessage || "Failed to remove item.");
                            });
                        }
                    })
                    .then(updatedHtml => {
                        // Update modal content with new cart details
                        document.querySelector("#cartDetailsContent").innerHTML = updatedHtml;

                        // Reattach event listeners to newly loaded content
                        attachEventListeners();

                        // Update the cart quantity in the _LoginPartial
                        updateCartQuantity();
                    })
                    .catch(error => {
                        console.error("Error removing item:", error);
                        alert(`An error occurred: ${error.message}`);
                    });
            });
        });
    }

    // Function to attach the "Clear Cart" button listener
    function attachClearCartListener() {
        const clearCartButton = document.querySelector(".clear-cart");
        if (clearCartButton) {
            clearCartButton.addEventListener("click", async (event) => {
                event.preventDefault();

                // Show confirmation dialog
                const result = await Swal.fire({
                    title: "Are you sure?",
                    text: "This will clear all items from your cart. This action cannot be undone.",
                    icon: "warning",
                    showCancelButton: true,
                    confirmButtonText: "Yes, clear it!",
                    cancelButtonText: "No, keep my cart",
                });

                if (result.isConfirmed) {
                    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;

                    try {
                        const response = await fetch("/Cart/ClearCart", {
                            method: "POST",
                            headers: {
                                "Content-Type": "application/json",
                                "RequestVerificationToken": token, // Pass token here
                            },
                        });

                        if (response.ok) {
                            // Refresh the modal content
                            const updatedHtml = await response.text();
                            document.querySelector("#cartDetailsContent").innerHTML = updatedHtml;

                            // Reattach event listeners to the new content
                            attachEventListeners();

                            // Update the cart quantity in the _LoginPartial
                            updateCartQuantity();

                            // Show success message
                            Swal.fire({
                                icon: "success",
                                title: "Cart Cleared",
                                text: "Your cart has been successfully cleared.",
                                timer: 2000,
                                showConfirmButton: false,
                            });
                        } else {
                            const errorMessage = await response.text();
                            console.error("Error clearing cart:", errorMessage);

                            // Show error message
                            Swal.fire({
                                icon: "error",
                                title: "Oops...",
                                text: `An error occurred: ${errorMessage || "Failed to clear cart."}`,
                            });
                        }
                    } catch (error) {
                        console.error("Error clearing cart:", error);

                        // Show error message
                        Swal.fire({
                            icon: "error",
                            title: "Oops...",
                            text: `An error occurred: ${error.message}`,
                        });
                    }
                }
            });
        }
    }



    // Function to update the cart quantity displayed in the _LoginPartial
    function updateCartQuantity() {
        fetch("/Cart/GetCartQuantity") // Assuming you have a method to get the current cart quantity
            .then(response => response.json())
            .then(quantity => {
                document.querySelector("#viewCartButton sub").textContent = quantity;
            })
            .catch(error => {
                console.error("Error fetching updated cart quantity:", error);
            });
    }
});