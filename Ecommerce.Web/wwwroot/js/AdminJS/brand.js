$(function () {
    initializeFilters();
    initializeBulkActions();
    initializeViewToggle();
    initializeBrandForm();
});

// Initialize filters
function initializeFilters() {
    $('#searchInput').on('input', function () {
        filterBrands();
    });

    $('#statusFilter, #sortBy').on('change', function () {
        filterBrands();
    });
}

// Filter brands
function filterBrands() {
    const searchTerm = $('#searchInput').val().toLowerCase();
    const statusFilter = $('#statusFilter').val();
    let visibleCount = 0;

    $('#brandsTableBody tr, #brandsGridContainer .brand-card').each(function () {
        const $item = $(this);
        const brandName = $item.data('name');
        const status = $item.data('status');

        let shouldShow = true;

        if (searchTerm && !brandName.includes(searchTerm)) {
            shouldShow = false;
        }

        if (statusFilter && status !== statusFilter) {
            shouldShow = false;
        }

        if (shouldShow) {
            $item.show();
            visibleCount++;
        } else {
            $item.hide();
        }
    });

    $('#showingCount').text(visibleCount);
}

// Initialize bulk actions
function initializeBulkActions() {
    $('#selectAll').on('change', function () {
        $('.brand-checkbox').prop('checked', $(this).is(':checked'));
        updateBulkActions();
    });

    $(document).on('change', '.brand-checkbox', function () {
        updateBulkActions();
        const totalCheckboxes = $('.brand-checkbox').length;
        const checkedCheckboxes = $('.brand-checkbox:checked').length;
        $('#selectAll').prop('checked', totalCheckboxes === checkedCheckboxes);
    });
}

// Update bulk actions
function updateBulkActions() {
    const checkedCount = $('.brand-checkbox:checked').length;
    $('#selectedCount').text(checkedCount + ' brands selected');

    if (checkedCount > 0) {
        $('#bulkActionsCard').slideDown();
    } else {
        $('#bulkActionsCard').slideUp();
    }
}

// Initialize view toggle
function initializeViewToggle() {
    $('#listViewBtn').on('click', function () {
        $(this).removeClass('btn-outline-primary').addClass('btn-primary');
        $('#gridViewBtn').removeClass('btn-primary').addClass('btn-outline-secondary');
        $('#listView').show();
        $('#gridView').hide();
    });

    $('#gridViewBtn').on('click', function () {
        $(this).removeClass('btn-outline-secondary').addClass('btn-primary');
        $('#listViewBtn').removeClass('btn-primary').addClass('btn-outline-primary');
        $('#gridView').show();
        $('#listView').hide();
    });
}

// Initialize brand form
function initializeBrandForm() {
    $('#brandImage').on('change', function () {
        const file = this.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = function (e) {
                $('#previewImg').attr('src', e.target.result);
                $('#imagePreview').show();
            };
            reader.readAsDataURL(file);
        }
    });

    $('#brandForm').on('submit', function (e) {
        e.preventDefault();

        const formData = new FormData(this);

        $('#saveBrandBtn').prop('disabled', true)
            .html('<i class="fas fa-spinner fa-spin me-2"></i>Saving...');

        $.ajax({
            url: 'admin/brand/Create',
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (response) {
                $('#addBrandModal').modal('hide');
                showAlert('Brand saved successfully!', 'success');
                setTimeout(() => location.reload(), 1500);
            },
            error: function () {
                showAlert('Failed to save brand.', 'error');
                $('#saveBrandBtn').prop('disabled', false)
                    .html('<i class="fas fa-save me-2"></i>Save Brand');
            }
        });
    });
}

// View brand details
function viewBrand(brandId) {
    $.ajax({
        url: '/admin/api/brands/' + brandId,
        type: 'GET',
        success: function (brand) {
            let content = `
                    <div class="row">
                        <div class="col-md-4 text-center">
                            ${brand.brandImage ?
                    `<img src="${brand.brandImage}" class="img-fluid mb-3" style="max-height: 200px;">` :
                    '<div class="bg-light p-5 mb-3"><i class="fas fa-image fa-3x text-muted"></i></div>'}
                        </div>
                        <div class="col-md-8">
                            <h4>${brand.brandName}</h4>
                            <table class="table table-sm">
                                <tr><td class="fw-bold">Brand ID:</td><td>${brand.brandId}</td></tr>
                                <tr><td class="fw-bold">Status:</td><td>${brand.isActive ? '<span class="badge bg-success">Active</span>' : '<span class="badge bg-secondary">Inactive</span>'}</td></tr>
                                <tr><td class="fw-bold">Products:</td><td>${brand.productCount} products</td></tr>
                                <tr><td class="fw-bold">Created:</td><td>${new Date(brand.createdAt).toLocaleString()}</td></tr>
                            </table>
                        </div>
                    </div>
                `;
            $('#brandDetailsContent').html(content);
            $('#viewBrandModal').modal('show');
        }
    });
}

// Edit brand
function editBrand(brandId) {
    $.ajax({
        url: '/admin/api/brands/' + brandId,
        type: 'GET',
        success: function (brand) {
            $('#modalTitle').text('Edit Brand');
            $('#brandId').val(brand.brandId);
            $('#brandName').val(brand.brandName);
            $('#isActive').prop('checked', brand.isActive);

            if (brand.brandImage) {
                $('#previewImg').attr('src', brand.brandImage);
                $('#imagePreview').show();
            }

            $('#addBrandModal').modal('show');
        }
    });
}

// Toggle brand status
function toggleBrandStatus(brandId, currentStatus) {
    const newStatus = currentStatus === 'true' ? false : true;

    $.ajax({
        url: '/admin/api/brands/' + brandId + '/status',
        type: 'POST',
        data: { isActive: newStatus },
        success: function () {
            showAlert('Brand status updated successfully!', 'success');
            location.reload();
        },
        error: function () {
            showAlert('Failed to update brand status.', 'error');
        }
    });
}

// View products
function viewProducts(brandId) {
    window.location.href = '/admin/products?brandId=' + brandId;
}

// Delete brand
function deleteBrand(brandId) {    
    showConfirmModal("Are you sure you want to delete this brand? ", () => {
        $.ajax({
            url: '/admin/brand/delete/' + brandId,
            type: 'DELETE',
            success: function () {
                $(`[data-brand-id="${brandId}"]`).fadeOut(function () {
                    $(this).remove();
                });
                showToast('Brand deleted successfully!', 'success');
            },
            error: function () {
                showToast('Failed to delete brand.', 'danger');
            }
        });
    })

}

// Bulk actions
function bulkAction(action) {
    const selectedBrands = $('.brand-checkbox:checked').map(function () {
        return $(this).val();
    }).get();

    if (selectedBrands.length === 0) {
        alert('Please select at least one brand.');
        return;
    }

    let confirmMessage = '';
    let endpoint = '';

    switch (action) {
        case 'activate':
            confirmMessage = `Activate ${selectedBrands.length} brand(s)?`;
            endpoint = '/admin/api/brands/bulk/activate';
            break;
        case 'deactivate':
            confirmMessage = `Deactivate ${selectedBrands.length} brand(s)?`;
            endpoint = '/admin/api/brands/bulk/deactivate';
            break;
        case 'delete':
            confirmMessage = `Delete ${selectedBrands.length} brand(s)? This action cannot be undone.`;
            endpoint = '/admin/api/brands/bulk/delete';
            break;
    }

    if (confirm(confirmMessage)) {
        $.ajax({
            url: endpoint,
            type: 'POST',
            data: { brandIds: selectedBrands },
            success: function (response) {
                if (action === 'delete') {
                    selectedBrands.forEach(function (brandId) {
                        $(`[data-brand-id="${brandId}"]`).fadeOut(function () {
                            $(this).remove();
                        });
                    });
                } else {
                    location.reload();
                }
                showAlert(`Bulk ${action} completed successfully.`, 'success');
                updateBulkActions();
            },
            error: function (xhr) {
                const error = xhr.responseJSON?.message || `Failed to perform bulk ${action}.`;
                showAlert(error, 'error');
            }
        });
    }
}

// Export brands
function exportBrands() {
    const params = new URLSearchParams({
        search: $('#searchInput').val(),
        status: $('#statusFilter').val()
    });

    window.location.href = '/admin/api/brands/export?' + params.toString();
}

// Show alert
function showAlert(message, type) {
    const alertClass = type === 'success' ? 'alert-success' :
        type === 'error' ? 'alert-danger' : 'alert-info';
    const iconClass = type === 'success' ? 'fa-check-circle' :
        type === 'error' ? 'fa-exclamation-triangle' : 'fa-info-circle';

    const alert = $(`
    <div class="alert ${alertClass} alert-dismissible fade show position-fixed"
        style="top: 20px; right: 20px; z-index: 9999; min-width: 300px;" role="alert">
        <i class="fas ${iconClass} me-2"></i>${message}
        <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
    </div>
    `);

    $('body').append(alert);

    setTimeout(function () {
        alert.fadeOut(function () {
            $(this).remove();
        });
    }, 5000);
}

// Reset modal on close
$('#addBrandModal').on('hidden.bs.modal', function () {
    $('#modalTitle').text('Add New Brand');
    $('#brandForm')[0].reset();
    $('#brandId').val('');
    $('#imagePreview').hide();
    $('#previewImg').attr('src', '');
    $('#saveBrandBtn').prop('disabled', false)
        .html('<i class="fas fa-save me-2"></i>Save Brand');
});

// Sorting functionality
$('#sortBy').on('change', function () {
    const sortValue = $(this).val();
    const $container = $('#listView').is(':visible') ? $('#brandsTableBody') : $('#brandsGridContainer');
    const $items = $container.children().get();

    $items.sort(function (a, b) {
        const $a = $(a);
        const $b = $(b);

        switch (sortValue) {
            case 'name-asc':
                return $a.data('name').localeCompare($b.data('name'));
            case 'name-desc':
                return $b.data('name').localeCompare($a.data('name'));
            case 'products-desc':
                return parseInt($b.find('.badge.bg-info').text()) - parseInt($a.find('.badge.bg-info').text());
            case 'products-asc':
                return parseInt($a.find('.badge.bg-info').text()) - parseInt($b.find('.badge.bg-info').text());
            default:
                return 0;
        }
    });

    $.each($items, function (index, item) {
        $container.append(item);
    });
});

// Keyboard shortcuts
$(document).on('keydown', function (e) {
    // Ctrl+A to select all
    if (e.ctrlKey && e.key === 'a' && e.target.tagName !== 'INPUT') {
        e.preventDefault();
        $('#selectAll').prop('checked', true).trigger('change');
    }

    // Escape to clear selection
    if (e.key === 'Escape') {
        $('#selectAll').prop('checked', false);
        $('.brand-checkbox').prop('checked', false);
        updateBulkActions();
    }
});
