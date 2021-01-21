using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_TimeScore : MonoBehaviour
{
    // UI Text指定用
    public Text TimeText;
    // 獲得コイン受け皿
    private int cleartime;

    // Use this for initialization
    void Start()
    {
        cleartime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        TimeText.text = string.Format("{0:0000}", cleartime);
    }
}
