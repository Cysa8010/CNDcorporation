using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


/** コーディング形式
 * public method
 * ↓
 * member
 * ↓
 * private method
 */

/**
 + @class      SSceneManager
 + @brief      シーン切り替えやフェードを制御
*/
public class SSceneManager : MonoBehaviour
{
    // ** public method **

    /**
     + @fn             ChangeScene
     + @brief          初期化
     + @param[in]      int index ...遷移先シーンのリスト番号
     + @author         Tsukasa Yamato
     + @note           切り替える時にとりあえずこれを呼べばなんとかなる
     + @attention      番号を間違うと...うん、ガードに引っかかる
     +
    */
    public void ChangeScene(int index)
    {
        fade.SetFadeOut(this.LoadScene, 0);
    }

    // ** member **

    [SerializeField] string[]   sceneName;      //<! 移動先シーン候補
    [SerializeField] GameObject canvas = null;  //<! ロード画面BG
    [SerializeField] Slider     slider = null;  //<! ロードゲージ
    [SerializeField] SFadeCtrl  fade;           //<! フェードポリゴン

    // ** private method **

    /**
     + @fn             Start
     + @brief          初期化
     + @author         Tsukasa Yamato
     + @note           Start is called before the first frame update
     + @attention      呼び出しはシステム側から
     +
    */
    private void Start()
    {
        //カーソル非表示
        Cursor.visible = false;
        //中央にロック
        Cursor.lockState = CursorLockMode.Locked;

        fade.Start();
    }

    /**
     + @fn             Update
     + @brief          更新
     + @author         Tsukasa Yamato
     + @note           Update is called once per frame
     + @attention      呼び出しはシステム側から
     +
    */
    private void Update()
    {
        fade.Update();

        // 強制終了処理
        // Escキー
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    /**
     + @fn             LoadScene
     + @brief          シーン読み込み開始
     + @param[in]      int index ...遷移先シーンのリスト番号
     + @author         Tsukasa Yamato
     + @note           内部でコルーチンを開始するようになっている
     + @attention      外部アクセス禁止
     +
    */
    private void LoadScene(int index)
    {
        if (index < 0 || sceneName.Length <= index)
        {
            Debug.Log("LoadScene is Error.");
            return;
        }
        // コルーチンでロード画面を実行
        StartCoroutine(LoadSceneExecute(sceneName[index]));
    }

    /**
     + @fn             LoadSceneExecute
     + @brief          シーン読み込み本体
     + @param[in]      string name ...遷移先シーンの名前
     + @author         Tsukasa Yamato
     + @note           コルーチンによりシーンのロードをしながら進捗を表示
     + @attention      外部アクセス禁止
     +
    */
    private IEnumerator LoadSceneExecute(string name)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name);

        // 遷移の禁止
        asyncLoad.allowSceneActivation = false;

        // スライダーの値更新とロード画面の表示
        slider.value = 0f;

        // ロード画面の表示
        canvas.SetActive(true);
        // 見えるように遅延行為
        yield return new WaitForSeconds(1f);

        while (true)
        {
            // 分割用の処理戻し
            yield return null;

            // バーの更新
            slider.value = asyncLoad.progress;
            // 0.9fにすることで完全に終わり切る前にフック
            if (asyncLoad.progress >= 0.9f)
            {
                // バーをマックスに
                slider.value = 1f;

                // 遷移の許可
                asyncLoad.allowSceneActivation = true;

                // ロードバーが100%になっても1秒だけ表示維持
                yield return new WaitForSeconds(1f);
                break;
            }
        }
        // ロード画面の非表示
        canvas.SetActive(false);
        // バーが見えるように遅延処理
        yield return new WaitForSeconds(1f);
    }

}
