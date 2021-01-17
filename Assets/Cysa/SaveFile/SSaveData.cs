using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SSaveData
{
    /* Player Param */
    public int recovery;    //!< 回復量
    public int limitEnergy; //!< エネルギー上限
    public int move;        //!< 移動量
    public int speed;       //!< 加速量
    public int durability;  //!< 耐久値

    /* Player Custom */
    public int body;        //!< 機体番号
    public int propeller;   //!< プロペラ番号
    public int weapon;      //!< 武器番号
    public int motor;       //!< モーター番号

    /* Resource */
    public int money;       //!< 所持金
}
