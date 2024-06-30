$(function () {
    function calculateTotalPrice() {
        var startDate = new Date($('#StartDate').val());
        var endDate = new Date($('#EndDate').val());
        var pricePerNight = parseFloat($('#PricePerNight').val());
        var totalPrice = 0;

        if (startDate && endDate && startDate < endDate) {
            var timeDifference = Math.abs(endDate - startDate);
            var daysDifference = Math.ceil(timeDifference / (1000 * 3600 * 24));
            totalPrice = daysDifference * pricePerNight;
        }

        $('#TotalPrice').val(totalPrice.toFixed(2));
    }

    $('#StartDate, #EndDate').on('change', function () {
        calculateTotalPrice();
    });
});
