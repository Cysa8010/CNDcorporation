﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;      //!< for AudioManager

public class SPlayerStatusKNC : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        gamepad = transform.gameObject.AddComponent<SGamePadAdjuster>();

        pp = new SPropeller();
        pp.Initialize(transform.Find("Body/PropellerFL"), transform.Find("Body/PropellerFR"),
            transform.Find("Body/PropellerBL"), transform.Find("Body/PropellerBR"));

        power_on = true;
    }

    // Update is called once per frame
    /* Update func
     * Key Update => Apply
     */
    void Update()
    {
        // キー処理
        Vector3 direction = Vector3.zero;
        float r = 0f;
        bool isShot = false;
        bool isTrigger = false; //ボタンを押したかの判定用

        if (power_on)
        {
            if (bulletEnergy.energy >= bulletEnergy.consume)
            {
                // Shot
                if (Input.GetKeyDown(KeyCode.Mouse0) || gamepad.RTrigger.trigger)
                {
                    isShot = true;
                    Shot();
                }
            }

            // PowerAccele
            if (Input.GetAxis("Mouse X")!=0f || Input.GetAxis("Mouse Y")!=0f || Input.anyKey || gamepad.Left.press || gamepad.Right.press || gamepad.Up.press || gamepad.Down.press || gamepad.Front.press || gamepad.Back.press || gamepad.LRot.press || gamepad.RRot.press)
            {
                isTrigger = true;   //ボタンを押した状態
                //pp.Accele(gamepad.LTrigger.value);
                pp.Accele(1.0f);

                // ここにアクセル的なやつ
                // 連動するのはドローンの起動レベルとプロペラの回転量
                //transform.gameObject.GetComponent<Rigidbody>().useGravity = false;
                //transform.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                //transform.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 12f,0)*gamepad.LTrigger.value);
                if (!FlySE_On)
                {
                    //飛行SE再生
                    SEManager.Instance.Play(SEPath.DRONE_FLY, 0.3f, 0, 1, true, null);
                    FlySE_On = true;
                }
            }
            else
            {
                isTrigger = false;
                //飛行SE停止
                SEManager.Instance.Stop(SEPath.DRONE_FLY);
                FlySE_On = false;
                //transform.gameObject.GetComponent<Rigidbody>().useGravity = true;
                //Translate(Vector3.down);
            }

            if (isTrigger)
            {
                // Left
                if (Input.GetKey(KeyCode.A) || gamepad.Left.press)
                {
                    direction.x = -0.2f;
                }
                // Right
                if (Input.GetKey(KeyCode.D) || gamepad.Right.press)
                {
                    direction.x = 0.2f;
                }

                // Front
                if (Input.GetKey(KeyCode.W) || gamepad.Front.press)
                {
                    direction.z = 0.2f;
                }

                // Back
                if (Input.GetKey(KeyCode.S) || gamepad.Back.press)
                {
                    direction.z = -0.2f;
                }

                // Up
                if (Input.GetKey(KeyCode.Space) || gamepad.Up.press)
                {
                    RiseandFall(gamepad.Up.value != 0f ? -gamepad.Up.value : 1);
                }

                // Down
                if (/*Input.GetKey(KeyCode.F) ||*/ gamepad.Down.press)
                {
                    RiseandFall(gamepad.Down.value != 0f ? -gamepad.Down.value : -1);
                }


                float mouse_move_x = Input.GetAxis("Mouse X") * mouseSensitivity;
                float mouse_move_y = Input.GetAxis("Mouse Y") * mouseSensitivity;

                // Turn
                if ((mouse_move_x < 0f) || gamepad.LRot.press)
                {
                    r += -2f;
                }
                if ((mouse_move_x > 0f) || gamepad.RRot.press)
                {
                    r += 2f;
                }

                r += 2f * mouse_move_x * 50;
            }




        }



        // 実行/Apply

        //Translate(direction);
        TranslateInertia(direction);
        Rotate(r);




        // Update
        pp.Update();
    }

    // 平行移動
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
    void TranslateInertia(Vector3 direction)
    {
        // 各軸移動量の算出
        Vector3 vec = Vector3.zero;
        vec += transform.forward * direction.z;
        vec += transform.right * direction.x;
        vec += transform.up * direction.y;

        transform.GetComponent<Rigidbody>().AddForce(speed * Time.deltaTime * vec * 100f);
        transform.GetComponent<Rigidbody>().velocity *= 0.99f;
    }

    void RiseandFall(float direction)
    {
        // 各軸移動量の算出
        Vector3 vec = Vector3.zero;
        vec += transform.up * direction;

        transform.GetComponent<Rigidbody>().AddForce(rise * Time.deltaTime * vec * 100f);
        transform.GetComponent<Rigidbody>().velocity *= 0.99f;
    }

    // 旋回
    void Rotate(float lr)
    {
        // ここちゃんと組もうね
        //transform.Rotate(new Vector3(0, lr, 0));
        transform.eulerAngles += new Vector3(0f, lr * rot * Time.deltaTime, 0f);
        //transform.localEulerAngles += new Vector3(0f, lr * rot * Time.deltaTime, 0f);
    }

    // 弾発射
    void Shot()
    {
        //射撃SE再生
        SEManager.Instance.Play(SEPath.DRONE_SHOOT, 0.3f, 0, 1, false, null);
        GameObject go = Instantiate(bullet, emitter.transform.position, Quaternion.identity);
        //go.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
        go.GetComponent<Rigidbody>().AddForce(emitter.transform.forward * 500);
        bulletEnergy.ConsumeEnergy();
    }


    /* 速度(1m/s) = accele */
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private float rise = 1f;
    /* 移動量 */
    [SerializeField]
    private Vector3 move = Vector3.zero;

    /* バッテリー */
    //[SerializeField] private float energy = 1.0f;
    /* ECS */
    //[SerializeField] private int ecs = 60;// よくわかんないから放置
    /* プロペラ */
    //[SerializeField] private float propera = 1.0f;

    // 撃つ弾
    [SerializeField]
    private GameObject bullet = null;
    // 発射口
    [SerializeField]
    private GameObject emitter = null;

    private SGamePadAdjuster gamepad = null;
    [SerializeField]
    private float rot = 1f;

    public SPropeller pp = null;
    [SerializeField]
    bool power_on = false;
    bool FlySE_On = false;

    [SerializeField] float mouseSensitivity = 0.1f; // いわゆるマウス感度

    //GameObject BulletEnergyObject;
    [SerializeField] private SBulletEnergy bulletEnergy = null;


    public bool IsPower_On()
    {
        return power_on;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayArea"))
        {
            power_on = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("PlayArea"))
        {
            power_on = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            //激突SE再生
            SEManager.Instance.Play(SEPath.DRONE_CLASH2, 0.3f, 0, 1, false, null);
        }
        if (collision.gameObject.CompareTag("PlayArea"))
        {
            power_on = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayArea"))
        {
            power_on = true;
        }
    }
}
