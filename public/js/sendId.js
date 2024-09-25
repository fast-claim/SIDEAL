// Inicializamos la variable num
let num = null;

// Función que actualiza el valor de num
function setNum(id) {
    num = id;
}

// Función que devuelve el valor actualizado de num
function getNum() {
    return num;
}

// Exportamos las funciones
export { setNum, getNum };