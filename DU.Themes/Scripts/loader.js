function dataLoader() {

    var __spinnerOptions = {
        lines: 11 // The number of lines to draw
        , length: 6 // The length of each line
        , width: 13 // The line thickness
        , radius: 46 // The radius of the inner circle
        , scale: 0.75 // Scales overall size of the spinner
        , corners: 1 // Corner roundness (0..1)
        , color: '#000' // #rgb or #rrggbb or array of colors
        , opacity: 0.25 // Opacity of the lines
        , rotate: 0 // The rotation offset
        , direction: 1 // 1: clockwise, -1: counterclockwise
        , speed: 1 // Rounds per second
        , trail: 36 // Afterglow percentage
        , fps: 20 // Frames per second when using setTimeout() as a fallback for CSS
        , zIndex: 2e9 // The z-index (defaults to 2000000000)
        , className: 'spinner' // The CSS class to assign to the spinner
        , top: '50%' // Top position relative to parent
        , left: '50%' // Left position relative to parent
        , shadow: false // Whether to render a shadow
        , hwaccel: true // Whether to use hardware acceleration
        , position: 'absolute' // Element positioning
    }

    var __getWithFilter = function (url, doneCallback, errorCallback ,elementToSpin) {

        var self = this;
        self.spinner = new Spinner(__spinnerOptions).spin(elementToSpin);

        $.ajax({
            url: url,
            method: "POST",
            contentType: "application/json; UTF8",
        }).done(function (data) {
            doneCallback(data);
        }).error(function (xhr, param1, param2) {
            errorCallback(xhr, param1, param2);
        }).always(function () {
            self.spinner.stop();
        });
    }

    return {
        getWithFilter: __getWithFilter
    }
}

var loader = new dataLoader();