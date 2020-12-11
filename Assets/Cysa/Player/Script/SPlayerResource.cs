using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPlayerResource : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        NoBody = GameObject.CreatePrimitive(PrimitiveType.Cube);
        NoBody.name = "NullGameObject";
        //NoBody.active = false;
        NoBody.SetActive(false);
        
        // セーブファイル読み込み
        if (!LoadDataFile())
        {
            Debug.Log("セーブデータの読み込みに失敗");
            //return;
        }

        if (!ConstructModel())
        {
            Debug.Log("モデルの構築に失敗");
            return;
        }

    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.F6))
        {
            DismantleModel();
            ConstructModel();
        }
#endif //! UNITY_EDITOR
    }

    bool LoadDataFile()
    {
        Debug.Log("まだセーブデータの読み込みを作っていません!!");
        return false;
        //return true;
    }
    bool ConstructModel()
    {
        // モデルの構築

        // ボディ
        if(!AddChild(transform.Find("Body").gameObject, GetBody(bodyNum)))
        {
            Debug.Log("Body Faild!");
            return false;
        }

        // FLプロペラ
        if (!AddChild(transform.Find("Body/PropellerFL").gameObject, GetPropeller(propellerNum)))
        {
            Debug.Log("PropellerFL Faild!");
            return false;
        }

        // FRプロペラ
        if (!AddChild(transform.Find("Body/PropellerFR").gameObject, GetPropeller(propellerNum)))
        {
            Debug.Log("PropellerFR Faild!");
            return false;
        }

        // BLプロペラ
        if (!AddChild(transform.Find("Body/PropellerBL").gameObject, GetPropeller(propellerNum)))
        {
            Debug.Log("PropellerBL Faild!");
            return false;
        }

        // BRプロペラ
        if (!AddChild(transform.Find("Body/PropellerBR").gameObject, GetPropeller(propellerNum)))
        {
            Debug.Log("PropellerBR Faild!");
            return false;
        }

        // L武器
        if (!AddChild(transform.Find("Body/Weapon").gameObject, GetWeapon(weaponNum)))
        {
            Debug.Log("WeaponL Faild!");
            return false;
        }

        return true;
    }
    bool DismantleModel()
    {
        // モデルの解体

        // ボディ
        Destroy(transform.Find("Body").GetChild(5).gameObject);

        // FLプロペラ
        Destroy(transform.Find("Body/PropellerFL").GetChild(0).gameObject);

        // FRプロペラ
        Destroy(transform.Find("Body/PropellerFR").GetChild(0).gameObject);
        
        // BLプロペラ
        Destroy(transform.Find("Body/PropellerBL").GetChild(0).gameObject);
        
        // BRプロペラ
        Destroy(transform.Find("Body/PropellerBR").GetChild(0).gameObject);

        // 武器
        Destroy(transform.Find("Body/Weapon").GetChild(1).gameObject);

        

        return true;
    }
    bool AddChild(GameObject parent,GameObject child)
    {
        if(!parent)
        {
            Debug.Log("親を知りません");
            return false;
        }
        if (!Instantiate(child, parent.transform))
        {
            Debug.Log("子作り失敗");
            return false;
        }
        return true;
    }

    public GameObject GetBody(int index) { return (-1 < index && index < body.Length) ? body[index] : NoBody; }
    public GameObject GetPropeller(int index) { return (-1 < index && index < propeller.Length) ? propeller[index] : NoBody; }
    public GameObject GetWeapon(int index) { return (-1 < index && index < weapon.Length) ? weapon[index] : NoBody; }

    /* プレイヤーモデルインデックス */
    [SerializeField]// ボディ
    private int bodyNum = 0;
    [SerializeField]// プロペラ
    private int propellerNum = 0;
    [SerializeField]// 武器
    private int weaponNum = 0;
    [SerializeField]
    private string saveData = "player.sd";

    /* プレイヤーの各リソース */
    [SerializeField]
    private GameObject[] body;
    [SerializeField]
    private GameObject[] propeller;
    [SerializeField]
    private GameObject[] weapon;

    /* 参照エラー用NullObject */
    private GameObject NoBody;
}
