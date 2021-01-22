using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using KanKikuchi.AudioManager;

public class SButtonCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (eventSystem.currentSelectedGameObject.gameObject.GetInstanceID() == button.gameObject.GetInstanceID())
		{
			RectTransform rt = transform as RectTransform;
			rt.localScale = new Vector3(1.1f, 1.1f, 1);
			bSelect = true;
			if (bOldSelect ^ bSelect)
			{
                // SE
                SEManager.Instance.Play(SEPath.KEY_CROSS);
			}


		}
        else
        {
            RectTransform rt = transform as RectTransform;
            rt.localScale = new Vector3(1f, 1f, 1);
            bSelect = false;
        }
        bOldSelect = bSelect;
    }
    public void OnClick()
    {
        if (!scene)
        {
            Debug.Log("Null");
            return;
        }
        // ここにSE
        SEManager.Instance.Play(SEPath.KEY_ENTER);
        scene.ChangeScene(sceneIndex);
    }

    [SerializeField] private SSceneManager scene = null;
    [SerializeField] private Button button = null;
    [SerializeField] private EventSystem eventSystem = null;
    [SerializeField] private int sceneIndex = 0;
    private bool bSelect = false;
    private bool bOldSelect = false;
}
