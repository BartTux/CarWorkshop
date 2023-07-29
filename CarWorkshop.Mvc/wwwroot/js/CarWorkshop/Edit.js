$(document).ready(function () {
    loadCarWorkshopServices();

    $("#createCarWorkshopServiceModal form").submit(function (event) {
        event.preventDefault();

        $.ajax({
            url: $(this).attr('action'),
            type: $(this).attr('method'),
            data: $(this).serialize(),

            success: function (data) {
                toastr["success"]("Created car workshop service correctly");
                loadCarWorkshopServices();
            },

            error: function (xhr) {
                if (xhr.status === 400) {
                    var errors = xhr.responseJSON;
                    displayErrors(errors);
                } else {
                    toastr["error"]("Something went wrong...");
                }
            }
        }); 
    });

    const displayErrors = errors => {
        const descriptionError = $('.description-error');
        const costError = $('.cost-error');

        descriptionError.empty();
        costError.empty();

        if (errors.hasOwnProperty("Description")) {
            descriptionError.append(errors["Description"][0]);
        }

        if (errors.hasOwnProperty("Cost")) {
            costError.append(errors["Cost"][0]);
        }
    }
});
