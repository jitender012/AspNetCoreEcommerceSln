$(function () {
    initializeFilters();
    initializeBulkActions();
    initializeViewToggle();
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


//------------- create/edit brand js --------------

$(function () {
    // Load existing image if editing
    const existingImage = $('#existingImagePath').val();
    if (existingImage) {
        showPreview(existingImage);
    }

    // Character count for description
    $('#BrandDescription').on('input', function () {
        const count = $(this).val().length;
        $('#charCount').text(count);

        if (count > 450) {
            $('#charCount').addClass('text-warning');
        } else {
            $('#charCount').removeClass('text-warning');
        }
    });

    // Initialize character count
    $('#BrandDescription').trigger('input');

    // File input change handler
    $('#imageFileInput').on('change', function (e) {
        const file = e.target.files[0];
        if (file) {
            // Validate file size (5MB)
            if (file.size > 5 * 1024 * 1024) {
                alert('File size must be less than 5MB');
                $(this).val('');
                return;
            }

            // Validate file type
            if (!file.type.match('image.*')) {
                alert('Please select a valid image file');
                $(this).val('');
                return;
            }

            // Show preview
            const reader = new FileReader();
            reader.onload = function (e) {
                showPreview(e.target.result);
            };
            reader.readAsDataURL(file);
        }
    });

    // Show preview
    function showPreview(imageSrc) {
        $('#imagePreview').attr('src', imageSrc);
        $('#uploadContent').hide();
        $('#previewArea').show();
    }

    // Remove image
    $('#removeImageBtn').on('click', function () {
        $('#imageFileInput').val('');
        $('#existingImage').val('');
        $('#uploadContent').show();
        $('#previewArea').hide();
    });

    // Drag and drop handlers
    const uploadArea = $('#uploadArea');

    uploadArea.on('dragover', function (e) {
        e.preventDefault();
        e.stopPropagation();
        $(this).addClass('drag-over');
    });

    uploadArea.on('dragleave', function (e) {
        e.preventDefault();
        e.stopPropagation();
        $(this).removeClass('drag-over');
    });

    uploadArea.on('drop', function (e) {
        e.preventDefault();
        e.stopPropagation();
        $(this).removeClass('drag-over');

        const files = e.originalEvent.dataTransfer.files;
        if (files.length > 0) {
            $('#imageFileInput')[0].files = files;
            $('#imageFileInput').trigger('change');
        }
    });

    // Click on upload area
    uploadArea.on('click', function (e) {
        if (e.target === this || $(e.target).closest('.upload-content').length) {
            $('#imageFileInput').trigger('click');
        }
    });

    // Status switch handler
    $('#isActiveSwitch').on('change', function () {
        if ($(this).is(':checked')) {
            $('#statusText').text('Active').removeClass('text-danger').addClass('text-success');
        } else {
            $('#statusText').text('Inactive').removeClass('text-success').addClass('text-danger');
        }
    });

    // Form submission
    $('#brandForm').on('submit', function (e) {
        const brandName = $('#BrandName').val().trim();

        // Show loading state
        $('#saveBtn').prop('disabled', true)
            .html('<i class="fas fa-spinner fa-spin me-2"></i>Saving...');
    });

});

//toggle brand status
function toggleBrandStatus(brandId, isActive) {
    const $row = $(`tr[data-brand-id='${brandId}']`);
    if ($row.length > 0) {
        console.log("element is there")
    }
    $.ajax({
        url: `/brands/update-status/${brandId}`,
        type: 'POST',
        success: function (html) {
            $row.replaceWith(html);
            showToast('Brand status updated successfully.', 'success');
        },
        error: function (xhr) {
            const error = xhr.responseJSON?.message || 'Failed to update brand status.';
            showToast(error, 'danger');
        }
    });
}

//delete brand
function deleteBrand(brandId) {
    showConfirmModal("Delete this Brand?", () => {
        $.ajax({
            url: `/brands/delete/${brandId}`,
            type: 'DELETE',
            success: function (response) {
                const $row = $(`tr[data-brand-id='${brandId}']`);

                if (response) {
                    if ($row.length) {
                        showToast(response.message, "success")
                        $row.fadeOut(400, function () {
                            $(this).remove();
                        });
                    }
                    else {
                        window.location.href = "/brands";
                    }
                }
                else {
                    showToast(response.message, "danger")
                }
            }
        })
    })

}