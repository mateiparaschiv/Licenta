// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
window.addEventListener('DOMContentLoaded', (event) => {
    document.querySelectorAll("#deleteReviewLink").forEach(item => {
        item.addEventListener("click", function (e) {
            e.preventDefault(); // Prevents the default action (navigating to a new page)
            let reviewId = this.getAttribute("data-review-id");

            fetch(`/Reviews/DeleteReview?reviewId=${reviewId}`, {
                method: 'POST'
            }).then(response => {
                if (!response.ok) {
                    console.error('Failed to delete review');
                } else {
                    // Remove the deleted review from the DOM, or reload the page, etc.
                    location.reload();
                }
            });
        });
    });
});

$(document).ready(function () {
    $('#Message').keydown(function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            $(this).closest('form').submit();
        }
    });
});