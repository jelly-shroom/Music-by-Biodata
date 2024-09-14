using UnityEngine;

public class MainControl : MonoBehaviour
{
    private SpeechInputManager speechInputManager;
    private ChatGPTIntegration chatGPT;
    // private SunoIntegration sunoIntegration;
    private BioDataManager bioDataManager;
    private MovementTracker movementTracker;
    private AudioVisualGenerator audioVisualGenerator;

    [System.Obsolete]
    private async void Start()
    {
        speechInputManager = GetComponent<SpeechInputManager>();
        chatGPT = GetComponent<ChatGPTIntegration>();
        // sunoIntegration = GetComponent<SunoIntegration>();
        bioDataManager = GetComponent<BioDataManager>();
        movementTracker = GetComponent<MovementTracker>();
        audioVisualGenerator = GetComponent<AudioVisualGenerator>();

        speechInputManager.OnSpeechRecognized += ProcessSpeechInput;
    }

    [System.Obsolete]
    private async void ProcessSpeechInput(string userInput)
    {
        string moodPrompt = await chatGPT.GenerateMoodPrompt(userInput);
        // string audioTrack = await sunoIntegration.GenerateAudio(moodPrompt);
        string bioData = await bioDataManager.FetchBioData();
        Vector3 movementData = movementTracker.TrackMovement();

        audioVisualGenerator.GenerateAudioVisual(bioData, movementData);
    }
}