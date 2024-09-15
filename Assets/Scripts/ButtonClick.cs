using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    [SerializeField] private GameObject calmEnvironment;
    [SerializeField] private GameObject energizingEnvironment;
    [SerializeField] private GameObject startEnvironment;

    // Start is called before the first frame update
    void Start()
    {
        calmEnvironment.SetActive(false);
        energizingEnvironment.SetActive(false);
    }

    // Update is called once per frame
    public void StartCalmingEnvironment()
    {
        calmEnvironment.SetActive(true);
        energizingEnvironment.SetActive(false);
        startEnvironment.SetActive(false);
    }

    public void StartEnergizingEnvironment()
    {
        calmEnvironment.SetActive(false);
        energizingEnvironment.SetActive(true);
        startEnvironment.SetActive(false);
    }
}
