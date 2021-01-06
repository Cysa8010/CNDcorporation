using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPointLight : MonoBehaviour
{
    [SerializeField] GameObject Light;
    [SerializeField] GameObject Light1;
    [SerializeField] GameObject Light2;
    [SerializeField] GameObject Light3;
    [SerializeField] GameObject Light4;
    [SerializeField] float Time;
    [SerializeField] float Time1;
    void Start()
    {
        Invoke("LightOn", Time);
        Invoke("LightOn1", Time1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void LightOn()
    {
        Light.SetActive(true);
    }
    void LightOn1()
    {
        Light1.SetActive(true);
        Light2.SetActive(true);
        Light3.SetActive(true);
        Light4.SetActive(true);
    }
}
