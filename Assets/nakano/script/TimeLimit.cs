﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeLimit : MonoBehaviour
{
    // 制限時間の上限値
    [SerializeField] private float limitTime;

    //トータルの制限時間
    [SerializeField]
    private float totalTime;
    //　制限時間（分）
    [SerializeField]
    private int minute;
    //　制限時間（秒）
    [SerializeField]
    private float seconds;
    //　制限時間（秒）
    [SerializeField]
    private float mseconds;

    //　前回Update時の秒数
    private float oldSeconds;


    [SerializeField] private Text timerText;

    private void Start()
    {
        limitTime = totalTime = minute * 60 + seconds;
        //timerText = GetComponentInChildren<Text>();
    }

    private void Update()
    {
        //　制限時間が0秒以下なら何もしない
        if (totalTime <= 0f)
        {
            return;
        }

        totalTime -= Time.deltaTime;

        //　再設定
        minute = (int)totalTime / 60;
        seconds = totalTime % 60; //57秒の時　57 - 0 * 60 = 57 - 0 = 57
        mseconds = totalTime * 100 % 100;

        timerText.text = minute.ToString("00") + ":" + seconds.ToString("00") + ":" + mseconds.ToString("00"); 

        //　タイマー表示用UIテキストに時間を表示する
        //if ((int)seconds != (int)oldSeconds)
        //{
        //    timerText.text = minute.ToString("00") + ":" + seconds.ToString("00") + " : ";
        //}
        oldSeconds = seconds;
        //　制限時間以下になったらコンソールに『制限時間終了』という文字列を表示する
        if (totalTime <= 0f)
        {
            Debug.Log("制限時間終了");
        }
    }

    public void GetClearTime(ref int minu,ref int sec,ref int msec)
	{
        float tmp = limitTime - totalTime;

        minu = (int)tmp / 60;
        sec = (int)(tmp % 60); //57秒の時　57 - 0 * 60 = 57 - 0 = 57
        msec = (int)(tmp * 100 % 100);

    }
    public float GetTotalTime()
    {
        return totalTime;
    }
    public float GetLimitTime()
    {
        return limitTime;
    }
}
