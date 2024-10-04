$(document).ready(function () {
    // Función para buscar artículos
    $("#buscarArticulo").on('click', function () {
        // Obtener el valor del campo de búsqueda
        let requiredText = $("#textoArticulo").val().toLowerCase();

        // Recorrer los artículos y mostrar/ocultar según la coincidencia
        $(".max-w-sm").each(function () {
            let title = $(this).find(".tituloArticulo").text().toLowerCase();
            let subtitle = $(this).find(".subtituloArticulo").text().toLowerCase();

            // Si el título o el subtítulo contienen el texto buscado, mostrar el artículo
            if (requiredText !== "" && (title.includes(requiredText) || subtitle.includes(requiredText))) {
                $(this).show();
            } else {
                $(this).hide();
            }
        });
    });

    // Función para mostrar todos los artículos nuevamente
    $("#mostrarTodo").on('click', function () {
        // Mostrar todos los artículos
        $(".max-w-sm").each(function () {
            $(this).show();  // Mostrar todos los artículos
        });
    });
});
