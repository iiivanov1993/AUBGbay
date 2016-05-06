$(document).ready(function () {

    $("#imageChooser img").click(function () {
        var src = $(this).attr('src');
        $("#item-display").attr("src", src);

    });

});