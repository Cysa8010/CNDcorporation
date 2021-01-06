using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SUserCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gamepad = transform.gameObject.AddComponent<SGamePadAdjuster>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Vector3.zero;

        // Left
        if (Input.GetKey(KeyCode.A) || gamepad.Left.press)
        {
            direction.x = -1f;
        }
        // Right
        if (Input.GetKey(KeyCode.D) || gamepad.Right.press)
        {
            direction.x = 1f;
        }

        // Front
        if (Input.GetKey(KeyCode.W) || gamepad.Front.press)
        {
            direction.z = 1f;
        }

        // Back
        if (Input.GetKey(KeyCode.S) || gamepad.Back.press)
        {
            direction.z = -1f;
        }

        Translate(direction);
    }

    void Translate(Vector3 direction)
    {
        // 各軸移動量の算出
        Vector3 vec = Vector3.zero;
        vec += transform.forward * direction.z;
        vec += transform.right * direction.x;
        vec += transform.up * direction.y;

        //transform.GetComponent<Rigidbody>().AddForce(speed * Time.deltaTime * vec * 10f);
        //move += speed * Time.deltaTime * vec;
        //move *= 0.99f;
        //transform.localPosition += move;
        transform.localPosition += vec * speed * Time.deltaTime;
    }

    private SGamePadAdjuster gamepad = null;

    /* 速度(1m/s) = accele */
    [SerializeField] private float speed = 1f;
}
