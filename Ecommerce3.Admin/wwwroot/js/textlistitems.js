$(document).ready(() => {
    $('[id$="-TextListItemType"]').each(function () {
        const type = $(this).val();

        const addModal = $(`#add${type}Modal`);
        addModal.on('show.bs.modal', () => show_AddTextItemView(type));
        addModal.on('hidden.bs.modal', () => hide_AddTextItemView(type));

        const editModal = $(`#edit${type}Modal`);
        editModal.on('show.bs.modal', (event) => show_EditTextItemView(event, type));
        editModal.on('hidden.bs.modal', () => hide_EditTextItemView(type));

        const deleteModal = $(`#delete${type}Modal`);
        deleteModal.on('show.bs.modal', (event) => show_DeleteTextItemView(event, type));
        deleteModal.on('hidden.bs.modal', () => hide_DeleteTextItemView(type));

        $(`#${type}-Add-Save`).on('click', () => add_TextItemsSaveClicked(type));
        $(`#${type}-Edit-Save`).on('click', () => edit_TextItemsSaveClicked(type));
        $(`#${type}-Delete`).on('click', () => textItemsDeleteClicked(type));
    });
});


function show_AddTextItemView(type) {
    // Clear previous errors
    $(`#${type}-AddTextError`).text('');
    $(`#${type}-AddSortOrderError`).text('');
}

function hide_AddTextItemView(type) {
    // Clear form fields
    $(`#${type}-AddText`).val('');
    $(`#${type}-AddSortOrder`).val('');
    $(`#${type}-AddTextError`).text('');
    $(`#${type}-AddSortOrderError`).text('');
}

async function show_EditTextItemView(event, type) {
    const textListItemId = $(event.relatedTarget).data('tli-id');
    try {
        const textListItem = await doAjax('/api/TextListItems/' + textListItemId, 'GET', null, true).promise();

        $(`#${type}-EditId`).val(textListItem.id);
        $(`#${type}-EditText`).val(textListItem.text);
        $(`#${type}-EditSortOrder`).val(textListItem.sortOrder);
    } catch (err) {
        alert('Error occurred, please try again.')
    }
}

function hide_EditTextItemView(type) {
    $(`#${type}-EditTextError`).text('');
    $(`#${type}-EditSortOrderError`).text('');
}

function show_DeleteTextItemView(event, type) {
    const textListItemId = $(event.relatedTarget).data('tli-id');
    const textListItemText = $(event.relatedTarget).data('tli-text');
    $(`#${type}-DeleteId`).val(textListItemId);
    $(`#${type}-DeleteText`).text(textListItemText);
}

function hide_DeleteTextItemView(type) {
    $(`#${type}-DeleteId`).val('');
    $(`#${type}-DeleteText`).text('');
}

async function add_TextItemsSaveClicked(type) {
    if (!add_TextListItemValidate(type)) return;

    const data = new FormData();
    data.append('__RequestVerificationToken', $("[name='__RequestVerificationToken']").val());
    data.append('ParentEntity', $(`#${type}-AddParentEntity`).val());
    data.append('ParentEntityId', $(`#${type}-AddParentEntityId`).val());
    data.append('Entity', $(`#${type}-AddEntity`).val());
    data.append('Type', type);
    data.append('Text', $(`#${type}-AddText`).val());
    data.append('SortOrder', $(`#${type}-AddSortOrder`).val());

    try {
        const result = await fetch('/TextListItems/Add', {method: 'POST', body: data, credentials: 'same-origin'});
        if (result.ok) {
            const response = await result.text();
            $(`#textlist${type}`).replaceWith(response);
            $(`#add${type}Modal`).modal('hide');
        } else {
            const error = await result.json();
            for (const key in error.errors) {
                if (key.endsWith('AddText'))
                    $(`#${type}-AddTextError`).text(error.errors[key][0]);

                if (key.endsWith('AddSortOrder'))
                    $(`#${type}-AddSortOrderError`).text(error.errors[key][0]);
            }
        }
    } catch (err) {
        alert('Error occurred while saving, please try again.');
    }
}

async function edit_TextItemsSaveClicked(type) {
    if (!edit_TextListItemValidate(type)) return;

    const data = new FormData();
    data.append('__RequestVerificationToken', $("[name='__RequestVerificationToken']").val());
    data.append('Id', $(`#${type}-EditId`).val());
    data.append('ParentEntity', $(`#${type}-EditParentEntity`).val());
    data.append('ParentEntityId', $(`#${type}-EditParentEntityId`).val());
    data.append('Type', type);
    data.append('Text', $(`#${type}-EditText`).val());
    data.append('SortOrder', $(`#${type}-EditSortOrder`).val());

    try {
        const result = await fetch('/TextListItems/Edit', {method: 'POST', body: data, credentials: 'same-origin'});
        if (result.ok) {
            const response = await result.text();
            $(`#textlist${type}`).replaceWith(response);
            $(`#edit${type}Modal`).modal('hide');
        } else {
            const error = await result.json();
            for (const key in error.errors) {
                if (key.endsWith('EditText'))
                    $(`#${type}-EditTextError`).text(error.errors[key][0]);

                if (key.endsWith('EditSortOrder'))
                    $(`#${type}-EditSortOrderError`).text(error.errors[key][0]);
            }
        }
    } catch (err) {
        alert('Error occurred while updating, please try again.');
    }
}

async function textItemsDeleteClicked(type) {
    const data = new FormData();
    data.append('__RequestVerificationToken', $("[name='__RequestVerificationToken']").val());
    data.append('Id', $(`#${type}-DeleteId`).val());
    data.append('ParentEntity', $(`#${type}-DeleteParentEntity`).val());
    data.append('ParentEntityId', $(`#${type}-DeleteParentEntityId`).val());
    data.append('Type', type);

    try {
        const result = await fetch('/TextListItems/Delete', {method: 'POST', body: data, credentials: 'same-origin'});
        if (result.ok) {
            const response = await result.text();
            $(`#textlist${type}`).replaceWith(response);
            $(`#delete${type}Modal`).modal('hide');
        } else {
            const error = await result.json();
            const errorMessage = error.message || error.errors?.[Object.keys(error.errors)[0]]?.[0] || 'Failed to delete item.';
            alert(errorMessage);
        }
    } catch (err) {
        alert('Error occured while deleting, please try again.');
    }
}

function add_TextListItemValidate(type) {
    let isValid = true;
    const text = $(`#${type}-AddText`);
    const sortOrder = $(`#${type}-AddSortOrder`);
    const textError = $(`#${type}-AddTextError`);
    const sortOrderError = $(`#${type}-AddSortOrderError`);

    //clear errors.
    textError.text('');
    sortOrderError.text('');

    //validate.
    if (text.val() === '') {
        textError.text('Text is required.');
        isValid = false;
    }

    //Sort order.
    if (sortOrder.val() === '') {
        sortOrderError.text('Sort order is required.');
        isValid = false;
    } else if (!isValidNumberStrict(sortOrder.val())) {
        sortOrderError.text('Please enter a valid sort order.');
        isValid = false;
    }

    return isValid;
}

function edit_TextListItemValidate(type) {
    let isValid = true;
    const text = $(`#${type}-EditText`);
    const sortOrder = $(`#${type}-EditSortOrder`);
    const textError = $(`#${type}-EditTextError`);
    const sortOrderError = $(`#${type}-EditSortOrderError`);

    //clear errors.
    textError.text('');
    sortOrderError.text('');

    //validate.
    if (text.val() === '') {
        textError.text('Text is required.');
        isValid = false;
    }

    //Sort order.
    if (sortOrder.val() === '') {
        sortOrderError.text('Sort order is required.');
        isValid = false;
    } else if (!isValidNumberStrict(sortOrder.val())) {
        sortOrderError.text('Please enter a valid sort order.');
        isValid = false;
    }

    return isValid;
}