/* Script for: Importar
 */

/* On ready */
$(function () {

    $('#loadFilesBtn').click(function () {
        $('#defaultModal').show();
    });

    $("#botonCargarArchivos").click(function () {
        function explode() {
            $("#defaultModal").show();
        }
        setTimeout(explode, 2000);
    });

});