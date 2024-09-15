using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class EnvLoader : MonoBehaviour
{
    private static Dictionary<string, string> envVariables = new Dictionary<string, string>();

    void Start()
    {
        LoadEnv();
        string apiKey = GetEnvVariable("OPENAI_API_KEY");
        Debug.Log("OpenAI API Key: " + apiKey);  // Use your key here for API calls
    }

    // Load the .env file
    public static void LoadEnv()
    {
        string envPath = Path.Combine(Application.dataPath, ".env");
        if (File.Exists(envPath))
        {
            foreach (var line in File.ReadAllLines(envPath))
            {
                if (line.Contains("="))
                {
                    var split = line.Split(new[] { '=' }, 2);
                    envVariables[split[0]] = split[1];
                }
            }
        }
        else
        {
            Debug.LogError(".env file not found!");
        }
    }

    // Get a specific environment variable
    public static string GetEnvVariable(string key)
    {
        return envVariables.ContainsKey(key) ? envVariables[key] : null;
    }
}
