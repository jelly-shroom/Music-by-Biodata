using UnityEngine;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;

public class SunoIntegration : MonoBehaviour
{
    private static readonly HttpClient client = new HttpClient();

    public async Task<string> GenerateAudio(string moodPrompt)
    {
        var request = new
        {
            prompt = moodPrompt,
            // Add other parameters as required by Suno API
        };

        // Option 1: Using the extension method
        var response = await client.PostAsJsonAsync("https://api.suno.ai/v1/generate", request);


        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return json; // Parse audio URL or content as necessary
    }
}