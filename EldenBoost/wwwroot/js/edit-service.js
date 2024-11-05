
const maxAmountField = document.getElementById("maxAmountField");
const optionsSection = document.getElementById("options-section");

function toggleServiceTypeFields() {
    maxAmountField.style.display = serviceType === 1 ? 'block' : 'none';  // Slider
    optionsSection.style.display = serviceType === 2 ? 'block' : 'none';  // Option
}

document.addEventListener("DOMContentLoaded", toggleServiceTypeFields);

// Add dynamic option fields (Name + Price)
document.getElementById("add-option-btn").addEventListener("click", function () {
    const optionsContainer = document.getElementById("service-options-container");
    const newOptionIndex = optionsContainer.children.length;
    const newOptionField = `<div class="form-group">
                                                  <input type="text" class="form-control" name="ServiceOptions[${newOptionIndex}].Name" placeholder="Option Name">
                                                  <input type="number" step="0.01" class="form-control mt-2" name="ServiceOptions[${newOptionIndex}].Price" placeholder="Option Price">
                                                  <button type="button" class="btn btn-danger mt-2 remove-option-btn">Remove</button>
                                             </div>`;
    optionsContainer.insertAdjacentHTML('beforeend', newOptionField);
});

// Event delegation to handle remove button click
document.getElementById("service-options-container").addEventListener("click", function (e) {
    if (e.target && e.target.classList.contains("remove-option-btn")) {
        e.target.closest(".form-group").remove();
    }
});