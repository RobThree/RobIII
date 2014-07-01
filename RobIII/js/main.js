$(document).ready(function () {
    // Lightbox
    $(document).delegate('*[data-toggle="lightbox"]', 'click', function (event) {
        event.preventDefault();
        $(this).ekkoLightbox();
    });

    // Handle contact-form submit
    $("#contact-form").on("submit", function (event) {
        event.preventDefault();
        $('body').addClass('wait');             // Make sure the user gets some 'progress' feedback
        var f = $(this);                        // Get reference to form
        var data = f.serialize();               // Get data to post
        var inputs = f.find(':input');          // Get all form inputs...
        inputs.attr('disabled', 'disabled');    // ...and disable them

        // Hide any (old) alerts and when done...
        f.children('.alert').fadeOut().promise().done(function () {
            // ...remove them from the DOM
            f.remove('.alert');

            // Now post the data
            $.post('/api/contactform', data).done(function (result) {
                // We should receive an array of errors; when it's length == 0 we have no errors
                if (result.length === 0) {
                    // Hide alle form groups; no need to fill out a form twice
                    f.find('.form-group, p').slideUp();
                    // Tell user everything was OK
                    f.prepend($('<div class="alert alert-success">Your message has beent sent.</div>').hide().fadeIn());
                } else {
                    // Build a list (<ul>) of error messages
                    var erl = $('<ul>');
                    $.each(result, function (i, msg) {
                        $('<li />', { text: msg }).appendTo(erl);
                    });

                    // Show error to user
                    f.prepend($('<div class="alert alert-warning"></div>')
                        .append('<p>Error sending form:</p>')
                        .append(erl)
                        .hide().fadeIn());

                    // Re-enable inputs
                    inputs.removeAttr('disabled');
                }
            }).fail(function (e) {
                // Server error (404/500 etc.)
                f.prepend($('<div class="alert alert-danger"></div>').text(e.status + ": " + e.statusText).hide().fadeIn());
            }).always(function () {
                // Error or success, we can new stop the 'in progress' feedback
                $('body').removeClass('wait');
            });
        });
    });

    $('.js').show();
});