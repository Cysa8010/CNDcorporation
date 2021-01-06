using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameSceneManager : MonoBehaviour
{
    int i;
    [SerializeField] int EnemyNum;
    int State;//0=ゲーム中, 1=ゲームクリア, 2=ゲームオーバー
    // Start is called before the first frame update
    void Start()
    {
        i = 0;
        State = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (State == 0)
        {
            if (i >= EnemyNum)
            {
                State = 2;
            }
        }
        //GameClear
        if(State == 1)
        {
            Debug.Log("GameClear,SceneChange");
            //SceneManager.LoadScene("Scene_Game", LoadSceneMode.Single);
        }
        //GameOver
        if (State == 2)
        {
            Debug.Log("GameOver,SceneChange");
            //SceneManager.LoadScene("Scene_Game", LoadSceneMode.Single);
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
}
