$(document).ready(() => {
    $('#addImageModal').on('show.bs.modal', getAddImageView);
})

async function getAddImageView() {
    const data = { parentEntityType: $('#ParentEntityType').val() };
    const response = await doAjax('/Images/Add', 'GET', data, true).promise();
}