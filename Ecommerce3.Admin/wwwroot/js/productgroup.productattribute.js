$(document).ready(() => {

        const textAddModal = $(`#addAttributeModal`);
        textAddModal.on('show.bs.modal', () => show_AddProductAttributeView());
        textAddModal.on('hidden.bs.modal', () => hide_AddProductAttributeView());

        const textEditModal = $(`#editAttributeModal`);
        textEditModal.on('show.bs.modal', (e) => show_EditProductAttributeView(e));
        textEditModal.on('hidden.bs.modal', () => hide_EditProductAttributeView());

        $(document).on('change', '#addNameAttribute', function () {
                const attributeId = $(this).val();

                if (!attributeId) {
                        $('#addAttributeValuesTableBody').empty();
                        $('#addAttributeValuesWrapper').addClass('d-none');
                        return;
                }

                getAttributeValues(attributeId);
        });

        $(document).on('change', '#addAttributeValuesTableBody .form-check-input', function () {
                    const sortInput = $(this).closest('tr').find('input[type="number"]');
                    if (this.checked) {
                            sortInput.removeClass('d-none');
                    } else {
                            sortInput.addClass('d-none');
                            sortInput.val('');
                    }
        });

        $(document).on('change', '#editAttributeValuesTableBody .form-check-input', function () {
                const sortInput = $(this).closest('tr').find('input[type="number"]');
                if (this.checked) {
                        sortInput.removeClass('d-none');
                } else {
                        sortInput.addClass('d-none');
                        sortInput.val('');
                }
        });
        
        $('#add_AttributeSave').on('click', add_AttributeSaveClicked);
        $('#edit_AttributeSave').on('click', edit_AttributeSaveClicked);
});

async function show_AddProductAttributeView() {
        const productGroupId = $('#productGroupId').val();
        const modalBody = $('#addAttributeModalBody');

        try {
                const response = await fetch(
                    `/ProductGroups/AddAttribute?productGroupId=${productGroupId}`, 
                    { method: 'GET', credentials: 'same-origin'});
                
                const html = await response.text();
                modalBody.html(html);
        } catch (err) {
                alert('Error occured, please try again.')
        }
}

async function getAttributeValues(attributeId) {
        const container = $('#attributeValuesContainer');

        try {
                const response = await fetch(
                    `/ProductGroups/GetAttributeValues?productAttributeId=${attributeId}`,
                    { method: 'GET', credentials: 'same-origin'});

                const html = await response.text();
                container.html(html);
        } catch (err) {
                console.error('Error loading attribute values:', err);
                alert('Error loading attribute values.');
        }
}

async function hide_AddProductAttributeView() {
        $('#addNameAttribute').val('');
        $('#addAttributeValuesTableBody').empty();
        $('#addAttributeValuesWrapper').addClass('d-none');
}

async function add_AttributeSaveClicked() {
        if (!add_AttrValidate()) return;

        const formData = new FormData();
        formData.append('__RequestVerificationToken', $("[name='__RequestVerificationToken']").val());
        formData.append('productGroupId', $('#productGroupId').val());
        formData.append('ProductAttributeId', $('#addNameAttribute').val());
        formData.append('ProductAttributeSortOrder', $('#addSortOrderAttribute').val());
        $('#addAttributeValuesTableBody .form-check-input:checked').each(function () {

                const row = $(this).closest('tr');
                const valueId = $(this).data('value-id');
                const valueSortOrder = row.find('input[type="number"]').val();

                formData.append(`Values[${valueId}]`, valueSortOrder);
        });

        try {
                const result = await fetch(`/ProductGroups/AddAttribute`, {method: 'POST', body: formData, credentials: 'same-origin'});
                if (result.ok) {
                        const response = await result.text();
                        $('#AttributesTableBody').replaceWith(response);
                        $('#addAttributeModal').modal('hide');
                } else {
                        const error = await result.json();
                        for (const key in error.errors) {

                                if (key.endsWith('NameAttribute'))
                                        $('#addNameAttributeError').text(error.errors[key]);
                                if (key.endsWith('SortOrder'))
                                        $('#addSortOrderAttributeError').text(error.errors[key]);
                                
                                if (key.endsWith('ProductAttributeId'))
                                        $('#addNameAttributeError').text(error.errors[key]);

                                if (key.endsWith('ProductAttributeSortOrder'))
                                        $('#addSortOrderAttributeError').text(error.errors[key]);

                                if (key.startsWith('Values')) {
                                        // example key: Values[3]
                                        const match = key.match(/\[(\d+)\]/);
                                        if (!match) continue;

                                        const valueId = match[1];
                                        const row = $(`#addAttributeValuesTableBody .form-check-input[data-value-id="${valueId}"]`).closest('tr');
                                        const sortInput = row.find('input[type="number"]');
                                        $('<div class="text-danger small attribute-value-error mt-1"></div>').text(error.errors[key]).insertAfter(sortInput);
                                }
                        }
                }
        } catch (err) {
                console.error('Error saving attribute:', err);
                alert('Error saving attribute. Please try again.');
        }
}

function add_AttrValidate() {
        let isValid = true;

        const attributeName = $('#addNameAttribute');
        const attributeNameError = $('#addNameAttributeError');

        const attributeSortOrder = $('#addSortOrderAttribute');
        const attributeSortOrderError = $('#addSortOrderAttributeError');

        const attributeValuesError = $('#attributeValuesError');

        // Clear errors
        attributeNameError.text('');
        attributeSortOrderError.text('');
        attributeValuesError.text('');
        $('.attribute-value-error').remove();

        // Attribute name
        if (attributeName.val() === '') {
                attributeNameError.text('Attribute name is required.');
                isValid = false;
        }

        // Attribute sort order
        if (attributeSortOrder.val() === '') {
                attributeSortOrderError.text('Attribute sort order is required.');
                isValid = false;
        } else if (!isValidNumberStrict(attributeSortOrder.val())) {
                attributeSortOrderError.text('Please enter a valid sort order.');
                isValid = false;
        }

        // Stop here if top-level is invalid
        if (!isValid) {
                return false;
        }

        // Attribute values
        const checkedValues = $('#addAttributeValuesTableBody .form-check-input:checked');

        if (checkedValues.length === 0) {
                attributeValuesError.text('Please select at least one attribute value.');
                isValid = false;
        } else {

                checkedValues.each(function () {

                        const row = $(this).closest('tr');
                        const sortInput = row.find('input[type="number"]');

                        // Create error span if not exists
                        let errorSpan = row.find('.attribute-value-error');
                        if (errorSpan.length === 0) {
                                errorSpan = $('<div class="text-danger small attribute-value-error mt-1"></div>');
                                sortInput.after(errorSpan);
                        }

                        // Clear previous error
                        errorSpan.text('');

                        // Validate sort order
                        if (sortInput.val() === '') {
                                errorSpan.text('Sort order is required.');
                                isValid = false;
                        } else if (!isValidNumberStrict(sortInput.val())) {
                                errorSpan.text('Please enter a valid sort order.');
                                isValid = false;
                        }
                });
        }

        return isValid;
}

async function show_EditProductAttributeView(event) {
        const productGroupId = $('#productGroupId').val();
        const productAttributeId = $(event.relatedTarget).data('product-attribute-id');
        const modalBody = $('#editAttributeModalBody');
        
        try {
                const response = await fetch(
                    `/ProductGroups/EditAttribute?productGroupId=${productGroupId}&productAttributeId=${productAttributeId}`,
                    { method: 'GET', credentials: 'same-origin'});

                const html = await response.text();
                modalBody.html(html);
        } catch (err) {
                alert('Error occured, please try again.')
        }
}

async function hide_EditProductAttributeView() {
        $('#editNameAttribute').val('');
        $('#editAttributeValuesTableBody').empty();
        $('#editAttributeValuesWrapper').addClass('d-none');
}

async function edit_AttributeSaveClicked() {
        if (!edit_AttrValidate()) return;

        const formData = new FormData();
        formData.append('__RequestVerificationToken', $("[name='__RequestVerificationToken']").val());
        formData.append('productGroupId', $('#productGroupId').val());
        formData.append('ProductAttributeId', $('#editProductAttributeId').val());
        formData.append('ProductAttributeSortOrder', $('#editSortOrderAttribute').val());
        $('#editAttributeValuesTableBody .form-check-input:checked').each(function () {

                const row = $(this).closest('tr');
                const valueId = $(this).data('value-id');
                const valueSortOrder = row.find('input[type="number"]').val();

                formData.append(`Values[${valueId}]`, valueSortOrder);
        });

        try {
                const result = await fetch(`/ProductGroups/EditAttribute/`, {method: 'POST', body: formData, credentials: 'same-origin'});
                if (result.ok) {
                        const response = await result.text();
                        $('#AttributesTableBody').replaceWith(response);
                        $('#editAttributeModal').modal('hide');
                } else {
                        const error = await result.json();
                        for (const key in error.errors) {

                                if (key.endsWith('NameAttribute'))
                                        $('#editNameAttributeError').text(error.errors[key]);
                                if (key.endsWith('SortOrder'))
                                        $('#editSortOrderAttributeError').text(error.errors[key]);

                                if (key.endsWith('ProductAttributeId'))
                                        $('#editNameAttributeError').text(error.errors[key]);

                                if (key.endsWith('ProductAttributeSortOrder'))
                                        $('#editSortOrderAttributeError').text(error.errors[key]);

                                if (key.startsWith('Values')) {
                                        // example key: Values[3]
                                        const match = key.match(/\[(\d+)\]/);
                                        if (!match) continue;

                                        const valueId = match[1];
                                        const row = $(`#editAttributeValuesTableBody .form-check-input[data-value-id="${valueId}"]`).closest('tr');
                                        const sortInput = row.find('input[type="number"]');
                                        $('<div class="text-danger small attribute-value-error mt-1"></div>').text(error.errors[key]).insertAfter(sortInput);
                                }
                        }
                }
        } catch (err) {
                console.error('Error saving attribute:', err);
                alert('Error saving attribute. Please try again.');
        }
}

function edit_AttrValidate() {
        let isValid = true;

        const attributeName = $('#editNameAttribute');
        const attributeNameError = $('#editNameAttributeError');

        const attributeSortOrder = $('#editSortOrderAttribute');
        const attributeSortOrderError = $('#editSortOrderAttributeError');

        const attributeValuesError = $('#attributeValuesError');

        // Clear errors
        attributeNameError.text('');
        attributeSortOrderError.text('');
        attributeValuesError.text('');
        $('.attribute-value-error').remove();

        // Attribute name
        if (attributeName.val() === '') {
                attributeNameError.text('Attribute name is required.');
                isValid = false;
        }

        // Attribute sort order
        if (attributeSortOrder.val() === '') {
                attributeSortOrderError.text('Attribute sort order is required.');
                isValid = false;
        } else if (!isValidNumberStrict(attributeSortOrder.val())) {
                attributeSortOrderError.text('Please enter a valid sort order.');
                isValid = false;
        }

        // Stop here if top-level is invalid
        if (!isValid) {
                return false;
        }

        // Attribute values
        const checkedValues = $('#editAttributeValuesTableBody .form-check-input:checked');

        if (checkedValues.length === 0) {
                attributeValuesError.text('Please select at least one attribute value.');
                isValid = false;
        } else {

                checkedValues.each(function () {

                        const row = $(this).closest('tr');
                        const sortInput = row.find('input[type="number"]');

                        // Create error span if not exists
                        let errorSpan = row.find('.attribute-value-error');
                        if (errorSpan.length === 0) {
                                errorSpan = $('<div class="text-danger small attribute-value-error mt-1"></div>');
                                sortInput.after(errorSpan);
                        }

                        // Clear previous error
                        errorSpan.text('');

                        // Validate sort order
                        if (sortInput.val() === '') {
                                errorSpan.text('Sort order is required.');
                                isValid = false;
                        } else if (!isValidNumberStrict(sortInput.val())) {
                                errorSpan.text('Please enter a valid sort order.');
                                isValid = false;
                        }
                });
        }

        return isValid;
}