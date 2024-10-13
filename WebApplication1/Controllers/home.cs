using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> GetAIBasedResult(string SearchText)
        {
            try
            {
                string apiKey = "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"; // Coloca tu API Key de OpenAI aquí
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

                    // Verificar si la solicitud fue exitosa
                    if (!response.IsSuccessStatusCode)
                    {
                        return BadRequest($"Error en la solicitud: {responseString}");
                    }

                    // Procesar la respuesta y manejar excepciones si las claves no están presentes
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
                            // Manejar el caso donde no se encuentra el campo "message" o "content"
                            return BadRequest("La respuesta no contiene un mensaje válido.");
                        }
                    }
                    else
                    {
                        // Manejar el caso donde no se encuentra el campo "choices"
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
