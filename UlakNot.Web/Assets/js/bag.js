$(function () {
    var noteids = [];

    $("div[data-note-id]").each(function (i, e) {
        noteids.push($(e).data("note-id"));
    });

    $.ajax({
        method: "POST",
        url: "/Note/GetBag",
        data: { id_bag: noteids }
    }).done(function (data) {
        if (data.result != null && data.result.length > 0) {
            for (var i = 0; i < data.result.length; i++) {
                var id = data.result[i];
                var bagNote = $("div[data-note-id=" + id + "]");
                var btn = bagNote.find("button[data-baged]");
                var span = btn.find("span.icon");

                btn.data("baged", true);
                span.removeClass("fa-share-square-o");
                span.addClass("fa-share-square");
            }
        }
    }).fail(function () {
    });

    $("button[data-baged]").click(function () {
        var btn = $(this);
        var baged = btn.data("baged");
        var noteid = btn.data("note-id");
        var spanStar = btn.find("span.icon");
        var spanCount = btn.find("span.bag-count");

        console.log(baged);
        console.log("bag count : " + spanCount.text());

        $.ajax({
            method: "POST",
            url: "/Note/InTheBag",
            data: { "noteid": noteid, "baged": !baged }
        }).done(function (data) {
            console.log(data);

            if (data.hasError) {
                alert(data.errorMessage);
            } else {
                baged = !baged;
                btn.data("baged", baged);
                spanCount.text(data.result);

                console.log("bag count(after) : " + spanCount.text());

                spanStar.removeClass("fa-share-square-o");
                spanStar.removeClass("fa-share-square");

                if (baged) {
                    spanStar.addClass("fa-share-square");
                } else {
                    spanStar.addClass("fa-share-square-o");
                }
            }
        }).fail(function () {
            alert("Sunucu ile bağlantı kurulamadı.");
        })
    });
});