
    const links = document.querySelectorAll('.popular a');
    const serviceTitle = document.getElementById('serviceTitle');

    links.forEach(link => {
        link.addEventListener('mouseenter', function () {
            serviceTitle.textContent = this.getAttribute('data-title');
            serviceTitle.classList.add('title-glow')
        });

        link.addEventListener('mouseleave', function () {

            serviceTitle.textContent = "Select Service";
            serviceTitle.classList.remove('title-glow')
        });
    });

    function updateDisplay() {
        const width = window.innerWidth;
        const rowDiv = document.getElementById('cards-div');
        const popularDiv = document.querySelector('.popular');

        if (width > 768) {
            popularDiv.style.display = 'grid'; 
            rowDiv.style.display = 'none'; 
        } else {
            popularDiv.style.display = 'none'; 
            rowDiv.style.display = 'block'; 
        }
    }

    updateDisplay();
    window.addEventListener('resize', updateDisplay);