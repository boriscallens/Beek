$(function() {
    
    $('#titleStartsWith').keyup(function(event) {
        $.idle(function() {
            jQuery.getJSON("test", ($('#titleStartsWith').val());
        }, 700);
    });
});