$(document).ready(() => {
    $($('#Name')).on('change', name_changed);
});

function name_changed(event) {
    const name = $(event.target).val();
    const slug = toSlug(name);

    $('#Slug').val(slug);
    $('#H1').val(name);
    $('#MetaTitle').val(name);
}