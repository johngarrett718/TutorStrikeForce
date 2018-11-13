(function ($) {

    $(document).ready(() => {

        $('.saleList').on('click', function (e) {
            e.preventDefault();
            var element = $(this);
            var month = element.data('month');
            var abbreviatedMonth = element.data('monthabbr');
            var day = element.data('day');
            var year = element.data('year');
            var salesRepId = element.data('salesrepid');

            $.ajax({
                url: `/Sale/ClientSales`,
                type: "GET",
                data: { month, day, year, salesRepId }
            }).done(function (partialViewResult) {
                var newTitle = `Sales Per Day By Director Details - ${abbreviatedMonth} ${day}, ${year}`;
                $('#dialog').html(partialViewResult);
                $('#dialog').dialog({
                    title: newTitle,
                    width: '500px'
                }).dialog('open');
            });
        });

        $('#Client_Email').on('change', function (e) {
            const input = $('#Client_Email');
            const email = input.val();
            if (validateEmail(email)) {
                $.ajax({
                    url: '/Client/GetByEmail',
                    type: 'GET',
                    data: { email }
                }).done(function (results) {
                    if (results) {
                        $("#clientExistsIcon").removeClass("hidden");
                        $("#clientNotExistsIcon").addClass("hidden");
                        $("#Client_FirstName").val(results.firstName);
                        $("#Client_LastName").val(results.lastName);
                        $("#Client_City").val(results.city);
                        $("#Client_Hours").val(results.hours);
                        $("#Client_Id").val(results.clientId);
                        console.log(results);
                    } else {
                        $("#clientExistsIcon").addClass("hidden");
                        $("#clientNotExistsIcon").removeClass("hidden");
                        clearClientInputs();
                    }
                });
            } else {
                $("#clientExistsIcon").addClass("hidden");
                $("#clientNotExistsIcon").removeClass("hidden");
                clearClientInputs();
            }
        });

        function validateEmail(email) {
            var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            return re.test(String(email).toLowerCase());
        }

        function clearClientInputs() {
            $("#Client_FirstName").val("");
            $("#Client_LastName").val("");
            $("#Client_City").val("");
            $("#Client_Hours").val("");
        }
    });
})(window.jQuery);