$(document).ready(function () {
    $("#buscarArticulo").on('click', function () {
        // Obtener el valor del campo de búsqueda
        let requiredText = $("#textoArticulo").val().toLowerCase();

        // Filtrar los elementos que contienen el texto
        $(".tituloArticulo").filter(function () {
            return $(this).text().toLowerCase().includes(requiredText);
        }).each(function () {
            
            $(this).closest('.max-w-sm').prependTo('.flex.justify-center.space-x-6:first');
        });
        // Filtrar los elementos que contienen el texto
        $(".subtituloArticulo").filter(function () {
            return $(this).text().toLowerCase().includes(requiredText);
        }).each(function () {
            
            $(this).closest('.max-w-sm').prependTo('.flex.justify-center.space-x-6:first');
        });
    });
});
