using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameSceneManager : MonoBehaviour
{
    int i;
    bool AreaError;
    float CurrentTime;
    private int EnemyNum;
    [SerializeField] int MaxEnemyNum = 0;
    [SerializeField] GameObject GameClearImage;
    [SerializeField] GameObject GameOverImage;
    [SerializeField] GameObject PlayAreaErrorImage;
    [SerializeField] GameObject Player;
    [SerializeField] private SSceneManager scene = null;
    [SerializeField] private TimeLimit timeLimit = null;

    int coin;
    int minu;
    int sec;
    int msec;

    int State;//0=ゲーム中, 1=ゲームクリア, 2=ゲームオーバー
    // Start is called before the first frame update
    void Start()
    {
        i = 0;
        EnemyNum = MaxEnemyNum;
        State = 0;
        AreaError = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (State == 0)
        {
            if (!Player.GetComponent<SPlayerStatusKNC>().IsPower_On())
            {
                AreaError = true;
            }
            if (AreaError)
            {
                PlayAreaErrorImage.SetActive(true);
                CurrentTime += Time.deltaTime;
                if (CurrentTime > 1.5f)
                {
                    CurrentTime = 0;
                    PlayAreaErrorImage.SetActive(false);
                    AreaError = false;
                }
            }
            if (i >= EnemyNum)
            {
                State = 1;
            }
        }
        //GameClear
        if(State == 1)
        {
            GameClearImage.SetActive(true);
            CurrentTime += Time.deltaTime;
            if (CurrentTime > 4.0)//4s経過でシーン遷移
            {
                CurrentTime = 0;
                Debug.Log("GameClear,SceneChange");
                //SceneManager.LoadScene("Scene_Game", LoadSceneMode.Single);
                coin = 100;
                timeLimit.GetClearTime(ref minu, ref sec, ref msec);
                SceneManager.sceneLoaded += ResultSceneLoaded;
                scene.ChangeScene(0);
            }
        }
        //GameOver
        if (State == 2)
        {
            GameOverImage.SetActive(true);
            CurrentTime += Time.deltaTime;
            if (CurrentTime > 4.0)//4s経過でシーン遷移
            {
                CurrentTime = 0;
                Debug.Log("GameOver,SceneChange");
                //SceneManager.LoadScene("Scene_Game", LoadSceneMode.Single);
                coin = 0;
                timeLimit.GetClearTime(ref minu, ref sec, ref msec);
                SceneManager.sceneLoaded += ResultSceneLoaded;
                scene.ChangeScene(0);
            }
        }
    }
    public void AddCount()
    {
        i += 1;
    }
    public void GameOver()
    {
        State = 2;
    }


    public int GetEnemyCount()
    {
        return EnemyNum;
    }

    public int GetMaxEnemy()
	{
        return MaxEnemyNum;
	}

    private void ResultSceneLoaded(Scene next, LoadSceneMode mode)
    {
        // シーン切り替え時に呼ばれる
        var gameManager = GameObject.Find("ResultManager").GetComponent<ResultManager>();
        gameManager.SetResultData(coin, minu, sec, msec);
        SceneManager.sceneLoaded -= ResultSceneLoaded;
    }

}
