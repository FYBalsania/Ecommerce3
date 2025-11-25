$(document).ready(() => {
    $($('#Name')).on('change', name_changed);

    //Product attribute colour value
    const addImageModel = $('#addProductAttributeColourValueModal');
    addImageModel.on('show.bs.modal', show_AddProductAttributeColourView);
    addImageModel.on('hidden.bs.modal', hide_AddProductAttributeColourView);

    const editImageModel = $('#editProductAttributeColourValueModal');
    editImageModel.on('show.bs.modal', show_EditProductAttributeColourView);
    editImageModel.on('hidden.bs.modal', hide_EditProductAttributeColourView);

    const deleteImageModel = $('#deleteProductAttributeColourValueModal');
    deleteImageModel.on('show.bs.modal', show_DeleteProductAttributeColourView);
    deleteImageModel.on('hidden.bs.modal', hide_DeleteProductAttributeColourView);

    $('#addProductAttributeColour_Save').on('click', addProductAttributeColour_SaveClicked)
    $('#editProductAttributeColour_Save').on('click', editProductAttributeColour_SaveClicked)
    $('#deleteProductAttributeColour').on('click', deleteProductAttributeColourClicked)
});

function name_changed(event) {
    const name = $(event.target).val();
    const slug = toSlug(name);

    $('#Slug').val(slug);
    $('#Display').val(name);
    $('#Breadcrumb').val(name);
}

async function show_AddProductAttributeColourView(event) {}

async function show_EditProductAttributeColourView(event) {}

async function show_DeleteProductAttributeColourView(event) {}

function hide_AddProductAttributeColourView() {}

function hide_EditProductAttributeColourView() {}

function hide_DeleteProductAttributeColourView() {}

async function addProductAttributeColour_SaveClicked(event) {}

async function editProductAttributeColour_SaveClicked(event) {}

async function deleteProductAttributeColourClicked(event) {}