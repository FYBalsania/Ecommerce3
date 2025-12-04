$(document).ready(function () {
    const edit_BooleanValueModal = $('#edit_BooleanValueModal');
    edit_BooleanValueModal.on('show.bs.modal', show_EditBooleanValueView);
    edit_BooleanValueModal.on('hide.bs.modal', hide_EditBooleanValueView);
});

async function show_EditBooleanValueView(event) {
    //Register event handlers.
    $('#edit_BooleanValueSave').on('click', edit_BooleanValueSaveClicked);

    const productAttributeValueId = $(event.relatedTarget).data('value-id');
    try {
        const response = await doAjax('/api/ProductAttributeValues/' + productAttributeValueId, 'GET', null, true).promise();
        $('#edit_ValueId').val(response.id);
        $('#edit_BooleanValue').val(response.booleanValue)
        $('#edit_Slug').val(response.slug);
        $('#edit_Display').val(response.display);
        $('#edit_Breadcrumb').val(response.breadcrumb);
        $('#edit_SortOrder').val(response.sortOrder);
    } catch (err) {
        alert('Error occured, please try again.')
    }
}

function hide_EditBooleanValueView(event) {
    $('#edit_BooleanValueSave').off('click', edit_BooleanValueSaveClicked);

    //reset validation errors.
    $('#edit_BooleanValueError').text('')
    $('#edit_SlugError').text('')
    $('#edit_DisplayError').text('')
    $('#edit_BreadcrumbError').text('')
    $('#edit_SortOrderError').text('')

    //reset input elements and set to default values.
    $('#edit_ValueId').val('');
    $('#edit_DecimalValue').val('')
    $('#edit_Slug').val('')
    $('#edit_Display').val('')
    $('#edit_Breadcrumb').val('')
    $('#edit_SortOrder').val('')
}

async function edit_BooleanValueSaveClicked(event) {
    const slug = $('#edit_Slug');
    const display = $('#edit_Display');
    const breadcrumb = $('#edit_Breadcrumb');
    const sortOrder = $('#edit_SortOrder');

    const slugError = $('#edit_SlugError');
    const displayError = $('#edit_DisplayError');
    const breadcrumbError = $('#edit_BreadcrumbError');
    const sortOrderError = $('#edit_SortOrderError');

    //reset validation errors.
    slugError.text('')
    displayError.text('')
    breadcrumbError.text('')
    sortOrderError.text('')

    let isValid = validateString(slug[0], slugError[0], 256);
    isValid = isValid && validateSlug(slug[0], slugError[0]);
    isValid = isValid && validateString(display[0], displayError[0], 256);
    isValid = isValid && validateString(breadcrumb[0], breadcrumbError[0], 256);
    isValid = isValid && validateDecimal(sortOrder[0], sortOrderError[0]);
    if (!isValid) return;

    const data = new FormData();
    data.append('__RequestVerificationToken', $("[name='__RequestVerificationToken']").val());
    data.append('Id', $('#edit_ValueId').val());
    data.append('ProductAttributeID', $("#Id").val());
    data.append('Slug', slug.val());
    data.append('Display', display.val());
    data.append('Breadcrumb', breadcrumb.val());
    data.append('SortOrder', sortOrder.val());

    try {
        const result = await fetch('/ProductAttributeBooleanValues/Edit', {
            method: 'POST',
            body: data,
            credentials: 'same-origin'
        });
        if (result.ok) {
            const response = await result.text();
            $('#valuesTableBody').replaceWith(response);
            $('#edit_BooleanValueModal').modal('hide');
        } else {
            const error = await result.json();
            for (const key in error.errors) {
                if (key.endsWith('Value'))
                    valueError.text(error.errors[key]);
                if (key.endsWith('Slug'))
                    slugError.text(error.errors[key]);
                if (key.endsWith('Display'))
                    displayError.text(error.errors[key]);
                if (key.endsWith('Breadcrumb'))
                    breadcrumbError.text(error.errors[key]);
                if (key.endsWith('SortOrder'))
                    sortOrderError.text(error.errors[key]);
            }
        }
    } catch (err) {
        alert('Error occured while saving, please try again.');
    }
}