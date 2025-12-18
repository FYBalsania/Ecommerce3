const valueAddQuill = {};
const valueEditQuill = {};

$(document).ready(() => {
    $('.kvpPrefix').each(function () {
        const prefix = this.value;
        const type = prefix.replace('kvp-', '');

        // Initialize modals
        $(`#${prefix}-AddModal`)
            .on('show.bs.modal', () => show_AddKVPItemView(prefix))
            .on('hidden.bs.modal', () => hide_AddKVPItemView(prefix));

        $(`#${prefix}-EditModal`)
            .on('show.bs.modal', (e) => show_EditKVPItemView(e, prefix))
            .on('hidden.bs.modal', () => hide_EditKVPItemView(prefix));

        $(`#${prefix}-DeleteModal`)
            .on('show.bs.modal', (e) => show_DeleteKVPItemView(e, prefix))
            .on('hidden.bs.modal', () => hide_DeleteKVPItemView(prefix));

        // Initialize button handlers
        $(`#${prefix}-Add-Save`).on('click', () => add_KVPItemsSaveClicked(prefix, type));
        $(`#${prefix}-Edit-Save`).on('click', () => edit_KVPItemsSaveClicked(prefix, type));
        $(`#${prefix}-Delete`).on('click', () => kvpItemsDeleteClicked(prefix, type));

        initializeValueQuillRTE(prefix);
    });
});

function initializeValueQuillRTE(prefix) {
    const toolbarConfig = [
        [{header: [1, 2, 3, false]}],
        [{font: []}],
        ['bold', 'italic', 'underline', 'strike'],
        [{color: []}, {background: []}],
        [{list: 'ordered'}, {list: 'bullet'}],
        [{align: []}],
        ['link', 'image', 'blockquote', 'code-block'],
        ['clean']
    ];

    // Initialize Add Quill editor
    valueAddQuill[prefix] = new Quill(`#${prefix}-AddValue`, {
        theme: 'snow',
        modules: { toolbar: toolbarConfig }
    });

    valueAddQuill[prefix].on('text-change', () => {
        $(`#${prefix}-AddValue-Input`).val(valueAddQuill[prefix].root.innerHTML);
    });

    // Initialize Edit Quill editor
    valueEditQuill[prefix] = new Quill(`#${prefix}-EditValue`, {
        theme: 'snow',
        modules: { toolbar: toolbarConfig }
    });

    valueEditQuill[prefix].on('text-change', () => {
        $(`#${prefix}-EditValue-Input`).val(valueEditQuill[prefix].root.innerHTML);
    });
}

function show_AddKVPItemView(prefix) {
    // Clear Quill editor and errors
    if (valueAddQuill[prefix]) {
        valueAddQuill[prefix].setText('');
    }

    $(`#${prefix}-AddKeyError`).text('');
    $(`#${prefix}-AddValueError`).text('');
    $(`#${prefix}-AddSortOrderError`).text('');
}

function hide_AddKVPItemView(prefix) {
    // Clear all fields
    $(`#${prefix}-AddKey`).val('');

    if (valueAddQuill[prefix]) {
        valueAddQuill[prefix].setText('');
    }

    $(`#${prefix}-AddValue-Input`).val('');
    $(`#${prefix}-AddSortOrder`).val('');
    $(`#${prefix}-AddKeyError`).text('');
    $(`#${prefix}-AddValueError`).text('');
    $(`#${prefix}-AddSortOrderError`).text('');
}

async function show_EditKVPItemView(event, prefix) {
    const kvpListItemId = $(event.relatedTarget).data('kvp-id');

    try {
        const kvpListItem = await doAjax(
            `/api/KVPListItems/${kvpListItemId}`,
            'GET',
            null,
            true
        ).promise();

        $(`#${prefix}-EditId`).val(kvpListItem.id);
        $(`#${prefix}-EditKey`).val(kvpListItem.key);

        // Set content in Quill editor
        if (valueEditQuill[prefix]) {
            valueEditQuill[prefix].root.innerHTML = kvpListItem.value;
        }

        $(`#${prefix}-EditValue-Input`).val(kvpListItem.value);
        $(`#${prefix}-EditSortOrder`).val(kvpListItem.sortOrder);

        // Clear any previous errors
        $(`#${prefix}-EditKeyError`).text('');
        $(`#${prefix}-EditValueError`).text('');
        $(`#${prefix}-EditSortOrderError`).text('');
    } catch (err) {
        console.error('Error loading KVP item:', err);
        alert('Error loading item. Please try again.');
    }
}

function hide_EditKVPItemView(prefix) {
    // Clear Quill editor
    if (valueEditQuill[prefix]) {
        valueEditQuill[prefix].setText('');
    }

    $(`#${prefix}-EditKeyError`).text('');
    $(`#${prefix}-EditValueError`).text('');
    $(`#${prefix}-EditSortOrderError`).text('');
}

function show_DeleteKVPItemView(event, prefix) {
    $(`#${prefix}-DeleteId`).val($(event.relatedTarget).data('kvp-id'));
    $(`#${prefix}-DeleteKey`).text($(event.relatedTarget).data('kvp-key'));
}

function hide_DeleteKVPItemView(prefix) {
    $(`#${prefix}-DeleteId`).val('');
    $(`#${prefix}-DeleteKey`).text('');
}

async function add_KVPItemsSaveClicked(prefix, type) {
    if (!validateKVPListItem(prefix, 'Add')) return;

    const data = new FormData();
    data.append('__RequestVerificationToken', $("[name='__RequestVerificationToken']").val());
    data.append('ParentEntity', $(`#${prefix}-AddParentEntity`).val());
    data.append('ParentEntityId', $(`#${prefix}-AddParentEntityId`).val());
    data.append('Entity', $(`#${prefix}-AddEntity`).val());
    data.append('Type', type);
    data.append('Key', $(`#${prefix}-AddKey`).val());
    data.append('Value', $(`#${prefix}-AddValue-Input`).val());
    data.append('SortOrder', $(`#${prefix}-AddSortOrder`).val());

    try {
        const result = await fetch('/KVPListItems/Add', { method: 'POST', body: data });

        if (result.ok) {
            const html = await result.text();
            $(`#${prefix}-List`).replaceWith(html);
            $(`#${prefix}-AddModal`).modal('hide');
        } else {
            const error = await result.json();
            displayKVPValidationErrors(prefix, 'Add', error.errors);
        }
    } catch (err) {
        console.error('Error saving KVP item:', err);
        alert('Error occurred while saving. Please try again.');
    }
}

async function edit_KVPItemsSaveClicked(prefix, type) {
    if (!validateKVPListItem(prefix, 'Edit')) return;

    const data = new FormData();
    data.append('__RequestVerificationToken', $("[name='__RequestVerificationToken']").val());
    data.append('Id', $(`#${prefix}-EditId`).val());
    data.append('ParentEntity', $(`#${prefix}-EditParentEntity`).val());
    data.append('ParentEntityId', $(`#${prefix}-EditParentEntityId`).val());
    data.append('Type', type);
    data.append('Key', $(`#${prefix}-EditKey`).val());
    data.append('Value', $(`#${prefix}-EditValue-Input`).val());
    data.append('SortOrder', $(`#${prefix}-EditSortOrder`).val());

    try {
        const result = await fetch('/KVPListItems/Edit', { method: 'POST', body: data });

        if (result.ok) {
            const html = await result.text();
            $(`#${prefix}-List`).replaceWith(html);
            $(`#${prefix}-EditModal`).modal('hide');
        } else {
            const error = await result.json();
            displayKVPValidationErrors(prefix, 'Edit', error.errors);
        }
    } catch (err) {
        console.error('Error updating KVP item:', err);
        alert('Error occurred while updating. Please try again.');
    }
}

async function kvpItemsDeleteClicked(prefix, type) {
    const data = new FormData();
    data.append('__RequestVerificationToken', $("[name='__RequestVerificationToken']").val());
    data.append('Id', $(`#${prefix}-DeleteId`).val());
    data.append('ParentEntity', $(`#${prefix}-DeleteParentEntity`).val());
    data.append('ParentEntityId', $(`#${prefix}-DeleteParentEntityId`).val());
    data.append('Type', type);

    try {
        const result = await fetch('/KVPListItems/Delete', { method: 'POST', body: data });

        if (result.ok) {
            const html = await result.text();
            $(`#${prefix}-List`).replaceWith(html);
            $(`#${prefix}-DeleteModal`).modal('hide');
        } else {
            const error = await result.json();
            const errorMessage = error.message ||
                error.errors?.[Object.keys(error.errors)[0]]?.[0] ||
                'Failed to delete item.';
            alert(errorMessage);
        }
    } catch (err) {
        console.error('Error deleting KVP item:', err);
        alert('Error occurred while deleting. Please try again.');
    }
}

// Consolidated validation function
function validateKVPListItem(prefix, mode) {
    let isValid = true;
    const key = $(`#${prefix}-${mode}Key`);
    const value = $(`#${prefix}-${mode}Value-Input`);
    const sortOrder = $(`#${prefix}-${mode}SortOrder`);
    const keyError = $(`#${prefix}-${mode}KeyError`);
    const valueError = $(`#${prefix}-${mode}ValueError`);
    const sortOrderError = $(`#${prefix}-${mode}SortOrderError`);

    // Clear errors
    keyError.text('');
    valueError.text('');
    sortOrderError.text('');

    // Validate key
    if (!key.val().trim()) {
        keyError.text('Key is required.');
        isValid = false;
    }

    // Validate value
    if (!value.val().trim()) {
        valueError.text('Value is required.');
        isValid = false;
    }

    // Validate sort order
    if (!sortOrder.val().trim()) {
        sortOrderError.text('Sort order is required.');
        isValid = false;
    } else if (!isValidNumberStrict(sortOrder.val())) {
        sortOrderError.text('Please enter a valid sort order.');
        isValid = false;
    }

    return isValid;
}

// Helper function to display validation errors from server
function displayKVPValidationErrors(prefix, mode, errors) {
    for (const key in errors) {
        if (key.endsWith(`${mode}Key`)) {
            $(`#${prefix}-${mode}KeyError`).text(errors[key][0]);
        }
        if (key.endsWith(`${mode}Value`)) {
            $(`#${prefix}-${mode}ValueError`).text(errors[key][0]);
        }
        if (key.endsWith(`${mode}SortOrder`)) {
            $(`#${prefix}-${mode}SortOrderError`).text(errors[key][0]);
        }
    }
}