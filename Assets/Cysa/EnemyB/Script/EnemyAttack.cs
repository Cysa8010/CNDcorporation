using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private float sec;
    void Start()
    {
        sec = 0.0f;
    }
    
    void Update()
    {
        //攻撃範囲内に5s以上居た場合攻撃する
        if(sec >= 5.0)
        {
            Attack();
        }
    }
    void Attack()
    {
        //仮
        //ここでPlayerのHP減らしたりすれば良いんじゃないか
        Debug.Log("Attack!");
        Debug.Log("Timerリセット(攻撃した)");
        sec = 0.0f;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("Time加算開始");
            sec += Time.deltaTime;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Timerリセット(対象から離れた)");
            sec = 0.0f;
        }
    }
}
