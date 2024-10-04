$(document).ready(function () {
    $("#buscarArticulo").on('click', function () {
        // Obtener el valor del campo de búsqueda
        let requiredText = $("#textoArticulo").val().toLowerCase();

        // Seleccionar el contenedor de los artículos
        let container = $(".container .flex.flex-wrap");

        // Recorrer los artículos y filtrar los que coinciden
        $(".max-w-sm").each(function () {
            let title = $(this).find(".tituloArticulo").text().toLowerCase();
            let subtitle = $(this).find(".subtituloArticulo").text().toLowerCase();

            if (title.includes(requiredText) || subtitle.includes(requiredText)) {
                // Mover el artículo coincidente al inicio del contenedor
                $(this).prependTo(container);
            }
        });
    });
});
