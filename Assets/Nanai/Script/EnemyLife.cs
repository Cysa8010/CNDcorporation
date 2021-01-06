using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;

public class EnemyLife : MonoBehaviour
{
    GameObject GameSceneManagerObject;
    GameSceneManager ManagerScript;
    [SerializeField] int Life;
    [SerializeField] int Damage;
    [SerializeField] GameObject particleObject;

    void Start()
    {
        GameSceneManagerObject = GameObject.Find("SceneManager");
        ManagerScript = GameSceneManagerObject.GetComponent<GameSceneManager>();
    }
    void Update()
    {
        if (Life <= 0)
        {
            //捕獲パーティクル生成
            Instantiate(particleObject, this.transform.position, Quaternion.Euler(-90f,0f,0f));
            //捕獲SE再生
            SEManager.Instance.Play(SEPath.ENEMY_ARREST, 0.3f, 0, 1, false, null);
            ManagerScript.AddCount();
            Destroy(this.gameObject);
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "PlayerBullet")
        {
            Life -= Damage;
        }
    }
}
