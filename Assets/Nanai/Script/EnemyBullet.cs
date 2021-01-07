using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;

public class EnemyBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.CompareTag("Enemy"))
        {
            if (collision.transform.CompareTag("Player"))
            {
                //被弾SE再生
                SEManager.Instance.Play(SEPath.DRONE_CLASH1, 0.3f, 0, 1, false, null);
            }
                Destroy(transform.gameObject);
        }
    }
}
