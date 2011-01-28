$(document).ready(function () {
    $("#BeekTitleContains").autocomplete({
        source: ResolveUrl("/Search/TitleNames")
    });
    $("#AuthorNameContains").autocomplete({
        source: ResolveUrl("/Search/AuthorNames/")
    });
});

function ResolveUrl(url) {
    return ($('#applicationRoot').text() + url).replace("//", "/");
}