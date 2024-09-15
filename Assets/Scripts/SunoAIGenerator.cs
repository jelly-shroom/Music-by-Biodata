using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;
using Newtonsoft.Json.Linq;

public class SunoAIGenerator : MonoBehaviour
{
    private const string API_ENDPOINT = "https://studio-api.suno.ai/api/external/generate/";
    private const string AUTH_TOKEN = "rlMNMVLqqyU1UiNkUsLRyGKbDCshsJe5"; // Replace with your actual token

    [SerializeField] private string prompt = "";
    [SerializeField] private string gptDescriptionPrompt = "a pop song in piano";
    [SerializeField] private string modelVersion = "chirp-v3-5";

    void Start()
    {
        StartCoroutine(GenerateMusic());
    }

    IEnumerator GenerateMusic()
    {
        // Prepare the request body
        string jsonBody = $"{{\"prompt\":\"{prompt}\",\"gpt_description_prompt\":\"{gptDescriptionPrompt}\",\"mv\":\"{modelVersion}\"}}";

        using (UnityWebRequest www = UnityWebRequest.PostWwwForm(API_ENDPOINT, ""))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();

            // Set headers
            www.SetRequestHeader("accept", "/");
            www.SetRequestHeader("accept-language", "en-US,en;q=0.9");
            www.SetRequestHeader("authorization", $"Bearer {AUTH_TOKEN}");
            www.SetRequestHeader("content-type", "application/json");
            www.SetRequestHeader("origin", "https://suno.com/");
            www.SetRequestHeader("referer", "https://suno.com/");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error: {www.error}");
            }
            else
            {
                string responseText = www.downloadHandler.text;
                Debug.Log($"Response: {responseText}");

                // Parse the JSON response
                JObject responseJson = JObject.Parse(responseText);
                string generationId = responseJson["id"].ToString();

                // Start polling for the audio URL
                StartCoroutine(PollForAudioUrl(generationId));
            }
        }
    }

    IEnumerator PollForAudioUrl(string generationId)
    {
        string pollUrl = $"https://studio-api.suno.ai/api/feed/v2/?ids={generationId}";

        while (true)
        {
            using (UnityWebRequest www = UnityWebRequest.Get(pollUrl))
            {
                www.SetRequestHeader("authorization", $"Bearer {AUTH_TOKEN}");

                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.Success)
                {
                    string responseText = www.downloadHandler.text;
                    JObject responseJson = JObject.Parse(responseText);

                    if (responseJson["clips"] != null && responseJson["clips"].HasValues)
                    {
                        string audioUrl = responseJson["clips"][0]["audio_url"].ToString();
                        if (!string.IsNullOrEmpty(audioUrl))
                        {
                            Debug.Log($"Audio URL: {audioUrl}");
                            // Here you can start downloading or playing the audio
                            yield break;
                        }
                    }
                }
            }

            yield return new WaitForSeconds(5f); // Wait 5 seconds before polling again
        }
    }
}