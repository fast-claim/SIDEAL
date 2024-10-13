let chatBot = 'Hola';
let url = `https://localhost:59716/api/Home/${chatBot}`;

async function fetchChatBotResponse() {
    try {
        const response = await fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json' 
            },
            body: JSON.stringify({ SearchText: chatBot }) 
        });

        if (!response.ok) {
            throw new Error(`Error en la solicitud: ${response.status}`);
        }

        const data = await response.json();
        console.log(data);
    } catch (error) {
        console.error('Error detectado:', error.message);
    }
}

fetchChatBotResponse();
