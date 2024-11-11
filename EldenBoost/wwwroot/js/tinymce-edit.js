
document.addEventListener("DOMContentLoaded", function () {
    tinymce.init({
        selector: '#contentTextArea',
        content_css: 'dark',
        statusbar: false,
        height: 500,
        menubar: true,
        plugins: [
            'advlist autolink lists link image charmap print preview anchor',
            'searchreplace visualblocks code fullscreen',
            'insertdatetime media table paste code help wordcount'
        ],
        toolbar: 'code | undo redo | formatselect | bold italic backcolor | \
                              alignleft aligncenter alignright alignjustify | \
                              bullist numlist outdent indent | removeformat | help'
    });
});