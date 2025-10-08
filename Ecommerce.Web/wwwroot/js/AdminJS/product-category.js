
$(document).ready(function () {
    initializeFilters();
    initializeBulkActions();
    initializeViewToggle();
});

// Initialize filters
function initializeFilters() {
    $('#searchInput').on('input', filterCategories);
    $('#hierarchyFilter, #statusFilter, #sortBy').on('change', filterCategories);
}

// Filter categories
function filterCategories() {
    const searchTerm = $('#searchInput').val().toLowerCase();
    const hierarchyFilter = $('#hierarchyFilter').val();
    const statusFilter = $('#statusFilter').val();
    let visibleCount = 0;

    $('.category-card, #listView tbody tr').each(function () {
        const $item = $(this);
        const name = $item.data('name');
        const status = $item.data('status');
        const level = $item.data('level');

        let shouldShow = true;

        if (searchTerm && !name.includes(searchTerm)) {
            shouldShow = false;
        }

        if (hierarchyFilter && level !== hierarchyFilter) {
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
        $('#listViewBtn, #treeViewBtn').removeClass('active');
        $('#gridView').show();
        $('#listView, #treeView').hide();
    });

    $('#listViewBtn').click(function () {
        $(this).addClass('active');
        $('#gridViewBtn, #treeViewBtn').removeClass('active');
        $('#listView').show();
        $('#gridView, #treeView').hide();
    });

    $('#treeViewBtn').click(function () {
        $(this).addClass('active');
        $('#gridViewBtn, #listViewBtn').removeClass('active');
        $('#treeView').show();
        $('#gridView, #listView').hide();
    });
}

// View products
function viewProducts(categoryId) {
    window.location.href = '/admin/products?categoryId=' + categoryId;
}

// Toggle status
function toggleStatus(categoryId, currentStatus) {
    $.ajax({
        url: '/admin/api/product-categories/' + categoryId + '/status',
        type: 'POST',
        data: { isActive: currentStatus === 'false' },
        success: function () {
            showAlert('Category status updated!', 'success');
            location.reload();
        },
        error: function () {
            showAlert('Failed to update status.', 'error');
        }
    });
}

// Delete category
function deleteCategory(categoryId, productCount) {
    if (productCount > 0) {
        if (!confirm(`This category has ${productCount} product(s). Are you sure you want to delete it?`)) {
            return;
        }
    } else if (!confirm('Are you sure you want to delete this category?')) {
        return;
    }

    $.ajax({
        url: '/admin/api/product-categories/' + categoryId,
        type: 'DELETE',
        success: function () {
            $(`[data-category-id="${categoryId}"]`).fadeOut(function () {
                $(this).remove();
            });
            showAlert('Category deleted successfully!', 'success');
        },
        error: function () {
            showAlert('Failed to delete category.', 'error');
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

    if (!confirm(`${action.charAt(0).toUpperCase() + action.slice(1)} ${selectedCategories.length} categories?`)) {
        return;
    }

    const endpoint = `/admin/api/product-categories/bulk/${action}`;

    $.ajax({
        url: endpoint,
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ categoryIds: selectedCategories }),
        success: function () {
            showAlert(`Bulk ${action} completed!`, 'success');
            setTimeout(() => location.reload(), 1500);
        },
        error: function () {
            showAlert(`Failed to perform bulk ${action}.`, 'error');
        }
    });
}

// Export categories
function exportCategories() {
    window.location.href = '/admin/api/product-categories/export';
}

// Show alert
function showAlert(message, type) {
    const alertClass = type === 'success' ? 'alert-success' : 'alert-danger';
    const iconClass = type === 'success' ? 'fa-check-circle' : 'fa-exclamation-triangle';

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