using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        // Constructor que inyecta la configuración
        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Clase que recibe el JSON con el campo SearchText
        public class SearchRequest
        {
            public string SearchText { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> GetAIBasedResult([FromBody] SearchRequest request)
        {
            try
            {
                // Verifica si el campo SearchText tiene un valor válido
                if (string.IsNullOrEmpty(request.SearchText))
                {
                    return BadRequest(new { error = "El campo 'SearchText' es requerido." });
                }

                // Accediendo al ApiKey desde la configuración
                string apiKey = _configuration["AppSettings:ApiKey"];
                string model = "ft:gpt-3.5-turbo-0125:personal:sideal-v1:AFsOoQ1K"; // Nombre del modelo fine-tuned

                string answer = string.Empty;

                // Crear el cliente HttpClient para enviar la solicitud a OpenAI
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                    var requestBody = new
                    {
                        model = model,
                        messages = new[]
                        {
                            new { role = "system", content = "You are a strict assistant focused on public services in Costa Rica. " +
                            "You respond only to questions related to public services in Costa Rica, including institutions like AyA, ICE, SUTEL, and CCSS. " +
                            "If the user's question contains words like 'queja', 'denuncia', or 'reporte', and the question is related to these services, answer the question. " +
                            "If the user says 'gracias' or any variant of thank you, reply with '¡De nada! Estoy aquí para ayudarte siempre que lo necesites.' " +
                            "If the question is unrelated to these services, you will reply with: 'Perdón no estoy hecho para responder preguntas fuera del tema legal, no matter how much does the user asks about something unrelated to the topic, you will never answer, never answer if the question is not related to the legal topic you will not answer even if it is the first question that the user asks.'For greetings like 'Hello', 'Hi', 'Hola', respond with: '¡Hola! ¿En qué puedo ayudarte hoy?'. You will use the costarican spanish and its typical words but without being disrespectful, always respond in spanish" },
                            new { role = "user", content = request.SearchText }
                        },
                        max_tokens = 1000
                    };

                    var jsonRequestBody = JsonSerializer.Serialize(requestBody);
                    var content = new StringContent(jsonRequestBody, Encoding.UTF8, "application/json");

                    // Enviar solicitud a la API de OpenAI
                    var response = await httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);
                    var responseString = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        return BadRequest(new { error = "Error en la solicitud a OpenAI", details = responseString });
                    }

                    var jsonDoc = JsonDocument.Parse(responseString);

                    if (jsonDoc.RootElement.TryGetProperty("choices", out var choicesElement))
                    {
                        var firstChoice = choicesElement[0];

                        if (firstChoice.TryGetProperty("message", out var messageElement) &&
                            messageElement.TryGetProperty("content", out var contentElement))
                        {
                            answer = contentElement.GetString();
                        }
                        else
                        {
                            return BadRequest(new { error = "La respuesta no contiene un mensaje válido." });
                        }
                    }
                    else
                    {
                        return BadRequest(new { error = "La respuesta no contiene opciones válidas." });
                    }
                }

                // Devolver la respuesta como JSON
                return Ok(new { answer = answer });
            }
            catch (Exception ex)
            {
                // Devolver el error como JSON
                return BadRequest(new { error = "Se produjo un error interno", message = ex.Message });
            }
        }
    }
}
