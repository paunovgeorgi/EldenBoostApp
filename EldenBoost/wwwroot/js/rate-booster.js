
function rateBooster(boosterId, orderId) {
    Swal.fire({
        title: 'Rate the Booster',
        text: 'Please select a rating from 1 to 5:',
        input: 'range',
        inputAttributes: {
            min: 1,
            max: 5,
            step: 1
        },
        inputValue: 3, // Default value
        showCancelButton: true,
        confirmButtonText: 'Submit Rating',
        preConfirm: (rating) => {
            if (rating) {
                // Create a form and append it to the document
                var form = document.createElement('form');
                form.method = 'post';
                form.action = '/Booster/Rate';

                // Add CSRF token if your app uses it
                var csrfToken = document.querySelector('input[name="__RequestVerificationToken"]').value;
                var csrfInput = document.createElement('input');
                csrfInput.type = 'hidden';
                csrfInput.name = '__RequestVerificationToken';
                csrfInput.value = csrfToken;
                form.appendChild(csrfInput);

                // Append the boosterId, orderId, and rating to the form as hidden inputs
                var boosterInput = document.createElement('input');
                boosterInput.type = 'hidden';
                boosterInput.name = 'boosterId';
                boosterInput.value = boosterId;
                form.appendChild(boosterInput);

                var orderInput = document.createElement('input');
                orderInput.type = 'hidden';
                orderInput.name = 'orderId';
                orderInput.value = orderId;
                form.appendChild(orderInput);

                var ratingInput = document.createElement('input');
                ratingInput.type = 'hidden';
                ratingInput.name = 'rating';
                ratingInput.value = rating;
                form.appendChild(ratingInput);

                document.body.appendChild(form);
                form.submit();

                // Disable the rate button to prevent duplicate ratings
                document.querySelector(`button[onclick="rateBooster(${boosterId}, ${orderId})"]`).disabled = true;
            }
        }
    });
}