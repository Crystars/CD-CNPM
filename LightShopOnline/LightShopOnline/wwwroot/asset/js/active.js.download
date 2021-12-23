(function($) {
    "use strict";

    var initSwitchCurrency = function() {
        $('body').on('click', '.currency-dropdown li a', function() {
            var currency = $(this).data('currency');
            $.cookie('parlo_currency', currency, {
                path: '/'
            });
            location.reload();
            $(document.body).trigger('wc_fragment_refresh');
        });
    }
    initSwitchCurrency();

})(jQuery);