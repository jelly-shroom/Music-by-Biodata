using UnityEngine;
using UnityEngine.XR;

public class MovementTracker : MonoBehaviour
{
    public AudioSource audioSource; // Single AudioSource to play different clips

    public AudioClip drumClip; // Audio clip for the drum sound (head bob)
    public AudioClip pitchClip; // Audio clip for the ambient background sound
    public AudioClip melodyClip; // Audio clip for melody or another musical layer
    public AudioClip ambClip; // Audio clip for melody or another musical layer

    private Vector3 previousHeadPosition;
    private float headBobThreshold = 0.001f; // Sensitivity for detecting head bobbing
    private float minPitch = .1f; // Minimum pitch for the drum sound
    private float maxPitch = 5.0f; // Maximum pitch for the drum sound
    private float handHeightScale = 4.0f; // Scale to adjust the pitch effect

    void Start()
    {
        //audioSource = GetComponent<AudioSource>();

        // Start playing ambient sound as looping background music
        PlayPitchSound();

        previousHeadPosition = Camera.main.transform.position; // Initialize previous head position
    }

    void Update()
    {

        PlayAmbientSound();
        // Get head position
        Vector3 currentHeadPosition = Camera.main.transform.position;

        // Get hand positions
        Vector3 leftHandPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LHand);
        Vector3 rightHandPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RHand);

        // Detect head bobbing (up and down movement) for drum sound
        float headVerticalMovement = currentHeadPosition.y - previousHeadPosition.y;

        if (Mathf.Abs(headVerticalMovement) > headBobThreshold)
        {
            PlayDrumSound();
        }

        // Adjust drum pitch based on hand heights (average between left and right hand)
        float averageHandHeight = (leftHandPosition.y + rightHandPosition.y) / 2.0f;
        float normalizedHeight = Mathf.Clamp01((averageHandHeight + handHeightScale) / handHeightScale); // Normalize height
        audioSource.pitch = Mathf.Lerp(minPitch, maxPitch, normalizedHeight); // Adjust pitch of the drum sound

        // Adjust melody volume based on hand distance
        float handDistance = Vector3.Distance(leftHandPosition, rightHandPosition);
        audioSource.volume = Mathf.Clamp01(handDistance / 2.0f); // Adjust volume based on hand distance

        // Control melody playback speed based on head movement speed
        float movementSpeed = (currentHeadPosition - previousHeadPosition).magnitude / Time.deltaTime;
        audioSource.pitch = Mathf.Clamp(1.0f + movementSpeed / 2.0f, 1.0f, 2.0f); // Increase tempo with speed of movement

        // Store the current head position for the next frame
        previousHeadPosition = currentHeadPosition;
    }

    private void PlayDrumSound()
    {
        //audioSource.volume = 1.0f; // Set the volume to maximum (1.0f is the max value)
        // Play the drum sound when head bobbing is detected
        audioSource.PlayOneShot(drumClip);

    }

    private void PlayPitchSound()
    {
        // Set ambient sound as the clip for looping background music
        audioSource.clip = pitchClip;
        audioSource.loop = true;
        audioSource.Play();
    }
    private void PlayAmbientSound()
    {
        // Set ambient sound as the clip for looping background music
        audioSource.clip = ambClip;
        audioSource.loop = true;
        audioSource.Play();
    }

    private void PlayMelodySound()
    {
        // Set melody clip for playing
        audioSource.clip = melodyClip;
        audioSource.loop = false; // You can adjust based on your needs
        audioSource.Play();
    }
}