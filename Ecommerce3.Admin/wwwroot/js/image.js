$(document).ready(() => {
    const addImageModel = $('#addImageModal');
    addImageModel.on('show.bs.modal', show_AddImageView);
    addImageModel.on('hidden.bs.modal', hide_AddImageView);

    const editImageModel = $('#editImageModal');
    editImageModel.on('show.bs.modal', show_EditImageView);
    editImageModel.on('hidden.bs.modal', hide_EditImageView);
    
    const deleteImageModel = $('#deleteImageModal');
    deleteImageModel.on('show.bs.modal', show_DeleteImageView);
    deleteImageModel.on('hidden.bs.modal', hide_DeleteImageView);

    $('#add_Save').on('click', add_SaveClicked)
    $('#edit_Save').on('click', edit_SaveClicked)
    $('#delete').on('click', deleteClicked)
})

async function show_AddImageView(event) {
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

async function show_EditImageView(event) {
    const imageId = $(event.relatedTarget).data('image-id');
    const data = {entity: $('#ParentEntityType').val()};
    try {
        //get image type ids and names.
        const imageTypeIdAndNames = await doAjax('/api/imagetypes/IdAndNamesByEntity', 'GET', data, true).promise();

        //populate image types dropdown.
        const imageTypesElement = $('#edit_ImageTypeId');
        imageTypeIdAndNames.forEach(imageTypeIdAndName => {
            const option = $('<option></option>');
            option.attr('value', imageTypeIdAndName.key);
            option.text(imageTypeIdAndName.value);
            imageTypesElement.append(option);
        })

        //fetch image details.
        const imageDetails = await doAjax('/api/image/' + imageId, 'GET', null, true).promise();

        //populate image details.
        $('#edit_Id').val(imageDetails.id);
        $('#edit_File').val(imageDetails.fileName);
        $('#edit_ImageTypeId').val(imageDetails.imageTypeId);
        $('#edit_ImageSize').val(imageDetails.size);
        $('#edit_AltText').val(imageDetails.altText);
        $('#edit_Title').val(imageDetails.title);
        $('#edit_Loading').val(String(imageDetails.loading).trim());
        $('#edit_SortOrder').val(imageDetails.sortOrder);
        $('#edit_Link').val(imageDetails.link);
        edit_LinkChanged();
        $('#edit_LinkTarget').val(imageDetails.linkTarget);
        $('#edit_Image').attr('src', imageDetails.path);
    } catch (err) {
        alert('Error occured, please try again.')
    }
}

async function show_DeleteImageView(event) {
    const imageId = $(event.relatedTarget).data('image-id');
    const imageName = $(event.relatedTarget).data('image-name');
    const data = {entity: $('#ParentEntityType').val()};
    
    //populate image details.
    $('#delete_Id').val(imageId);
    $('#delete_ImageName').text(imageName);
}

function hide_AddImageView() {
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

function hide_EditImageView() {
    const addLinkElement = $('#edit_Link');

    //remove event handlers.
    addLinkElement.off('change', edit_LinkChanged);

    //reset validation errors.
    $('#edit_ImageTypeIdError').text('');
    $('#edit_ImageSizeError').text('');
    $('#edit_FileError').text('');
    $('#edit_AltTextError').text('');
    $('#edit_TitleError').text('');
    $('#edit_LoadingError').text('');
    $('#edit_SortOrderError').text('');
    $('#edit_LinkError').text('');
    $('#edit_LinkTargetError').text('');

    //reset input elements and set to default values.
    $('#edit_Id').val('');
    $('#edit_ImageTypeId option[value!=""]').remove();
    $('#edit_ImageSize').val('').prop('selected', true);
    $('#edit_AltText').val('');
    $('#edit_Title').val('');
    $('#edit_Loading').val('eager');
    $('#edit_SortOrder').val('');
    addLinkElement.val('');
    $('#edit_LinkTarget').val('').prop('disabled', true);
}

function hide_DeleteImageView() {
    //reset input elements and set to default values.
    $('#delete_Id').val('');
    $('#delete_ImageName').text('');
}

function add_LinkChanged(event) {
    const link = $(event.target);
    const linkTarget = $('#add_LinkTarget');

    if (link.val().trim() === '') {
        linkTarget.val('').prop('disabled', true);
    } else {
        linkTarget.prop('disabled', false);
    }
}

function edit_LinkChanged(event) {
    const link = event ? $(event.target) : $('#edit_Link');   // works with or without event
    const linkTarget = $('#edit_LinkTarget');

    if (link.val().trim() === '') {
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

    try {
        const result = await fetch('/Images/Add', {method: 'POST', body: data, credentials: 'same-origin'});
        if (result.ok) {
            const response = await result.text();
            $('#imagesTableBody').replaceWith(response);
            $('#addImageModal').modal('hide');
        } else {
            const error = await result.json();
            for (const key in error.errors) {
                if (key.endsWith('ImageTypeId'))
                    $('#add_ImageTypeIdError').text(error.errors[key]);
                if (key.endsWith('File'))
                    $('#add_FileError').text(error.errors[key]);
                if (key.endsWith('SortOrder'))
                    $('#add_SortOrderError').text(error.errors[key]);
                if (key.endsWith('Link'))
                    $('#add_LinkError').text(error.errors[key]);
                if (key.endsWith('LinkTarget'))
                    $('#add_LinkTargetError').text(error.errors[key]);
            }
        }
    } catch (err) {
        alert('Error occured while saving image, please try again.');
    }
}

async function edit_SaveClicked(event) {
    if (!edit_Validate()) return;

    const data = new FormData();
    data.append('__RequestVerificationToken', $("[name='__RequestVerificationToken']").val());
    data.append('ParentEntityType', $('#ParentEntityType').val());
    data.append('Id', $('#edit_Id').val());
    data.append('ParentEntityId', $('#ParentEntityId').val());
    data.append('ImageEntityType', $('#ImageEntityType').val());
    data.append('ImageTypeId', $('#edit_ImageTypeId').val());
    data.append('ImageSize', $('#edit_ImageSize').val());
    data.append('AltText', $('#edit_AltText').val());
    data.append('Title', $('#edit_Title').val());
    data.append('Loading', $('#edit_Loading').val());
    data.append('SortOrder', $('#edit_SortOrder').val());
    data.append('Link', $('#edit_Link').val());
    data.append('LinkTarget', $('#edit_LinkTarget').val());

    try {
        const result = await fetch('/Images/Edit', {method: 'POST', body: data, credentials: 'same-origin'});
        if (result.ok) {
            const response = await result.text();
            $('#imagesTableBody').replaceWith(response);
            $('#editImageModal').modal('hide');
        } else {
            const error = await result.json();
            for (const key in error.errors) {
                if (key.endsWith('ImageTypeId'))
                    $('#edit_ImageTypeIdError').text(error.errors[key]);
                if (key.endsWith('File'))
                    $('#edit_FileError').text(error.errors[key]);
                if (key.endsWith('SortOrder'))
                    $('#edit_SortOrderError').text(error.errors[key]);
                if (key.endsWith('Link'))
                    $('#edit_LinkError').text(error.errors[key]);
                if (key.endsWith('LinkTarget'))
                    $('#edit_LinkTargetError').text(error.errors[key]);
            }
        }
    } catch (err) {
        alert('Error occured while saving image, please try again.');
    }
}

async function deleteClicked(event) {
    const data = new FormData();
    data.append('__RequestVerificationToken', $("[name='__RequestVerificationToken']").val());
    data.append('ParentEntityType', $('#ParentEntityType').val());
    data.append('Id', $('#delete_Id').val());
    data.append('ParentEntityId', $('#ParentEntityId').val());
    data.append('ImageEntityType', $('#ImageEntityType').val());

    try {
        const result = await fetch('/Images/Delete', {method: 'POST', body: data, credentials: 'same-origin'});
        if (result.ok) {
            const response = await result.text();
            $('#imagesTableBody').replaceWith(response);
            $('#deleteImageModal').modal('hide');
        } else {
            const error = await result.json();
            for (const key in error.errors) {
                if (key.endsWith('ImageTypeId'))
                    $('#edit_ImageTypeIdError').text(error.errors[key]);
                if (key.endsWith('File'))
                    $('#edit_FileError').text(error.errors[key]);
                if (key.endsWith('SortOrder'))
                    $('#edit_SortOrderError').text(error.errors[key]);
                if (key.endsWith('Link'))
                    $('#edit_LinkError').text(error.errors[key]);
                if (key.endsWith('LinkTarget'))
                    $('#edit_LinkTargetError').text(error.errors[key]);
            }
        }
    } catch (err) {
        alert('Error occured while saving image, please try again.');
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

function edit_Validate() {
    let isValid = true;
    const imageTypeIdError = $('#edit_ImageTypeIdError');
    const imageSizeError = $('#edit_ImageSizeError');
    const fileError = $('#edit_FileError');
    const sortOrderError = $('#edit_SortOrderError');
    const linkError = $('#edit_LinkError');
    const linkTarget = $('#edit_LinkTargetError');

    //clear errors.
    imageTypeIdError.text('');
    imageSizeError.text('');
    fileError.text('');
    sortOrderError.text('');
    linkError.text('');
    linkTarget.text('');

    //validate.
    //Image type.
    if ($('#edit_ImageTypeId').val() === '') {
        imageTypeIdError.text('Image type is required.');
        isValid = false;
    }

    //Image size.
    if ($('#edit_ImageSize').val() === '') {
        imageSizeError.text('Image size is required.');
        isValid = false;
    }

    //Sort order.
    const sortOrder = $('#edit_SortOrder');
    if (sortOrder.val() === '') {
        sortOrderError.text('Please enter sort order.');
        isValid = false;
    } else if (!isValidNumberStrict(sortOrder.val())) {
        sortOrderError.text('Please enter a valid sort order.');
        isValid = false;
    }

    //Link.
    const link = $('#edit_Link');
    if (link.val() !== '' && !isHttpsOrRelativeUrl(link.val())) {
        linkError.text('Please enter a valid link.');
        isValid = false;
    }

    //Link target.
    if (link.val() !== '' && $('#edit_LinkTarget').val() === '') {
        linkTarget.text('Please select link target.');
        isValid = false;
    }

    return isValid;
}