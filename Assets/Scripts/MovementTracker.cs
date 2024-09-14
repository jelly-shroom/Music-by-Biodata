using UnityEngine;
using UnityEngine.XR;

public class MovementTracker : MonoBehaviour
{
    [System.Obsolete]
    public Vector3 TrackMovement()
    {
        // Example: Getting the position of the right hand controller
        Vector3 rightHandPosition = InputTracking.GetLocalPosition(XRNode.RightHand);
        Vector3 leftHandPosition = InputTracking.GetLocalPosition(XRNode.LeftHand);

        // Implement logic to use position data as needed
        return rightHandPosition; // Or return a combination of data as needed
    }
}