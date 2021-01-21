using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SEnemyCount : MonoBehaviour
{
    public GameObject defobj;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        text.text = "MISSION  0 / 3";
    }

    // Update is called once per frame
    void Update()
    {
        if(defobj==null)
		{
            text.text = "ターゲット名 : Unknown\n残敵数: 0 / 0";
            return;
        }
        text.text = defobj.GetComponent<GameSceneManager>().GetEnemyCount() + " / 3";

    }
}
