$(document).ready(() => {
    const add_DecimalValueModal = $('#add_DecimalValueModal');
    add_DecimalValueModal.on('shown.bs.modal', shown_AddDecimalValueView);
    add_DecimalValueModal.on('hidden.bs.modal', hide_AddDecimalValueView);

    const edit_DecimalValueModal = $('#edit_DecimalValueModal');
    edit_DecimalValueModal.on('show.bs.modal', show_EditValueView);
    edit_DecimalValueModal.on('hidden.bs.modal', hide_EditValueView);

    const delete_ValueModal = $('#delete_DecimalValueModal');
    delete_ValueModal.on('show.bs.modal', show_DeleteValueView);
    delete_ValueModal.on('hidden.bs.modal', hide_DeleteValueView);
});

//Add start.
function add_DecimalValueChanged(event) {
    const result = valueChanged($(event.target).val());

    $('#add_Slug').val(result.slug);
    $('#add_Display').val(result.display);
    $('#add_Breadcrumb').val(result.breadcrumb);
}

function shown_AddDecimalValueView(event) {
    //Register event handlers.
    $('#add_DecimalValueSave').on('click', add_DecimalValueSaveClicked);
    $('#add_DecimalValue').on('change', add_DecimalValueChanged);
}

async function add_DecimalValueSaveClicked(event) {
    const decimalValue = $('#add_DecimalValue');
    const slug = $('#add_Slug');
    const display = $('#add_Display');
    const breadcrumb = $('#add_Breadcrumb');
    const sortOrder = $('#add_SortOrder');

    const decimalValueError = $('#add_DecimalValueError');
    const slugError = $('#add_SlugError');
    const displayError = $('#add_DisplayError');
    const breadcrumbError = $('#add_BreadcrumbError');
    const sortOrderError = $('#add_SortOrderError');

    let isValid = validateDecimal(decimalValue[0], decimalValueError[0]);
    isValid = isValid && validateString(slug[0], slugError[0], 256);
    isValid = isValid && validateSlug(slug[0], slugError[0]);
    isValid = isValid && validateString(display[0], displayError[0], 256);
    isValid = isValid && validateString(breadcrumb[0], breadcrumbError[0], 256);
    isValid = isValid && validateDecimal(sortOrder[0], sortOrderError[0]);

    if (!isValid) return;

    const data = new FormData();
    data.append('__RequestVerificationToken', $("[name='__RequestVerificationToken']").val());
    data.append('ProductAttributeID', $("#Id").val());
    data.append('DecimalValue', decimalValue.val());
    data.append('Slug', slug.val());
    data.append('Display', display.val());
    data.append('Breadcrumb', breadcrumb.val());
    data.append('SortOrder', sortOrder.val());

    try {
        const result = await fetch('/ProductAttributeDecimalValues/Add', {
            method: 'POST',
            body: data,
            credentials: 'same-origin'
        });
        if (result.ok) {
            const response = await result.text();
            $('#valuesTableBody').replaceWith(response);
            $('#add_DecimalValueModal').modal('hide');
        } else {
            const error = await result.json();
            for (const key in error.errors) {
                if (key.endsWith('Value'))
                    decimalValueError.text(error.errors[key]);
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

function hide_AddDecimalValueView() {
    //Deregister event handlers.
    $('#add_DecimalValueSave').off('click', add_DecimalValueSaveClicked);

    //reset validation errors.
    $('#add_DecimalValueError').text('')
    $('#add_SlugError').text('')
    $('#add_DisplayError').text('')
    $('#add_BreadcrumbError').text('')
    $('#add_SortOrderError').text('')

    //reset input elements and set to default values.
    $('#add_DecimalValue').val('').off('change', add_DecimalValueChanged);
    $('#add_Slug').val('')
    $('#add_Display').val('')
    $('#add_Breadcrumb').val('')
    $('#add_SortOrder').val('')
}
//Add end.

//Edit start.
function edit_DecimalValueChanged(event) {
    const result = valueChanged($(event.target).val());

    $('#edit_Slug').val(result.slug);
    $('#edit_Display').val(result.display);
    $('#edit_Breadcrumb').val(result.breadcrumb);
}

async function show_EditValueView(event) {
    //Register event handlers.
    $('#edit_DecimalValueSave').on('click', edit_DecimalValueSaveClicked);

    const productAttributeValueId = $(event.relatedTarget).data('value-id');
    try {
        const response = await doAjax('/api/ProductAttributeValues/' + productAttributeValueId, 'GET', null, true).promise();
        $('#edit_ValueId').val(response.id);
        $('#edit_DecimalValue').val(response.decimalValue).on('change', edit_DecimalValueChanged);
        $('#edit_Slug').val(response.slug);
        $('#edit_Display').val(response.display);
        $('#edit_Breadcrumb').val(response.breadcrumb);
        $('#edit_SortOrder').val(response.sortOrder);
    } catch (err) {
        alert('Error occured, please try again.')
    }
}

function hide_EditValueView() {
    //Deregister event handlers.
    $('#edit_DecimalValueSave').off('click', edit_DecimalValueChanged);

    //reset validation errors.
    $('#edit_DecimalValueError').text('')
    $('#edit_SlugError').text('')
    $('#edit_DisplayError').text('')
    $('#edit_BreadcrumbError').text('')
    $('#edit_SortOrderError').text('')

    //reset input elements and set to default values.
    $('#edit_ValueId').val('');
    $('#edit_DecimalValue').val('').off('change', edit_DecimalValueChanged);
    $('#edit_Slug').val('')
    $('#edit_Display').val('')
    $('#edit_Breadcrumb').val('')
    $('#edit_SortOrder').val('')
}

async function edit_DecimalValueSaveClicked(event) {
    const value = $('#edit_DecimalValue');
    const slug = $('#edit_Slug');
    const display = $('#edit_Display');
    const breadcrumb = $('#edit_Breadcrumb');
    const sortOrder = $('#edit_SortOrder');

    const valueError = $('#edit_DecimalValueError');
    const slugError = $('#edit_SlugError');
    const displayError = $('#edit_DisplayError');
    const breadcrumbError = $('#edit_BreadcrumbError');
    const sortOrderError = $('#edit_SortOrderError');

    let isValid = validateDecimal(value[0], valueError[0]);
    isValid = isValid && validateString(slug[0], slugError[0], 256);
    isValid = isValid && validateSlug(slug[0], slugError[0]);
    isValid = isValid && validateString(display[0], displayError[0], 256);
    isValid = isValid && validateString(breadcrumb[0], breadcrumbError[0], 256);
    isValid = isValid && validateDecimal(sortOrder[0], sortOrderError[0]);
    if (!isValid) return;

    const data = new FormData();
    data.append('__RequestVerificationToken', $("[name='__RequestVerificationToken']").val());
    data.append('Id', $('#edit_ValueId').val());
    data.append('ProductAttributeID', $("#Id").val());
    data.append('DecimalValue', value.val());
    data.append('Slug', slug.val());
    data.append('Display', display.val());
    data.append('Breadcrumb', breadcrumb.val());
    data.append('SortOrder', sortOrder.val());

    try {
        const result = await fetch('/ProductAttributeDecimalValues/Edit', {
            method: 'POST',
            body: data,
            credentials: 'same-origin'
        });
        if (result.ok) {
            const response = await result.text();
            $('#valuesTableBody').replaceWith(response);
            $('#edit_DecimalValueModal').modal('hide');
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
//Edit end.

//Delete start.
{
    async function show_DeleteValueView(event) {
        $('#delete_Id').val($(event.relatedTarget).data('value-id'));
        $('#delete_Value').text($(event.relatedTarget).data('value'))
        $('#delete_ValueButton').on('click', delete_ValueClicked);
    }

    function hide_DeleteValueView() {
        //reset input elements and set to default values.
        $('#delete_Id').val('');
        $('#delete_Value').text('');
        $('#delete_ValueButton').off('click', delete_ValueClicked);
    }

    async function delete_ValueClicked(event) {
        const data = new FormData();
        data.append('__RequestVerificationToken', $("[name='__RequestVerificationToken']").val());
        data.append('Id', $('#delete_Id').val());
        data.append('ProductAttributeID', $("#Id").val());

        try {
            const result = await fetch('/ProductAttributeValues/Delete', {
                method: 'POST',
                body: data,
                credentials: 'same-origin'
            });
            if (result.ok) {
                const response = await result.text();
                $('#valuesTableBody').replaceWith(response);
                $('#delete_ValueModal').modal('hide');
            } else {
                const error = await result.json();
                console.log(error);
            }
        } catch (err) {
            alert('Error occured while deleting value, please try again.');
        }
    }
}