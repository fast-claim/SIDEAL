function sendMail() {
    /* let parms = {
       name: document.getElementById("nombre").value,
        apellido: document.getElementById("apellido").value,
        correo: document.getElementById("email").value, // Asegúrate de que este id sea "email"
        //telefono: document.getElementById("telefono").value,
        sugerencia: document.getElementById("sugerencia").value,
    };*/

    // Enviar el correo original
    //emailjs.send("sideal", "template_zxfb42q", parms).then(alert("El correo se ha mandado correctamente"));
    let name = document.getElementById("nombre").value,
    apellido = document.getElementById("apellido").value,
    correo = document.getElementById("email").value,
    sugerencia = document.getElementById("sugerencia").value
    console.log(name)

    emailjs.send("sideal","template_zxfb42q",{
        name: name,
        apellido:apellido ,
        correo: correo,
        sugerencia:sugerencia,
    });

       
}
