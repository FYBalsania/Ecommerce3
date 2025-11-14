$(document).ready(() => {
    const addImageModel = $('#addImageModal');
    addImageModel.on('show.bs.modal', showAddImageView);
    addImageModel.on('hidden.bs.modal', hideAddImageView);

    $('#add_Save').on('click', add_SaveClicked)
})

async function showAddImageView() {
    const data = {entity: $('#ParentEntityType').val()};
    try {
        //get image type ids and names.
        const imageTypeIdAndNames = await doAjax('/api/imagetypes/IdAndNamesByEntity', 'GET', data, true).promise();

        //populate image types dropdown.
        const imageTypesElement = $('#add_ImageTypeId');
        imageTypeIdAndNames.forEach(imageTypeIdAndName => {
            const option = $('<option></option>');
            option.attr('value', imageTypeIdAndName.key);
            option.text(imageTypeIdAndName.value);
            imageTypesElement.append(option);
        })

        //attach event handlers.
        $('#add_Link').on('change', add_LinkChanged);
    } catch (err) {
        alert('Error occured, please try again.')
    }
}

function hideAddImageView() {
    const addLinkElement = $('#add_Link');

    //remove event handlers.
    addLinkElement.off('change', add_LinkChanged);

    //reset validation errors.
    $('#add_ImageTypeIdError').text('');
    $('#add_ImageSizeError').text('');
    $('#add_FileError').text('');
    $('#add_AltTextError').text('');
    $('#add_TitleError').text('');
    $('#add_LoadingError').text('');
    $('#add_SortOrderError').text('');
    $('#add_LinkError').text('');
    $('#add_LinkTargetError').text('');

    //reset input elements and set to default values.
    $('#add_ImageTypeId option[value!=""]').remove();
    $('#add_ImageSize').val('').prop('selected', true);
    $('#add_File').val('');
    $('#add_AltText').val('');
    $('#add_Title').val('');
    $('#add_Loading').val('eager');
    $('#add_SortOrder').val('');
    addLinkElement.val('');
    $('#add_LinkTarget').val('').prop('disabled', true);
}

function add_LinkChanged(event) {
    const link = $(event.target);
    const linkTarget = $('#add_LinkTarget');

    if (link.val() === '') {
        linkTarget.val('').prop('disabled', true);
    } else {
        linkTarget.prop('disabled', false);
    }
}

async function add_SaveClicked(event) {
    if (!add_Validate()) return;

    const data = new FormData();
    data.append('__RequestVerificationToken', $("[name='__RequestVerificationToken']").val());
    data.append('ParentEntityType', $('#ParentEntityType').val());
    data.append('ParentEntityId', $('#ParentEntityId').val());
    data.append('ImageEntityType', $('#ImageEntityType').val());
    data.append('ImageTypeId', $('#add_ImageTypeId').val());
    data.append('ImageSize', $('#add_ImageSize').val());
    data.append('File', $('#add_File')[0].files[0]);
    data.append('AltText', $('#add_AltText').val());
    data.append('Title', $('#add_Title').val());
    data.append('Loading', $('#add_Loading').val());
    data.append('SortOrder', $('#add_SortOrder').val());
    data.append('Link', $('#add_Link').val());
    data.append('LinkTarget', $('#add_LinkTarget').val());

    console.log(data);

    try {
        const result = await doAjax('/images/Add', 'POST', data, false, false).promise();
    } catch (err) {
        console.log(err);
        alert('Error occured, please try again.');
    }

}

function add_Validate() {
    let isValid = true;
    const imageTypeIdError = $('#add_ImageTypeIdError');
    const imageSizeError = $('#add_ImageSizeError');
    const fileError = $('#add_FileError');
    const sortOrderError = $('#add_SortOrderError');
    const linkError = $('#add_LinkError');
    const linkTarget = $('#add_LinkTargetError');

    //clear errors.
    imageTypeIdError.text('');
    imageSizeError.text('');
    fileError.text('');
    sortOrderError.text('');
    linkError.text('');
    linkTarget.text('');

    //validate.
    //Image type.
    if ($('#add_ImageTypeId').val() === '') {
        imageTypeIdError.text('Image type is required.');
        isValid = false;
    }

    //Image size.
    if ($('#add_ImageSize').val() === '') {
        imageSizeError.text('Image size is required.');
        isValid = false;
    }

    //File.
    if ($('#add_File')[0].files.length === 0) {
        fileError.text('Image is required.');
        isValid = false;
    }

    //Sort order.
    const sortOrder = $('#add_SortOrder');
    if (sortOrder.val() === '') {
        sortOrderError.text('Please enter sort order.');
        isValid = false;
    } else if (!isValidNumberStrict(sortOrder.val())) {
        sortOrderError.text('Please enter a valid sort order.');
        isValid = false;
    }

    //Link.
    const link = $('#add_Link');
    if (link.val() !== '' && !isHttpsOrRelativeUrl(link.val())) {
        linkError.text('Please enter a valid link.');
        isValid = false;
    }

    //Link target.
    if (link.val() !== '' && $('#add_LinkTarget').val() === '') {
        linkTarget.text('Please select link target.');
        isValid = false;
    }

    return isValid;
}