
    $(function () {
        $("#txtSearch").autocomplete({
            source: '@Url.Action("GetSearchNotes")',
            minLength: 1
        });
    });
