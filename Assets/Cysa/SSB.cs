using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SSB : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 1);
    }

    void OnDisable()
    {
        // スクリプトが無効になった時に実行
        Debug.Log("PrintOnDisable: script was disabled");
    }

    void OnEnable()
    {
        // スクリプトが有効になった時に実行
        Debug.Log("PrintOnEnable: script was enabled");
    }
}
