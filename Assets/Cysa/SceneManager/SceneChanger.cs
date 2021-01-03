using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 + @class      SceneChanger
 + @brief      デバック用シーン切り替え
 + @note        詳細にはかかない。デバック用なので命名規則も守っていない
*/
public class SceneChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    GameObject.Find("GameObject").GetComponent<SSceneManager>().LoadScene(0);
        //}

        // とりあえず、エンターキーで切り替え
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GameObject.Find("SceneManager").GetComponent<SSceneManager>().ChangeScene(0);
        }

    }
}
