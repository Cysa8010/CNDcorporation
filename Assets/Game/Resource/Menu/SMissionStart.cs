﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SMissionStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Fire2"))
        //{
        //    if (!scene)
        //    {
        //        Debug.Log("Null");
        //        return;
        //    }
        //    scene.ChangeScene(0);
        //}
        if(eventSystem.currentSelectedGameObject.gameObject.GetInstanceID()==button.gameObject.GetInstanceID())
		{
            RectTransform rt = transform as RectTransform;
            rt.localScale = new Vector3(1.1f, 1.1f, 1);
		}
        else
		{
            RectTransform rt = transform as RectTransform;
            rt.localScale = new Vector3(1f, 1f, 1);
        }
        
    }

    public void OnClick()
    {
        if (!scene)
        {
            Debug.Log("Null");
            return;
        }
        scene.ChangeScene(1);

    }

    [SerializeField] private SSceneManager scene = null;
    [SerializeField] private Button button = null;
    [SerializeField] private EventSystem eventSystem = null;
}
