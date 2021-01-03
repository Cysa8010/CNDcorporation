using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SPropeller
{
    // func
    public bool Initialize(Transform FL,Transform FR,Transform BL,Transform BR)
    {
        // Set propeller
        this.FL = FL;
        if (!this.FL)
            return false;
        this.FR = FR;
        if (!this.FR)
            return false;
        this.BL = BL;
        if (!this.BL)
            return false;
        this.BR = BR;
        if (!this.BR)
            return false;

        

        return true;
    }

    public void Update()
    {
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

        to = (limitTake_off<angle) ? true : false;
    }

    public void Accele(float value)
    {
        accele = Mathf.Clamp(value, 0f, 1f);
        // 回転
        angle += accele * LIMIT_ACCELE;

    }

    // status

    // propeller
    private Transform FL = null;
    private Transform FR = null;
    private Transform BL = null;
    private Transform BR = null;

    [SerializeField]private float angle = 0f;
    // 表のアクセル
    private float accele = 0f; //0.0-1.0
    // 加速上限値
    private const float LIMIT_ACCELE = 0.5f;
    [SerializeField] private bool to = false;
    public bool take_off { get { return to; } }
    [SerializeField] private float limitTake_off = 25f;

}
