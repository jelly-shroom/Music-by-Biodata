using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateGradient : MonoBehaviour
{
    [SerializeField] Material material;
    public float gradientValue1 = 0.0f; // Gradient value 1
    public float gradientValue2 = 5.0f; // Gradient value 2
    public float speedY = 0.001f; // Speed for Y

    public HeartRateDataReader heartRateDataReader; // Reference to HeartRateDataReader

    void Start()
    {
        // Set the material from the GameObject's Renderer
        material = this.gameObject.GetComponent<Renderer>().material;
    }

    void Update()
    {
        // Use currbpm to modify gradientValue1
        if (heartRateDataReader != null)
        {
            gradientValue1 = heartRateDataReader.currbpm * 0.01f; // Adjust multiplier as needed
        }

        // Increment the second gradient value over time
        gradientValue2 += speedY * Time.deltaTime;

        // Set the gradient values in the shader
        material.SetFloat("_Value1", gradientValue1);
        material.SetFloat("_Value2", gradientValue2);
    }
}