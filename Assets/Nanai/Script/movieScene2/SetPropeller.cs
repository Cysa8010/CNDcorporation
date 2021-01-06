using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPropeller : MonoBehaviour
{
    [SerializeField] float Time;
    void Start()
    {
        Invoke("PropellerOn", Time);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void PropellerOn()
    {
        GetComponent<SDronePropellerRotation>().enabled = true;
    }
}
