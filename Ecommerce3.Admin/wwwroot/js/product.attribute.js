$(document).ready(() => {
    // const delete_ValueModal = $('#delete_ValueModal');
    // delete_ValueModal.on('show.bs.modal', show_DeleteValueView);
    // delete_ValueModal.on('hidden.bs.modal', hide_DeleteValueView);
    // $('#delete_Value').on('click', delete_ValueClicked);
    //Product attribute value.

    //Product attribute boolean value
    // const editProductAttributeBooleanValueModal = $('#editProductAttributeBooleanValueModal');
    // editProductAttributeBooleanValueModal.on('show.bs.modal', show_EditProductAttributeBooleanValueView);
    // editProductAttributeBooleanValueModal.on('hidden.bs.modal', hide_EditProductAttributeBooleanValueView);
    //
    // $('#editProductAttributeBooleanValue_Save').on('click', editProductAttributeBooleanValue_SaveClicked)

    //Product attribute colour value
    // const addProductAttributeColourValueModal = $('#addProductAttributeColourValueModal');
    // addProductAttributeColourValueModal.on('hidden.bs.modal', hide_AddProductAttributeColourValueView);
    //
    // const editProductAttributeColourValueModal = $('#editProductAttributeColourValueModal');
    // editProductAttributeColourValueModal.on('show.bs.modal', show_EditProductAttributeColourValueView);
    // editProductAttributeColourValueModal.on('hidden.bs.modal', hide_EditProductAttributeColourValueView);
    //
    // const deleteProductAttributeColourValueModal = $('#deleteProductAttributeColourValueModal');
    // deleteProductAttributeColourValueModal.on('show.bs.modal', show_DeleteProductAttributeColourValueView);
    // deleteProductAttributeColourValueModal.on('hidden.bs.modal', hide_DeleteProductAttributeColourValueView);
    //
    // $('#addProductAttributeColourValue_Save').on('click', addProductAttributeColourValue_SaveClicked)
    // $('#editProductAttributeColourValue_Save').on('click', editProductAttributeColourValue_SaveClicked)
    // $('#deleteProductAttributeColourValue').on('click', deleteProductAttributeColourValueClicked)

    //Product attribute date only value
    // const addProductAttributeDateOnlyValueModal = $('#addProductAttributeDateOnlyValueModal');
    // addProductAttributeDateOnlyValueModal.on('hidden.bs.modal', hide_AddProductAttributeDateOnlyValueView);
    //
    // const editProductAttributeDateOnlyValueModal = $('#editProductAttributeDateOnlyValueModal');
    // editProductAttributeDateOnlyValueModal.on('show.bs.modal', show_EditProductAttributeDateOnlyValueView);
    // editProductAttributeDateOnlyValueModal.on('hidden.bs.modal', hide_EditProductAttributeDateOnlyValueView);
    //
    // const deleteProductAttributeDateOnlyValueModal = $('#deleteProductAttributeDateOnlyValueModal');
    // deleteProductAttributeDateOnlyValueModal.on('show.bs.modal', show_DeleteProductAttributeDateOnlyValueView);
    // deleteProductAttributeDateOnlyValueModal.on('hidden.bs.modal', hide_DeleteProductAttributeDateOnlyValueView);
    //
    // $('#addProductAttributeDateOnlyValue_Save').on('click', addProductAttributeDateOnlyValue_SaveClicked)
    // $('#editProductAttributeDateOnlyValue_Save').on('click', editProductAttributeDateOnlyValue_SaveClicked)
    // $('#deleteProductAttributeDateOnlyValue').on('click', deleteProductAttributeDateOnlyValueClicked)

    //Product attribute decimal value
    // const addProductAttributeDecimalValueModal = $('#addProductAttributeDecimalValueModal');
    // addProductAttributeDecimalValueModal.on('hidden.bs.modal', hide_AddProductAttributeDecimalValueView);
    //
    // const editProductAttributeDecimalValueModal = $('#editProductAttributeDecimalValueModal');
    // editProductAttributeDecimalValueModal.on('show.bs.modal', show_EditProductAttributeDecimalValueView);
    // editProductAttributeDecimalValueModal.on('hidden.bs.modal', hide_EditProductAttributeDecimalValueView);
    //
    // const deleteProductAttributeDecimalValueModal = $('#deleteProductAttributeDecimalValueModal');
    // deleteProductAttributeDecimalValueModal.on('show.bs.modal', show_DeleteProductAttributeDecimalValueView);
    // deleteProductAttributeDecimalValueModal.on('hidden.bs.modal', hide_DeleteProductAttributeDecimalValueView);
    //
    // $('#addProductAttributeDecimalValue_Save').on('click', addProductAttributeDecimalValue_SaveClicked)
    // $('#editProductAttributeDecimalValue_Save').on('click', editProductAttributeDecimalValue_SaveClicked)
    // $('#deleteProductAttributeDecimalValue').on('click', deleteProductAttributeDecimalValueClicked)
});

function valueChanged(value) {
    return {slug: toSlug(value), display: value, breadcrumb: value};
}

async function show_EditProductAttributeBooleanValueView(event) {
    const productAttributeValueId = $(event.relatedTarget).data('value-id');

    try {
        //fetch product attribute details details.
        const response = await doAjax('/api/productattribute/' + productAttributeValueId, 'GET', null, true).promise();

        //populate image details.
        $('#edit_BooleanValueId').val(response.id);
        $('#edit_BooleanValue').val(response.value);
        $('#edit_BooleanSlug').val(response.slug);
        $('#edit_BooleanDisplay').val(response.display);
        $('#edit_BooleanBreadcrumb').val(response.breadcrumb);
        $('#edit_BooleanSortOrder').val(response.sortOrder);
        $('#edit_Boolean').val(response.booleanValue);
    } catch (err) {
        alert('Error occured, please try again.')
    }
}

function hide_EditProductAttributeBooleanValueView() {
    //reset validation errors.
    $('#edit_BooleanValueError').text('')
    $('#edit_BooleanSlugError').text('')
    $('#edit_BooleanDisplayError').text('')
    $('#edit_BooleanBreadcrumbError').text('')
    $('#edit_BooleanSortOrderError').text('')
    $('#edit_Boolean').text('')

    //reset input elements and set to default values.
    $('#edit_BooleanValue').val('');
    $('#edit_BooleanSlug').val('')
    $('#edit_BooleanDisplay').val('')
    $('#edit_BooleanBreadcrumb').val('')
    $('#edit_BooleanSortOrder').val('')
    $('#edit_Boolean').val('')
}

async function editProductAttributeBooleanValue_SaveClicked(event) {

    const value = $('#edit_DateOnlyValue');
    const slug = $('#edit_DateOnlySlug');
    const display = $('#edit_DateOnlyDisplay');
    const breadcrumb = $('#edit_DateOnlyBreadcrumb');
    const sortOrder = $('#edit_DateOnlySortOrder');
    const boolean = $('#edit_Boolean');
    const valueError = $('#edit_DateOnlyValueError');
    const slugError = $('#edit_DateOnlySlugError');
    const displayError = $('#edit_DateOnlyDisplayError');
    const breadcrumbError = $('#edit_DateOnlyBreadcrumbError');
    const sortOrderError = $('#edit_DateOnlySortOrderError');
    const booleanError = $('#edit_BooleanError');

    if (!booleanValidate(value, slug, display, breadcrumb, sortOrder, boolean,
        valueError, slugError, displayError, breadcrumbError, sortOrderError, booleanError))
        return;

    const data = new FormData();
    data.append('__RequestVerificationToken', $("[name='__RequestVerificationToken']").val());
    data.append('Id', $('#edit_DateOnlyValueId').val());
    data.append('ProductAttributeID', $("#productAttributeId").val());
    data.append('Value', value.val());
    data.append('Slug', slug.val());
    data.append('Display', display.val());
    data.append('Breadcrumb', breadcrumb.val());
    data.append('SortOrder', sortOrder.val());
    data.append('BooleanValue', boolean.val());

    try {
        const result = await fetch('/ProductAttributes/AddBooleanValue', {
            method: 'POST',
            body: data,
            credentials: 'same-origin'
        });
        if (result.ok) {
            const response = await result.text();
            $('#valuesTableBody').replaceWith(response);
            $('#editProductAttributeBooleanValueModal').modal('hide');
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
                if (key.endsWith('Boolean'))
                    booleanError.text(error.errors[key]);
            }
        }
    } catch (err) {
        alert('Error occured while saving image, please try again.');
    }
}


function hide_AddProductAttributeColourValueView() {
    //reset validation errors.
    $('#add_ColourValueError').text('')
    $('#add_ColourSlugError').text('')
    $('#add_ColourDisplayError').text('')
    $('#add_ColourBreadcrumbError').text('')
    $('#add_ColourSortOrderError').text('')
    $('#add_ColourHexCodeError').text('')
    $('#add_ColourFamilyError').text('')
    $('#add_ColourFamilyHexCodeError').text('')

    //reset input elements and set to default values.
    $('#add_ColourValue').val('');
    $('#add_ColourSlug').val('')
    $('#add_ColourDisplay').val('')
    $('#add_ColourBreadcrumb').val('')
    $('#add_ColourSortOrder').val('')
    $('#add_ColourHexCode').val('')
    $('#add_ColourFamily').val('')
    $('#add_ColourFamilyHexCode').val('')
}

async function show_EditProductAttributeColourValueView(event) {
    const productAttributeValueId = $(event.relatedTarget).data('value-id');

    try {
        //fetch product attribute details details.
        const response = await doAjax('/api/productattribute/' + productAttributeValueId, 'GET', null, true).promise();

        //populate image details.
        $('#edit_ColourValueId').val(response.id);
        $('#edit_ColourValue').val(response.value);
        $('#edit_ColourSlug').val(response.slug);
        $('#edit_ColourDisplay').val(response.display);
        $('#edit_ColourBreadcrumb').val(response.breadcrumb);
        $('#edit_ColourSortOrder').val(response.sortOrder);
        $('#edit_ColourHexCode').val(response.hexCode);
        $('#edit_ColourFamily').val(response.colourFamily);
        $('#edit_ColourFamilyHexCode').val(response.colourFamilyHexCode);
    } catch (err) {
        alert('Error occured, please try again.')
    }
}

function hide_EditProductAttributeColourValueView() {
    //reset validation errors.
    $('#edit_ColourValueError').text('')
    $('#edit_ColourSlugError').text('')
    $('#edit_ColourDisplayError').text('')
    $('#edit_ColourBreadcrumbError').text('')
    $('#edit_ColourSortOrderError').text('')
    $('#edit_ColourHexCodeError').text('')
    $('#edit_ColourFamilyError').text('')
    $('#edit_ColourFamilyHexCodeError').text('')

    //reset input elements and set to default values.
    $('#edit_ColourValue').val('');
    $('#edit_ColourSlug').val('')
    $('#edit_ColourDisplay').val('')
    $('#edit_ColourBreadcrumb').val('')
    $('#edit_ColourSortOrder').val('')
    $('#edit_ColourHexCode').val('')
    $('#edit_ColourFamily').val('')
    $('#edit_ColourFamilyHexCode').val('')
}

async function show_DeleteProductAttributeColourValueView(event) {
    const valueId = $(event.relatedTarget).data('value-id');
    const value = $(event.relatedTarget).data('value');

    //populate image details.
    $('#delete_Id').val(valueId);
    $('#delete_ColourValue').text(value);
}

function hide_DeleteProductAttributeColourValueView() {
    //reset input elements and set to default values.
    $('#delete_Id').val('');
    $('#delete_ColourValue').text('');
}

async function addProductAttributeColourValue_SaveClicked(event) {

    const discriminator = $('#add_ColourDiscriminator')
    const value = $('#add_ColourValue');
    const slug = $('#add_ColourSlug');
    const display = $('#add_ColourDisplay');
    const breadcrumb = $('#add_ColourBreadcrumb');
    const sortOrder = $('#add_ColourSortOrder');
    const hexCode = $('#add_ColourHexCode');
    const colourFamily = $('#add_ColourFamily');
    const colourFamilyHexCode = $('#add_ColourFamilyHexCode');
    const valueError = $('#add_ColourValueError');
    const slugError = $('#add_ColourSlugError');
    const displayError = $('#add_ColourDisplayError');
    const breadcrumbError = $('#add_ColourBreadcrumbError');
    const sortOrderError = $('#add_ColourSortOrderError');
    const hexCodeError = $('#add_ColourHexCodeError');
    const colourFamilyError = $('#add_ColourFamilyError');
    const colourFamilyHexCodeError = $('#add_ColourFamilyHexCodeError');

    if (!colourValidate(value, slug, display, breadcrumb, sortOrder, hexCode, colourFamily, colourFamilyHexCode,
        valueError, slugError, displayError, breadcrumbError, sortOrderError, hexCodeError, colourFamilyError, colourFamilyHexCodeError))
        return;

    const data = new FormData();
    data.append('__RequestVerificationToken', $("[name='__RequestVerificationToken']").val());
    data.append('ProductAttributeID', $("#productAttributeId").val());
    data.append('Discriminator', discriminator.val());
    data.append('Value', value.val());
    data.append('Slug', slug.val());
    data.append('Display', display.val());
    data.append('Breadcrumb', breadcrumb.val());
    data.append('SortOrder', sortOrder.val());
    data.append('HexCode', hexCode.val());
    data.append('ColourFamily', colourFamily.val());
    data.append('ColourFamilyHexCode', colourFamilyHexCode.val());

    try {
        const result = await fetch('/ProductAttributes/AddColourValue', {
            method: 'POST',
            body: data,
            credentials: 'same-origin'
        });
        if (result.ok) {
            const response = await result.text();
            $('#valuesTableBody').replaceWith(response);
            $('#addProductAttributeColourValueModal').modal('hide');
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
                if (key.endsWith('HexCode'))
                    hexCodeError.text(error.errors[key]);
                if (key.endsWith('ColourFamily'))
                    colourFamilyError.text(error.errors[key]);
                if (key.endsWith('ColourFamilyHexCode'))
                    colourFamilyHexCodeError.text(error.errors[key]);
            }
        }
    } catch (err) {
        alert('Error occured while saving image, please try again.');
    }
}

async function editProductAttributeColourValue_SaveClicked(event) {

    const value = $('#edit_ColourValue');
    const slug = $('#edit_ColourSlug');
    const display = $('#edit_ColourDisplay');
    const breadcrumb = $('#edit_ColourBreadcrumb');
    const sortOrder = $('#edit_ColourSortOrder');
    const hexCode = $('#edit_ColourHexCode');
    const colourFamily = $('#edit_ColourFamily');
    const colourFamilyHexCode = $('#edit_ColourFamilyHexCode');
    const valueError = $('#edit_ColourValueError');
    const slugError = $('#edit_ColourSlugError');
    const displayError = $('#edit_ColourDisplayError');
    const breadcrumbError = $('#edit_ColourBreadcrumbError');
    const sortOrderError = $('#edit_ColourSortOrderError');
    const hexCodeError = $('#edit_ColourHexCodeError');
    const colourFamilyError = $('#edit_ColourFamilyError');
    const colourFamilyHexCodeError = $('#edit_ColourFamilyHexCodeError');

    if (!colourValidate(value, slug, display, breadcrumb, sortOrder, hexCode, colourFamily, colourFamilyHexCode,
        valueError, slugError, displayError, breadcrumbError, sortOrderError, hexCodeError, colourFamilyError, colourFamilyHexCodeError))
        return;

    const data = new FormData();
    data.append('__RequestVerificationToken', $("[name='__RequestVerificationToken']").val());
    data.append('Id', $('#edit_ColourValueId').val());
    data.append('ProductAttributeID', $("#productAttributeId").val());
    data.append('Value', value.val());
    data.append('Slug', slug.val());
    data.append('Display', display.val());
    data.append('Breadcrumb', breadcrumb.val());
    data.append('SortOrder', sortOrder.val());
    data.append('HexCode', hexCode.val());
    data.append('ColourFamily', colourFamily.val());
    data.append('ColourFamilyHexCode', colourFamilyHexCode.val());

    try {
        const result = await fetch('/ProductAttributes/EditColourValue', {
            method: 'POST',
            body: data,
            credentials: 'same-origin'
        });
        if (result.ok) {
            const response = await result.text();
            $('#valuesTableBody').replaceWith(response);
            $('#editProductAttributeColourValueModal').modal('hide');
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
                if (key.endsWith('HexCode'))
                    hexCodeError.text(error.errors[key]);
                if (key.endsWith('ColourFamily'))
                    colourFamilyError.text(error.errors[key]);
                if (key.endsWith('ColourFamilyHexCode'))
                    colourFamilyHexCodeError.text(error.errors[key]);
            }
        }
    } catch (err) {
        alert('Error occured while saving image, please try again.');
    }
}

async function deleteProductAttributeColourValueClicked(event) {
    const data = new FormData();
    data.append('__RequestVerificationToken', $("[name='__RequestVerificationToken']").val());
    data.append('Id', $('#delete_Id').val());
    data.append('ProductAttributeID', $("#productAttributeId").val());

    try {
        const result = await fetch('/ProductAttributes/DeleteColourValue', {
            method: 'POST',
            body: data,
            credentials: 'same-origin'
        });
        if (result.ok) {
            const response = await result.text();
            $('#valuesTableBody').replaceWith(response);
            $('#deleteProductAttributeColourValueModal').modal('hide');
        } else {
            const error = await result.json();
            console.log(error);
        }
    } catch (err) {
        alert('Error occured while deleting colour value, please try again.');
    }
}


function hide_AddProductAttributeDateOnlyValueView() {
    //reset validation errors.
    $('#add_DateOnlyValueError').text('')
    $('#add_DateOnlySlugError').text('')
    $('#add_DateOnlyDisplayError').text('')
    $('#add_DateOnlyBreadcrumbError').text('')
    $('#add_DateOnlySortOrderError').text('')
    $('#add_DateOnlyError').text('')

    //reset input elements and set to default values.
    $('#add_DateOnlyValue').val('');
    $('#add_DateOnlySlug').val('')
    $('#add_DateOnlyDisplay').val('')
    $('#add_DateOnlyBreadcrumb').val('')
    $('#add_DateOnlySortOrder').val('')
    $('#add_DateOnly').val('')
}

async function show_EditProductAttributeDateOnlyValueView(event) {
    const productAttributeValueId = $(event.relatedTarget).data('value-id');

    try {
        //fetch product attribute details details.
        const response = await doAjax('/api/productattribute/' + productAttributeValueId, 'GET', null, true).promise();

        //populate image details.
        $('#edit_DateOnlyValueId').val(response.id);
        $('#edit_DateOnlyValue').val(response.value);
        $('#edit_DateOnlySlug').val(response.slug);
        $('#edit_DateOnlyDisplay').val(response.display);
        $('#edit_DateOnlyBreadcrumb').val(response.breadcrumb);
        $('#edit_DateOnlySortOrder').val(response.sortOrder);
        $('#edit_DateOnly').val(response.dateOnlyValue);
    } catch (err) {
        alert('Error occured, please try again.')
    }
}

function hide_EditProductAttributeDateOnlyValueView() {
    //reset validation errors.
    $('#edit_DateOnlyValueError').text('')
    $('#edit_DateOnlySlugError').text('')
    $('#edit_DateOnlyDisplayError').text('')
    $('#edit_DateOnlyBreadcrumbError').text('')
    $('#edit_DateOnlySortOrderError').text('')
    $('#edit_DateOnlyError').text('')

    //reset input elements and set to default values.
    $('#edit_DateOnlyValue').val('');
    $('#edit_DateOnlySlug').val('')
    $('#edit_DateOnlyDisplay').val('')
    $('#edit_DateOnlyBreadcrumb').val('')
    $('#edit_DateOnlySortOrder').val('')
    $('#edit_DateOnly').val('')
}

async function show_DeleteProductAttributeDateOnlyValueView(event) {
    const valueId = $(event.relatedTarget).data('value-id');
    const value = $(event.relatedTarget).data('value');

    //populate image details.
    $('#delete_Id').val(valueId);
    $('#delete_DateOnlyValue').text(value);
}

function hide_DeleteProductAttributeDateOnlyValueView() {
    //reset input elements and set to default values.
    $('#delete_Id').val('');
    $('#delete_DateOnlyValue').text('');
}

async function addProductAttributeDateOnlyValue_SaveClicked(event) {

    const discriminator = $('#add_DateOnlyDiscriminator');
    const value = $('#add_DateOnlyValue');
    const slug = $('#add_DateOnlySlug');
    const display = $('#add_DateOnlyDisplay');
    const breadcrumb = $('#add_DateOnlyBreadcrumb');
    const sortOrder = $('#add_DateOnlySortOrder');
    const dateOnly = $('#add_DateOnly');
    const valueError = $('#add_DateOnlyValueError');
    const slugError = $('#add_DateOnlySlugError');
    const displayError = $('#add_DateOnlyDisplayError');
    const breadcrumbError = $('#add_DateOnlyBreadcrumbError');
    const sortOrderError = $('#add_DateOnlySortOrderError');
    const dateOnlyError = $('#add_DateOnlyError');

    if (!dateOnlyValidate(value, slug, display, breadcrumb, sortOrder, dateOnly,
        valueError, slugError, displayError, breadcrumbError, sortOrderError, dateOnlyError))
        return;

    const data = new FormData();
    data.append('__RequestVerificationToken', $("[name='__RequestVerificationToken']").val());
    data.append('ProductAttributeID', $("#productAttributeId").val());
    data.append('Discriminator', discriminator.val());
    data.append('Value', value.val());
    data.append('Slug', slug.val());
    data.append('Display', display.val());
    data.append('Breadcrumb', breadcrumb.val());
    data.append('SortOrder', sortOrder.val());
    data.append('DateOnlyValue', dateOnly.val());

    try {
        const result = await fetch('/ProductAttributes/EditDateOnlyValue', {
            method: 'POST',
            body: data,
            credentials: 'same-origin'
        });
        if (result.ok) {
            const response = await result.text();
            $('#valuesTableBody').replaceWith(response);
            $('#editProductAttributeDateOnlyValueModal').modal('hide');
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
                if (key.endsWith('DateOnly'))
                    dateOnlyError.text(error.errors[key]);
            }
        }
    } catch (err) {
        alert('Error occured while saving image, please try again.');
    }
}

async function editProductAttributeDateOnlyValue_SaveClicked(event) {

    const value = $('#edit_DateOnlyValue');
    const slug = $('#edit_DateOnlySlug');
    const display = $('#edit_DateOnlyDisplay');
    const breadcrumb = $('#edit_DateOnlyBreadcrumb');
    const sortOrder = $('#edit_DateOnlySortOrder');
    const dateOnly = $('#edit_DateOnly');
    const valueError = $('#edit_DateOnlyValueError');
    const slugError = $('#edit_DateOnlySlugError');
    const displayError = $('#edit_DateOnlyDisplayError');
    const breadcrumbError = $('#edit_DateOnlyBreadcrumbError');
    const sortOrderError = $('#edit_DateOnlySortOrderError');
    const dateOnlyError = $('#edit_DateOnlyError');

    if (!dateOnlyValidate(value, slug, display, breadcrumb, sortOrder, dateOnly,
        valueError, slugError, displayError, breadcrumbError, sortOrderError, dateOnlyError))
        return;

    const data = new FormData();
    data.append('__RequestVerificationToken', $("[name='__RequestVerificationToken']").val());
    data.append('Id', $('#edit_DateOnlyValueId').val());
    data.append('ProductAttributeID', $("#productAttributeId").val());
    data.append('Value', value.val());
    data.append('Slug', slug.val());
    data.append('Display', display.val());
    data.append('Breadcrumb', breadcrumb.val());
    data.append('SortOrder', sortOrder.val());
    data.append('DateOnlyValue', dateOnly.val());

    try {
        const result = await fetch('/ProductAttributes/AddDateOnlyValue', {
            method: 'POST',
            body: data,
            credentials: 'same-origin'
        });
        if (result.ok) {
            const response = await result.text();
            $('#valuesTableBody').replaceWith(response);
            $('#addProductAttributeDateOnlyValueModal').modal('hide');
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
                if (key.endsWith('DateOnly'))
                    dateOnlyError.text(error.errors[key]);
            }
        }
    } catch (err) {
        alert('Error occured while saving image, please try again.');
    }
}

async function deleteProductAttributeDateOnlyValueClicked(event) {
    const data = new FormData();
    data.append('__RequestVerificationToken', $("[name='__RequestVerificationToken']").val());
    data.append('Id', $('#delete_Id').val());
    data.append('ProductAttributeID', $("#productAttributeId").val());

    try {
        const result = await fetch('/ProductAttributes/DeleteDateOnlyValue', {
            method: 'POST',
            body: data,
            credentials: 'same-origin'
        });
        if (result.ok) {
            const response = await result.text();
            $('#valuesTableBody').replaceWith(response);
            $('#deleteProductAttributeDateOnlyValueModal').modal('hide');
        } else {
            const error = await result.json();
            console.log(error);
        }
    } catch (err) {
        alert('Error occured while deleting dateonly value, please try again.');
    }
}


function hide_AddProductAttributeDecimalValueView() {
    //reset validation errors.
    $('#add_DecimalValueError').text('')
    $('#add_DecimalSlugError').text('')
    $('#add_DecimalDisplayError').text('')
    $('#add_DecimalBreadcrumbError').text('')
    $('#add_DecimalSortOrderError').text('')
    $('#add_DecimalError').text('')

    //reset input elements and set to default values.
    $('#add_DecimalValue').val('');
    $('#add_DecimalSlug').val('')
    $('#add_DecimalDisplay').val('')
    $('#add_DecimalBreadcrumb').val('')
    $('#add_DecimalSortOrder').val('')
    $('#add_Decimal').val('')
}

async function show_EditProductAttributeDecimalValueView(event) {
    const productAttributeValueId = $(event.relatedTarget).data('value-id');

    try {
        //fetch product attribute details details.
        const response = await doAjax('/api/productattribute/' + productAttributeValueId, 'GET', null, true).promise();

        //populate image details.
        $('#edit_DecimalValueId').val(response.id);
        $('#edit_DecimalValue').val(response.value);
        $('#edit_DecimalSlug').val(response.slug);
        $('#edit_DecimalDisplay').val(response.display);
        $('#edit_DecimalBreadcrumb').val(response.breadcrumb);
        $('#edit_DecimalSortOrder').val(response.sortOrder);
        $('#edit_Decimal').val(response.decimalValue);
    } catch (err) {
        alert('Error occured, please try again.')
    }
}

function hide_EditProductAttributeDecimalValueView() {
    //reset validation errors.
    $('#edit_DecimalValueError').text('')
    $('#edit_DecimalSlugError').text('')
    $('#edit_DecimalDisplayError').text('')
    $('#edit_DecimalBreadcrumbError').text('')
    $('#edit_DecimalSortOrderError').text('')
    $('#edit_DecimalError').text('')

    //reset input elements and set to default values.
    $('#edit_DecimalValue').val('');
    $('#edit_DecimalSlug').val('')
    $('#edit_DecimalDisplay').val('')
    $('#edit_DecimalBreadcrumb').val('')
    $('#edit_DecimalSortOrder').val('')
    $('#edit_Decimal').val('')
}

async function show_DeleteProductAttributeDecimalValueView(event) {
    const valueId = $(event.relatedTarget).data('value-id');
    const value = $(event.relatedTarget).data('value');

    //populate image details.
    $('#delete_Id').val(valueId);
    $('#delete_DecimalValue').text(value);
}

function hide_DeleteProductAttributeDecimalValueView() {
    //reset input elements and set to default values.
    $('#delete_Id').val('');
    $('#delete_DecimalValue').text('');
}

async function addProductAttributeDecimalValue_SaveClicked(event) {

    const discriminator = $('#add_DecimalDiscriminator');
    const value = $('#add_DecimalValue');
    const slug = $('#add_DecimalSlug');
    const display = $('#add_DecimalDisplay');
    const breadcrumb = $('#add_DecimalBreadcrumb');
    const sortOrder = $('#add_DecimalSortOrder');
    const Decimal = $('#add_Decimal');
    const valueError = $('#add_DecimalValueError');
    const slugError = $('#add_DecimalSlugError');
    const displayError = $('#add_DecimalDisplayError');
    const breadcrumbError = $('#add_DecimalBreadcrumbError');
    const sortOrderError = $('#add_DecimalSortOrderError');
    const decimalError = $('#add_DecimalError');

    if (!decimalValidate(value, slug, display, breadcrumb, sortOrder, Decimal,
        valueError, slugError, displayError, breadcrumbError, sortOrderError, decimalError))
        return;

    const data = new FormData();
    data.append('__RequestVerificationToken', $("[name='__RequestVerificationToken']").val());
    data.append('ProductAttributeID', $("#productAttributeId").val());
    data.append('Discriminator', discriminator.val());
    data.append('Value', value.val());
    data.append('Slug', slug.val());
    data.append('Display', display.val());
    data.append('Breadcrumb', breadcrumb.val());
    data.append('SortOrder', sortOrder.val());
    data.append('DecimalValue', decimal.val());

    try {
        const result = await fetch('/ProductAttributes/AddDecimalValue', {
            method: 'POST',
            body: data,
            credentials: 'same-origin'
        });
        if (result.ok) {
            const response = await result.text();
            $('#valuesTableBody').replaceWith(response);
            $('#addProductAttributeDecimalValueModal').modal('hide');
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
                if (key.endsWith('Decimal'))
                    decimalError.text(error.errors[key]);
            }
        }
    } catch (err) {
        alert('Error occured while saving image, please try again.');
    }
}

async function editProductAttributeDecimalValue_SaveClicked(event) {

    const value = $('#edit_DecimalValue');
    const slug = $('#edit_DecimalSlug');
    const display = $('#edit_DecimalDisplay');
    const breadcrumb = $('#edit_DecimalBreadcrumb');
    const sortOrder = $('#edit_DecimalSortOrder');
    const decimal = $('#edit_Decimal');
    const valueError = $('#edit_DecimalValueError');
    const slugError = $('#edit_DecimalSlugError');
    const displayError = $('#edit_DecimalDisplayError');
    const breadcrumbError = $('#edit_DecimalBreadcrumbError');
    const sortOrderError = $('#edit_DecimalSortOrderError');
    const decimalError = $('#edit_DecimalError');

    if (!decimalValidate(value, slug, display, breadcrumb, sortOrder, decimal,
        valueError, slugError, displayError, breadcrumbError, sortOrderError, decimalError))
        return;

    const data = new FormData();
    data.append('__RequestVerificationToken', $("[name='__RequestVerificationToken']").val());
    data.append('Id', $('#edit_DecimalValueId').val());
    data.append('ProductAttributeID', $("#productAttributeId").val());
    data.append('Value', value.val());
    data.append('Slug', slug.val());
    data.append('Display', display.val());
    data.append('Breadcrumb', breadcrumb.val());
    data.append('SortOrder', sortOrder.val());
    data.append('DecimalValue', decimal.val());

    try {
        const result = await fetch('/ProductAttributes/EditDecimalValue', {
            method: 'POST',
            body: data,
            credentials: 'same-origin'
        });
        if (result.ok) {
            const response = await result.text();
            $('#valuesTableBody').replaceWith(response);
            $('#editProductAttributeDecimalValueModal').modal('hide');
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
                if (key.endsWith('Decimal'))
                    decimalError.text(error.errors[key]);
            }
        }
    } catch (err) {
        alert('Error occured while saving image, please try again.');
    }
}

async function deleteProductAttributeDecimalValueClicked(event) {
    const data = new FormData();
    data.append('__RequestVerificationToken', $("[name='__RequestVerificationToken']").val());
    data.append('Id', $('#delete_Id').val());
    data.append('ProductAttributeID', $("#productAttributeId").val());

    try {
        const result = await fetch('/ProductAttributes/DeleteDecimalValue', {
            method: 'POST',
            body: data,
            credentials: 'same-origin'
        });
        if (result.ok) {
            const response = await result.text();
            $('#valuesTableBody').replaceWith(response);
            $('#deleteProductAttributeDecimalValueModal').modal('hide');
        } else {
            const error = await result.json();
            console.log(error);
        }
    } catch (err) {
        alert('Error occured while deleting decimal value, please try again.');
    }
}


function colourValidate(value, slug, display, breadcrumb, sortOrder, hexCode, colourFamily, colourFamilyHexCode,
                        valueError, slugError, displayError, breadcrumbError, sortOrderError, hexCodeError, colourFamilyError, colourFamilyHexCodeError) {

    let isValid = true;

    // clear all errors for this type
    hexCodeError.text('');
    colourFamilyError.text('');
    colourFamilyHexCodeError.text('');

    // run common validation
    if (!validate(value, slug, display, breadcrumb, sortOrder,
        valueError, slugError, displayError, breadcrumbError, sortOrderError))
        isValid = false;

    // colour-specific checks
    if (hexCode.val().trim() === '') {
        hexCodeError.text('Hex code is required.');
        isValid = false;
    }

    if (colourFamily.val().trim() === '') {
        colourFamilyError.text('Colour family is required.');
        isValid = false;
    }

    if (colourFamilyHexCode.val().trim() === '') {
        colourFamilyHexCodeError.text('Colour family hex code is required.');
        isValid = false;
    }

    return isValid;
}

function dateOnlyValidate(value, slug, display, breadcrumb, sortOrder, dateOnly,
                          valueError, slugError, displayError, breadcrumbError, sortOrderError, dateOnlyError) {

    let isValid = true;

    // clear all errors for this type
    dateOnlyError.text('');

    // run common validation
    if (!validate(value, slug, display, breadcrumb, sortOrder,
        valueError, slugError, displayError, breadcrumbError, sortOrderError))
        isValid = false;

    // Date only specific check
    if (dateOnly.val().trim() === '') {
        dateOnlyError.text('Date only value is required.');
        isValid = false;
    }

    return isValid;
}

function decimalValidate(value, slug, display, breadcrumb, sortOrder, decimal,
                         valueError, slugError, displayError, breadcrumbError, sortOrderError, decimalError) {

    let isValid = true;

    // clear all errors for this type
    decimalError.text('');

    // run common validation
    if (!validate(value, slug, display, breadcrumb, sortOrder,
        valueError, slugError, displayError, breadcrumbError, sortOrderError))
        isValid = false;

    // Decimal specific check
    if (decimal.val().trim() === '') {
        decimalError.text('Decimal value is required.');
        isValid = false;
    }

    return isValid;
}

function booleanValidate(value, slug, display, breadcrumb, sortOrder, boolean,
                         valueError, slugError, displayError, breadcrumbError, sortOrderError, booleanError) {
    let isValid = true;

    // clear all errors for this type
    booleanError.text('');

    // run common validation
    if (!validate(value, slug, display, breadcrumb, sortOrder,
        valueError, slugError, displayError, breadcrumbError, sortOrderError))
        isValid = false;

    // Decimal specific check
    if (boolean.val().trim() === '') {
        booleanError.text('Boolean value is required.');
        isValid = false;
    }

    return isValid;
}