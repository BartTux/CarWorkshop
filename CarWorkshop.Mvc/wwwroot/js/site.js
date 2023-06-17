// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const loadCarWorkshopServices = (searchPhrase = null, pageNumber = 1, pageSize = 5) => {
    const container = $("#services");
    const carWorkshopEncodedName = container.data("encodedName");

    $.ajax({
        url: `/CarWorkshop/${carWorkshopEncodedName}/CarWorkshopService`,
        data: { searchPhrase: searchPhrase, pageNumber: pageNumber, pageSize: pageSize },
        type: 'GET',
        success: function (viewHtml) {
            if (!viewHtml.length)
                container.html("There is no services for this car workshop");
            else
                container.html(viewHtml);
        },
        error: function () {
            toastr["error"]("Something went wrong...");
        }
    });
}