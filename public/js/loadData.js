import { getNum, setNum } from './sendId.js';

// Función para actualizar num cuando se haga clic en un botón
function redirect(id) {
    // Usa la función setNum para actualizar el valor de num
    setNum(id);
}

// Expone la función redirect al objeto global window para que pueda ser accedida desde el HTML
window.redirect = redirect;

// Cuando la página se carga, se ejecuta el código
window.addEventListener('load', function() {
    let h1 = document.querySelector('#h1'), 
        content = document.querySelector('#content');
    
    // Obtenemos el valor actualizado de num cuando lo necesitemos
    let num = getNum();
    console.log(num);

    fetch('dailyData/articles.json')
    .then(response => response.json())
    .then(article => {
        // Esperamos que num ya haya sido actualizado en algún momento
        let idtrue = article.find(item => item.id === num);
        if (idtrue) {
            h1.innerHTML = idtrue.h1;
            content.innerHTML = idtrue.content;
        } else {
            content.innerHTML = "<div class='w-11 bg-red-600'>Error</div>";
        }
    });
});
