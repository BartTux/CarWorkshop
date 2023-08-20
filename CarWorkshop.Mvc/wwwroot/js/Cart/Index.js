$(document).ready(function () {
    loadCartForUser();

    const modal = bootstrap.Modal;

    $(document).on('click', '.increaseServiceInCart', function () {
        const serviceId = $(this).data('serviceId');
        const url = `/Cart/Service/${serviceId}/Increase`;
        const toastrInfo = 'The amount of service in the cart has been increased';

        sendRequest(url, 'patch', toastrInfo);
    });

    $(document).on('click', '.decreaseServiceInCart', function () {
        const serviceId = $(this).data('serviceId');

        $.ajax({
            url: `/Cart/Service/${serviceId}/Decrease`,
            type: 'patch',

            success: function () {
                loadCartForUser();
            },

            error: function () {
                toastr['error']('Something went wrong...');
            }
        });
    });

    $(document).on('click', '.deleteCartService', function () {
        const serviceId = $(this).data('serviceId');
        
        $.ajax({
            url: `/Cart/Service/${serviceId}/Delete`,
            type: 'get',

            success: function (data) {
                $('#modalPlaceholder').html(data);
                modal.getOrCreateInstance($('#modalPlaceholder').find('.modal')).show();
            }
        });
    });

    $(document).on('submit', '#DeleteCartServiceModal form', function (event) {
        event.preventDefault();

        $.ajax({
            url: $(this).attr('action'),
            type: 'delete',

            success: function () {
                toastr['success']('Successfully deleted service from the cart');
                loadCartForUser();
                modal.getOrCreateInstance($('#modalPlaceholder').find('.modal')).hide();
            },

            error: function () {
                toastr['error']('Somenthing went wrong...');
            }
        });
    });
});
