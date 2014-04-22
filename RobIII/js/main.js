$(document).ready(function () {
    $('#nav a').click(function (e) {
        e.preventDefault();
        $(this).tab('show');
    });
    $('#nav-tabs .tab-pane:empty').append($('<div>').addClass('loader').append($('<i>').addClass('glyphicon glyphicon-refresh')));
});