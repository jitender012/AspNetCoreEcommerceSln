//index page scripts

$(() => {
    $('#myTable').DataTable();
})

$(document).on('click', '.delete-feature-category-btn', function (e) {
    e.preventDefault();

    var FeatureCategoryId = $(this).data("id");
    var dataRow = $('#' + FeatureCategoryId);

    showConfirmModal("Are you sure want to delete?", () => {
        $.ajax({
            type: "POST",
            url: '/Admin/FeatureCategory/Delete',
            data: { id: FeatureCategoryId },
            success: function (response) {
                if (response.success) {
                    showToast("Feature Category Deleted.", 'danger');
                    dataRow.remove();
                } else {
                    alert(response.message);
                }
            },
            error: function () {
                showToast(response.message, 'warning');
            }
        });
    })

})


//Create Feature Form-----
$(() => {
    $('.create-form').on('submit', function (e) {
        e.preventDefault();

        let form = $(this);
        let formData = form.serialize();
        let id = $('#featureList').data('id');

        $.ajax({
            type: "POST",
            url: '/Admin/ProductFeature/Create',
            data: formData,
            success: function (res) {
                if (res.success) {
                    showToast(res.message, 'success');
                    $('#addFeaturesForm').modal('hide');
                    form[0].reset();
                    $('#featureList').load('/Admin/FeatureCategory/GetFeatureList/' + id);
                } else {
                    showToast(res.message, 'danger');
                }
            },
            error: function () {
                showToast(res.message, 'danger');
            }
        });
    });
});

//Delete Feature starts -----
let deleteFeatureId = null;

$(document).on('click', '.delete-feature-btn', function () {
    deleteFeatureId = $(this).data('id');
    const modal = new bootstrap.Modal($('#deleteFeatureModal'));
    modal.show();
});

$(function () {
    $(document).on('click', '#confirmDeleteBtn', function () {
        $.ajax({
            type: 'POST',
            url: '/Admin/ProductFeature/Delete',
            data: { id: deleteFeatureId },
            success: function (res) {
                if (res.success) {
                    showToast(res.message, 'success');
                    $('#feature-row-' + deleteFeatureId).remove();
                } else {
                    showToast(res.message, 'danger');
                }
            },
            error: function () {
                showToast("Error deleting feature.", 'danger');
            }
        });

        const modal = bootstrap.Modal.getInstance($('#deleteFeatureModal'));
        modal.hide();
    })
});


//Link Category Form-----
$(() => {
    $('.link-product-category-form').on('submit', function (e) {
        e.preventDefault();

        let featCatId = $('input[name="FeatCatId"]').val();
        let prodCatId = $('select[name="ProductCategoryId"]').val();

        $.ajax({
            type: "POST",
            url: '/Admin/FeatureCategory/LinkFeatCatToProdCat',
            data: {
                featCatId: featCatId,
                prodCatId: prodCatId
            },
            success: function (res) {
                if (res.success) {
                    showToast(res.message, 'success');
                    $('#link-product-category-modal').modal('hide');
                    $('#productCategoryList').load('/Admin/FeatureCategory/GetProductCategoryList/' + featCatId);
                } else {
                    showToast(res.message, 'danger');
                }
            },
            error: function () {
                showToast(res.message, 'danger');
            }
        });
    });
});

let productCategoryId = null;
let categoryName = "";

$(() => {
    $(document).on('click', '.remove-productCategory-btn', function () {
        productCategoryId = $(this).data('id');
        categoryName = $(this).closest('div[id^="product-category-"]').find('input[type="text"]').val();
        $('#remove-productCategory .modal-body').text(`Are you sure you want to remove "${categoryName}"?`);

        showConfirmModal(`Are you sure you want to remove "${categoryName}"?`, () => {
            let featureCategoryId = $('#featureList').data('id');

            $.ajax({
                type: 'POST',
                url: '/Admin/FeatureCategory/UnlinkCategoryFeature',
                data: {
                    CategoryId: productCategoryId,
                    FeatureCategoryId: featureCategoryId
                },
                success: function (res) {
                    if (res.success) {
                        showToast(res.message, 'success');
                        $('#product-category-' + productCategoryId).remove();
                        $('#remove-productCategory').modal("hide");
                    } else {
                        showToast(res.message, 'danger');
                    }
                },
                error: function () {
                    showToast("Error deleting feature.", 'danger');
                }
            });

            const modal = bootstrap.Modal.getInstance($('#remove-productCategory'));
            modal.hide();
        })
    });
})
