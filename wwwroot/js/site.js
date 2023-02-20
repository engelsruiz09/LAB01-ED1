// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

$(document).ready(function () {
    // Obtenemos el formulario y el elemento que deseamos hacer aparecer
    var myForm = $('#my-form');
    var elementToShow = $('#element-to-show');

    // Agregamos un controlador de eventos al formulario
    myForm.submit(function (event) {
        event.preventDefault(); // Evitamos que se envíe el formulario
        elementToShow.show(); // Hacemos aparecer el elemento
    });
});