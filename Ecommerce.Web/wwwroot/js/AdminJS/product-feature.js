
$(document).ready(function () {
    initializeFilters();
    initializeBulkActions();
});

// Initialize filters
function initializeFilters() {
    $('#searchInput').on('input', function () {
        filterFeatures();
    });

    $('#categoryFilter, #typeFilter, #mandatoryFilter, #sortBy').on('change', function () {
        filterFeatures();
    });
}

// Filter features
function filterFeatures() {
    const searchTerm = $('#searchInput').val().toLowerCase();
    const categoryFilter = $('#categoryFilter').val();
    const typeFilter = $('#typeFilter').val();
    const mandatoryFilter = $('#mandatoryFilter').val();
    let visibleCount = 0;

    $('#featuresTableBody tr').each(function () {
        const $row = $(this);
        const name = $row.data('name');
        const category = $row.data('category');
        const type = $row.data('type').toString();
        const mandatory = $row.data('mandatory').toString();

        let shouldShow = true;

        if (searchTerm && !name.includes(searchTerm)) {
            shouldShow = false;
        }

        if (categoryFilter && category !== categoryFilter) {
            shouldShow = false;
        }

        if (typeFilter && type !== typeFilter) {
            shouldShow = false;
        }

        if (mandatoryFilter && mandatory !== mandatoryFilter) {
            shouldShow = false;
        }

        if (shouldShow) {
            $row.show();
            visibleCount++;
        } else {
            $row.hide();
        }
    });

    $('#showingCount').text(visibleCount);
}

// Initialize bulk actions
function initializeBulkActions() {
    $('#selectAll').change(function () {
        $('.feature-checkbox').prop('checked', $(this).is(':checked'));
        updateBulkActions();
    });

    $(document).on('change', '.feature-checkbox', function () {
        updateBulkActions();
        const totalCheckboxes = $('.feature-checkbox').length;
        const checkedCheckboxes = $('.feature-checkbox:checked').length;
        $('#selectAll').prop('checked', totalCheckboxes === checkedCheckboxes);
    });
}

// Update bulk actions
function updateBulkActions() {
    const checkedCount = $('.feature-checkbox:checked').length;
    $('#selectedCount').text(checkedCount + ' features selected');

    if (checkedCount > 0) {
        $('#bulkActionsCard').slideDown();
    } else {
        $('#bulkActionsCard').slideUp();
    }
}

// View feature options
function viewOptions(featureId) {
    $.ajax({
        url: '/admin/api/features/' + featureId + '/options',
        type: 'GET',
        success: function (options) {
            let content = '<div class="list-group">';

            if (options && options.length > 0) {
                options.forEach(function (option) {
                    content += `
                        <div class="list-group-item d-flex justify-content-between align-items-center">
                            <div>
                                <strong>${option.value}</strong>
                                ${option.displayOrder ? '<span class="text-muted ms-2">(Order: ' + option.displayOrder + ')</span>' : ''}
                            </div>
                            <span class="badge ${option.isActive ? 'bg-success' : 'bg-secondary'}">
                                ${option.isActive ? 'Active' : 'Inactive'}
                            </span>
                        </div>
                    `;
                });
            } else {
                content += '<p class="text-muted text-center py-3">No options available</p>';
            }

            content += '</div>';

            $('#optionsContent').html(content);
            $('#editOptionsBtn').attr('onclick', `window.location.href='/admin/features/${featureId}/options'`);
            $('#optionsModal').modal('show');
        },
        error: function () {
            showAlert('Failed to load feature options.', 'error');
        }
    });
}

// Toggle mandatory status
function toggleMandatory(featureId, currentStatus) {
    const newStatus = currentStatus === 'true' ? false : true;

    $.ajax({
        url: '/admin/api/features/' + featureId + '/mandatory',
        type: 'POST',
        data: { isMandatory: newStatus },
        success: function () {
            showAlert('Feature status updated successfully!', 'success');
            location.reload();
        },
        error: function () {
            showAlert('Failed to update feature status.', 'error');
        }
    });
}

// Duplicate feature
function duplicateFeature(featureId) {
    if (confirm('Create a copy of this feature?')) {
        $.ajax({
            url: '/admin/api/features/' + featureId + '/duplicate',
            type: 'POST',
            success: function (response) {
                showAlert('Feature duplicated successfully!', 'success');
                setTimeout(() => location.reload(), 1500);
            },
            error: function () {
                showAlert('Failed to duplicate feature.', 'error');
            }
        });
    }
}

// Delete feature
function deleteFeature(id) {
    showConfirmModal("Are you sure you want to delete this Feature?",()=>
        $.ajax({
            url: '/admin/ProductFeature/delete/' + id,
            type: 'DELETE',
            success: function () {
                $(`[data-feature-id="${id}"]`).fadeOut(function () {
                    $(this).remove();
                });
                showToast('Feature deleted successfully!', 'success');
            },
            error: function () {
                showToast('Failed to delete feature.', 'danger');
            }
        })
    )
       
   
}

// Bulk actions
function bulkAction(action) {
    const selectedFeatures = $('.feature-checkbox:checked').map(function () {
        return $(this).val();
    }).get();

    if (selectedFeatures.length === 0) {
        alert('Please select at least one feature.');
        return;
    }

    let confirmMessage = '';
    let endpoint = '';

    switch (action) {
        case 'mandatory':
            confirmMessage = `Set ${selectedFeatures.length} feature(s) as mandatory?`;
            endpoint = '/admin/api/features/bulk/mandatory';
            break;
        case 'optional':
            confirmMessage = `Set ${selectedFeatures.length} feature(s) as optional?`;
            endpoint = '/admin/api/features/bulk/optional';
            break;
        case 'delete':
            confirmMessage = `Delete ${selectedFeatures.length} feature(s)? This action cannot be undone.`;
            endpoint = '/admin/api/features/bulk/delete';
            break;
    }

    if (confirm(confirmMessage)) {
        $.ajax({
            url: endpoint,
            type: 'POST',
            data: { featureIds: selectedFeatures },
            success: function () {
                showAlert(`Bulk ${action} completed successfully.`, 'success');
                setTimeout(() => location.reload(), 1500);
            },
            error: function () {
                showAlert(`Failed to perform bulk ${action}.`, 'error');
            }
        });
    }
}

// Category management
function addCategory() {
    const categoryName = $('#newCategoryName').val().trim();

    if (!categoryName) {
        alert('Please enter a category name.');
        return;
    }

    $.ajax({
        url: '/admin/api/feature-categories',
        type: 'POST',
        data: { name: categoryName },
        success: function () {
            showAlert('Category added successfully!', 'success');
            setTimeout(() => location.reload(), 1500);
        },
        error: function () {
            showAlert('Failed to add category.', 'error');
        }
    });
}

function editCategory(categoryName) {
    const newName = prompt('Enter new category name:', categoryName);

    if (newName && newName !== categoryName) {
        $.ajax({
            url: '/admin/api/feature-categories/rename',
            type: 'POST',
            data: { oldName: categoryName, newName: newName },
            success: function () {
                showAlert('Category renamed successfully!', 'success');
                setTimeout(() => location.reload(), 1500);
            },
            error: function () {
                showAlert('Failed to rename category.', 'error');
            }
        });
    }
}

function deleteCategory(categoryName, featureCount) {
    if (featureCount > 0) {
        alert(`This category has ${featureCount} feature(s). Please reassign or delete features first.`);
        return;
    }

    if (confirm(`Delete category "${categoryName}"?`)) {
        $.ajax({
            url: '/admin/api/feature-categories',
            type: 'DELETE',
            data: { name: categoryName },
            success: function () {
                showAlert('Category deleted successfully!', 'success');
                setTimeout(() => location.reload(), 1500);
            },
            error: function () {
                showAlert('Failed to delete category.', 'error');
            }
        });
    }
}

// Export features
function exportFeatures() {
    window.location.href = '/admin/api/features/export';
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