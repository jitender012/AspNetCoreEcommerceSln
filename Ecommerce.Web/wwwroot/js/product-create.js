$(function () {
    // Category selection handler
    $('#categorySelect').on('change', function () {
        const categoryId = $(this).val();

        if (categoryId) {
            loadCategoryAttributes(categoryId);
            updateProgress();
        } else {
            $featuresContainer.empty();
            $('#cardMessage').removeClass("d-none");
        }
    });
    // Load category attributes
    const $featuresContainer = $('#featuresList');
    function loadCategoryAttributes(categoryId) {
        $.ajax({
            url: "/Seller/Product/GetFeatures",
            type: "GET",
            data: { categoryId: categoryId },
            success: function (response) {
                if (!response || response.length === 0) {
                    $featuresContainer.append('');
                    return;
                }
                $('#cardMessage').addClass("d-none");
                $featuresContainer.html(response);
            },
            error: function () {
                showToast("Error fetching category attributes.", "error");
            }
        })
    }
})



let currentStep = 1;
const totalSteps = 4;
let uploadedImages = [];

function updateProgressBar() {
    const progress = ((currentStep - 1) / (totalSteps - 1)) * 100;
    document.getElementById('progressLine').style.width = progress + '%';

    document.querySelectorAll('.step').forEach((step, index) => {
        step.classList.remove('active', 'completed');
        if (index + 1 < currentStep) {
            step.classList.add('completed');
        } else if (index + 1 === currentStep) {
            step.classList.add('active');
        }
    });
}

function nextStep() {
    if (validateStep(currentStep)) {
        document.querySelector(`.form-step[data-step="${currentStep}"]`).classList.remove('active');
        currentStep++;
        document.querySelector(`.form-step[data-step="${currentStep}"]`).classList.add('active');
        updateProgressBar();

        if (currentStep === 4) {
            populateReview();
        }

        window.scrollTo({ top: 0, behavior: 'smooth' });
    }
}

function prevStep() {
    document.querySelector(`.form-step[data-step="${currentStep}"]`).classList.remove('active');
    currentStep--;
    document.querySelector(`.form-step[data-step="${currentStep}"]`).classList.add('active');
    updateProgressBar();
    window.scrollTo({ top: 0, behavior: 'smooth' });
}

function validateStep(step) {
    let isValid = true;
    const currentStepElement = document.querySelector(`.form-step[data-step="${step}"]`);
    const inputs = currentStepElement.querySelectorAll('input[required], select[required], textarea[required]');

    inputs.forEach(input => {
        if (!input.value.trim()) {
            input.classList.add('is-invalid');
            isValid = false;
        } else {
            input.classList.remove('is-invalid');
        }
    });

    if (step === 3 && uploadedImages.length === 0) {
        alert('Please upload at least one product image');
        isValid = false;
    }

    return isValid;
}

function handleImageUpload(event) {
    const files = event.target.files;
    const maxImages = 6;

    if (uploadedImages.length + files.length > maxImages) {
        alert(`You can upload maximum ${maxImages} images`);
        return;
    }

    Array.from(files).forEach((file, index) => {
        if (uploadedImages.length >= maxImages) return;

        const reader = new FileReader();
        reader.onload = function (e) {
            uploadedImages.push(e.target.result);
            displayImagePreviews();
        };
        reader.readAsDataURL(file);
    });
}

function displayImagePreviews() {
    const container = document.getElementById('imagePreviewContainer');
    container.innerHTML = '';

    uploadedImages.forEach((image, index) => {
        const previewDiv = document.createElement('div');
        previewDiv.className = 'image-preview';
        previewDiv.innerHTML = `
                    <img src="${image}" alt="Preview ${index + 1}">
                    <button type="button" class="remove-btn" onclick="removeImage(${index})">
                        <i class="bi bi-x"></i>
                    </button>
                `;
        container.appendChild(previewDiv);
    });
}

function removeImage(index) {
    uploadedImages.splice(index, 1);
    displayImagePreviews();
}


function populateReview() {
    document.getElementById('reviewProductName').textContent = document.getElementById('productName').value;
    document.getElementById('reviewBrand').textContent = document.getElementById('brandName').value;

    const category = document.getElementById('category');
    document.getElementById('reviewCategory').textContent = category.options[category.selectedIndex].text;

    const price = parseFloat(document.getElementById('price').value) || 0;
    const discount = parseFloat(document.getElementById('discount').value) || 0;
    const finalPrice = price - (price * discount / 100);

    document.getElementById('reviewPrice').textContent = '₹' + price.toFixed(2);
    document.getElementById('reviewDiscount').textContent = discount + '%';
    document.getElementById('reviewFinalPrice').textContent = '₹' + finalPrice.toFixed(2);

    document.getElementById('reviewSKU').textContent = document.getElementById('sku').value;
    document.getElementById('reviewStock').textContent = document.getElementById('stock').value + ' units';
}


updateProgressBar();