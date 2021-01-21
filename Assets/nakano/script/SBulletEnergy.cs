using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SBulletEnergy : MonoBehaviour
{
    //エネルギーの上・下限値
    public const float Min = 0.0f;
    public const float Max = 100.0f;

    public float energy;
   
     
    public float charge;  //何もしてないと補充
    public float consume; //エネルギー使用量

    [SerializeField] private Image image;

  
   
    
    // Start is called before the first frame update
    void Start()
    {
        //image = this.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        ////エネルギー消費部分
        // if (Input.GetKeyDown(KeyCode.Mouse1) && energy >= consume)
        // {

        // }
        //ConsumeEnergy();

        //エネルギー回復。何もしていないと回復
         energy = Mathf.Clamp(energy + charge, Min, Max);


        image.fillAmount = energy / 100.0f;
    }

    public void ConsumeEnergy()
    {
        energy = Mathf.Clamp(energy - consume, Min, Max);
    }
}
