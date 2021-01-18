using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SPlayerSave : MonoBehaviour
{
    /* public method */
    public void LoadData()
	{
        // 読み込み
        string datastr = "";
        StreamReader reader;
        reader = new StreamReader(GetPath());
        datastr = reader.ReadToEnd();
        reader.Close();

        playerData = writeData = JsonUtility.FromJson<SSaveData>(datastr);
        Debug.Log("Load Data : " + datastr);
    }

    public void WriteData()
	{
        string jsonstr = JsonUtility.ToJson(writeData);

        Debug.Log("Write Data : " + jsonstr);

        StreamWriter writer;

        writer = new StreamWriter(GetPath(), false);
        writer.Write(jsonstr);// バッファへ書き込み
        writer.Flush();// ファイルへ出力
        writer.Close();

        // 一応書き換えたのでやる
        LoadData();
    }

    /* private method */

    // Start is called before the first frame update
    void Start()
    {
        // 読み込み
        LoadData();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private string GetPath()
	{
        // ファイルパス
        string path;
#if UNITY_EDITOR
        // エディタ実行時のパス
        // Directory.GetCurrentDirectory();//Editor上では普通にカレントディレクトリを確認
        path = Directory.GetCurrentDirectory() + filePath;
#else
        // exe実行時のパス
        //AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');//EXEを実行したカレントディレクトリ (ショートカット等でカレントディレクトリが変わるのでこの方式で)
        path = Application.dataPath + "/savedata.json";
#endif

        return path;
    }

    /* member */
    [SerializeField] private string filePath = "/savedata.json";
    [SerializeField] private SSaveData playerData = null;
    [SerializeField] private SSaveData writeData = null;

    public SSaveData data { get { return writeData; } set { writeData = value; } }

}
