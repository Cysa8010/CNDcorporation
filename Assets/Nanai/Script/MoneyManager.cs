using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] public SPlayerSave savedata;
    int money;
    void Start()
    {
        //ロード
        savedata.LoadData();
        money = savedata.data.money;
    }
    
    void Update()
    {
        if (Input.GetKey(KeyCode.M))
        {
            AddMoney(5);
        }
    }
    void AddMoney(int add)
    {
        money += add;
        //セーブ
        savedata.data.money = money;
        savedata.WriteData();
    }
}
