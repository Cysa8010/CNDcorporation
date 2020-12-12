using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

/* ドローン本体制御
 * ドローンのプロパティ
 * ドローンの操作
*/

public class SDroneControll : MonoBehaviour
{
    /* 操作
     * FBLR      - AddForce
     * Up-Down   - AddForce
     * TurningLR - Transform
     * TriggerWeapon - CreateBullet
     * SwitchWeapon  - ChangeSelector
    */
    [Flags]
    public enum ControllState
    {
        NONE = 0,       //!< 待機
        MOVE = 1,       //!< 移動
        TURN = 1 << 1,       //!< 旋回
        ACTION1 = 1 << 2,    //!< 武器1(左)
        ACTION2 = 1 << 3     //!< 武器2(右)
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!useScript) return;

        if(useScript.state.HasFlag(ControllState.MOVE))
        {
            Translate(useScript.direction);
        }
        if (useScript.state.HasFlag(ControllState.TURN))
        {
            Turn(useScript.turnAngle);
        }
        if (useScript.state.HasFlag(ControllState.ACTION1))
        {
            // 弾生成
            GameObject gameObject = Instantiate(weapon1, GameObject.Find("BulletSponerL").transform.position, Quaternion.identity);
            gameObject.GetComponent<Rigidbody>().AddForce(transform.forward*1000.0f);

        }
        if (useScript.state.HasFlag(ControllState.ACTION2))
        {
            // 弾生成
            GameObject gameObject = Instantiate(weapon2, GameObject.Find("BulletSponerR").transform.position, Quaternion.identity);
            gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 1000.0f);
        }
    }

    // ドローンを操作する関数
    // FBLR
    void Translate(Vector3 vec)
    {
        transform.GetComponent<Rigidbody>().AddForce(vec);
    }
    // 旋回(回転ともいう)
    void Turn(float rot)
    {
        transform.Rotate(new Vector3(0, rot, 0));
    }

    /* -- プロパティ -- */
    int hp;
    int atk;

    [SerializeField]
    private SIUnitController useScript = null;
    [SerializeField]
    private GameObject weapon1 = null;
    [SerializeField]
    private GameObject weapon2 = null;
}
