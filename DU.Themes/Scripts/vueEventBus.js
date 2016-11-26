var bus = new Vue();

window.onkeydown = function () {

    if (event.keyCode === 27) {
        bus.$emit('window-esc-keypdown');
    }
}