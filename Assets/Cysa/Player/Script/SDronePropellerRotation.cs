using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SDronePropellerRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        isInit = Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInit)
            isInit = Init();

        RotatePropeller();
    }

    [SerializeField]
    private float angle = 0f;
    [Range(0f,1f)]//0.0-1.0
    public float accele = 0f;
    private bool isInit = false;
    
    private Transform FL = null;
    private Transform FR = null;
    private Transform BL = null;
    private Transform BR = null;

    bool Init()
    {
        FL = transform.Find("Body/PropellerFL");
        if (!FL)
            return false;
        FR = transform.Find("Body/PropellerFR");
        if (!FR)
            return false;
        BL = transform.Find("Body/PropellerBL");
        if (!BL)
            return false;
        BR = transform.Find("Body/PropellerBR");
        if (!BR)
            return false;
        return true;
    }

    void RotatePropeller()
    {
        angle += accele;

        angle *= 0.99f;

        Vector3 rot = Vector3.zero;

        rot = FL.localEulerAngles;
        rot.y += angle;
        FL.localEulerAngles = rot;

        rot = FR.localEulerAngles;
        rot.y += angle;
        FR.localEulerAngles = rot;

        rot = BL.localEulerAngles;
        rot.y += angle;
        BL.localEulerAngles = rot;

        rot = BR.localEulerAngles;
        rot.y += angle;
        BR.localEulerAngles = rot;
    }
}
