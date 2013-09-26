function autoComplete(id, url, control, event) {
    i = 0;
    var text = $("#" + id).val();
    $.ajax({
        type: "POST",
        url: url,
        data: "{'chars':'" + text + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(data) {
            //alert(text + "/" + $("#" + id).val());
            if (text == $("#" + id).val()) {
                $("#" + id + "_div").html('');
                i = -1;
                if (data.d.length > 0) {
                    document.getElementById(id + "_div").style.visibility = "visible";
                    document.getElementById(id + "_div").style.height = "auto";
                    document.getElementById(id + "_div").style.width = document.getElementById(id).style.width;
                    var c = 0;
                    $.each(data.d, function() {
                        $("#" + id + "_div").html(
                            $("#" + id + "_div").html() +
                    "<div widht=\"100%\" id='" + id + c + "' onmouseover='this.style.color=\"white\";this.style.backgroundColor=\"#3D489B\";this.style.cursor=\"pointer\"' onmouseout='this.style.color=\"black\";this.style.backgroundColor=\"transparent\"' onclick='clickAutocomplete(\"" + id + "\",this.id)'>" + this + "</div>");
                        c++;
                        //alert($("'#" + this.ID + "_div" + "'").html());
                    });
                } else {
                    document.getElementById(id + "_div").style.visibility = "hidden";
                }
            }
        }
    });
}

function funcNavigateDiv(id, key) {
    if (document.getElementById(id + "_div").style.visibility == "visible" && $("#" + id + "_div" + "> div").size() > i && $("#" + id + "_div" + "> div").size() >= 0) {
        if (key == 40) {
            if (i < $("#" + id + "_div" + "> div").size() - 1) {
                i++;
                if (i != 0) {
                    document.getElementById(id + (i - 1)).style.color = "black";
                    document.getElementById(id + (i - 1)).style.backgroundColor = "transparent";
                }
                document.getElementById(id + (i)).style.color = "white";
                document.getElementById(id + (i)).style.backgroundColor = "#3D489B";
            }
        }
        else if (key == 38) {
            if (i != 0) {
                i--;
                if (i != $("#" + id + "_div" + "> div").size()) {
                    document.getElementById(id + (i + 1)).style.color = "black";
                    document.getElementById(id + (i + 1)).style.backgroundColor = "transparent";
                }
                document.getElementById(id + i).style.color = "white";
                document.getElementById(id + i).style.backgroundColor = "#3D489B";
            }
        }
        else if (key == 13) {
            $("#" + id).val($("#" + id + i).text());
        }
    }
}


function clickAutocomplete(tbid, id) {
    $("#" + tbid).val($("#" + id).text());
    $("#" + tbid + "_div").html('');
    document.getElementById(tbid + "_div").style.visibility = "hidden";
}

$(document).keypress(function(e) {
    if (e.keyCode === 13) {
        e.preventDefault();
        return false;
    }
});