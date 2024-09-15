using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[Serializable]
public class HeartRateData
{
    public int bpm;
    public string timestamp;
    public int context;
    public int? timer_duration_seconds;
}

[Serializable]
public class HeartRateDataContainer
{
    public HeartRateData[] heart_rate;  // Match the key in the JSON file
}


public class HeartRateDataReader : MonoBehaviour
{
    // private string dataDirPath = "/Users/mcwiradharma/Library/Mobile Documents/com~apple~CloudDocs/Downloads/hackmit/Music-by-Biodata";
    // private string dataFileName = "data.json";

    // private FileDataHandler fileDataHandler;
    public int currbpm = 0;
    void Start()
    {
        var textAsset = Resources.Load<TextAsset>("data");
        string data = textAsset.text.ToString();
        // fileDataHandler = new FileDataHandler(dataDirPath, dataFileName);
        // string data = fileDataHandler.Load();
        List<int> bpmList = ExtractBpmFromJson(data);
        currbpm = bpmList[bpmList.Count - 1];
        // Do something with bpmList
        Debug.Log("BPM: " + currbpm);
    }

    private List<int> ExtractBpmFromJson(string jsonData)
    {
        List<int> bpmList = new List<int>();
        HeartRateDataContainer heartRateDataContainer = JsonUtility.FromJson<HeartRateDataContainer>(jsonData);

        // Check if the container and heart_rate array are not null
        if (heartRateDataContainer != null && heartRateDataContainer.heart_rate != null)
        {
            foreach (HeartRateData heartRateData in heartRateDataContainer.heart_rate)
            {
                bpmList.Add(heartRateData.bpm);
            }
        }

        return bpmList;
    }
}