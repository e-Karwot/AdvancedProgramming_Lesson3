const uri = "https://localhost:44354/api/KsiazkaItems";
let Ksiazka = null;
function getCount(data) {
    const el = $("#counter");
}
$(document).ready(function () {
    getData();
});
function getData() {
    $.ajax({
        type: "GET",
        url: uri,
        cache: false,
        success: function (data) {
            const tBody = $("#Ksiazka");
            $(tBody).empty();
            getCount(data.length);
            $.each(data, function (key, item) {
                const tr = $("<tr></tr>")
                    .append($("<td></td>").text(item.bookname))
                    .append($("<td></td>").text(item.author))
                    .append(
                        $("<td></td>").append(
                            $("<button>Edytuj</button>").on("click", function () {
                                editItem(item.id);
                            })
                        )
                    )
                    .append(
                        $("<td></td>").append(
                            $("<button>Usuń</button>").on("click", function () {
                                deleteItem(item.id);
                            })
                        )
                    );
                tr.appendTo(tBody);
            });
            Ksiazka = data;
        }
    });
}
function addItem() {
    const item = {
        bookname: $("#add-bookname").val(),
        author: $("#add-author").val()
    };
    $.ajax({
        type: "POST",
        accepts: "application/json",
        url: uri + '/CreateKsiazkaItem',
        contentType: "application/json",
        data: JSON.stringify(item),
        error: function (jqXHR, textStatus, errorThrown) {
            alert("Pola nie mogą być puste!");
        },
        success: function (result) {
            getData();
            $("#add-bookname").val(""),
            $("#add-author").val("");
        }
    });
}

function deleteItem(id) {
    $.ajax({
        url: uri + "/" + id,
        type: "DELETE",
        success: function (result) {
            getData();
        }
    });
}

function editItem(id) {
    $.each(Ksiazka, function (_key, item) {
        if (item.id === id) {
            $("#edit-bookname").val(item.bookname);
            $("#edit-author").val(item.author);
            $("#edit-id").val(item.id);
            
        }
    });
    $("#spoiler").css({ display: "block" });
}

function updateItem() {
    var id = parseInt($("#edit-id").val(), 10);
    const item = {
        id: id,
        bookname: $("#edit-bookname").val(),
        author: $("#edit-author").val()
    };
    $.ajax({
        type: "POST",
        accepts: "application/json",
        url: uri + '/UpdateKsiazkaItem',
        contentType: "application/json",
        data: JSON.stringify(item),
        error: function (jqXHR, textStatus, errorThrown) {
            alert("Pola nie mogą być puste!");
        },
        success: function (result) {
            getData();
            closeInput();
        }
    });
}

function closeInput() {
    $("#spoiler").css({ display: "none" });
}