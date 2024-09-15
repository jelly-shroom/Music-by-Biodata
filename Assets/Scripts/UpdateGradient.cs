using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateGradient : MonoBehaviour
{
    [SerializeField] Material material;
    public float gradientValue1 = 0.0f; // Gradient value 1
    public float gradientValue2 = 5.0f; // Gradient value 2
    public float speedX = 0.3f; // Speed for X
    public float speedY = 1.5f; // Speed for Y

    void Start()
    {
        // Set the gradient value in the shader
        material = this.gameObject.GetComponent<Renderer>().material;
    }

    void Update()
    {
        // Increment the gradient values over time
        gradientValue1 += speedX * Time.deltaTime;
        gradientValue2 += speedY * Time.deltaTime;

        // Set the gradient value in the shader
        material.SetFloat("_Value1", gradientValue1);
        material.SetFloat("_Value2", gradientValue2);
    }
}
