using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_CoinScore : MonoBehaviour
{
    // UI Text指定用
    public Text CoinText;
    // 獲得コイン受け皿
    private int gottencoin;
 
    // Use this for initialization
    void Start () {
        gottencoin = 0;
    }
     
    // Update is called once per frame
    void Update () {
        CoinText.text = string.Format("{0:0000} coin", gottencoin);
    }
}
