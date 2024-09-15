using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using Newtonsoft.Json;

public class ChatGPTIntegration : MonoBehaviour
{
    private EnvLoader envLoader;
    private string apiUrl = "https://api.openai.com/v1/chat/completions"; // For ChatGPT API
    private string apiKey;

    void Start()
    {
        envLoader = FindObjectOfType<EnvLoader>();

        // Check if EnvLoader was found
        if (envLoader != null)
        {
            GenerateMoodPrompt("I like soothing mellow music I can meditate to.");
        }
        else
        {
            Debug.LogError("EnvLoader not found in the scene. Make sure it is attached to a GameObject.");
        }
    }

    public async Task GenerateMoodPrompt(string userInput)
    {
        envLoader = FindObjectOfType<EnvLoader>();
        apiKey = envLoader.GetEnv("OPENAI_API_KEY");
        // Example: send a message to ChatGPT
        Debug.Log("OPENAI_API_KEY: " + apiKey);
        // userInput = "I like soothing mellow music I can meditate to.";

        // Setup request payload
        var requestData = new
        {
            model = "gpt-4o-mini", // or "gpt-4" depending on your access
            messages = new[]
            {
                new { role = "system", content = "Generate a music prompt for Suno based on the user's mood and goal." },
                new { role = "user", content = userInput } 
            },
            max_tokens = 200 // Response length
        };
        
        string json = JsonConvert.SerializeObject(requestData);
        byte[] postData = Encoding.UTF8.GetBytes(json);

        using (UnityWebRequest request = new UnityWebRequest(apiUrl, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(postData);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + apiKey);

            // Send the request and await the response
            UnityWebRequestAsyncOperation operation = request.SendWebRequest();
            await AwaitRequest(operation);

            // Handle the response
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + request.error);
            }
            else
            {
                // Parse the response
                string jsonResponse = request.downloadHandler.text;
                Debug.Log("Response: " + jsonResponse);

                // Optionally, process the response data as needed
                ProcessResponse(jsonResponse);
            }
        }
    }

    // Helper method to convert UnityWebRequestAsyncOperation to Task
    private Task AwaitRequest(UnityWebRequestAsyncOperation operation)
    {
        var taskCompletionSource = new TaskCompletionSource<bool>();

        operation.completed += _ => 
        {
            taskCompletionSource.SetResult(true);
        };

        return taskCompletionSource.Task;
    }

    // Method to process the response as needed
    private void ProcessResponse(string jsonResponse)
    {
        // You can process the response here
        // e.g., parse it into a model or extract relevant information
        Debug.Log("Processed response: " + jsonResponse);
    }
}