using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using dotenv.net;

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

        [HttpPost]
        public async Task<IActionResult> GetAIBasedResult(string SearchText)
        {
            try
            {
                // Accediendo al ApiKey desde la configuración
                string apiKey = _configuration["AppSettings:ApiKey"];
                string model = "ft:gpt-3.5-turbo-0125:personal:sideal-v1:AFsOoQ1K"; // Nombre del modelo fine-tuned

                string answer = string.Empty;

                // Crear el cliente HttpClient
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                    var requestBody = new
                    {
                        model = model,
                        messages = new[]
                        {
                            new { role = "system", content = "You are a helpful assistant." },
                            new { role = "user", content = SearchText }
                        },
                        max_tokens = 400
                    };

                    var jsonRequestBody = JsonSerializer.Serialize(requestBody);
                    var content = new StringContent(jsonRequestBody, Encoding.UTF8, "application/json");

                    // Enviar solicitud a la API de OpenAI
                    var response = await httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);
                    var responseString = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        return BadRequest($"Error en la solicitud: {responseString}");
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
                            return BadRequest("La respuesta no contiene un mensaje válido.");
                        }
                    }
                    else
                    {
                        return BadRequest("La respuesta no contiene opciones válidas.");
                    }
                }

                return Ok(answer);
            }
            catch (Exception ex)
            {
                return BadRequest($"Razon del error: {ex.Message}");
            }
        }
    }
}
