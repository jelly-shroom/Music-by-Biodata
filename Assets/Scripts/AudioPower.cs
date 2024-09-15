using UnityEngine;

public class AudioPower : MonoBehaviour
{
    public AudioSource audioSource;
    public float threshold = 100f;
    private float currentValue = 0f; //live terra heart rate data somehow 

    void Update()
    {
        // Simulate live numerical input (replace this with your actual input method)
        currentValue += Random.Range(-1f, 1f);

        // Check if the current value has reached or exceeded the threshold
        if (currentValue >= threshold)
        {
            // Stop the audio if it's playing
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
                Debug.Log("Audio stopped. Threshold reached!");
            }
        }

        // Optional: Display the current value
        Debug.Log("Current Value: " + currentValue);
    }

    // Method to set the current value externally (e.g., from a heart rate monitor)
    public void SetCurrentValue(float value)
    {
        currentValue = value;
    }
}