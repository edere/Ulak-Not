$(function () {
    var friendids = [];
    $("div[data-addi-id]").each(function (i, e) {
        friendids.push($(e).data("addi-id"));
    });
    console.log(friendids);

    $.ajax({
        method: "POST",
        url: "/Home/GetFriend",
        data: { ids: friendids }
    }).done(function (data) {
        if (data.result != null && data.result.length > 0) {
            for (var i = 0; i < data.result.length; i++) {
                var id = data.result[i];
                var userFriend = $("div[data-addi-id=" + id + "]");
                var btn = userFriend.find("button[data-addi]");
                var span = btn.children.first();

                btn.data("addi", true);
                //span.text('eklendi');
                //span.textContent = "İstek Gönderildi!";
                document.getElementById("add_friend").textContent = "İstek Gönderildi";
            }
        }
    }).fail(function () {
    });

    $("button[data-addi]").click(function () {
        var btn = $(this);
        var addi = btn.data("addi");
        var userid = btn.data("addi-id");
        var spanText = btn.find("span.ion");
        $.ajax({
            method: "POST",
            url: "/Home/AddFriend",
            data: { "userid": userid, "addi": !addi }
        }).done(function (data) {
            if (data.hasError) {
                alert(data.errorMessage);
            } else {
                addi = !addi;
                btn.data("addi", addi);

                if (addi) {
                    document.getElementById("add_friend").textContent = "İstek Gönderildi";
                    //spanText.text = " Arkadaşlık İsteği Gönderildi";
                } else {
                    document.getElementById("add_friend").textContent = "Arkadaş Ekle";
                    //spanText.text = " Arkadaş Ekle";
                }
            }
        }).fail(function () {
            alert("Sunucu ile bağlantı kurulamadı!");
        });
    });
});