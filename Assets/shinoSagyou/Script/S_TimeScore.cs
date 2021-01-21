using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_TimeScore : MonoBehaviour
{
    // UI Text指定用
    public Text TimeText;
    // 獲得コイン受け皿
    private int minu = 0;
    private int sec = 0;
    private int msec = 0;

    // Use this for initialization
    void Start()
    {
        //cleartime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //TimeText.text = string.Format("{0:00}\'{0:00}\"{0:00}", minu, sec, msec);
        TimeText.text = "0" + string.Format("{0}\'{1}\"{2}", minu, sec, msec);
    }

    public void SetTime(int minu,int sec,int msec)
	{
        this.minu = minu;
        this.sec = sec;
        this.msec = msec;
	}
}
