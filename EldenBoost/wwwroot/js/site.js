


function confirmDelete() {
    Swal.fire({
        title: 'Are you sure?',
        text: 'It\'s ok to say no mate',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#373A40',
        cancelButtonColor: '#686D76',
        confirmButtonText: 'Yes, delete it!',
        cancelButtonText: 'Cancel',
        customClass: {
            popup: 'custom-swal-size swal2-dark' 
        }
    }).then((result) => {
        if (result.isConfirmed) {
            document.getElementById('delete-form').submit();
        }
    })
}