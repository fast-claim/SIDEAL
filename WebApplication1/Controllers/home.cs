using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using dotenv.net;
using dotenv.net.Utilities;
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
                
                


                
                string apiKey = "sk-xce5K8EYNafVPvUQzpUfgjLfMdTJPpTwNV5-VXujaZT3BlbkFJleGrQrXLKVT9kWrxF6qDGW1phoQSW1bNN2aJnInRAA";


                string model = "gpt-3.5-turbo-0125"; // Modelo de chat
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

                    var response = await httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);
                    var responseString = await response.Content.ReadAsStringAsync();

                    // Procesar la respuesta
                    var jsonDoc = JsonDocument.Parse(responseString);
                    var completionText = jsonDoc.RootElement
                                            .GetProperty("choices")[0]
                                            .GetProperty("message")
                                            .GetProperty("content")
                                            .GetString();

                    answer = completionText;
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
