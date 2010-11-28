$(document).ready(function () {
    $("#BeekTitleContains").autocomplete({
        source: "/Search/TitleNames/"
    });
    $("#AuthorNameContains").autocomplete({
        source: "/Search/AuthorNames/"
    });
});