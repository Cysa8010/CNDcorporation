using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMove : MonoBehaviour
{
    bool move;
    [SerializeField] float StartMoveTime;
    [SerializeField] float Speed;
    // Start is called before the first frame update
    void Start()
    {
        move = false;
        Invoke("MoveWall", StartMoveTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (move == true)
        {
            Transform myTransform = this.transform;
            Vector3 pos = myTransform.position;
            pos.y += Speed;
            myTransform.position = pos;
        }
        if (this.transform.position.y > 11f)
        {
            move = false;
        }
    }
    void MoveWall()
    {
        move = true;
    }
}
