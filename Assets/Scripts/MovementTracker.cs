using UnityEngine;
using UnityEngine.XR;

public class MovementTracker : MonoBehaviour
{
    public AudioSource drumSource; // AudioSource for drum sounds
    public AudioSource pitchSource; // AudioSource for pitch-modulated sounds
    public AudioSource melodySource; // AudioSource for melody or another layer
    public AudioSource ambientSource; // AudioSource for ambient background sound

    public AudioClip drumClip; // Audio clip for the drum sound (head bob)
    public AudioClip pitchClip; // Audio clip for the ambient background sound
    public AudioClip melodyClip; // Audio clip for melody or another musical layer
    public AudioClip ambClip; // Audio clip for ambient background sound

    public GameObject room; // The room to scale
    public float heartbeatScaleFactor = 0.1f; // How much the room should scale with each heartbeat
    public float baseRoomScale = 1.0f; // The base scale of the room

    private Vector3 previousHeadPosition;
    private float headBobThreshold = 0.003f; // Sensitivity for detecting head bobbing
    private float minPitch = 0.01f; // Minimum pitch for the sound
    private float maxPitch = 7.0f; // Maximum pitch for the sound
    private float handHeightScale = 4.0f; // Scale to adjust the pitch effect
    private int frameCounter = 0;
    private int HR; // Heart rate

    void Start()
    {
        // Start playing the ambient sound as looping background music
        PlayPitchSound();
        PlayAmbientSound();

        HR = 68;
        previousHeadPosition = Camera.main.transform.position; // Initialize previous head position
    }

    void Update()
    {
        // Use currbpm to modify gradientValue1
        if (heartRateDataReader != null)
        {
            HR = heartRateDataReader.currbpm;
        }

        if (!ambientSource.isPlaying)
        {
            PlayAmbientSound(); // Ensure ambient sound keeps playing
            
        }

        // Get head position
        Vector3 currentHeadPosition = Camera.main.transform.position;

        // Get hand positions
        Vector3 leftHandPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LHand);
        Vector3 rightHandPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RHand);

        // Detect head bobbing (up and down movement) for drum sound
        float headVerticalMovement = currentHeadPosition.y - previousHeadPosition.y;

        if (Mathf.Abs(headVerticalMovement) > headBobThreshold && frameCounter % 4 == 0) 
        {
            PlayDrumSound();
        }

        // Adjust pitch based on hand heights (average between left and right hand)
        float averageHandHeight = (leftHandPosition.y + rightHandPosition.y) / 2.0f;
        float normalizedHeight = Mathf.Clamp01((averageHandHeight + handHeightScale) / handHeightScale); // Normalize height
        pitchSource.volume = Mathf.Lerp(minPitch, maxPitch, normalizedHeight); // Adjust pitch of the pitch-based sound

        // Control melody playback speed based on head movement speed
        float movementSpeed = (currentHeadPosition - previousHeadPosition).magnitude / Time.deltaTime;
        melodySource.volume = Mathf.Clamp(1.0f + movementSpeed / 2.0f, 1.0f, 2.0f); // Increase tempo with speed of movement

        // Store the current head position for the next frame
        previousHeadPosition = currentHeadPosition;

        if (frameCounter >= HR * 2)
        {
            PlayMelodySound();
            frameCounter = 0;
        }

        frameCounter++;
    }

    private void PlayDrumSound()
    {
        // Play the drum sound when head bobbing is detected
        drumSource.PlayOneShot(drumClip);
        // Apply room scaling like a heartbeat
        //float heartbeatScale = baseRoomScale + Mathf.Sin(Time.time * Mathf.PI * HR / 60.0f) * heartbeatScaleFactor;
        //room.transform.localScale = Vector3.one * heartbeatScale;
    }

    private void PlayAmbientSound()
    {
        // Set ambient sound as the clip for looping background music
        ambientSource.clip = ambClip;
        ambientSource.loop = true;
        ambientSource.Play();
    }

    private void PlayPitchSound()
    {
        // Set pitch-modulated sound as the clip for looping background music
        pitchSource.clip = pitchClip;
        pitchSource.volume = 0.3f;
        pitchSource.loop = true;
        pitchSource.Play();
    }

    private void PlayMelodySound()
    {
        // Set melody clip for playing
        melodySource.clip = melodyClip;
        //float heartbeatScale = baseRoomScale + Mathf.Sin(Time.time * Mathf.PI * HR / 60.0f) * heartbeatScaleFactor;
        //room.transform.localScale = Vector3.one * heartbeatScale;
        melodySource.loop = false; // You can adjust based on your needs
        melodySource.Play();
    }
}
