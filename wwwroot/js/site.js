// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// initialize with defaults


//TODO : TO BE IMPROVED
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

//$(document).ready(function () {
//    $('#albumModal').on('shown.bs.modal', function () {
//        $('#myInput').focus();
//    });

//    $("#albumModal").on("submit", "#form-addReview", function (e) {
//        e.preventDefault();  // prevent standard form submission

//        var form = $(this);
//        $.ajax({
//            url: form.attr("action"),
//            method: form.attr("method"),  // post
//            data: form.serialize(),
//            success: function (response) {
//                if (response.success) {
//                    // Review submission was successful
//                    // Perform any desired actions (e.g., show a success message, update the UI)
//                    // You can also reload or update specific sections of the page if needed
//                    alert("Review submitted successfully!");
//                    // Example: Reloading the review list
//                    $("#reviewListContainer").load("/Reviews/ReviewList"); // Assuming you have a container element with the id "reviewListContainer"
//                    // You can add more actions here as needed
//                    // Redirect to the album page
//                    window.location.href = '/Albums/?name=' + response.albumName;
//                } else {
//                    // Review submission failed
//                    // Handle the failure (e.g., show an error message, highlight form fields)
//                    alert("Failed to submit review!");
//                }
//                // Close the modal after submission
//                $('#albumModal').modal('hide');
//            },
//            error: function () {
//                // An error occurred during the AJAX request
//                // Handle the error (e.g., show an error message)
//                alert("An error occurred!");
//                // Close the modal after submission
//                $('#albumModal').modal('hide');
//            }
//        });
//    });
//});



