using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

/*
 * 
 */

public class SWriter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_EDITOR
        currentDirectory = Directory.GetCurrentDirectory();//Editor上では普通にカレントディレクトリを確認
#else
        currentDirectory = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');//EXEを実行したカレントディレクトリ (ショートカット等でカレントディレクトリが変わるのでこの方式で)
#endif
        //fullPath = @currentDirectory + @"\";
        Debug.Log(fullPath);
        Load();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F5))
        {
            Write();
            Load();
        }
    }

    bool WritePlayerSD(string path, int body,int propeller,int LWeapon,int RWeapon)
    {
        string file = currentDirectory + @"\data";
        if (!Directory.Exists(file))
        {
            Directory.CreateDirectory(file);
            file += @"\";
        }
        file += path;
        return true;

        /*BinaryWriter bw = null;
        try
        {
            try
            {
                bw = new BinaryWriter(new FileStream(file, FileMode.CreateNew));// 存在時上書き, 非存在時生成
                bw.Write((Int16)bodyNum);
                bw.Write((Int16)100);
                bw.Write((Int16)100);
                bw.Write((Int16)100);
            }
            finally
            {
                if (bw != null)
                {
                    bw.Close();
                }
            }
        }
        catch
        {
            Debug.Log("Writing file Error!!");
        }*/
    }
    void Write()
    {
        if(!Directory.Exists(@"./data"))
        {
            Directory.CreateDirectory(@"./data");
        }
        var path = @"./data/test.dat";
        BinaryWriter bw = null;
        try
        {
            try
            {
                bw = new BinaryWriter(new FileStream(path, FileMode.Create));
                bw.Write((Int16)bodyNum);
                bw.Write((Int16)propellerNum);
                bw.Write((Int16)weaponNum);
                //bw.Write((Int16)100);
            }
            finally
            {
                if(bw!=null)
                {
                    bw.Close();
                }
            }
        }
        catch
        {
            Debug.Log("Writing file Error!!");
        }
    }
    void Load()
    {
        string dir = currentDirectory + @"\data";
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
            dir += @"\";
        }
        string f = dir + @path;
        FileInfo fileInfo = new FileInfo(f);
        if (!fileInfo.Exists)
        {
            Debug.Log("404 Not Found of file!");
            return;
        }
        Debug.Log("Save File" + fileInfo.Exists + "Size : " + fileInfo.Length);
    }

    [SerializeField]
    private string fullPath { get { return @currentDirectory + @"\data\" + @path; } }
    [SerializeField]
    private string currentDirectory = null;
    
    [SerializeField]//@"player.sd"
    private string path = null;

    // 書込みデータ
    [SerializeField]// ボディ
    private int bodyNum = 0;
    [SerializeField]// プロペラ
    private int propellerNum = 0;
    [SerializeField]// 武器
    private int weaponNum = 0;
}
