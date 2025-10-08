
const categoryId = $("#categoryId").val();

// Link feature
function linkFeature(featureId, featureName, featureCategoryId) {

    $.ajax({
        url: `/admin/ProductConfiguration/LinkFeature?categoryId=${categoryId}&featureId=${featureId}`,
        type: 'POST',
        success: function () {
            showToast(`Feature "${featureName}" linked successfully!`, 'success');
            reloadFeatureManagement(categoryId, featureCategoryId);
        },
        error: function () {
            showToast('Failed to link feature.', 'danger');
        }

    });
}

// Unlink feature
function unlinkFeature(featureId, featureName, featureCategoryId) {


    $.ajax({
        url: `/admin/ProductConfiguration/UnlinkFeature?categoryId=${categoryId}&featureId=${featureId}`,
        type: 'POST',
        success: function () {
            showToast(`Feature "${featureName}" unlinked successfully!`, 'success');
            reloadFeatureManagement(categoryId, featureCategoryId);
        },
        error: function () {
            showToast('Failed to unlink feature.', 'danger');
        }
    });
}

// Link all features in a category
function linkAllFeatures(featureCategoryId) {

    // var featureIds = 

    showConfirmModal('Link all available features in this category?', () => {
        $.ajax({
            url: '/admin/ProductConfiguration/LinkFeature/' + categoryId + '/feature-categories/' + featureCategoryId + '/link-all',
            type: 'POST',
            success: function () {
                showAlert('All features linked successfully!', 'success');
                location.reload();
            },
            error: function () {
                showAlert('Failed to link features.', 'error');
            }
        });
    });
}

// Unlink all features in a category
function unlinkAllFeatures(featureCategoryId) {
    if (confirm('Unlink all features in this category?')) {
        $.ajax({
            url: '/admin/api/product-categories/' + categoryId + '/feature-categories/' + featureCategoryId + '/unlink-all',
            type: 'POST',
            success: function () {
                showAlert('All features unlinked successfully!', 'success');
                location.reload();
            },
            error: function () {
                showAlert('Failed to unlink features.', 'error');
            }
        });
    }
}

// Delete category
function deleteCategory() {
    if (confirm('Are you sure you want to delete this category? This action cannot be undone.')) {
        $.ajax({
            url: '/admin/api/product-categories/' + categoryId,
            type: 'DELETE',
            success: function () {
                window.location.href = '/admin/product-categories';
            },
            error: function () {
                showAlert('Failed to delete category.', 'error');
            }
        });
    }
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
function reloadFeatureManagement(categoryId, featureCategoryId) {
    $.get(`/admin/category/loadfeaturemanagement?categoryId=${categoryId}&featureCategoryId=${featureCategoryId}`, function (html) {
        $(`#accordionData_${featureCategoryId}`).html(html);
    });
}


    document.addEventListener('DOMContentLoaded', function() {
    const searchInput = document.getElementById('featureSearchInput');
    const clearBtn = document.getElementById('clearSearchBtn');
    const searchResults = document.getElementById('searchResults');
    const accordion = document.getElementById('featureCategoriesAccordion');

    if (searchInput) {
        searchInput.addEventListener('input', function () {
            const searchTerm = this.value.toLowerCase().trim();

            if (searchTerm.length > 0) {
                clearBtn.style.display = 'block';
                performSearch(searchTerm);
            } else {
                clearBtn.style.display = 'none';
                resetSearch();
            }
        });

    clearBtn.addEventListener('click', function() {
        searchInput.value = '';
    clearBtn.style.display = 'none';
    resetSearch();
        });
    }

    function performSearch(searchTerm) {
        const accordionItems = accordion.querySelectorAll('.accordion-item');
    let totalMatches = 0;
    let matchedCategories = 0;
        
        accordionItems.forEach(item => {
            const categoryName = item.getAttribute('data-category-name');
    const categoryMatches = categoryName.includes(searchTerm);

    const featureItems = item.querySelectorAll('.feature-item');
    let hasMatchingFeatures = false;
            
            featureItems.forEach(featureItem => {
                const featureName = featureItem.getAttribute('data-feature-name');
    const matches = featureName.includes(searchTerm);

    if (matches) {
        featureItem.style.display = '';
    featureItem.classList.add('bg-light');
    hasMatchingFeatures = true;
    totalMatches++;
                } else {
        featureItem.style.display = 'none';
    featureItem.classList.remove('bg-light');
                }
            });

    if (categoryMatches || hasMatchingFeatures) {
        item.style.display = '';
    matchedCategories++;

    // Auto-expand matching categories
    const collapseElement = item.querySelector('.accordion-collapse');
    if (collapseElement && !collapseElement.classList.contains('show')) {
                    const bsCollapse = new bootstrap.Collapse(collapseElement, {
        toggle: true
                    });
                }
            } else {
        item.style.display = 'none';
            }
        });

        // Update search results text
        if (totalMatches > 0 || matchedCategories > 0) {
        searchResults.innerHTML = `<i class="fas fa-check-circle text-success"></i> Found ${totalMatches} feature(s) in ${matchedCategories} category(ies)`;
        } else {
        searchResults.innerHTML = `<i class="fas fa-times-circle text-danger"></i> No results found`;
        }
    }

    function resetSearch() {
        const accordionItems = accordion.querySelectorAll('.accordion-item');
        
        accordionItems.forEach(item => {
        item.style.display = '';

    const featureItems = item.querySelectorAll('.feature-item');
            featureItems.forEach(featureItem => {
        featureItem.style.display = '';
    featureItem.classList.remove('bg-light');
            });

    // Collapse all items
    const collapseElement = item.querySelector('.accordion-collapse');
    if (collapseElement && collapseElement.classList.contains('show')) {
                const bsCollapse = bootstrap.Collapse.getInstance(collapseElement);
    if (bsCollapse) {
        bsCollapse.hide();
                }
            }
        });

    searchResults.innerHTML = '';
    }
});