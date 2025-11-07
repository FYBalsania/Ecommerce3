$(document).ready(() => {
    const addImageModel = $('#addImageModal');
    addImageModel.on('show.bs.modal', getAddImageView);
    addImageModel.on('hidden.bs.modal', clearAddImageView);

    $('#add_Save').on('click', add_SaveClicked)
})

async function getAddImageView() {
    const data = {parentEntityType: $('#ParentEntityType').val()};
    try {
        const response = await doAjax('/Images/Add', 'GET', data, true).promise();
        $('#addImageModalBody').html(response);
        $('#add_Link').on('change', add_LinkChanged);
    } catch (err) {
        alert('Error occured, please try again.')
    }
}

function clearAddImageView() {
    $('#add_Link').off('change', add_LinkChanged);

    $('#addImageModalBody').empty();
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
        await doAjax('/Images/Add', 'POST', data).promise();
    } catch (err) {
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