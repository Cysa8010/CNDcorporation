using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            //if (true)
            {
                Instantiate(particleObject, this.transform.position, Quaternion.Euler(-90f,0f,0f));
                ManagerScript.AddCount();
                Destroy(this.gameObject);
            }
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
