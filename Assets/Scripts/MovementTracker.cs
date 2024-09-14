using UnityEngine;

public class MovementTracker : MonoBehaviour
{
    public Vector3 TrackMovement()
    {
        Debug.Log("MovementTracker: Tracking movement");

        // Simulated movement data for testing
        Vector3 movementData = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        );

        Debug.Log($"MovementTracker: Movement data: {movementData}");
        return movementData;
    }
}