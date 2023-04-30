// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// initialize with defaults
$("#input-id").rating();

// with plugin options (do not attach the CSS class "rating" to your input if using this approach)
$("#input-id").rating({ 'size': 'lg' });

$(document).ready(function () {

    $('#albumModal').on('shown.bs.modal', function () {
        $('#myInput').focus()
    })

    $("#albumModal").on("submit", "#form-addReview", function (e) {
        e.preventDefault();  // prevent standard form submission

        var form = $(this);
        $.ajax({
            url: form.attr("action"),
            method: form.attr("method"),  // post
            data: form.serialize(),
            success: function (partialResult) {
                $("#formContent").html(partialResult);
            }
        });
    });
});