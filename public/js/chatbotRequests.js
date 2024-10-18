let chatBotElement = document.getElementById('textoChatbot'); 
let respuesta = document.getElementById('respuesta'); 
let url = `https://localhost:59716/api/Home`;

async function fetchChatBotResponse() {
    try {
        
        const chatBotText = chatBotElement.value;

        const response = await fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ SearchText: chatBotText })  
        });

        if (!response.ok) {
            throw new Error(`Error en la solicitud: ${response.status}`);
        }

        const data = await response.json();
        console.log(data.answer);
        respuesta.innerHTML = data.answer;  
    } catch (error) {
        console.error('Error detectado:', error.message);
    }
}


chatBotElement.addEventListener('keydown', function(event) {
    if (event.key === 'Enter') {
        event.preventDefault();  
        fetchChatBotResponse();  
    }
});
