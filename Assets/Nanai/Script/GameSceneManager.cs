using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameSceneManager : MonoBehaviour
{
    int i;
    [SerializeField] int EnemyNum;
    // Start is called before the first frame update
    void Start()
    {
        i = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (i >= EnemyNum)
        {
            Debug.Log("GameClear,SceneChange");
            //SceneManager.LoadScene("Scene_Game", LoadSceneMode.Single);
        }
    }
    public void AddCount()
    {
        i += 1;
    }
}
