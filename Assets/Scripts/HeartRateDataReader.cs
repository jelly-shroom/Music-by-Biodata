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
    public int timer_duration_seconds;
}

public class HeartRateDataReader : MonoBehaviour
{
    private string dataDirPath = "";
    private string dataFileName = "data.json";

    private FileDataHandler fileDataHandler;

    void Start()
    {
        fileDataHandler = new FileDataHandler(dataDirPath, dataFileName);
        string data = fileDataHandler.Load();
        List<int> bpmList = ExtractBpmFromJson(data);
        int currbpm = bpmList[bpmList.Count - 1];
        // Do something with bpmList
    }

    private List<int> ExtractBpmFromJson(string jsonData)
    {
        List<int> bpmList = new List<int>();
        HeartRateData[] heartRateDataArray = JsonUtility.FromJson<HeartRateDataArray>(jsonData).heartRateData;

        foreach (HeartRateData heartRateData in heartRateDataArray)
        {
            bpmList.Add(heartRateData.bpm);
        }

        return bpmList;
    }

    [Serializable]
    private class HeartRateDataArray
    {
        public HeartRateData[] heartRateData;
    }
}