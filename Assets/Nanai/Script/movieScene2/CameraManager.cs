using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] GameObject Camera1;
    [SerializeField] GameObject Camera2;
    [SerializeField] GameObject Camera3;
    [SerializeField] float CameraChangeTime1;
    [SerializeField] float CameraChangeTime2;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("ChangeCamera1", CameraChangeTime1);
        Invoke("ChangeCamera2", CameraChangeTime2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ChangeCamera1()
    {
        Camera1.SetActive(false);
        Camera2.SetActive(true);
    }
    void ChangeCamera2()
    {
        Camera2.SetActive(false);
        Camera3.SetActive(true);
    }
}
