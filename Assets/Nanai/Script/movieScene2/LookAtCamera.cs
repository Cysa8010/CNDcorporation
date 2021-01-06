using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] GameObject targetObject;
    [SerializeField] float Speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.LookAt(targetObject.transform);
        LookAtSlowly();
    }
    private void LookAtSlowly()
    {
        // 補完スピードを決める
        float speed = Speed;
        // ターゲット方向のベクトルを取得
        Vector3 relativePos = targetObject.transform.position - this.transform.position;
        // 方向を、回転情報に変換
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        // 現在の回転情報と、ターゲット方向の回転情報を補完する
        transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, speed);

    }
}
