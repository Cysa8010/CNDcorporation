using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMissionStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Fire2"))
        {
            if (!scene)
            {
                Debug.Log("Null");
                return;
            }
            scene.ChangeScene(0);
        }
    }

    public void OnClick()
    {
        if (!scene)
        {
            Debug.Log("Null");
            return;
        }
        scene.ChangeScene(0);

    }

    [SerializeField] private SSceneManager scene = null;
}
