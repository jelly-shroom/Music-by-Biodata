using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechInputManager : MonoBehaviour
{
    private DictationRecognizer dictationRecognizer;

    public delegate void SpeechRecognizedHandler(string recognizedText);
    public event SpeechRecognizedHandler OnSpeechRecognized;

    void Start()
    {
        StartDictationEngine();
    }

    private void StartDictationEngine()
    {
        dictationRecognizer = new DictationRecognizer();
        dictationRecognizer.DictationResult += OnDictationResult;
        dictationRecognizer.Start();
    }

    private void OnDictationResult(string text, ConfidenceLevel confidence)
    {
        OnSpeechRecognized?.Invoke(text);
    }
}