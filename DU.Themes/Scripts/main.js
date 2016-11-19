require('expose?$!expose?jQuery!../bower_components/jquery/dist/jquery');

$('[data-walkthrough]').click(function (evt) {
    var value = evt.target.nodeName === 'I' ? evt.target.parentNode.attributes['href'].value : evt.target.attributes['href'].value;
    $('[href=\'' + value + '\']').tab('show');
})
