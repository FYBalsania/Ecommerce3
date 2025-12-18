const valueAddQuill = {};
const valueEditQuill = {};


$(document).ready(() => {
    $('.kvpPrefix').each(function () {

        const prefix = this.value;
        const type = prefix.replace('kvp-', '');

        const kvpAddModal = $(`#${prefix}-AddModal`);
        kvpAddModal.on('show.bs.modal', () => show_AddKVPItemView(prefix));
        kvpAddModal.on('hidden.bs.modal', () => hide_AddKVPItemView(prefix));

        const kvpEditModal = $(`#${prefix}-EditModal`);
        kvpEditModal.on('show.bs.modal', (e) => show_EditKVPItemView(e, prefix));
        kvpEditModal.on('hidden.bs.modal', () => hide_EditKVPItemView(prefix));

        const kvpDeleteModal = $(`#${prefix}-DeleteModal`);
        kvpDeleteModal.on('show.bs.modal', (e) => show_DeleteKVPItemView(e, prefix));
        kvpDeleteModal.on('hidden.bs.modal', () => hide_DeleteKVPItemView(prefix));

        $(`#${prefix}-Add-Save`).on('click', () => add_KVPItemsSaveClicked(prefix, type));
        $(`#${prefix}-Edit-Save`).on('click', () => edit_KVPItemsSaveClicked(prefix, type));
        $(`#${prefix}-Delete`).on('click', () => kvpItemsDeleteClicked(prefix, type));

        initializeValueQuillRTE(prefix);
    });
});

function initializeValueQuillRTE(prefix) {
    // Initialize Add Quill editor
    valueAddQuill[prefix] = new Quill(`#${prefix}-AddValue`, {
        theme: 'snow',
        modules: {
            toolbar: [
                [{header: [1, 2, 3, false]}],
                [{font: []}],
                ['bold', 'italic', 'underline', 'strike'],
                [{color: []}, {background: []}],
                [{list: 'ordered'}, {list: 'bullet'}],
                [{align: []}],
                ['link', 'image', 'blockquote', 'code-block'],
                ['clean']
            ]
        }
    });
    valueAddQuill[prefix].on('text-change', function () {
        $(`#${prefix}-AddValue-Input`).val(valueAddQuill[prefix].root.innerHTML);
    });

    // Initialize Edit Quill editor
    valueEditQuill[prefix] = new Quill(`#${prefix}-EditValue`, {
        theme: 'snow',
        modules: {
            toolbar: [
                [{header: [1, 2, 3, false]}],
                [{font: []}],
                ['bold', 'italic', 'underline', 'strike'],
                [{color: []}, {background: []}],
                [{list: 'ordered'}, {list: 'bullet'}],
                [{align: []}],
                ['link', 'image', 'blockquote', 'code-block'],
                ['clean']
            ]
        }
    });
    valueEditQuill[prefix].on('text-change', function () {
        $(`#${prefix}-EditValue-Input`).val(valueEditQuill[prefix].root.innerHTML);
    });
}


function show_AddKVPItemView(prefix) {
    $(`#${prefix}-AddKeyError`).text('');
    $(`#${prefix}-AddValueError`).text('');
    $(`#${prefix}-AddSortOrderError`).text('');
}

function hide_AddKVPItemView(prefix) {
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

    const kvpListItem = await doAjax(
        '/api/KVPListItems/' + kvpListItemId,
        'GET',
        null,
        true
    ).promise();

    $(`#${prefix}-EditId`).val(kvpListItem.id);
    $(`#${prefix}-EditKey`).val(kvpListItem.key);
    if (valueEditQuill[prefix]) {
        valueEditQuill[prefix].root.innerHTML = kvpListItem.value;
    }        
    $(`#${prefix}-EditValue-Input`).val(kvpListItem.value);
    $(`#${prefix}-EditSortOrder`).val(kvpListItem.sortOrder);
}

function hide_EditKVPItemView(prefix) {
    $(`#${prefix}-AddKeyError`).text('');
    $(`#${prefix}-AddValueError`).text('');
    $(`#${prefix}-AddSortOrderError`).text('');
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
    if (!add_KVPListItemValidate(prefix)) return;

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
            for (const key in error.errors) {
                if (key.endsWith('AddKey'))
                    $(`#${prefix}-AddKeyError`).text(error.errors[key][0]);

                if (key.endsWith('AddValue'))
                    $(`#${prefix}-AddValueError`).text(error.errors[key][0]);

                if (key.endsWith('AddSortOrder'))
                    $(`#${prefix}-AddSortOrderError`).text(error.errors[key][0]);
            }
        }
    } catch (err) {
        alert('Error occurred while saving, please try again.');
    }
}

async function edit_KVPItemsSaveClicked(prefix, type) {
    if (!edit_KVPListItemValidate(prefix)) return;

    const data = new FormData();
    data.append('__RequestVerificationToken', $("[name='__RequestVerificationToken']").val());
    data.append('Id', $(`#${prefix}-EditId`).val());
    data.append('ParentEntity', $(`#${prefix}-EditParentEntity`).val());
    data.append('ParentEntityId', $(`#${prefix}-EditParentEntityId`).val());
    data.append('Type', type);
    data.append('Key', $(`#${prefix}-EditKey`).val());
    data.append('Value', $(`#${prefix}-EditValue-Input`).val());
    data.append('SortOrder', $(`#${prefix}-EditSortOrder`).val());

    try{
        const result = await fetch('/KVPListItems/Edit', { method: 'POST', body: data });
        if (result.ok) {
            const html = await result.text();
            $(`#${prefix}-List`).replaceWith(html);
            $(`#${prefix}-EditModal`).modal('hide');
        } else {
            const error = await result.json();
            for (const key in error.errors) {
                if (key.endsWith('AddKey'))
                    $(`#${prefix}-AddKeyError`).text(error.errors[key][0]);

                if (key.endsWith('AddValue'))
                    $(`#${prefix}-AddValueError`).text(error.errors[key][0]);

                if (key.endsWith('AddSortOrder'))
                    $(`#${prefix}-AddSortOrderError`).text(error.errors[key][0]);
            }
        }
    } catch (err) {
        alert('Error occurred while updating, please try again.');
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
            const errorMessage = error.message || error.errors?.[Object.keys(error.errors)[0]]?.[0] || 'Failed to delete item.';
            alert(errorMessage);
        }
    } catch (err) {
        alert('Error occured while deleting, please try again.');
    }
}

function add_KVPListItemValidate(prefix) {
    let isValid = true;
    const key = $(`#${prefix}-AddKey`);
    const value = $(`#${prefix}-AddValue-Input`);
    const sortOrder = $(`#${prefix}-AddSortOrder`);
    const keyError = $(`#${prefix}-AddKeyError`);
    const valueError = $(`#${prefix}-AddValueError`);
    const sortOrderError = $(`#${prefix}-AddSortOrderError`);

    //clear errors.
    keyError.text('');
    valueError.text('');
    sortOrderError.text('');

    //validate.
    if (key.val() === '') {
        keyError.text('Key is required.');
        isValid = false;
    }

    if (value.val() === '') {
        valueError.text('Value is required.');
        isValid = false;
    }

    if (sortOrder.val() === '') {
        sortOrderError.text('Sort order is required.');
        isValid = false;
    } else if (!isValidNumberStrict(sortOrder.val())) {
        sortOrderError.text('Please enter a valid sort order.');
        isValid = false;
    }

    return isValid;
}

function edit_KVPListItemValidate(prefix) {
    let isValid = true;
    const key = $(`#${prefix}-EditKey`);
    const value = $(`#${prefix}-EditValue-Input`);
    const sortOrder = $(`#${prefix}-EditSortOrder`);
    const keyError = $(`#${prefix}-EditKeyError`);
    const valueError = $(`#${prefix}-EditValueError`);
    const sortOrderError = $(`#${prefix}-EditSortOrderError`);

    //clear errors.
    keyError.text('');
    valueError.text('');
    sortOrderError.text('');

    //validate.
    if (key.val() === '') {
        keyError.text('Key is required.');
        isValid = false;
    }

    if (value.val() === '') {
        valueError.text('Value is required.');
        isValid = false;
    }

    if (sortOrder.val() === '') {
        sortOrderError.text('Sort order is required.');
        isValid = false;
    } else if (!isValidNumberStrict(sortOrder.val())) {
        sortOrderError.text('Please enter a valid sort order.');
        isValid = false;
    }

    return isValid;
}