$(document).ready(function () {
    // Almacenar el orden original de los artículos
    let originalOrder = $("#articlesContainer").children(".w-full").toArray();

    // Función para buscar y reordenar artículos
    $("#buscarArticulo").on('click', function () {
        // Obtener el valor del campo de búsqueda y convertirlo a minúsculas
        let requiredText = $("#textoArticulo").val().toLowerCase().trim();

        // Referencia al contenedor de artículos
        let container = $("#articlesContainer");

        // Arrays para almacenar artículos coincidentes y no coincidentes
        let matched = [];
        let unmatched = [];

        // Iterar sobre cada artículo para verificar coincidencias
        container.children(".w-full").each(function () {
            let title = $(this).find(".tituloArticulo").text().toLowerCase();
            let subtitle = $(this).find(".subtituloArticulo").text().toLowerCase();

            if (requiredText !== "" && (title.includes(requiredText) || subtitle.includes(requiredText))) {
                matched.push(this); // Artículo coincide
            } else {
                unmatched.push(this); // Artículo no coincide
            }
        });

       
        container.empty();

        if (requiredText === "") {
            
            $.each(originalOrder, function (index, elem) {
                container.append(elem);
                $(elem).show();
            });
        } else if (matched.length > 0) {
            
            $.each(matched, function (index, elem) {
                container.append(elem);
                $(elem).show();
            });

            
           
            $.each(unmatched, function (index, elem) {
                container.append(elem);
                $(elem).hide();
            });
            
        } else {
            container.append('<p class="w-full text-center text-red-500">No se encontraron artículos que coincidan con tu búsqueda.</p>');
        }
    });

    // Función para mostrar todos los artículos nuevamente en su orden original
    $("#mostrarTodo").on('click', function () {
        let container = $("#articlesContainer");
        container.empty();
        $.each(originalOrder, function (index, elem) {
            container.append(elem);
            $(elem).show();
        });
    });

    // Opcional: Permitir la búsqueda al presionar "Enter" en el campo de búsqueda
    $("#textoArticulo").on('keypress', function (e) {
        if (e.which === 13) { // 13 es el código de la tecla "Enter"
            $("#buscarArticulo").click();
        }
    });
});
