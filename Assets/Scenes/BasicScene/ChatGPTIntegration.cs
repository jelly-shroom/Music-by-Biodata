using UnityEngine;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;

public class ChatGPTIntegration : MonoBehaviour
{
    private static readonly HttpClient client = new HttpClient();

    public async Task<string> GenerateMoodPrompt(string userInput)
    {
        var request = new
        {
            model = "gpt-3.5-turbo",
            messages = new[]
            {
                new { role = "system", content = "Generate a music prompt for Suno based on the user's mood and goal." },
                new { role = "user", content = userInput }
            }
        };

        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "YOUR_OPENAI_API_KEY");

        var response = await client.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", request);


        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return json; // Parse as necessary
    }
}