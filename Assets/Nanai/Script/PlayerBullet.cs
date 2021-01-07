using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;

public class PlayerBullet : MonoBehaviour
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
        if (!collision.transform.CompareTag("Player"))
        {
            //着弾SE再生
            SEManager.Instance.Play(SEPath.DRONE_LANDING, 0.3f, 0, 1, false, null);
            Destroy(transform.gameObject);
        }
    }
}
