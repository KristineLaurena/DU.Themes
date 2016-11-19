var bootstrap = require('bootstrap'),   
    jquery = require('../bower_components/jquery/dist/jquery'),
    adminlte = require('../bower_components/AdminLTE/dist/js/app');


$('[data-walkthrough]').click(function (evt) {
    var value = evt.target.nodeName === 'I' ? evt.target.parentNode.attributes['href'].value : evt.target.attributes['href'].value;
    $('[href=\'' + value + '\']').tab('show');
})