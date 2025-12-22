$(document).ready(() => {

        const textAddModal = $(`#addAttributeModal`);
        textAddModal.on('show.bs.modal', () => show_AddProductAttributeView());
        textAddModal.on('hidden.bs.modal', () => hide_AddProductAttributeView());
        
        const nameAttribute = $('#addNameAttribute');
        nameAttribute.on('change', function () {
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
        
        $('#add_AttributeSave').on('click', add_AttributeSaveClicked);
});

async function show_AddProductAttributeView() {

        const data = { excludeProductGroupId: $('#productGroupId').val() };
        try {
                const attributeIdAndNames = await doAjax('/api/ProductAttributes', 'GET', data, true).promise();
                const nameDropdown = $('#addNameAttribute');
                attributeIdAndNames.forEach(attributeIdAndName => {
                        const option = $('<option></option>');
                        option.attr('value', attributeIdAndName.id);
                        option.text(attributeIdAndName.name);
                        nameDropdown.append(option);
                });
        } catch (err) {
                alert('Error occured, please try again.')
        }
}

async function getAttributeValues(attributeId) {
        try {
                const values = await doAjax(`/api/ProductAttributes/${attributeId}/values`, 'GET', null, true).promise();
                const tbody = $('#addAttributeValuesTableBody');
                const wrapper = $('#addAttributeValuesWrapper');

                tbody.empty();
                wrapper.removeClass('d-none');

                if (!values || values.length === 0) {
                        const row = $('<tr>').append(
                            $('<td>', {
                                    colspan: 2,
                                    class: 'text-muted text-center',
                                    text: 'No values available'
                            })
                        );
                        tbody.append(row);
                        return;
                }

                values.forEach(v => {
                        const row = $('<tr>').css('height', '56px');
                        
                        // Checkbox column
                        const checkboxCell = $('<td>', { class: 'w-75' });
                        const checkbox = $('<input>', {
                                type: 'checkbox',
                                class: 'form-check-input',
                                id: `attrValue_${v.value}`,
                                'data-value-id': v.id
                        });
                        const label = $('<label>', {
                                class: 'form-check-label ms-2',
                                for: `attrValue_${v.value}`,
                                text: v.value
                        });
                        checkboxCell.append(checkbox, label);

                        // Sort order column (hidden initially)
                        const sortCell = $('<td>');
                        const sortInput = $('<input>', {
                                type: 'number',
                                class: 'form-control form-control-sm d-none'
                        });
                        sortCell.append(sortInput);

                        row.append(checkboxCell, sortCell);
                        tbody.append(row);
                });

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

async function add_AttributeSaveClicked(event) {
        event.preventDefault();

        if (!add_AttrValidate()) return;

        const productGroupId = $('#productGroupId').val();

        const data = new FormData();
        data.append('__RequestVerificationToken', $("[name='__RequestVerificationToken']").val());
        data.append('ProductAttributeId', $('#addNameAttribute').val());
        data.append('ProductAttributeSortOrder', $('#addSortOrderAttribute').val());

        // Values → IDictionary<int, decimal>
        $('#addAttributeValuesTableBody .form-check-input:checked').each(function () {
                const row = $(this).closest('tr');
                const valueId = $(this).data('value-id');
                const valueSortOrder = row.find('input[type="number"]').val();

                // IMPORTANT: dictionary binding format
                data.append(`Values[${valueId}]`, valueSortOrder);
        });

        try {
                const result = await fetch(`/api/ProductGroupAttributes/${productGroupId}/attributes`, {method: 'POST', body: data, credentials: 'same-origin'});
                
        } catch (err) {
                alert('Error occurred while saving attribute. Please try again.');
        }
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
                        }

                        for (const key in error.errors) {

                                if (key.endsWith('ProductAttributeId')) {
                                        $('#addNameAttributeError').text(error.errors[key]);
                                }

                                if (key.endsWith('ProductAttributeSortOrder')) {
                                        $('#addSortOrderAttributeError').text(error.errors[key]);
                                }

                                if (key.startsWith('Values')) {
                                        // example key: Values[3]
                                        const match = key.match(/\[(\d+)\]/);
                                        if (!match) continue;

                                        const valueId = match[1];
                                        const row = $(`#addAttributeValuesTableBody .form-check-input[data-value-id="${valueId}"]`)
                                            .closest('tr');

                                        const sortInput = row.find('input[type="number"]');

                                        $('<div class="text-danger small attribute-value-error mt-1"></div>')
                                            .text(error.errors[key])
                                            .insertAfter(sortInput);
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