$(document).ready(function () {
    const modal = bootstrap.Modal;
    const enterCode = 13;

    var searchPhrase = $('.custom-search').val();
    var pageNumber = $('.page-link .bg-primary').data('pageNumber');
    var pageSize = $('.form-select option:selected').val();

    $(document).on('click', '.page-link', function () {
        pageNumber = $(this).data('pageNumber');
        searchPhrase = $('.custom-search').val();
        loadCarWorkshopServices(searchPhrase, pageNumber, pageSize);
    });

    $(document).on('change', '.form-select', function () {
        $('.form-select option:selected').each(function () {
            pageSize = $(this).val();
            searchPhrase = $('.custom-search').val();
            loadCarWorkshopServices(searchPhrase, pageNumber, pageSize)
        });
    });

    $(document).on('click', '.custom-search-button', function () {
        searchPhrase = $('.custom-search').val();
        loadCarWorkshopServices(searchPhrase, pageNumber, pageSize);
    });

    $(document).on('keydown', '.custom-search', function (event) {
        if (event.which === enterCode) {
            searchPhrase = $('.custom-search').val();
            loadCarWorkshopServices(searchPhrase, pageNumber, pageSize);
        }
    });

    $(document).on('search', '.custom-search', function () {
        if (this.value === '') {
            searchPhrase = null;
            loadCarWorkshopServices(searchPhrase, pageNumber, pageSize);
        }
    });

    $(document).on('click', '.custom-modal-button', function () {
        const url = $(this).data('url');
        console.log(url);
        const decodedUrl = decodeURIComponent(url);
        console.log(decodedUrl);
        
        $.get(decodedUrl).done(function (data) {
            $('#modalPlaceholder').html(data);
            modal.getOrCreateInstance($('#modalPlaceholder').find('.modal')).show();
        });
    });

    $(document).on('submit', '#editCarWorkshopServiceModal form', function (event) {
        event.preventDefault();

        $.ajax({
            url: $(this).attr('action'),
            type: 'post',
            data: $(this).serialize(),

            success: function () {
                toastr['success']('Modified car workshop service correctly');
                loadCarWorkshopServices();
                modal.getOrCreateInstance($('#modalPlaceholder').find('.modal')).hide();
            },

            error: function () {
                toastr['error']('Something went wrong...');
            }
        });
    });

    $(document).on('submit', '#deleteCarWorkshopServiceModal form', function (event) {
        event.preventDefault();

        $.ajax({
            url: $(this).attr('action'),
            type: 'post',
            data: $(this).serialize(),

            success: function () {
                toastr['success']('Removed car workshop service correctly');
                loadCarWorkshopServices();
                modal.getOrCreateInstance($('#modalPlaceholder').find('.modal')).hide();
            },

            error: function () {
                toastr['error']('Something went wrong...');
            }
        }); 
    });
});
