using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleButton : MonoBehaviour
{
    private Image image;

    private Button button;

    int cnt;
    public int MAX_COUNT = 60;
    public List<Color> colors = new List<Color>() { new Color(1, 1, 1, 1), new Color(1, 1, 1, 0) };
    //==============================================

    private float AddAlpha;
    private bool InFade;
    private bool OutFade;

    //計算式作成すればOK
    public float alpha;
    public float alpha_Sin;

    private void Start()
    {
        image = GetComponent<Image>();
        alpha = 1.0f;
    }

    void Update()
    {
        alpha_Sin = Mathf.Sin(Time.time) / 3 + 0.8f;

        image.color = new Color(1.0f, 1.0f, 1.0f, alpha);

        alpha = alpha_Sin;

    }

    public void OnClick()
    {
        if (!scene)
        {
            Debug.Log("Null");
            return;
        }
        // ここにSE
        //SEManager.Instance.Play(SEPath.KEY_ENTER);
        scene.ChangeScene(sceneIndex);
    }
    [SerializeField] private SSceneManager scene = null;
    [SerializeField] private int sceneIndex = 0;
}
