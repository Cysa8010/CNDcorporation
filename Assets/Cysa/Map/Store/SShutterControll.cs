using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SShutterControll : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        defaultScale = transform.localScale;
        transform.localScale = new Vector3(defaultScale.x, defaultScale.y * opcl, defaultScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(defaultScale.x, defaultScale.y * opcl, defaultScale.z);
    }

    [Range(0.01f,1f)][SerializeField] private float opcl = 0.01f;
    private Vector3 defaultScale;
}
