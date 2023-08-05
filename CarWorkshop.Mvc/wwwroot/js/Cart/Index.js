$(document).ready(function () {
    loadCartForUser();

    $(document).on('click', '.increaseServiceInCart', function () {
        const serviceId = $(this).data('serviceId');
        const [cartId, carWorkshopServiceId] = serviceId.split(',').map(id => parseInt(id.trim(), 10));

        $.ajax({
            url: `/Cart/${cartId}/Services/${carWorkshopServiceId}/Increase`,
            type: 'patch',

            success: function () {
                loadCartForUser();
            },

            error: function () {
                toastr['error']('Something went wrong...');
            }
        });
    });
});
