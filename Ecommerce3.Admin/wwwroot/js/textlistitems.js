const textAddQuill = {};
const textEditQuill = {};

$(document).ready(() => {
    $('.textPrefix').each(function () {
        const prefix = this.value;
        const type = prefix.replace('text-', '');

        // Initialize modals
        $(`#${prefix}-AddModal`)
            .on('show.bs.modal', () => show_AddTextItemView(prefix))
            .on('hidden.bs.modal', () => hide_AddTextItemView(prefix));

        $(`#${prefix}-EditModal`)
            .on('show.bs.modal', (e) => show_EditTextItemView(e, prefix))
            .on('hidden.bs.modal', () => hide_EditTextItemView(prefix));

        $(`#${prefix}-DeleteModal`)
            .on('show.bs.modal', (e) => show_DeleteTextItemView(e, prefix))
            .on('hidden.bs.modal', () => hide_DeleteTextItemView(prefix));

        // Initialize button handlers
        $(`#${prefix}-Add-Save`).on('click', () => add_TextItemsSaveClicked(prefix, type));
        $(`#${prefix}-Edit-Save`).on('click', () => edit_TextItemsSaveClicked(prefix, type));
        $(`#${prefix}-Delete`).on('click', () => textItemsDeleteClicked(prefix, type));

        initializeTextQuillRTE(prefix);
    });
});

function initializeTextQuillRTE(prefix) {
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
    textAddQuill[prefix] = new Quill(`#${prefix}-AddText`, {
        theme: 'snow',
        modules: { toolbar: toolbarConfig }
    });

    textAddQuill[prefix].on('text-change', () => {
        $(`#${prefix}-AddText-Input`).val(textAddQuill[prefix].root.innerHTML);
    });

    // Initialize Edit Quill editor
    textEditQuill[prefix] = new Quill(`#${prefix}-EditText`, {
        theme: 'snow',
        modules: { toolbar: toolbarConfig }
    });

    textEditQuill[prefix].on('text-change', () => {
        $(`#${prefix}-EditText-Input`).val(textEditQuill[prefix].root.innerHTML);
    });
}

function show_AddTextItemView(prefix) {
    // Clear Quill editor and errors
    if (textAddQuill[prefix]) {
        textAddQuill[prefix].setText('');
    }

    $(`#${prefix}-AddTextError`).text('');
    $(`#${prefix}-AddSortOrderError`).text('');
}

function hide_AddTextItemView(prefix) {
    // Clear all fields
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

    try {
        const textListItem = await doAjax(
            `/api/TextListItems/${textListItemId}`,
            'GET',
            null,
            true
        ).promise();

        $(`#${prefix}-EditId`).val(textListItem.id);

        // Set content in Quill editor
        if (textEditQuill[prefix]) {
            textEditQuill[prefix].root.innerHTML = textListItem.text;
        }

        $(`#${prefix}-EditText-Input`).val(textListItem.text);
        $(`#${prefix}-EditSortOrder`).val(textListItem.sortOrder);

        // Clear any previous errors
        $(`#${prefix}-EditTextError`).text('');
        $(`#${prefix}-EditSortOrderError`).text('');
    } catch (err) {
        console.error('Error loading text item:', err);
        alert('Error loading item. Please try again.');
    }
}

function hide_EditTextItemView(prefix) {
    // Clear Quill editor
    if (textEditQuill[prefix]) {
        textEditQuill[prefix].setText('');
    }

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
    if (!validateTextListItem(prefix, 'Add')) return;

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
            displayValidationErrors(prefix, 'Add', error.errors);
        }
    } catch (err) {
        console.error('Error saving text item:', err);
        alert('Error occurred while saving. Please try again.');
    }
}

async function edit_TextItemsSaveClicked(prefix, type) {
    if (!validateTextListItem(prefix, 'Edit')) return;

    const data = new FormData();
    data.append('__RequestVerificationToken', $("[name='__RequestVerificationToken']").val());
    data.append('Id', $(`#${prefix}-EditId`).val());
    data.append('ParentEntity', $(`#${prefix}-EditParentEntity`).val());
    data.append('ParentEntityId', $(`#${prefix}-EditParentEntityId`).val());
    data.append('Type', type);
    data.append('Text', $(`#${prefix}-EditText-Input`).val());
    data.append('SortOrder', $(`#${prefix}-EditSortOrder`).val());

    try {
        const result = await fetch('/TextListItems/Edit', { method: 'POST', body: data });

        if (result.ok) {
            const html = await result.text();
            $(`#${prefix}-List`).replaceWith(html);
            $(`#${prefix}-EditModal`).modal('hide');
        } else {
            const error = await result.json();
            displayValidationErrors(prefix, 'Edit', error.errors);
        }
    } catch (err) {
        console.error('Error updating text item:', err);
        alert('Error occurred while updating. Please try again.');
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
            const errorMessage = error.message ||
                error.errors?.[Object.keys(error.errors)[0]]?.[0] ||
                'Failed to delete item.';
            alert(errorMessage);
        }
    } catch (err) {
        console.error('Error deleting text item:', err);
        alert('Error occurred while deleting. Please try again.');
    }
}

// Consolidated validation function
function validateTextListItem(prefix, mode) {
    let isValid = true;
    const text = $(`#${prefix}-${mode}Text-Input`);
    const sortOrder = $(`#${prefix}-${mode}SortOrder`);
    const textError = $(`#${prefix}-${mode}TextError`);
    const sortOrderError = $(`#${prefix}-${mode}SortOrderError`);

    // Clear errors
    textError.text('');
    sortOrderError.text('');

    // Validate text
    if (!text.val().trim()) {
        textError.text('Text is required.');
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
function displayValidationErrors(prefix, mode, errors) {
    for (const key in errors) {
        if (key.endsWith(`${mode}Text`)) {
            $(`#${prefix}-${mode}TextError`).text(errors[key][0]);
        }
        if (key.endsWith(`${mode}SortOrder`)) {
            $(`#${prefix}-${mode}SortOrderError`).text(errors[key][0]);
        }
    }
}