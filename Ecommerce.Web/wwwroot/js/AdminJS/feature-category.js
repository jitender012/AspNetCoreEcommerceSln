//index page scripts



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



$(document).ready(function () {
    initializeFilters();
    initializeBulkActions();
    initializeViewToggle();
});

// Initialize filters
function initializeFilters() {
    $('#searchInput').on('input', filterCategories);
    $('#statusFilter, #sortBy').on('change', filterCategories);
}

// Filter categories
function filterCategories() {
    const searchTerm = $('#searchInput').val().toLowerCase();
    const statusFilter = $('#statusFilter').val();
    let visibleCount = 0;

    $('.category-card, #listView tbody tr').each(function () {
        const $item = $(this);
        const name = $item.data('name');
        const mandatory = $item.data('mandatory').toString();

        let shouldShow = true;

        if (searchTerm && !name.includes(searchTerm)) {
            shouldShow = false;
        }

        if (statusFilter === 'mandatory' && mandatory !== 'true') {
            shouldShow = false;
        }

        if (statusFilter === 'optional' && mandatory === 'true') {
            shouldShow = false;
        }

        if (shouldShow) {
            $item.show();
            visibleCount++;
        } else {
            $item.hide();
        }
    });
}

// Initialize bulk actions
function initializeBulkActions() {
    $('#selectAll').change(function () {
        $('.category-checkbox').prop('checked', $(this).is(':checked'));
        updateBulkActions();
    });

    $(document).on('change', '.category-checkbox', function () {
        updateBulkActions();
    });
}

// Update bulk actions
function updateBulkActions() {
    const checkedCount = $('.category-checkbox:checked').length;
    $('#selectedCount').text(checkedCount + ' categories selected');

    if (checkedCount > 0) {
        $('#bulkActionsCard').slideDown();
    } else {
        $('#bulkActionsCard').slideUp();
    }
}

// Initialize view toggle
function initializeViewToggle() {
    $('#gridViewBtn').click(function () {
        $(this).addClass('active');
        $('#listViewBtn').removeClass('active');
        $('#gridView').show();
        $('#listView').hide();
    });

    $('#listViewBtn').click(function () {
        $(this).addClass('active');
        $('#gridViewBtn').removeClass('active');
        $('#listView').show();
        $('#gridView').hide();
    });
}

// View category
function viewCategory(categoryId) {
    window.location.href = '/admin/feature-categories/' + categoryId;
}


// Toggle mandatory
function toggleMandatory(categoryId, currentStatus) {
    $.ajax({
        url: '/admin/api/feature-categories/' + categoryId + '/mandatory',
        type: 'POST',
        data: { isMandatory: currentStatus === 'false' },
        success: function () {
            showToast('Category status updated!', 'success');
            location.reload();
        }
    });
}



// Manage assignments
let currentAssignmentCategoryId = null;

function manageAssignments(categoryId) {
    currentAssignmentCategoryId = categoryId;
    $.ajax({
        url: '/admin/api/feature-categories/' + categoryId + '/assignments',
        type: 'GET',
        success: function (assignments) {
            // Clear all checkboxes
            $('#assignmentsContent input[type="checkbox"]').prop('checked', false);

            // Check assigned categories
            assignments.forEach(function (catId) {
                $('#assignmentsContent input[value="' + catId + '"]').prop('checked', true);
            });

            $('#assignmentsModal').modal('show');
        }
    });
}

// Save assignments
function saveAssignments() {
    const selectedCategories = [];
    $('#assignmentsContent input[type="checkbox"]:checked').each(function () {
        selectedCategories.push($(this).val());
    });

    $.ajax({
        url: '/admin/api/feature-categories/' + currentAssignmentCategoryId + '/assignments',
        type: 'POST',
        data: { productCategoryIds: selectedCategories },
        success: function () {
            showToast('Assignments saved successfully!', 'success');
            $('#assignmentsModal').modal('hide');
        }
    });
}

// Bulk actions
function bulkAction(action) {
    const selectedCategories = $('.category-checkbox:checked').map(function () {
        return $(this).val();
    }).get();

    if (selectedCategories.length === 0) {
        alert('Please select at least one category.');
        return;
    }

    let confirmMessage = '';
    let endpoint = '';

    switch (action) {
        case 'mandatory':
            confirmMessage = `Set ${selectedCategories.length} categories as mandatory?`;
            endpoint = '/admin/api/feature-categories/bulk/mandatory';
            break;
        case 'optional':
            confirmMessage = `Set ${selectedCategories.length} categories as optional?`;
            endpoint = '/admin/api/feature-categories/bulk/optional';
            break;
        case 'delete':
            confirmMessage = `Delete ${selectedCategories.length} categories?`;
            endpoint = '/admin/api/feature-categories/bulk/delete';
            break;
    }

    if (confirm(confirmMessage)) {
        $.ajax({
            url: endpoint,
            type: 'POST',
            data: { categoryIds: selectedCategories },
            success: function () {
                showToast(`Bulk ${action} completed!`, 'success');
                setTimeout(() => location.reload(), 1500);
            }
        });
    }
}

// Add Category form submission
$('#categoryForm').submit(function (e) {
    e.preventDefault();

    const data = {
        name: $('#categoryName').val(),
        isMandatory: $('#isMandatory').is(':checked')
    };


    $('#saveCategoryBtn').prop('disabled', true)
        .html('<i class="fas fa-spinner fa-spin me-2"></i>Saving...');

    $.ajax({

        url: '/Admin/FeatureCategory/Create',
        type: "POST",
        data: data,
        success: function () {
            showToast('Category saved successfully!', 'success');
            $('#createCategoryModal').modal('hide');
            setTimeout(() => location.reload(), 1500);
        },
        error: function () {
            showToast('Failed to save category.', 'danger');
            $('#saveCategoryBtn').prop('disabled', false)
                .html('<i class="fas fa-save me-2"></i>Save Category');
        }
    });
});


// Reset modal on close
$('#createCategoryModal').on('hidden.bs.modal', function () {
    $('#modalTitle').text('Add Feature Category');
    $('#categoryForm')[0].reset();
    $('#categoryId').val('');
    $('#saveCategoryBtn').prop('disabled', false)
        .html('<i class="fas fa-save me-2"></i>Save Category');
});

// Edit category
function editCategory(categoryId) {
    $.ajax({
        url: '/Admin/FeatureCategory/Edit',
        data: categoryId,
        type: 'GET',
        success: function (category) {
            $('#modalTitle').text('Edit Feature Category');
            $('#categoryId').val(category.featureCategoryId);
            $('#categoryName').val(category.name);
            $('#isMandatory').prop('checked', category.isMandatory);
            $('#createCategoryModal').modal('show');
        }
    });
}

// Delete category
function deleteCategory(categoryId) {

    showConfirmModal('Are you sure you want to delete this category?', () => {
        $.ajax({
            type: "POST",
            url: '/Admin/FeatureCategory/Delete',
            data: { id: categoryId },
            success: function (response) {
                if (response.success) {
                    $(`[data-category-id="${categoryId}"]`).fadeOut(function () {
                        $(this).remove();
                    });
                    showToast('Category deleted successfully!', 'success');
                } else {
                    alert(response.message);
                }
            },
            error: function () {
                showToast(response.message, 'warning');
            }
        });
    })
}



// Export categories
function exportCategories() {
    window.location.href = '/admin/api/feature-categories/export';
}


// Sorting
$('#sortBy').change(function () {
    const sortValue = $(this).val();
    const $gridContainer = $('#gridView .row');
    const $listContainer = $('#listView tbody');

    const $gridItems = $gridContainer.children('.category-card').get();
    const $listItems = $listContainer.children('tr').get();

    function sortItems(items, isGrid) {
        items.sort(function (a, b) {
            const $a = $(a);
            const $b = $(b);

            switch (sortValue) {
                case 'name-asc':
                    return $a.data('name').localeCompare($b.data('name'));
                case 'name-desc':
                    return $b.data('name').localeCompare($a.data('name'));
                case 'mandatory-first':
                    const aMandatory = $a.data('mandatory') === 'true';
                    const bMandatory = $b.data('mandatory') === 'true';
                    if (aMandatory === bMandatory) {
                        return $a.data('name').localeCompare($b.data('name'));
                    }
                    return bMandatory ? 1 : -1;
                default:
                    return 0;
            }
        });

        return items;
    }

    // Sort grid view
    const sortedGridItems = sortItems($gridItems, true);
    $.each(sortedGridItems, function (index, item) {
        $gridContainer.append(item);
    });

    // Sort list view
    const sortedListItems = sortItems($listItems, false);
    $.each(sortedListItems, function (index, item) {
        $listContainer.append(item);
    });
});

// Keyboard shortcuts
$(document).keydown(function (e) {
    // Ctrl+N for new category
    if (e.ctrlKey && e.keyCode === 78) {
        e.preventDefault();
        $('#createCategoryModal').modal('show');
    }

    // Escape to clear selection
    if (e.keyCode === 27) {
        $('#selectAll').prop('checked', false);
        $('.category-checkbox').prop('checked', false);
        updateBulkActions();
    }
});

// Add smooth scroll for page
$('a[href^="#"]').on('click', function (e) {
    e.preventDefault();
    const target = $(this.hash);
    if (target.length) {
        $('html, body').animate({
            scrollTop: target.offset().top - 100
        }, 500);
    }
});

// Category name validation
$('#categoryName').on('input', function () {
    const value = $(this).val().trim();
    if (value.length < 2) {
        $(this).addClass('is-invalid');
    } else {
        $(this).removeClass('is-invalid').addClass('is-valid');
    }
});

// Initialize tooltips if needed
$(function () {
    $('[data-bs-toggle="tooltip"]').tooltip();
});

// Print functionality
function printCategories() {
    window.print();
}

// Refresh data
function refreshData() {
    location.reload();
}

// Auto-save scroll position
$(window).scroll(function () {
    sessionStorage.setItem('scrollPosition', $(this).scrollTop());
});

// Restore scroll position
$(document).ready(function () {
    const scrollPosition = sessionStorage.getItem('scrollPosition');
    if (scrollPosition) {
        $(window).scrollTop(scrollPosition);
        sessionStorage.removeItem('scrollPosition');
    }
});