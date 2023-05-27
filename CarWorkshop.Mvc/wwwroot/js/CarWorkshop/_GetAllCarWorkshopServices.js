$(document).ready(function () {
    const modal = bootstrap.Modal;

    var pageNumber = $('.page-link .bg-primary').data('pageNumber');
    var pageSize = $('.form-select option:selected').val();

    $(document).on('click', '.page-link', function () {
        pageNumber = $(this).data('pageNumber');
        loadCarWorkshopServices(pageNumber, pageSize);
    });

    $(document).on('change', '.form-select', function () {
        $('.form-select option:selected').each(function () {
            pageSize = $(this).val();
            loadCarWorkshopServices(pageNumber, pageSize)
        });
    });

    $(document).on('click', '.custom-modal-button', function () {
        const url = $(this).data('url');
        const decodedUrl = decodeURIComponent(url);

        $.get(decodedUrl).done(function (data) {
            $('#modalPlaceholder').html(data);
            modal.getOrCreateInstance($('#modalPlaceholder').find('.modal')).show();
        });
    });

    $(document).on('submit', '#deleteCarWorkshopServiceModal form', function (event) {
        event.preventDefault();

        $.ajax({
            url: $(this).attr('action'),
            type: 'post',
            data: $(this).serialize(),

            success: function () {
                toastr["success"]("Removed car workshop service correctly");
                loadCarWorkshopServices();
                modal.getOrCreateInstance($('#modalPlaceholder').find('.modal')).hide();
            },

            error: function () {
                toastr["error"]("Something went wrong...");
            }
        }); 
    });
});
