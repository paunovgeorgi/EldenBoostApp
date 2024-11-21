
document.addEventListener('DOMContentLoaded', function () {
    var reviewModal = document.getElementById('reviewModal');
    reviewModal.addEventListener('show.bs.modal', function (event) {
        var button = event.relatedTarget; // Button that triggered the modal
        var nickname = button.getAttribute('data-nickname');
        var reviewDate = button.getAttribute('data-reviewdate');
        var content = button.getAttribute('data-content');
        var profilePicture = button.getAttribute('data-profilepicture');

        // Update the modal's content with the clicked review details
        var modalNickname = document.getElementById('modalNickname');
        var modalReviewDate = document.getElementById('modalReviewDate');
        var modalContent = document.getElementById('modalContent');
        var modalProfilePicture = document.getElementById('modalProfilePicture');

        modalNickname.textContent = nickname;
        modalReviewDate.textContent = reviewDate;
        modalContent.textContent = content;
        modalProfilePicture.src = profilePicture;
    });
});