
document.querySelectorAll('a.scroll-link').forEach(anchor => {
    anchor.addEventListener('click', function (e) {
        e.preventDefault();

        // Get the target element
        const target = document.querySelector(this.getAttribute('href'));

        // Get the position of the target and subtract the offset
        const targetPosition = target.getBoundingClientRect().top + window.pageYOffset - 80; // 40px offset

        // Smooth scroll to the target position
        window.scrollTo({
            top: targetPosition,
            behavior: 'smooth'
        });
    });
});