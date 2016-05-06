$(document).ready(function () {
    $("#showContactForm").click(function () {
        $("#contactForm").toggle("fast");
    });
});

function OnSuccess(response) {
    $("#email").val('');
    $("#emailResult").show();
    $("#emailResult").text("Email Sent Successfully");
    $("#emailResult").hide(5000);
    //$("#contactForm").hide("fast");
}