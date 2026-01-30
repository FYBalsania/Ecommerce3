$(document).ready(function () {
    $('.select-product').select2({
        theme: 'bootstrap-5',
        width: '100%'
    });

    $('.select-category').select2({
        placeholder: 'Select...',
        sorter: function (data) {
            return data;
        }
    }).on('select2:select', function (e) {
        var element = e.params.data.element;
        $(element).detach().appendTo($(this));
        $(this).trigger('change');
    });

    toggleRedirectUrl();
    $('#Status').on('change', function () {
        toggleRedirectUrl();
    });

    $('#Name').on('change', nameChanged);

    $(document).on('change', '#productGroup', function () {
        const productGroupId = $(this).val();
        getAttribute(productGroupId);
    });

    $('#UnitOfMeasureId').on('change', unit_of_measure_changed);
    $('#QuantityPerUnitOfMeasure').on('change', qty_per_unit_of_measure_changed)
})

async function unit_of_measure_changed(event) {
    const response = await fetch('/api/UnitOfMeasures/' + $(event.target).val());
    
    if (response.ok) {
        const uom = await response.json();
        $('#UnitOfMeasureDecimalPlaces').val(uom.decimalPlaces);
    }
    else {
        $('#UnitOfMeasureDecimalPlaces').val('');
        alert('Error loading unit of measure.');
    }

    $('#QuantityPerUnitOfMeasure').val('');
}

function qty_per_unit_of_measure_changed(event) {
    const qty = $(event.target).val();
    const decimalPlaces = $('#UnitOfMeasureDecimalPlaces').val();

    $(event.target).val(roundTo(qty, decimalPlaces));
}

function nameChanged(event) {
    const name = $(event.target).val();
    $('#Slug').val(toSlug(name));
    $('#Display').val(name);
    $('#Breadcrumb').val(name);
    $('#AnchorText').val(name);
    $('#AnchorTitle').val(name);
    $('#H1').val(name);
    $('#MetaTitle').val(name);
}

async function getAttribute(productGroupId) {
    const container = $('#productGroupAttributesDiv');

    try {
        const response = await fetch(
            `/Products/GetAttributes?productGroupId=${productGroupId}`,
            {method: 'GET', credentials: 'same-origin'});

        const html = await response.text();
        container.html(html);

        // Re-parse validation for the entire form
        if ($.validator && $.validator.unobtrusive) {
            alert('attached')
            const form = container.closest('form');
            // Remove old validator instance
            form.removeData('validator');
            form.removeData('unobtrusiveValidation');
            // Re-parse the entire form with new fields
            $.validator.unobtrusive.parse(form);
        }
    } catch (err) {
        console.error('Error loading attribute values:', err);
        alert('Error loading attribute values.');
    }
}

function toggleRedirectUrl() {
    const status = $('#Status').val();
    const redirectInput = $('#RedirectUrl');

    if (status === 'RedirectToUrl') {
        redirectInput.prop('disabled', false);
    } else {
        redirectInput.val('');
        redirectInput.prop('disabled', true);
    }
}