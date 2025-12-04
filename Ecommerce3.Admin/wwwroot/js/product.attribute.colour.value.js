$(document).ready(function () {
    const add_ColourValueModal = $('#add_ColourValueModal');
    add_ColourValueModal.on('show.bs.modal', show_AddColourValueView);
    add_ColourValueModal.on('hide.bs.modal', hide_AddColourValueView);

    const edit_ColourValueModal = $('#edit_ColourValueModal');
    edit_ColourValueModal.on('show.bs.modal', show_EditColourValueView);
    edit_ColourValueModal.on('hide.bs.modal', hide_EditColourValueView);
    
    const delete_ColourValueModal = $('#delete_ColourValueModal');
    delete_ColourValueModal.on('show.bs.modal', show_DeleteColourValueView);
    delete_ColourValueModal.on('hide.bs.modal', hide_DeleteColourValueView);
});

//Add start.
function add_ValueChange(event) {
    const result = valueChanged($(event.target).val());

    $('#add_Slug').val(result.slug);
    $('#add_Display').val(result.display);
    $('#add_Breadcrumb').val(result.breadcrumb);
}

function show_AddColourValueView(event) {
    //Register event handlers.
    $('#add_Value').on('change', add_ValueChange);
    $('#add_ColourValueSave').on('click', add_ColourValueSave);
}

function hide_AddColourValueView(event) {
    //Deregister event handlers.
    $('#add_ColourValueSave').off('click', add_ColourValueSave);

    //clear input elements and set to default values.
    $('#add_Value').val('').off('change', add_ValueChange);
    $('#add_Slug').val('');
    $('#add_Display').val('');
    $('#add_Breadcrumb').val('');
    $('#add_HexCode').val('');
    $('#add_ColourFamily').val('')
    $('#add_ColourFamilyHexCode').val('');
    $('#add_SortOrder').val('');

    //reset validation errors.
    $('#add_ValueError').text('')
    $('#add_SlugError').text('')
    $('#add_DisplayError').text('')
    $('#add_BreadcrumbError').text('')
    $('#add_HexCodeError').text('')
    $('#add_ColourFamilyError').text('')
    $('#add_ColourFamilyHexCodeError').text('')
    $('#add_SortOrderError').text('')
}

async function add_ColourValueSave(event) {
    const valueElement = $('#add_Value');
    const slugElement = $('#add_Slug');
    const displayElement = $('#add_Display');
    const breadcrumbElement = $('#add_Breadcrumb');
    const hexCodeElement = $('#add_HexCode');
    const colourFamilyElement = $('#add_ColourFamily');
    const colourFamilyHexCodeElement = $('#add_ColourFamilyHexCode');
    const sortOrderElement = $('#add_SortOrder');

    const valueErrorElement = $('#add_ValueError');
    const slugErrorElement = $('#add_SlugError');
    const displayErrorElement = $('#add_DisplayError');
    const breadcrumbErrorElement = $('#add_BreadcrumbError');
    const hexCodeErrorElement = $('#add_HexCodeError');
    const colourFamilyErrorElement = $('#add_ColourFamilyError');
    const colourFamilyHexCodeErrorElement = $('#add_ColourFamilyHexCodeError');
    const sortOrderErrorElement = $('#add_SortOrderError');

    valueErrorElement.text('');
    slugErrorElement.text('');
    displayErrorElement.text('');
    breadcrumbErrorElement.text('');
    hexCodeErrorElement.text('');
    colourFamilyErrorElement.text('');
    colourFamilyHexCodeErrorElement.text('');
    sortOrderErrorElement.text('');

    let isValid = validateString(valueElement[0], valueErrorElement[0], 256);
    isValid = isValid && validateString(slugElement[0], slugErrorElement[0], 256);
    isValid = isValid && validateSlug(slugElement[0], slugErrorElement[0]);
    isValid = isValid && validateString(displayElement[0], displayErrorElement[0], 256);
    isValid = isValid && validateString(breadcrumbElement[0], breadcrumbErrorElement[0], 256);
    isValid = isValid && validateOptionalString(hexCodeElement[0], hexCodeErrorElement[0], 8);
    isValid = isValid && validateString(colourFamilyElement[0], colourFamilyErrorElement[0], 128);
    isValid = isValid && validateOptionalString(colourFamilyHexCodeElement[0], colourFamilyHexCodeErrorElement[0], 8);
    isValid = isValid && validateDecimal(sortOrderElement[0], sortOrderErrorElement[0]);

    if (!isValid) return;

    const data = new FormData();
    data.append('__RequestVerificationToken', $("[name='__RequestVerificationToken']").val());
    data.append('ProductAttributeID', $("#Id").val());
    data.append('Value', valueElement.val());
    data.append('Slug', slugElement.val());
    data.append('Display', displayElement.val());
    data.append('Breadcrumb', breadcrumbElement.val());
    data.append('HexCode', hexCodeElement.val());
    data.append('ColourFamily', colourFamilyElement.val());
    data.append('ColourFamilyHexCode', colourFamilyHexCodeElement.val());
    data.append('SortOrder', sortOrderElement.val());

    try {
        const result = await fetch('/ProductAttributeColourValues/Add/', {
            method: 'POST',
            body: data,
            credentials: 'same-origin'
        });
        if (result.ok) {
            const response = await result.text();
            $('#valuesTableBody').replaceWith(response);
            $('#add_ColourValueModal').modal('hide');
        } else {
            const error = await result.json();
            for (const key in error.errors) {
                if (key.endsWith('Value'))
                    valueErrorElement.text(error.errors[key]);
                if (key.endsWith('Slug'))
                    slugErrorElement.text(error.errors[key]);
                if (key.endsWith('Display'))
                    displayErrorElement.text(error.errors[key]);
                if (key.endsWith('Breadcrumb'))
                    breadcrumbErrorElement.text(error.errors[key]);
                if (key.endsWith('HexCode'))
                    hexCodeErrorElement.text(error.errors[key]);
                if (key.endsWith('ColourFamily'))
                    colourFamilyErrorElement.text(error.errors[key]);
                if (key.endsWith('ColourFamilyHexCode'))
                    colourFamilyHexCodeErrorElement.text(error.errors[key]);
                if (key.endsWith('SortOrder'))
                    sortOrderErrorElement.text(error.errors[key]);
            }
        }
    } catch (err) {
        alert('Error occured while saving, please try again.');
    }
}
//Add end.

//Edit start.
function edit_ValueChange(event) {
    const result = valueChanged($(event.target).val());

    $('#edit_Slug').val(result.slug);
    $('#edit_Display').val(result.display);
    $('#edit_Breadcrumb').val(result.breadcrumb);
}

async function show_EditColourValueView(event) {
    //Register event handlers.
    $('#edit_ColourValueSave').on('click', edit_ColourValueSave);

    const productAttributeValueId = $(event.relatedTarget).data('value-id');
    try {
        const response = await doAjax('/api/ProductAttributeValues/' + productAttributeValueId, 'GET', null, true).promise();
        $('#edit_ColourValueId').val(response.id);
        $('#edit_Value').val(response.value).on('change', edit_ValueChange);
        $('#edit_Slug').val(response.slug);
        $('#edit_Display').val(response.display);
        $('#edit_Breadcrumb').val(response.breadcrumb);
        $('#edit_HexCode').val(response.hexCode);
        $('#edit_ColourFamily').val(response.colourFamily);
        $('#edit_ColourFamilyHexCode').val(response.colourFamilyHexCode);
        $('#edit_SortOrder').val(response.sortOrder);
    } catch (err) {
        alert('Error occured, please try again.')
    }
}

async function edit_ColourValueSave() {
    const valueElement = $('#edit_Value');
    const slugElement = $('#edit_Slug');
    const displayElement = $('#edit_Display');
    const breadcrumbElement = $('#edit_Breadcrumb');
    const hexCodeElement = $('#edit_HexCode');
    const colourFamilyElement = $('#edit_ColourFamily');
    const colourFamilyHexCodeElement = $('#edit_ColourFamilyHexCode');
    const sortOrderElement = $('#edit_SortOrder');

    const valueErrorElement = $('#edit_ValueError');
    const slugErrorElement = $('#edit_SlugError');
    const displayErrorElement = $('#edit_DisplayError');
    const breadcrumbErrorElement = $('#edit_BreadcrumbError');
    const hexCodeErrorElement = $('#edit_HexCodeError');
    const colourFamilyErrorElement = $('#edit_ColourFamilyError');
    const colourFamilyHexCodeErrorElement = $('#edit_ColourFamilyHexCodeError');
    const sortOrderErrorElement = $('#edit_SortOrderError');

    let isValid = validateString(valueElement[0], valueErrorElement[0], 256);
    isValid = isValid && validateString(slugElement[0], slugErrorElement[0], 256);
    isValid = isValid && validateSlug(slugElement[0], slugErrorElement[0]);
    isValid = isValid && validateString(displayElement[0], displayErrorElement[0], 256);
    isValid = isValid && validateString(breadcrumbElement[0], breadcrumbErrorElement[0], 256);
    isValid = isValid && validateOptionalString(hexCodeElement[0], hexCodeErrorElement[0], 8);
    isValid = isValid && validateString(colourFamilyElement[0], colourFamilyErrorElement[0], 128);
    isValid = isValid && validateOptionalString(colourFamilyHexCodeElement[0], colourFamilyHexCodeErrorElement[0], 8);
    isValid = isValid && validateDecimal(sortOrderElement[0], sortOrderErrorElement[0]);
    
    if (!isValid) return;

    const data = new FormData();
    data.append('__RequestVerificationToken', $("[name='__RequestVerificationToken']").val());
    data.append('Id', $('#edit_ColourValueId').val());
    data.append('ProductAttributeID', $("#Id").val());
    data.append('Value', valueElement.val());
    data.append('Slug', slugElement.val());
    data.append('Display', displayElement.val());
    data.append('Breadcrumb', breadcrumbElement.val());
    data.append('HexCode', hexCodeElement.val());
    data.append('ColourFamily', colourFamilyElement.val());
    data.append('ColourFamilyHexCode', colourFamilyHexCodeElement.val());
    data.append('SortOrder', sortOrderElement.val());

    try {
        const result = await fetch('/ProductAttributeColourValues/Edit/', {
            method: 'POST',
            body: data,
            credentials: 'same-origin'
        });
        if (result.ok) {
            const response = await result.text();
            $('#valuesTableBody').replaceWith(response);
            $('#edit_ColourValueModal').modal('hide');
        } else {
            const error = await result.json();
            for (const key in error.errors) {
                if (key.endsWith('Value'))
                    valueErrorElement.text(error.errors[key]);
                if (key.endsWith('Slug'))
                    slugErrorElement.text(error.errors[key]);
                if (key.endsWith('Display'))
                    displayErrorElement.text(error.errors[key]);
                if (key.endsWith('Breadcrumb'))
                    breadcrumbErrorElement.text(error.errors[key]);
                if (key.endsWith('HexCode'))
                    hexCodeErrorElement.text(error.errors[key]);
                if (key.endsWith('ColourFamily'))
                    colourFamilyErrorElement.text(error.errors[key]);
                if (key.endsWith('ColourFamilyHexCode'))
                    colourFamilyHexCodeErrorElement.text(error.errors[key]);
                if (key.endsWith('SortOrder'))
                    sortOrderErrorElement.text(error.errors[key]);
            }
        }
    } catch (err) {
        alert('Error occured while saving, please try again.');
    }
}

function hide_EditColourValueView() {
    //deregister event handlers.
    $('#edit_ColourValueSave').off('click', edit_ColourValueSave);

    //clear input elements and set to default values.
    $('#edit_ColourValueId').val('');
    $('#edit_Value').val('').off('change', edit_ValueChange);
    $('#edit_Slug').val('');
    $('#edit_Display').val('');
    $('#edit_Breadcrumb').val('');
    $('#edit_HexCode').val('');
    $('#edit_ColourFamily').val('');
    $('#edit_ColourFamilyHexCode').val('');
    $('#edit_SortOrder').val('');

    //reset errors.
    $('#edit_ValueError').text('')
    $('#edit_SlugError').text('')
    $('#edit_DisplayError').text('')
    $('#edit_BreadcrumbError').text('')
    $('#edit_HexCodeError').text('')
    $('#edit_ColourFamilyError').text('')
    $('#edit_ColourFamilyHexCodeError').text('')
    $('#edit_SortOrderError').text('')
}
//Edit end.

//Delete start.
function show_DeleteColourValueView(event) {
    $('#delete_Id').val($(event.relatedTarget).data('value-id'));
    $('#delete_Value').text($(event.relatedTarget).data('value'))
    $('#delete_ColourValue').on('click', delete_ColourValue);
}

function hide_DeleteColourValueView() {
    $('#delete_Id').val('');
    $('#delete_Value').text('');
    $('#delete_ColourValue').off('click', delete_ColourValue);
}

async function delete_ColourValue() {
    const data = new FormData();
    data.append('__RequestVerificationToken', $("[name='__RequestVerificationToken']").val());
    data.append('Id', $('#delete_Id').val());
    data.append('ProductAttributeID', $("#Id").val());

    try {
        const result = await fetch('/ProductAttributeColourValues/Delete', {
            method: 'POST',
            body: data,
            credentials: 'same-origin'
        });
        if (result.ok) {
            const response = await result.text();
            $('#valuesTableBody').replaceWith(response);
            $('#delete_ColourValueModal').modal('hide');
        } else {
            const error = await result.json();
            console.log(error);
        }
    } catch (err) {
        alert('Error occured while deleting value, please try again.');
    }
}
//Delete end.