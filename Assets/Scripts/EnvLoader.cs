using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnvLoader : MonoBehaviour
{
    private Dictionary<string, string> envVariables = new Dictionary<string, string>();

    void Start()
    {
        LoadEnvFile();
        // Example usage:
        Debug.Log("OPENAI_API_KEY: " + GetEnv("OPENAI_API_KEY"));
    }

    // Function to load the .env file
    private void LoadEnvFile()
    {
        string envPath = Path.Combine(Application.dataPath, ".env"); // Adjust this path as necessary
        if (File.Exists(envPath))
        {
            string[] lines = File.ReadAllLines(envPath);
            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line) && line.Contains("="))
                {
                    string[] keyValue = line.Split('=');
                    if (keyValue.Length == 2)
                    {
                        string key = keyValue[0].Trim();
                        string value = keyValue[1].Trim();
                        envVariables[key] = value;
                    }
                }
            }
        }
        else
        {
            Debug.LogError(".env file not found at path: " + envPath);
        }
    }

    // Function to get the value of a specific environment variable
    public string GetEnv(string key)
    {
        return envVariables.ContainsKey(key) ? envVariables[key] : null;
    }
}
