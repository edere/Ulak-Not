$(function () {
    var noteids = [];

    $("div[data-addi-id]").each(function (i, e) {
        noteids.push($(e).data("addi-id"));
    });

    $("button[data-addi]").click(function () {
        var btn = $(this);
        var addi = btn.data("addi");
        var userid = btn.data("addi-id");
        var spanStar = btn.find("span.ion");
        $.ajax({
            method: "POST",
            url: "/Home/AddFriend",
            data: { "userid": userid, "addi": !addi }
        }).done(function (data) {
            console.log(data);

            if (data.hasError) {
                alert(data.errorMessage);
            } else {
                addi = !addi;
                btn.data("addi", addi);
                spanStar.removeClass("fa-thumbs-o-up");
                spanStar.removeClass("fa-thumbs-up");

                if (addi) {
                    spanStar.addClass("fa-thumbs-up");
                } else {
                    spanStar.addClass("fa-thumbs-o-up");
                }
            }
        }).fail(function () {
            alert("Sunucu ile bağlantı kurulamadı.");
        })
    });
});