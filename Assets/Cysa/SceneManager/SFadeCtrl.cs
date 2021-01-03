using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/** コーディング形式
 * public method
 * ↓
 * member
 * ↓
 * private method
 */

/**
 + @class      SFadeCtrl
 + @brief      フェード処理係
*/
[System.Serializable]
public class SFadeCtrl
{
    // ** 定義 **

    public delegate void OutEndFunc(int i); //<! デリゲートの定義

    // ** public method **

    /**
     + @fn             SetFadeOut
     + @brief          フェードアウト開始
     + @param[in]      OutEndFunc oef ...フェードアウト後に実行する関数
     + @param[in]      int index ...遷移先シーンのリスト番号
     + @author         Tsukasa Yamato
     + @note           フェードアウト開始時に呼ぶ。
     + @attention      今のところ汎用性はないのでいじらない方がいい
     +
    */
    public void SetFadeOut(OutEndFunc oef,int index)
    {
        _state = 2;
        if (isFadeOut)
        {
            image.gameObject.SetActive(true);
        }
        else
        {
            _time = limitTime + 1f;
        }
        outendfunc = oef;
        funcIndex = index;
    }

    /**
     + @fn             Start
     + @brief          初期化
     + @author         Tsukasa Yamato
     + @note           Start is called before the first frame update
     + @attention      呼び出しはシステム側から
     +
    */
    public void Start()
    {
        if (isFadeIn)
        {
            _state = 1;
            image.gameObject.SetActive(true);
        }
    }

    /**
     + @fn             Update
     + @brief          更新
     + @author         Tsukasa Yamato
     + @note           Update is called once per frame
     + @attention      呼び出しはシステム側から
     +
    */
    public void Update()
    {
        if (0 < _state)
        {
            // Fade 処理
            FadeIn();
            FadeOut();
        }
    }

    // ** member **

    [SerializeField] private bool isFadeIn;// = false;  //<! フェードインするか
    [SerializeField] private bool isFadeOut;// = false; //<! フェードアウトするか
    [SerializeField] private float limitTime = 1.0f;    //<! フェード時間
    [SerializeField] private Image image;// = null;     //<! フェードポリゴン

    private int _state = 0;                             //<! 状態 : 0...N, 1...In, 2...Out
    private float _time = 0.0f;                         //<! 経過時間
    private OutEndFunc outendfunc;                      //<! フェードアウト後実行関数
    private int funcIndex;                              //<! 関数の引数用

    // ** private method **

    /**
     + @fn             FadeIn
     + @brief          フェードイン本体
     + @author         Tsukasa Yamato
     + @note           スリムにできなかった
     + @attention      外部からアクセス禁止
     +
    */
    private void FadeIn()
    {
        if (_state != 1)
            return;

        // End Fade?
        if (limitTime < _time)
        {
            _state = 0;
            _time = 0f;
            image.gameObject.SetActive(false);
            return;
        }

        float ratio = _time / limitTime;
        Color col = image.color;

        col.a = 1f - ratio;

        image.color = col;

        // Add Time
        _time += Time.deltaTime;
    }

    /**
     + @fn             FadeOut
     + @brief          フェードアウト本体
     + @author         Tsukasa Yamato
     + @note           スリムにできなかった
     + @attention      外部からアクセス禁止
     +
    */
    private void FadeOut()
    {
        if (_state != 2)
            return;

        // End Fade?
        if (limitTime < _time)
        {
            _state = 0;
            _time = 0f;
            image.gameObject.SetActive(false);

            outendfunc(funcIndex);
            return;
        }

        float ratio = _time / limitTime;
        Color col = image.color;

        col.a = ratio;

        image.color = col;

        // Add Time
        _time += Time.deltaTime;
    }


}
