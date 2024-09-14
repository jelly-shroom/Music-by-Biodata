using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;

public class MainControl : MonoBehaviour
{
    public TMP_InputField userInputField;
    public Button submitButton;

    private SunoIntegration sunoIntegration;
    private BioDataManager bioDataManager;
    private MovementTracker movementTracker;
    private AudioVisualGenerator audioVisualGenerator;

    private void Start()
    {
        Debug.Log("MainController: Initializing components");
        sunoIntegration = GetComponent<SunoIntegration>();
        bioDataManager = GetComponent<BioDataManager>();
        movementTracker = GetComponent<MovementTracker>();
        audioVisualGenerator = GetComponent<AudioVisualGenerator>();

        submitButton.onClick.AddListener(OnSubmitButtonClick);
        Debug.Log("MainController: Components initialized and button listener set");
    }

    private void OnSubmitButtonClick()
    {
        string userInput = userInputField.text;
        Debug.Log($"MainController: User input received: {userInput}");
        ProcessUserInput(userInput);
    }

    private async void ProcessUserInput(string userInput)
    {
        Debug.Log("MainController: Processing user input");

        string audioTrack = await sunoIntegration.GenerateAudio(userInput);
        Debug.Log($"MainController: Audio track generated: {audioTrack}");

        string bioData = await bioDataManager.FetchBioData();
        Debug.Log($"MainController: Bio data fetched: {bioData}");

        Vector3 movementData = movementTracker.TrackMovement();
        Debug.Log($"MainController: Movement data tracked: {movementData}");

        audioVisualGenerator.GenerateAudioVisual(audioTrack, bioData, movementData);
        Debug.Log("MainController: Audio-visual generation complete");
    }
}