$(document).ready(function () {
    $("#btnEvents").click(function () {
        $('#dEvents').toggle('slow', function () {
            // Animation complete.
        });
        return false;
    });

    $("#btnComments").click(function () {
        $('#dComments').toggle('slow', function () {
            // Animation complete.
        });
        return false;
    });

    $("#btnCustomers").click(function () {
        $('#dCustomers').toggle('slow', function () {
            // Animation complete.
        });
        return false;
    });

    $("#btnConfig").click(function () {
        $('#dConfig').toggle('slow', function () {
            // Animation complete.
        });
        return false;
    });
});

