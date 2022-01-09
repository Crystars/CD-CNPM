function update_minicart() {
    var sendurl = '/api/OrderDetailAPI/';
    $.ajax({
        type: "PUT",
        url: sendurl,
        cache: false,
        contentType: false,
        processData: false,
        success: function (cartCount) {
            // update minicart-number
            $('.minicart-counter').eq(0).html(cartCount);
        },
        error: function (data) {
            alert(data.responseText);
        }
    });
};

$(document).ready(function () {
    update_minicart();
});