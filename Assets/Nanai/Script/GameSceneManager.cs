using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameSceneManager : MonoBehaviour
{
    int i;
    bool AreaError;
    float CurrentTime;
    [SerializeField] int EnemyNum;
    [SerializeField] GameObject GameClearImage;
    [SerializeField] GameObject GameOverImage;
    [SerializeField] GameObject PlayAreaErrorImage;
    [SerializeField] GameObject Player;
    [SerializeField] private SSceneManager scene = null;

    int State;//0=ゲーム中, 1=ゲームクリア, 2=ゲームオーバー
    // Start is called before the first frame update
    void Start()
    {
        i = 0;
        State = 0;
        AreaError = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (State == 0)
        {
            if (!Player.GetComponent<SPlayerStatusK>().IsPower_On())
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
        return i;
    }



}
