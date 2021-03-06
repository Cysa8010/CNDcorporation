﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class RockMarker : MonoBehaviour
{
	public GameObject lockOnSystem;
	[SerializeField] private Image image = null;
	// Use this for initialization
	void Start()
	{
		this.gameObject.GetComponent<Image>().color = new Color(0f, 1f, 0f, 0f);
	}

	// Update is called once per frame
	void Update()
	{
		/*ロックオンシステム*/
		LockOnSystem l = lockOnSystem.GetComponent<LockOnSystem>();
		if(!l)
		{
			Debug.Log("Erorr LockOnSystem");
		}
		if (!l.getTarget())
		{
			Debug.Log("Erorr LockOnSystem.Target()");
			image.color = new Color(0f, 0f, 0f, 0f);//透明(非表示)
			return;
		}
		if (!Camera.main)
		{
			Debug.Log("Erorr MainCamera");
		}
		//ロックオンマーカーの置くべき座標を取得
		Vector2 position = RectTransformUtility.WorldToScreenPoint(Camera.main, l.getTarget().transform.position);
		//Vector2 position = RectTransformUtility.WorldToScreenPoint(null, l.getTarget().transform.position);
		//this.transform.GetComponent<RectTransform>().anchoredPosition = position;
		this.transform.position = new Vector3(position.x, position.y, 0f);

		//ロックオンマーカーの状態判定
		if (0 < l.getElapsedTime())
		{
			//ロックオンサークル内
			if (l.getIsLockOn())
			{
				//ロックオン完了
				image.color = new Color(0f, 1f, 0f, 1f);//緑
			}
			else
			{
				//ロックオン途中
				image.color = new Color(1f, 0f, 0f, 1f);//赤
			}
		}
		else
		{
			//ロックオンサークル外
			image.color = new Color(0f, 0f, 0f, 0f);//透明(非表示)
		}
	}
}