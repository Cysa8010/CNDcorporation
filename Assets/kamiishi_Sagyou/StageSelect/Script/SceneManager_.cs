using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; //追加
using UnityEngine.UI; //追加

public class SceneManager_ : MonoBehaviour
{
    // シーン管理スクリプト( ステージセレクト )
    // オブジェクト
    [SerializeField] private GameObject objSceneManager;


    // コンポーネント
    [SerializeField] private EventSystem eventSystem;


    // 変数 //
    [SerializeField] private int stageTotalNumber;  // ステージ総数 : 子オブジェクト数で取得
    [SerializeField] private int stageNumber;       // 選ばれたステージナンバー : これを基にステージ変更などしてください

    void Start()
    {
        // オブジェクト取得
        objSceneManager = this.gameObject;


        // コンポーネント取得
        eventSystem = this.gameObject.transform.parent.gameObject.transform.GetComponentInChildren<EventSystem>();

        // 子オブジェクト数からステージ数取得
        stageTotalNumber = this.gameObject.transform.childCount;

        // 初期化 

    }


    void Update()
    {

        // 更新処理

    }

    public void OnClickStageSelectButton()
    {
        // クリックされたステージ(ボタン)の番号を取得
        for(int i = 0; i < stageTotalNumber; i++)
        {
            if (eventSystem.currentSelectedGameObject == objSceneManager.gameObject.transform.GetChild(i).gameObject)
            {
                stageNumber = i + 1;
            }
        }

        Debug.Log("StageNumberCheck");
    }
}
