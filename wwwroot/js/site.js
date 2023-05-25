// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// initialize with defaults

$(document).ready(function () {
    $('#albumModal').on('shown.bs.modal', function () {
        $('#myInput').focus();
    });

    $("#albumModal").on("submit", "#form-addReview", function (e) {
        e.preventDefault();  // prevent standard form submission

        var form = $(this);
        var albumName = form.find("#albumModalLabel").text().replace("Add review for ", "");
        form.find("#Subject").val(albumName);

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


