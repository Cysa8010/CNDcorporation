using KanKikuchi.AudioManager;
using System.Collections;
using UnityEngine;

/// <summary>
/// Rigidbody の UseGravity と IsKinematic を false に
/// </summary>
public class SPlayerStatusTilt : MonoBehaviour
{
    #region 変数宣言

    /* 速度(1m/s) = accele */

    [SerializeField]
    private float speed = 1f;

    /* 移動量 */

    [SerializeField]
    private Vector3 move = Vector3.zero;

    private Vector3 moveDirection = Vector3.zero;

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
    private bool power_on = false;

    private bool FlySE_On = false;

    private bool bIsBusy = false;

    private float inertiaTime, inertiaProgress, tiltTime, tiltProgress = 0.0f;

    // 今の角度
    private Quaternion rotation;

    // 変更する角度
    [SerializeField] private float maxAngle = 10.0f;

    private float Angle = 0.0f;
    private Quaternion rotateAngle;
    private Quaternion horizontalAngle = Quaternion.identity;

    #endregion 変数宣言

    // Start is called before the first frame update
    private void Start()
    {
        gamepad = transform.gameObject.AddComponent<SGamePadAdjuster>();
        //emitter = transform.Find(@"Body\Weapon\Emitter").gameObject;
        pp = new SPropeller();
        pp.Initialize(transform.Find("Body/PropellerFL"), transform.Find("Body/PropellerFR"),
            transform.Find("Body/PropellerBL"), transform.Find("Body/PropellerBR"));

        power_on = true;
        rotation = transform.rotation;
    }

    // Update is called once per frame
    /* Update func
     * Key Update => Apply
     */

    private void Update()
    {
        // 今は簡易的に操作を直書き
        // 今後どうにかする
        Debug.Log("position" + transform.position);

        // キー処理
        Vector3 direction = Vector3.zero;
        float r = 0f;
        bool isShot = false;
        bool isTrigger = false; //ボタンを押したかの判定用

        if (power_on)
        {
            // Shot
            if (Input.GetKeyDown(KeyCode.Mouse1) || gamepad.RTrigger.trigger)
            {
                isShot = true;
                Shot();
            }

            // PowerAccele
            if (Input.GetKey(KeyCode.Space) || gamepad.Left.press || gamepad.Right.press || gamepad.Up.press || gamepad.Down.press || gamepad.Front.press || gamepad.Back.press || gamepad.LRot.press || gamepad.RRot.press)
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
                    //Angle += 0.1f;

                    //if (Angle > maxAngle)
                    //{
                    //    Angle = maxAngle;
                    //}
                    //else if (Angle < -maxAngle)
                    //{
                    //    Angle = -maxAngle;
                    //}

                    //rotateAngle = Quaternion.Euler(rotation.x, rotation.y, Angle);

                    if (!bIsBusy)
                    {
                        // transform.rotation = rotateAngle;
                        StartCoroutine(Inertia(direction, rotateAngle));
                    }
                }
                // Right
                if (Input.GetKey(KeyCode.D) || gamepad.Right.press)
                {
                    direction.x = 0.2f;
                    //Angle -= 0.1f;

                    //if (Angle > maxAngle)
                    //{
                    //    Angle = maxAngle;
                    //}
                    //else if (Angle < -maxAngle)
                    //{
                    //    Angle = -maxAngle;
                    //}

                    //rotateAngle = Quaternion.Euler(rotation.x, rotation.y, Angle);
                    if (!bIsBusy)
                    {
                        // transform.rotation = rotateAngle;
                        StartCoroutine(Inertia(direction, rotateAngle));
                    }
                }

                // Front
                if (Input.GetKey(KeyCode.W) || gamepad.Front.press)
                {
                    direction.z = 0.2f;

                    // Angle += 0.1f;
                    // 
                    // if (Angle > maxAngle)
                    // {
                    //     Angle = maxAngle;
                    // }
                    // else if (Angle < -maxAngle)
                    // {
                    //     Angle = -maxAngle;
                    // }
                    // 
                    // rotateAngle = Quaternion.Euler(Angle, rotation.y, rotation.z);
                    if (!bIsBusy)
                    {
                        //transform.rotation = rotateAngle;
                        StartCoroutine(Inertia(direction, rotateAngle));
                    }
                }

                // Back
                if (Input.GetKey(KeyCode.S) || gamepad.Back.press)
                {
                    direction.z = -0.2f;

                    // Angle -= 0.1f;
                    // 
                    // if (Angle > maxAngle)
                    // {
                    //     Angle = maxAngle;
                    // }
                    // else if (Angle < -maxAngle)
                    // {
                    //     Angle = -maxAngle;
                    // }
                    // 
                    // rotateAngle = Quaternion.Euler(Angle, rotation.y, rotation.z);

                    if (!bIsBusy)
                    {
                        // transform.rotation = rotateAngle;
                        StartCoroutine(Inertia(direction, rotateAngle));
                    }
                }

                // Up
                if (Input.GetKey(KeyCode.R) || gamepad.Up.press)
                {
                    direction.y = 0.3f;
                    if (!bIsBusy)
                    {
                        StartCoroutine(Inertia(direction, rotateAngle));
                    }
                }

                // Down
                if (Input.GetKey(KeyCode.F) || gamepad.Down.press)
                {
                    direction.y = -0.3f;
                    if (!bIsBusy)
                    {
                        StartCoroutine(Inertia(direction, rotateAngle));
                    }
                }

                // Turn
                if (Input.GetKey(KeyCode.Q) || gamepad.LRot.press)
                {
                    r += -2f;
                }
                if (Input.GetKey(KeyCode.E) || gamepad.RRot.press)
                {
                    r += 2f;
                }
            }
        }

        Rotate(r);

        // Update
        pp.Update();
    }

    /// <summary>
    /// 慣性をかけながら移動したい
    /// 現状は上下移動ができていない⇒デバッグ用のキーが悪いことが判明
    /// @todo
    /// @memo
    /// 前進ｘ横ｚ
    /// </summary>
    /// <returns></returns>
    private IEnumerator Inertia(Vector3 vec, Quaternion rotate)
    {
        // コルーチン2重防止ON
        bIsBusy = true;

        // 慣性用の時間変数
        inertiaTime = 0.0f;
        inertiaProgress = 0.0f;

        transform.GetComponent<Rigidbody>().AddForce(vec * speed);

        // @todo パッドにも対応させて！！！
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.R) || Input.GetKeyUp(KeyCode.F))
        {
            while (inertiaProgress < 1.0f)
            {
                inertiaTime += Time.deltaTime;

                // 0.5s計測
                inertiaProgress = inertiaTime / 0.5f;

                // ここで慣性を付ける
                transform.GetComponent<Rigidbody>().velocity *= 0.97f;

                yield return null;// StartCoroutine(Tilt(rotate));
            }

            // 0.5sで慣性終了
            transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
            rotateAngle = horizontalAngle;
            Angle = 0.0f;
        }

        bIsBusy = false;
    }

    #region 現状使ってません
    //private IEnumerator Tilt(Quaternion rotate)
    //{
    //    if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
    //    {
    //        // 傾きを直していく
    //        transform.rotation = Quaternion.Lerp(rotate, Quaternion.Euler(horizontalAngle.x, rotate.y, rotate.z), inertiaProgress);
    //        //transform.rotation = Quaternion.Lerp(rotate, horizontalAngle, inertiaProgress);
    //        yield return null;
    //
    //    }
    //    if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
    //    {
    //        // 傾きを直していく
    //        //transform.rotation = Quaternion.Lerp(rotate, horizontalAngle, inertiaProgress);
    //        transform.rotation = Quaternion.Lerp(rotate, Quaternion.Euler(rotate.x, rotate.y, horizontalAngle.z), inertiaProgress);
    //        yield return null;
    //
    //    }
    //}
    #endregion

    // 旋回
    private void Rotate(float lr)
    {
        // ここちゃんと組もうね
        //transform.Rotate(new Vector3(0, lr, 0));
        transform.eulerAngles += new Vector3(0f, lr * rot * Time.deltaTime, 0f);
        //transform.localEulerAngles += new Vector3(0f, lr * rot * Time.deltaTime, 0f);
    }

    // 弾発射
    private void Shot()
    {
        //射撃SE再生
        SEManager.Instance.Play(SEPath.DRONE_SHOOT, 0.3f, 0, 1, false, null);
        GameObject go = Instantiate(bullet, emitter.transform.position, Quaternion.identity);
        go.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
    }

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

    #region 一時コメントアウト

    // 平行移動 使ってなさそうだからCO
    // private void Translate(Vector3 direction)
    // {
    //     // 各軸移動量の算出
    //     Vector3 vec = Vector3.zero;
    //     vec += transform.forward * direction.z;
    //     vec += transform.right * direction.x;
    //     vec += transform.up * direction.y;
    //
    //     //transform.GetComponent<Rigidbody>().AddForce(speed * Time.deltaTime * vec * 10f);
    //     //move += speed * Time.deltaTime * vec;
    //     //move *= 0.99f;
    //     //transform.localPosition += move;
    //     transform.localPosition += vec * speed * Time.deltaTime;
    // }

    #endregion 一時コメントアウト
}