using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SEnemyCount : MonoBehaviour
{
    //public GameObject gameManager;
    [SerializeField] private GameSceneManager gameManager = null;
    [SerializeField] private Text text;

    // Start is called before the first frame update
    void Start()
    {
        text.text = "残敵数: 0 / 0";
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager == null)
		{
            text.text = "残敵数: 0 / 0";
            return;
        }
        text.text = "残敵数: " + gameManager.GetEnemyCount() + " / " + gameManager.GetMaxEnemy();

    }
}
