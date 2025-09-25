const imageInput = document.getElementById('productImages');
const imagePreviewContainer = document.getElementById('imagePreviewContainer');
const maxFiles = 5;

imageInput.addEventListener('change', function (event) {
    const selectedFiles = event.target.files;

    imagePreviewContainer.innerHTML = '';
    if (selectedFiles.length > maxFiles) {
        alert(`Max ${maxFiles} images allowed.`);
        imageInput.value = ''; // Clear selected files
        return;
    }
    for (let i = 0; i < selectedFiles.length; i++) {
        const file = selectedFiles[i];

        if (file.type.startsWith('image/')) {
            const reader = new FileReader();

            reader.onload = function (e) {
                const imgElement = document.createElement('img');
                imgElement.src = e.target.result;
                imgElement.alt = file.name;
                imgElement.style.maxWidth = '200px'; // Adjust as needed
                imgElement.style.margin = '5px'; // Adjust as needed
                imagePreviewContainer.appendChild(imgElement);
            }
            reader.readAsDataURL(file);
        } else {
            console.warn(`File ${file.name} is not an image.`);
        }
    }
})

$(document).ready(
    function () {
        $("#brandDropdown").select2({
            theme: 'bootstrap-5',
            tags: true,
            placeholder: "Select or add a brand",
            allowClear: true
        });
    }
)
$(document).ready(
    function () {
        $("#categoryDropdown").select2({
            theme: 'bootstrap-5',
            placeholder: "Select a category",
            allowClear: true
        });

        $('#categoryDropdown').change(function () {
            let categoryId = $(this).val();

            if (categoryId) {
                $.ajax({
                    url: "/Seller/Product/GetFeatureCategoriesWithFeatures",
                    type: "GET",
                    data: { categoryId: categoryId },
                    success: function (response) {
                        console.log(response);
                        var additionalDetails = $('#additionalDetails');
                        additionalDetails.empty();
                        $.each(response, function (i, category) {

                            additionalDetails.append(`<h4 style="margin-bottom:0; margin-top:10px;">${category.name}</h4>`);

                            $.each(category.productFeatures, function (j, feature) {
                                console.log(feature);
                                additionalDetails.append(`
                                            <div class="form-group py-1 col-12 col-md-6 col-lg-4">
                                                <label class="control-label" for="${feature.name}">${feature.name}</label>
                                                <input type="text" name="Features[${j}].Value" class="form-control"/>
                                                <input type="hidden" name="Features[${j}].Name" value="${feature.name}" />
                                                <input type="hidden" name="Features[${j}].ProductFeaturesId" value="${feature.productFeatureId}" />
                                            </div>
                                        `);
                            });
                        });
                    },
                    error: function () {
                        alert("Error fetching feature categories.");
                    }
                });
            }
        });
    }
)

$(document).ready(function () {
    $("#ImageFile").change(function () {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#imagePreview').attr('src', e.target.result);
            $('#imagePreview').show();
        }
        reader.readAsDataURL(this.files[0]);
    });
});