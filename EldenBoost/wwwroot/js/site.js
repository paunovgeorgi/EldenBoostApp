
var message = function () {

    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": false,
        "progressBar": true,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }

    var showSuccess = function (message) {
        toastr["success"](message)
    }

    var showError = function (message) {
        toastr["error"](message)
    }

    var showWarning = function (message) {
        toastr["warning"](message)
    }

    var showInfo = function (message) {
        toastr["info"](message)
    }

    return {
        showSuccess,
        showError,
        showInfo,
        showWarning
    }
}();



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
            popup: 'custom-swal-size swal2-dark' // Combine both classes here
        }
    }).then((result) => {
        if (result.isConfirmed) {
            document.getElementById('delete-form').submit();
        }
    })
}