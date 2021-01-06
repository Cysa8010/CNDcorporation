using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPlayerStatusM : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gamepad = transform.gameObject.AddComponent<SGamePadAdjuster>();
        //emitter = transform.Find(@"Body\Weapon\Emitter").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // 今は簡易的に操作を直書き
        // 今後どうにかする

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

        // Up
        if (Input.GetKey(KeyCode.R) || gamepad.Up.press)
        {
            direction.y = 1f;
        }

        // Down
        if (Input.GetKey(KeyCode.F) || gamepad.Down.press)
        {
            direction.y = -1f;
        }
        //Translate(direction);
        TranslateInertia(direction);

        float r = 0f;
        if(Input.GetKey(KeyCode.Q)||gamepad.LRot.press)
        {
            r += -1f;
        }
        if(Input.GetKey(KeyCode.E)||gamepad.RRot.press)
        {
            r += 1f;
        }
        Rotate(r);

        if (Input.GetKeyDown(KeyCode.Mouse1) || gamepad.RTrigger.trigger)
            Shot();

        if (Input.GetKey(KeyCode.Space) || gamepad.LTrigger.press)
        {
            // ここにアクセル的なやつ
            // 連動するのはドローンの起動レベルとプロペラの回転量
            //transform.gameObject.GetComponent<Rigidbody>().useGravity = false;
            //transform.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            //transform.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 12f,0)*gamepad.LTrigger.value);
        }
        else
        {
            //transform.gameObject.GetComponent<Rigidbody>().useGravity = true;
            //Translate(Vector3.down);
        }
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

        transform.GetComponent<Rigidbody>().AddForce(speed * Time.deltaTime * vec * 10f);
        transform.GetComponent<Rigidbody>().velocity *= 0.99f;
    }

    // 旋回
    void Rotate(float lr)
    {
        // ここちゃんと組もうね
        //transform.Rotate(new Vector3(0, lr, 0));
        transform.eulerAngles+= new Vector3(0f, lr * rot * Time.deltaTime, 0f);
        //transform.localEulerAngles += new Vector3(0f, lr * rot * Time.deltaTime, 0f);
    }

    // 弾発射
    void Shot()
    {
        GameObject go = Instantiate(bullet, emitter.transform.position, Quaternion.identity);
        go.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
    }


    /* 速度(1m/s) = accele */
    [SerializeField] private float speed = 1f;
    /* 移動量 */
    [SerializeField] private Vector3 move = Vector3.zero;

    /* バッテリー */
    //[SerializeField] private float energy = 1.0f;
    /* ECS */
    //[SerializeField] private int ecs = 60;// よくわかんないから放置
    /* プロペラ */
    //[SerializeField] private float propera = 1.0f;

    // 撃つ弾
    [SerializeField] private GameObject bullet = null;
    // 発射口
    [SerializeField] private GameObject emitter = null;

    private SGamePadAdjuster gamepad = null;
    [SerializeField] private float rot = 1f;
}
