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