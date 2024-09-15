using UnityEngine;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;

/*
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
} */
/*
public class SunoAIService : MonoBehaviour
{
    private string sunoAPIKey;

    void Start()
    {
        sunoAPIKey = EnvLoader.GetSunoAIKey();
    }

    public void GenerateMusic(string prompt)
    {
        StartCoroutine(CallSunoAI(prompt));
    }

    IEnumerator CallSunoAI(string prompt)
    {
        string requestBody = JsonUtility.ToJson(new
        {
            prompt = prompt
        });

        using (UnityWebRequest request = new UnityWebRequest("https://studio-api.suno.ai/api/external/generate/", "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(requestBody);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + sunoAPIKey);

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string responseText = request.downloadHandler.text;
                Debug.Log("Generated Music: " + responseText);
                // Handle the generated music (e.g., play it or download it)
            }
            else
            {
                Debug.LogError("Error: " + request.error);
            }
        }
    }
}
*/