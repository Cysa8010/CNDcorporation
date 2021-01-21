using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SChargeArea : MonoBehaviour
{
    GameObject BulletEnergyObject;
    SBulletEnergy BulletEnergyScript;

    public float ChargeValue;
    private bool isCharged;


    // Start is called before the first frame update
    void Start()
    {
        BulletEnergyObject = GameObject.Find("gage");
        BulletEnergyScript = BulletEnergyObject.GetComponent<SBulletEnergy>();
        isCharged = false;

    }

    // Update is called once per frame
    void Update()
    {

    }

    void AddCharge()
    {
        BulletEnergyScript.energy += ChargeValue;

    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AddCharge();
        }

    }


}
