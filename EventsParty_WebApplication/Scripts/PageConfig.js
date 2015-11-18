$(document).ready(function () {
    $("#dConfig").show();

    $("#btnAdd").click(function () {
        if ($('#dAdd').css("visibility") == "hidden") {
            $('#lbMsg').text('');
            $('#tbNewType').val('');
            $('#dAdd').css("visibility", "visible");
        }
        else {
            $('#dAdd').css("visibility", "hidden");
        }
        return false;
    });

   
    $(".editClass").click(function (event) {
        $tr = $(event.target).closest("tr");
        $text = $tr.children('td:nth-child(2)').text();
        $top = $tr.position().top - 2;
        $("#dEdit").css("position", "absolute").css("left", $("#gvItems").width() + 20).css("top", $top);

        if ($("#tbEditNew").val() != $text) {
            $("#tbEditNew").val($text);
            if ($("#dEdit").css("display") == "none") {
                $("#dEdit").toggle("slow");
            }
        }
        else {
            $("#dEdit").toggle("slow");
        }
        $("#hf").val($text);
        return false;
    });
});