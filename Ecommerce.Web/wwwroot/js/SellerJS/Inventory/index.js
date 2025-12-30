//inventory index js
$('#addProduct').on('shown.bs.modal', function () {

    $.ajax({
        url: '/seller/ProductVariant/ProductVariantDropdown',
        type: 'GET',
        success: function (data) {
            var select = $('#variantSelect');
            select.empty();
            select.append('<option value="">-- Select Variant --</option>');
            $.each(data, function (index, item) {
                select.append('<option value="' + item.id + '">' + item.name + '</option>');
            });
        },
        error: function () {
            alert('Error loading products');
        }
    });

    $('#variantSelect').select2({
        dropdownParent: $('#addProduct'),
        width: '100%'
    });
});

$('#variantSelect').on('change', function () {
    var selectedVariantId = $(this).val();

    // Prevent making a request if the default "Select" option is clicked
    if (!selectedVariantId) {
        $('#storeSelect').empty().append('<option value="">-- Select Store --</option>');
        return;
    }

    $.ajax({
        url: '/seller/Warehouse/GetAvailableStoresForVariant',
        type: 'GET', // Explicitly define the method
        data: { variantId: selectedVariantId },
        success: function (data) {
            var select = $('#storeSelect');
            select.empty();
            select.append('<option value="">-- Select Store --</option>');

            $.each(data, function (index, item) {
                // Ensure property names match your C# model's JSON casing
                select.append($('<option>', {
                    value: item.id || item.Id,
                    text: item.name || item.Name
                }));
            }); // Fixed: changed comma to semicolon
        },
        error: function (xhr, status, error) {
            console.error("AJAX Error: ", status, error);
            alert('Error loading stores.');
        }
    });
});



// Add Category form submission
$('#inventoryForm').on('submit', function (e) {    
    e.preventDefault();

    const data = {
        ProductVariantId: $('#variantSelect').val(),
        WarehouseId: $('#storeSelect').val(),
        StockQuantity: $('#quantity').val(),
        ReservedQuantity: $('#reserved_quantity').val()        
    };
    
    $('#saveCategoryBtn').prop('disabled', true)
        .html('<i class="fas fa-spinner fa-spin me-2"></i>Saving...');

    $.ajax({

        url: '/Seller/Inventory/CreateInventory',
        type: "POST",
        data: data,
        success: function (result) {
            showToast(result.message, 'success');
            $('#addProduct').modal('hide');
            setTimeout(() => location.reload(), 1500);
        },
        error: function () {
            showToast(result.message, 'danger');
            $('#saveCategoryBtn').prop('disabled', false)
                .html('<i class="fas fa-save me-2"></i>Save Category');
        }
    });
});