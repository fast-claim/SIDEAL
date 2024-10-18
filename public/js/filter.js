document.addEventListener('DOMContentLoaded', function () {
    // Seleccionamos los botones de filtrado y los contenedores de los elementos a filtrar
    const filterButtons = document.querySelectorAll('button[data-filter]');
    const articles = document.querySelectorAll('[data-institucion]');

    filterButtons.forEach(button => {
        button.addEventListener('click', function () {
            const filter = this.getAttribute('data-filter');

            articles.forEach(article => {
                const institution = article.getAttribute('data-institucion');

                if (filter === 'all' || institution === filter) {
                    article.style.display = 'flex'; // Mostrar el artículo
                } else {
                    article.style.display = 'none'; // Ocultar el artículo
                }
            });
        });
    });
});