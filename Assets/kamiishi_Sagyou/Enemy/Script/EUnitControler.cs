using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EUnitControler : MonoBehaviour
{
    void Start()
    {
        ResetValue();
    }

    void Update()
    {
   //     ResetValue();
    }

    protected void ResetValue()
    {
        moveValue_ = Vector3.zero;
        state_ = EnemyControll.EnemyControllState.NONE;
    }

    /* -- Getter -- */
    public Vector3 direction { get { return moveValue_; } }
    public EnemyControll.EnemyControllState state { get { return state_; } }
    public float turnAngle { get { return turnValue_; } }

    /* -- Member -- */
    // 外部アクセス禁止変数(継承範囲内では許可)
    protected Vector3 moveValue_;
    protected EnemyControll.EnemyControllState state_;
    protected float turnValue_;
}
