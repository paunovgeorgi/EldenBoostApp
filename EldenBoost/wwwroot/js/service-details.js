
document.addEventListener("DOMContentLoaded", function () {
    const slider = document.getElementById("levelSlider");
    const priceDisplay = document.getElementById("priceDisplay");
    const updatedPriceField = document.getElementById("updatedPrice"); 
    const sliderValue = document.getElementById("sliderValue");
    const hasStream = document.getElementById("hasStream");
    const isExpress = document.getElementById("isExpress");
    const optionSelect = document.getElementById("optionSelect");
    const sliderValueHidden = document.getElementById("sliderValueHidden");

    function updatePrice() {
        let updatedPrice = basePrice; // Use the global basePrice variable

        // Option price adjustment
        if (optionSelect) {
            const selectedOption = optionSelect.options[optionSelect.selectedIndex];
            const optionPrice = parseFloat(selectedOption.getAttribute("data-price"));
            updatedPrice = optionPrice; // Set price to the selected option price
        }

        // Slider adjustment
        const level = slider ? slider.value : 1;
        if (slider) {
            updatedPrice *= level; // Adjust price based on slider value
            sliderValue.textContent = level;
            sliderValueHidden.value = level;
        }

        // Checkbox adjustments
        if (hasStream.checked) {
            updatedPrice += 5;
        }

        if (isExpress.checked) {
            updatedPrice *= 1.2;
        }

        // Update display and hidden fields
        priceDisplay.textContent = updatedPrice.toFixed(2);
        updatedPriceField.value = updatedPrice.toFixed(2);
    }

    // Event listeners for updates
    if (slider) slider.addEventListener("input", updatePrice);
    if (hasStream) hasStream.addEventListener("change", updatePrice);
    if (isExpress) isExpress.addEventListener("change", updatePrice);
    if (optionSelect) optionSelect.addEventListener("change", updatePrice); // Use updatePrice for option change

    // Initialize with the default values
    updatePrice();
});
