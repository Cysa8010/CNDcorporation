using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_CoinScore : MonoBehaviour
{
    // UI Text指定用
    [SerializeField] private Text CoinText;
    // 獲得コイン受け皿
    private int gottencoin = 0;
 
    // Use this for initialization
    void Start () {
        //gottencoin = 0;
    }
     
    // Update is called once per frame
    void Update () {
        CoinText.text = string.Format("￥ {0}", gottencoin);
    }

    public void SetCoin(int coin)
	{
        gottencoin = coin;
	}
}
