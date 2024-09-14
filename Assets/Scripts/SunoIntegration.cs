using UnityEngine;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

public class SunoIntegration : MonoBehaviour
{
    private readonly HttpClient client = new HttpClient();
    private const string SUNO_API_ENDPOINT = "https://api.suno.ai/v1/generate"; // Replace with actual Suno API endpoint

    public async Task<string> GenerateAudio(string userInput)
    {
        Debug.Log($"SunoIntegration: Generating audio for input: {userInput}");

        var request = new
        {
            prompt = userInput,
            // Add other parameters as required by Suno API
        };

        var jsonContent = JsonConvert.SerializeObject(request);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        try
        {
            var response = await client.PostAsync(SUNO_API_ENDPOINT, content);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Debug.Log($"SunoIntegration: Received response from Suno: {responseString}");
            return responseString; // This should be the audio track URL or content
        }
        catch (HttpRequestException e)
        {
            Debug.LogError($"SunoIntegration: Error in Suno request: {e.Message}");
            return null;
        }
    }
}