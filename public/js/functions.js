//$(document).ready(function(){

//});
getData();

function getData() {
    let info = document.getElementById("curiousData");

    // Cargar el JSON
    fetch('js/dailyData/dailyData.json')
        .then(response => response.json())
        .then(datos => {
            // Generar un número aleatorio entre 1001 y 1215
            let min = 1001;
            let max = 1215;
            let numeroAleatorio = Math.floor(Math.random() * (max - min + 1)) + min;
            
            // Buscar el objeto con el id generado
            let objetoConId = datos.find(item => item.id === numeroAleatorio);
            
            // Si se encuentra el objeto, mostrar el dato, de lo contrario, mostrar un error
            if (objetoConId) {
                info.innerHTML = objetoConId.dato;  // Mostrar el dato en el elemento HTML
            } else {
                console.log(`No se encontró ningún objeto con ID ${numeroAleatorio}`);
            }
        })
        .catch(error => console.error('Error al cargar el JSON:', error));
}