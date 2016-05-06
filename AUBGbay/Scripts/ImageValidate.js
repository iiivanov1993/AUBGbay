$(document).ready(function () {

    $('input[type="file"]').change(function () {
        var files = $('input[type="file"]')[0].files;
        if (files.length > 3) {
            $('#btnSubmit').attr("disabled", true);
            $("span[data-valmsg-for=Images]").text("Maximum images quantity is 3");
            //alert("Maximum images quantity is 3");
        }
        else {
            $("span[data-valmsg-for=Images]").text("");
            $('#btnSubmit').attr("disabled", false);
        }
    });
});
