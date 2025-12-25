$(document).ready(function () {
    $('#Name').on('change', nameChanged);

    $(document).on('change', '#addProductGroup', function () {
        const productGroupId = $(this).val();
        getAttribute(productGroupId);
    });
})

function nameChanged(event) {
    const name = $(event.target).val();
    $('#Slug').val(toSlug(name));
    $('#Display').val(name);
    $('#Breadcrumb').val(name);
    $('#AnchorText').val(name);
    $('#AnchorTitle').val(name);
    $('#H1').val(name);
    $('#MetaTitle').val(name);
}

async function getAttribute(productGroupId) {
    const container = $('#addProductGroupAttributesDiv');

    try {
        const response = await fetch(
            `/Products/GetAttributes?productGroupId=${productGroupId}`,
            { method: 'GET', credentials: 'same-origin'});

        const html = await response.text();
        container.html(html);

        // Re-parse validation for the entire form
        if ($.validator && $.validator.unobtrusive) {
            const form = container.closest('form');
            // Remove old validator instance
            form.removeData('validator');
            form.removeData('unobtrusiveValidation');
            // Re-parse the entire form with new fields
            $.validator.unobtrusive.parse(form);
        }
    } catch (err) {
        console.error('Error loading attribute values:', err);
        alert('Error loading attribute values.');
    }
}