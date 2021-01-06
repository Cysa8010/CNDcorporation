using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    GameObject GameSceneManagerObject;
    GameSceneManager ManagerScript;
    [SerializeField] int Life;
    [SerializeField] int Damage;

    void Start()
    {
        GameSceneManagerObject = GameObject.Find("GameManager");
        ManagerScript = GameSceneManagerObject.GetComponent<GameSceneManager>();
    }
    void Update()
    {
        if (Life <= 0)
        {
            ManagerScript.GameOver();
        }
    }
    void OnCollisionEnter(Collision other)
    {
        //近接攻撃型の敵のこと全く考えてなかったけど後でどうにかしようね
        if (other.gameObject.tag == "EnemyBullet")
        {
            Life -= Damage;
        }
    }
}
