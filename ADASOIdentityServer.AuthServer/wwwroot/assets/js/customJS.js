$(document).ready(function () {
    $('form').submit(function (e) {
        var isChecked = $('#AcceptTerms').is(':checked');

        $('#AcceptTerms').change(function () {
            $(this).val($(this).is(':checked') ? 'true' : 'false');
        });

        if (!isChecked) {           
            e.preventDefault(); // Formun gönderilmesini durdur
            $('#AcceptTermsValidation').text('Kullanıcı sözleşmesini kabul etmelisiniz.');
        } else {
            $('#AcceptTermsValidation').text(''); // Uyarıyı temizle
        }
    });
});
