// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function showToast(message, type = 'success') {
    const toast = $(`
        <div class="toast align-items-center text-white bg-${type} border-0 show" role="alert" aria-live="assertive" aria-atomic="true" style="min-width: 250px; margin-bottom: 10px;">
            <div class="d-flex">
                <div class="toast-body">${message}</div>
            </div>
        </div>
    `);

    $('#toast-container').append(toast);

    setTimeout(() => {
        toast.fadeOut(500, () => toast.remove());
    }, 2500);
}

function showConfirmModal(message, onConfirm) {
    $('#confirmModalBody').text(message);

    const modal = new bootstrap.Modal($('#commonConfirmModal'));
    modal.show();

    $('#commonConfirmBtn').off('click').on('click', () => {
        modal.hide();
        if (typeof onConfirm === 'function') onConfirm();
    });
}
