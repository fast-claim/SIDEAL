let chatBotElement = document.getElementById('textoChatbot');
let chatContainer = document.getElementById('chatContainer');
let url = `https://localhost:59716/api/Home`;

async function fetchChatBotResponse() {
    try {
        const chatBotText = chatBotElement.value;

        if (chatBotText.trim() === "") {
            return;
        }

        // Mensaje del usuario
        let defaultMessage = document.getElementById('firstMessage');
        defaultMessage.innerHTML = ''
        let userMessageDiv = document.createElement('div');
        userMessageDiv.className = 'bg-gray-300 text-base sm:text-lg max-w-xl w-auto mt-3 rounded-2xl p-4 text-right text-black self-end break-words shadow-md';
        userMessageDiv.innerHTML = chatBotText;
        chatContainer.appendChild(userMessageDiv);

        chatBotElement.value = '';

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

        // Respuesta del bot
        
        let botResponseDiv = document.createElement('div');
        botResponseDiv.className = 'bg-blue-600 text-base sm:text-lg max-w-xl w-auto mt-3 rounded-2xl p-4 text-left text-white self-start break-words shadow-md'; // Alineado a la izquierda
        botResponseDiv.innerHTML = data.answer;
        
        chatContainer.appendChild(botResponseDiv);

        // Hacer scroll al final del contenedor para mostrar el último mensaje
        chatContainer.scrollTop = chatContainer.scrollHeight;
        chatContainer.className = 'flex flex-col p-4 h-full overflow-y-auto mb-24'; // Contenedor flexible con scroll


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