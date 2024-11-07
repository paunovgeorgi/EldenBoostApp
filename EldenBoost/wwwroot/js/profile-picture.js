

function openChangeProfilePicModal() {
    Swal.fire({
        title: 'Change Profile Picture',
        html:
            '<form id="changeProfilePicForm" action="/User/ChangeProfilePic" method="POST">' +
            '<input type="hidden" name="__RequestVerificationToken" value="' + document.querySelector('input[name="__RequestVerificationToken"]').value + '">' +
            '<input id="imageUrlInput" name="imageUrl" class="swal2-input" placeholder="Enter the image URL">' +
            '</form>',
        focusConfirm: false,
        showCancelButton: true,
        confirmButtonText: 'Submit',
        cancelButtonText: 'Cancel',
        preConfirm: () => {
            const imageUrl = document.getElementById('imageUrlInput').value;
            if (!imageUrl) {
                Swal.showValidationMessage('Please enter an image URL');
                return false;
            }
            // Manually submit the form via JavaScript
            return new Promise((resolve) => {
                const form = document.getElementById('changeProfilePicForm');
                const xhr = new XMLHttpRequest();
                xhr.open('POST', form.action, true);
                xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');

                xhr.onload = function () {
                    if (xhr.status === 200) {
                        resolve();  // Resolve the promise on success
                    } else {
                        Swal.showValidationMessage('There was an issue updating your profile picture.');
                    }
                };

                xhr.onerror = function () {
                    Swal.showValidationMessage('An error occurred while processing your request.');
                };

                // Send the form data
                xhr.send(new URLSearchParams(new FormData(form)).toString());
            });
        }
    }).then((result) => {
        if (result.isConfirmed) {
            Swal.fire({
                title: 'Success',
                text: 'You\'ve successfully updated your profile picture!',
                icon: 'success'
            }).then(() => {
                location.reload();  // Optionally reload the page to show the updated profile picture
            });
        }
    });
}