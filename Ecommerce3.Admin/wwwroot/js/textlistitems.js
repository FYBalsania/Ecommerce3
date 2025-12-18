const textAddQuill = {};
const textEditQuill = {};

$(document).ready(() => {
    $('.textPrefix').each(function () {

        const prefix = this.value;
        const type   = prefix.replace('text-', '');

        const textAddModal = $(`#${prefix}-AddModal`);
        textAddModal.on('show.bs.modal', () => show_AddTextItemView(prefix));
        textAddModal.on('hidden.bs.modal', () => hide_AddTextItemView(prefix));

        const textEditModal = $(`#${prefix}-EditModal`);
        textEditModal.on('show.bs.modal', (e) => show_EditTextItemView(e, prefix));
        textEditModal.on('hidden.bs.modal', () => hide_EditTextItemView(prefix));

        const textDeleteModal = $(`#${prefix}-DeleteModal`);
        textDeleteModal.on('show.bs.modal', (e) => show_DeleteTextItemView(e, prefix));
        textDeleteModal.on('hidden.bs.modal', () => hide_DeleteTextItemView(prefix));

        $(`#${prefix}-Add-Save`).on('click', () => add_TextItemsSaveClicked(prefix, type));
        $(`#${prefix}-Edit-Save`).on('click', () => edit_TextItemsSaveClicked(prefix, type));
        $(`#${prefix}-Delete`).on('click', () => textItemsDeleteClicked(prefix, type));
        
        initializeTextQuillRTE(prefix);
    });
});

function initializeTextQuillRTE(prefix) {
    // Initialize Add Quill editor
    textAddQuill[prefix] = new Quill(`#${prefix}-AddText`, {
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
    textAddQuill[prefix].on('text-change', function () {
        $(`#${prefix}-AddText-Input`).val(textAddQuill[prefix].root.innerHTML);
    });

    // Initialize Edit Quill editor
    textEditQuill[prefix] = new Quill(`#${prefix}-EditText`, {
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
    textEditQuill[prefix].on('text-change', function () {
        $(`#${prefix}-EditText-Input`).val(textEditQuill[prefix].root.innerHTML);
    });
}

function show_AddTextItemView(prefix) {
    $(`#${prefix}-AddTextError`).text('');
    $(`#${prefix}-AddSortOrderError`).text('');
}

function hide_AddTextItemView(prefix) {
    // Clear Quill editor content
    if (textAddQuill[prefix]) {
        textAddQuill[prefix].setText('');
    }

    $(`#${prefix}-AddText-Input`).val('');
    $(`#${prefix}-AddSortOrder`).val('');
    $(`#${prefix}-AddTextError`).text('');
    $(`#${prefix}-AddSortOrderError`).text('');
}

async function show_EditTextItemView(event, prefix) {
    const textListItemId = $(event.relatedTarget).data('tli-id');

    const textListItem = await doAjax(
        '/api/TextListItems/' + textListItemId,
        'GET',
        null,
        true
    ).promise();

    $(`#${prefix}-EditId`).val(textListItem.id);
    if (textEditQuill[prefix]) {
        textEditQuill[prefix].root.innerHTML = textListItem.text;
    }    
    $(`#${prefix}-EditText-Input`).val(textListItem.text);
    $(`#${prefix}-EditSortOrder`).val(textListItem.sortOrder);
}

function hide_EditTextItemView(prefix) {
    $(`#${prefix}-EditTextError`).text('');
    $(`#${prefix}-EditSortOrderError`).text('');
}

function show_DeleteTextItemView(event, prefix) {
    $(`#${prefix}-DeleteId`).val($(event.relatedTarget).data('tli-id'));
    $(`#${prefix}-DeleteText`).text($(event.relatedTarget).data('tli-text'));
}

function hide_DeleteTextItemView(prefix) {
    $(`#${prefix}-DeleteId`).val('');
    $(`#${prefix}-DeleteText`).text('');
}

async function add_TextItemsSaveClicked(prefix, type) {
    if (!add_TextListItemValidate(prefix)) return;

    const data = new FormData();
    data.append('__RequestVerificationToken', $("[name='__RequestVerificationToken']").val());
    data.append('ParentEntity', $(`#${prefix}-AddParentEntity`).val());
    data.append('ParentEntityId', $(`#${prefix}-AddParentEntityId`).val());
    data.append('Entity', $(`#${prefix}-AddEntity`).val());
    data.append('Type', type);
    data.append('Text', $(`#${prefix}-AddText-Input`).val());
    data.append('SortOrder', $(`#${prefix}-AddSortOrder`).val());

    try {
        const result = await fetch('/TextListItems/Add', { method: 'POST', body: data });
        if (result.ok) {
            const html = await result.text();
            $(`#${prefix}-List`).replaceWith(html);
            $(`#${prefix}-AddModal`).modal('hide');
        } else {
            const error = await result.json();
            for (const key in error.errors) {
                if (key.endsWith('AddText'))
                    $(`#${prefix}-AddTextError`).text(error.errors[key][0]);

                if (key.endsWith('AddSortOrder'))
                    $(`#${prefix}-AddSortOrderError`).text(error.errors[key][0]);
            }
        }
    } catch (err) {
        alert('Error occurred while saving, please try again.');
    }
}

async function edit_TextItemsSaveClicked(prefix, type) {
    if (!edit_TextListItemValidate(prefix)) return;

    const data = new FormData();
    data.append('__RequestVerificationToken', $("[name='__RequestVerificationToken']").val());
    data.append('Id', $(`#${prefix}-EditId`).val());
    data.append('ParentEntity', $(`#${prefix}-EditParentEntity`).val());
    data.append('ParentEntityId', $(`#${prefix}-EditParentEntityId`).val());
    data.append('Type', type);
    data.append('Text', $(`#${prefix}-EditText-Input`).val());
    data.append('SortOrder', $(`#${prefix}-EditSortOrder`).val());
    
    try{
        const result = await fetch('/TextListItems/Edit', { method: 'POST', body: data });
        if (result.ok) {
            const html = await result.text();
            $(`#${prefix}-List`).replaceWith(html);
            $(`#${prefix}-EditModal`).modal('hide');
        } else {
            const error = await result.json();
            for (const key in error.errors) {
                if (key.endsWith('EditText'))
                    $(`#${prefix}-EditTextError`).text(error.errors[key][0]);

                if (key.endsWith('EditSortOrder'))
                    $(`#${prefix}-EditSortOrderError`).text(error.errors[key][0]);
            }
        }
    } catch (err) {
        alert('Error occurred while updating, please try again.');
    }
}

async function textItemsDeleteClicked(prefix, type) {
    const data = new FormData();
    data.append('__RequestVerificationToken', $("[name='__RequestVerificationToken']").val());
    data.append('Id', $(`#${prefix}-DeleteId`).val());
    data.append('ParentEntity', $(`#${prefix}-DeleteParentEntity`).val());
    data.append('ParentEntityId', $(`#${prefix}-DeleteParentEntityId`).val());
    data.append('Type', type);

    try {
        const result = await fetch('/TextListItems/Delete', { method: 'POST', body: data });

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

function add_TextListItemValidate(prefix) {
    let isValid = true;
    const text = $(`#${prefix}-AddText-Input`);
    const sortOrder = $(`#${prefix}-AddSortOrder`);
    const textError = $(`#${prefix}-AddTextError`);
    const sortOrderError = $(`#${prefix}-AddSortOrderError`);

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

function edit_TextListItemValidate(prefix) {
    let isValid = true;
    const text = $(`#${prefix}-EditText-Input`);
    const sortOrder = $(`#${prefix}-EditSortOrder`);
    const textError = $(`#${prefix}-EditTextError`);
    const sortOrderError = $(`#${prefix}-EditSortOrderError`);

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