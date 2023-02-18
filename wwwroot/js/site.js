function buscar() {
    console.time('buscar')
    var filtro = $("#buscar").val().toUpperCase();

    $("#tabla td").each(function () {
        var textoEnTd = $(this).text().toUpperCase();
        if (textoEnTd.indexOf(filtro) >= 0) {
            $(this).addClass("existe");
        } else {
            $(this).removeClass("existe");
        }
    })

    $("#tabla tbody tr").each(function () {
        if ($(this).children(".existe").length > 0) {
            $(this).show();
        } else {
            $(this).hide();
        }
    })
    console.timeEnd('buscar');
}

function buscarEspecifico(campo) {
    console.time('buscarEspecifico');
    var filtro = $("#buscar").val().toUpperCase();
    $("#tabla #" + campo).each(
            function () {
                var textoEnTd = $(this).text().toUpperCase();
                if (textoEnTd.indexOf(filtro) >= 0) {
                    $(this).addClass("existe");
                } else {
                    $(this).removeClass("existe");
                }
            }
        )


    $("#tabla tbody tr").each(function () {
        if ($(this).children(".existe").length > 0) {
            $(this).show();
        } else {
            $(this).hide();
        }
    })
    console.timeEnd('buscarEspecifico');
}
