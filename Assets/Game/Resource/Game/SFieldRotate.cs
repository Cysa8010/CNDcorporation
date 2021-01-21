using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFieldRotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += new Vector3(0, rotate, 0) * Time.deltaTime;
    }

    [SerializeField] private float rotate = 20;
}
