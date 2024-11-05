
document.getElementById("ServiceType").addEventListener("change", function () {
    var selectedType = this.value;
    document.getElementById("slider-section").style.display = (selectedType == 1) ? 'block' : 'none'; 
    document.getElementById("options-section").style.display = (selectedType == 2) ? 'block' : 'none';
});


document.getElementById("add-option-btn").addEventListener("click", function () {
    var optionsContainer = document.getElementById("service-options-container");
    var newOptionIndex = optionsContainer.children.length;
    var newOptionField = `<div class="form-group">
                                                      <input type="text" class="form-control" name="ServiceOptions[${newOptionIndex}].Name" placeholder="Option Name">
                                                      <input type="number" step="0.01" class="form-control mt-2" name="ServiceOptions[${newOptionIndex}].Price" placeholder="Option Price">
                                                  </div>`;
    optionsContainer.insertAdjacentHTML('beforeend', newOptionField);
});