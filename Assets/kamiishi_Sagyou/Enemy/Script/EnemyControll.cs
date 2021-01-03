using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class EnemyControll : MonoBehaviour
{
    /* 操作
      * FBLR      - AddForce
      * Up-Down   - AddForce
      * TurningLR - Transform
      * TriggerWeapon - CreateBullet
      * SwitchWeapon  - ChangeSelector
     */
    [Flags]
    public enum EnemyControllState
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


        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    Turn(-0.1f);
        //}
        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    Turn(0.1f);
        //}
        if (!useScript) return;

        if (useScript.state.HasFlag(EnemyControllState.MOVE))
        {
            Translate(useScript.direction);
        }
        if (useScript.state.HasFlag(EnemyControllState.TURN))
        {
            Turn(useScript.turnAngle);
        }
        //if (useScript.state.HasFlag(ControllState.ACTION1))
        //{
        //    // 弾生成
        //    GameObject gameObject = Instantiate(weapon1, GameObject.Find("BulletSponerL").transform.position, Quaternion.identity);
        //    gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 1000.0f);

        //}
        //if (useScript.state.HasFlag(ControllState.ACTION2))
        //{
        //    // 弾生成
        //    GameObject gameObject = Instantiate(weapon2, GameObject.Find("BulletSponerR").transform.position, Quaternion.identity);
        //    gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 1000.0f);
        //}
    }

    // ドローンを操作する関数
    // FBLR
    void Translate(Vector3 vec)
    {
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x + vec.x , this.gameObject.transform.position.y + vec.y, this.gameObject.transform.position.z + vec.z);
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
    private EUnitControler useScript = null;
 //   public GameObject weapon1 = null;
 //   [SerializeField]
 //   private GameObject weapon2;
}
