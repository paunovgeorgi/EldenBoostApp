document.addEventListener('DOMContentLoaded', function () {
    const themeSwitcherLinks = document.querySelectorAll('.theme-switcher');
    const themeStylesheet = document.getElementById('theme-stylesheet');

    if (!themeStylesheet) {
        console.error('Theme stylesheet link with id "theme-stylesheet" is missing.');
        return;
    }

    themeSwitcherLinks.forEach(link => {
        link.addEventListener('click', function (e) {
            e.preventDefault();
            const theme = this.getAttribute('data-theme');
            themeStylesheet.href = `/css/theme-${theme}.css`;
            localStorage.setItem('theme', theme); // Save preference
        });
    });

    // Load saved theme on page load
    const savedTheme = localStorage.getItem('theme');
    if (savedTheme) {
        themeStylesheet.href = `/css/theme-${savedTheme}.css`;
    }
});
