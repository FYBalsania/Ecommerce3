$(document).ready(function () {
    $('#Name').on('change', nameChanged);
})

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