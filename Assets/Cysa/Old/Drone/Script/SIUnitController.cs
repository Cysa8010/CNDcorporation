using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SIUnitController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ResetValue();
    }

    // Update is called once per frame
    void Update()
    {
        ResetValue();
    }

    protected void ResetValue()
    {
        turnValue_ = 0f;
        moveValue_ = Vector3.zero;
        state_ = SDroneControll.ControllState.NONE;
    }

    /* -- Getter -- */
    public Vector3 direction { get { return moveValue_; } }
    public SDroneControll.ControllState state { get { return state_; } }
    public float turnAngle { get { return turnValue_; } }

    /* -- Member -- */
    // 外部アクセス禁止変数(継承範囲内では許可)
    protected Vector3 moveValue_;
    protected SDroneControll.ControllState state_;
    protected float turnValue_;
}
