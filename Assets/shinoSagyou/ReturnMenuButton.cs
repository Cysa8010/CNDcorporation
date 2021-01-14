using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnMenuButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return)||Input.GetButtonDown("Fire2"))
		{
            Debug.Log("Fire2");
            if (!scene)
            {
                Debug.Log("Null");
                return;
            }
            scene.ChangeScene(0);
        }
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Fire3"))
        {
            Debug.Log("Fire3");
        }
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Fire1");
        }
    }

    public void OnClick()
    {
        if(!scene)
		{
            Debug.Log("Null");
            return;
		}
        scene.ChangeScene(0);

    }

    [SerializeField] private SSceneManager scene = null;
}
