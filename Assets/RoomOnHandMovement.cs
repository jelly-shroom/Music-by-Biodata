using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomOnHandMovement : MonoBehaviour
{
    private Vector3 previousLHand;
    private Vector3 previousRHand;
    public GameObject roof; // Assign via Inspector or find in Start

    void Start()
    {
        if (roof == null)
        {
            // Optionally, find the roof object if it's not assigned via Inspector
            roof = GameObject.Find("Roof");
            if (roof == null)
            {
                Debug.LogError("Roof object not found! Please assign the roof GameObject.");
            }
        }
        
        // Initialize previous hand positions
        previousLHand = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LHand);
        previousRHand = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RHand);
    }

    void Update()
    {
        // Get head position
        Vector3 currentHeadPosition = Camera.main.transform.position;

        // Get hand positions
        Vector3 leftHandPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LHand);
        Vector3 rightHandPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RHand);

        // Calculate vertical movement of hands
        float verticalLHandMovement = leftHandPosition.y - previousLHand.y;
        float verticalRHandMovement = rightHandPosition.y - previousRHand.y;

        // Move roof up if hands are moving up and above head position
        if ((verticalLHandMovement > 0 && leftHandPosition.y >= currentHeadPosition.y) &&
            (verticalRHandMovement > 0 && rightHandPosition.y >= currentHeadPosition.y) 
            && (roof.transform.position.y <= currentHeadPosition.y + 13)) 
        {
            Vector3 newPosition = roof.transform.position;
            newPosition.y += 0.1f; // Adjust the increment value as needed
            roof.transform.position = newPosition;
        }
        // Move roof down if hands are moving down and roof is at least 2 units above head
        else if ((verticalLHandMovement < 0 && roof.transform.position.y >= -.5f) ||
                 (verticalRHandMovement < 0 && roof.transform.position.y >= -.5f)) 
        {
            Vector3 newPosition = roof.transform.position;
            newPosition.y -= 0.1f; // Adjust the decrement value as needed
            roof.transform.position = newPosition;
        }

        // Update previous hand positions
        previousLHand = leftHandPosition;
        previousRHand = rightHandPosition;
    }
}
