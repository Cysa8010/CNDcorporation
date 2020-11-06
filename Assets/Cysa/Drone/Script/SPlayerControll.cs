using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPlayerControll : SIUnitController
{/* ! Cation !                ↑MonoBehaviourじゃないよ↑  */
    // Start is called before the first frame update
    void Start()
    {
        // とりあえず変数の初期化
        ResetValue();
    }

    // Update is called once per frame
    void Update()
    {
        ResetValue();

        // ここにキー入力またはAIのアルゴリズム

        // 移動

        //[Flags]
        int dire = 0;

        // Left
        if (Input.GetKey(KeyCode.A)|| Input.GetAxis("Horizontal")<-0.8f)
        {
            state_ |= SDroneControll.ControllState.MOVE;
            dire |= 1;
            moveValue_ += - transform.right;
        }
        // Right
        if (Input.GetKey(KeyCode.D)||Input.GetAxis("Horizontal") > 0.8f)
        {
            state_ |= SDroneControll.ControllState.MOVE;
            dire |= 1;
            moveValue_ += transform.right;
        }

        // Front
        if (Input.GetKey(KeyCode.W)|| Input.GetAxis("Vertical")>0.8f)
        {
            state_ |= SDroneControll.ControllState.MOVE;
            dire |= 2;
            moveValue_ += transform.forward;
        }

        // Back
        if (Input.GetKey(KeyCode.S) || Input.GetAxis("Vertical") <-0.8f)
        {
            state_ |= SDroneControll.ControllState.MOVE;
            dire |= 2;
            moveValue_ += - transform.forward;
        }

        // Up
        if (Input.GetKey(KeyCode.R) || Input.GetAxis("Vertical2") < -0.8f)
        {
            state_ |= SDroneControll.ControllState.MOVE;
            dire |= 4;
            moveValue_ += transform.up;
        }

        // Down
        if (Input.GetKey(KeyCode.F) || Input.GetAxis("Vertical2") > 0.8f)
        {
            state_ |= SDroneControll.ControllState.MOVE;
            dire |= 4;
            moveValue_ += -transform.up;
        }

        if(dire==3||dire==5||dire==6)
        {
            moveValue_ /= 2f;
        }
        if(dire==7)
        {
            moveValue_ /= 3;
        }

        moveValue_*=speed;

        // 旋回
        if (Input.GetKey(KeyCode.Q) || Input.GetAxis("Horizontal2") < -0.8f)
        {
            state_ |= SDroneControll.ControllState.TURN;
            turnValue_ = -0.5f;
        }
        if (Input.GetKey(KeyCode.E) || Input.GetAxis("Horizontal2") > 0.8f)
        {
            state_ |= SDroneControll.ControllState.TURN;
            turnValue_ = 0.5f;
        }

        // 武器
        if (Input.GetKey(KeyCode.Mouse0))//MouseLeft
        {
            state_ |= SDroneControll.ControllState.ACTION1;
        }
        if (Input.GetKey(KeyCode.Mouse1))//MouseLeft
        {
            state_ |= SDroneControll.ControllState.ACTION2;
        }

        Debug.Log("H "+Input.GetAxis("Horizontal")+" V "+ Input.GetAxis("Vertical")+ "H " + Input.GetAxis("Horizontal2") + " V " + Input.GetAxis("Vertical2"));
    }

    [SerializeField]
    private float speed = 1.0f;
}
