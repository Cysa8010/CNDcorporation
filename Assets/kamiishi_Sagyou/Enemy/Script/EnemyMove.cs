using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : EUnitControler
{
    [SerializeField] private int moveTileNumber , listSize;         //移動管理用
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool firstPosCheck , moveStop;
    [SerializeField] private Vector2 wayPos;  // 次に向かう座標
    [SerializeField] private AStar _aStar;          
    [SerializeField] private AStarSample aStarSample;   //
    [SerializeField] private List<AStarGrid.Cell> _shortestWay = new List<AStarGrid.Cell>(); //移動先座標データ保管用

    void Start()
    {
        // 変数の初期化
        moveTileNumber = 0;
        moveSpeed = 0.02f;
        firstPosCheck = false;
        moveStop = false;
        // 親のコンポーネント取得
        aStarSample = this.gameObject.transform.GetComponentInParent<AStarSample>();

 
        // グリッドデータ取得
        _aStar = aStarSample.GetAstarGrid();

    }

    // Update is called once per frame
    void Update()
    {
        // 座標データの更新
        // 更新頻度をいじるならここで
        _shortestWay = aStarSample.GetShortestWay();

        // 初期座標の取得
        if (firstPosCheck ==false)
        {
            listSize = _shortestWay.Count;
            moveTileNumber = listSize - 1;

            wayPos.x = (float)(_shortestWay[moveTileNumber].coord.x + 0.5f);
            wayPos.y = (float)(_shortestWay[moveTileNumber].coord.y + 0.5f);
            firstPosCheck = true;
        }
        // ゴール到着したら
        if (((_shortestWay[0].coord.x + 0.4f <= this.gameObject.transform.position.x) && (this.gameObject.transform.position.x <= _shortestWay[0].coord.x + 0.6f)) &&
           ((_shortestWay[0].coord.y + 0.4f <= this.gameObject.transform.position.z) && (this.gameObject.transform.position.z <= _shortestWay[0].coord.y + 0.6f)))
        {
            moveStop = true;
            state_ = EnemyControll.EnemyControllState.MOVE;
            moveValue_ = new Vector3(0, 0, 0);
        }

        if (!moveStop)
        {


            // 目的座標の更新
            if (((_shortestWay[moveTileNumber].coord.x + 0.4f <= this.gameObject.transform.position.x) && (this.gameObject.transform.position.x <= _shortestWay[moveTileNumber].coord.x + 0.6f)) &&
               ((_shortestWay[moveTileNumber].coord.y + 0.4f <= this.gameObject.transform.position.z) && (this.gameObject.transform.position.z <= _shortestWay[moveTileNumber].coord.y + 0.6f)))
            {

                wayPos.x = (float)(_shortestWay[moveTileNumber - 1].coord.x + 0.5f);
                wayPos.y = (float)(_shortestWay[moveTileNumber - 1].coord.y + 0.5f);
                moveTileNumber -= 1;
            }
            // 移動(受け取ったデータを基に移動) , x座標とy座標を別々に更新する


            // x座標の距離のほうがz座標の移動距離より多い
            if (System.Math.Abs((wayPos.x - this.gameObject.transform.position.x)) > System.Math.Abs((wayPos.y - this.gameObject.transform.position.z)))
            {
                // x座標
                if (wayPos.x > this.gameObject.transform.position.x)
                {
                    E_Move(1);
                }
                else if (wayPos.x < this.gameObject.transform.position.x)
                {
                    E_Move(3);
                }
            }
            // z座標
            else
            {
                if (wayPos.y > this.gameObject.transform.position.z)
                {
                    E_Move(0);
                }
                else if (wayPos.y < this.gameObject.transform.position.z)
                {
                    E_Move(2);
                }
            }
        }

    }

    private void E_Move(int moveDirection)
    {
        // 上
        if (moveDirection == 0)
        {
            state_ = EnemyControll.EnemyControllState.MOVE;
            moveValue_ = new Vector3(0, 0, moveSpeed);
        }
        // 右
        if (moveDirection == 1)
        {
            state_ = EnemyControll.EnemyControllState.MOVE;
            moveValue_ = new Vector3(moveSpeed, 0, 0);
        }
        // 下
        if (moveDirection == 2)
        {
            state_ = EnemyControll.EnemyControllState.MOVE;
            moveValue_ = new Vector3(0, 0, -moveSpeed);
        }
        // 左
        if (moveDirection == 3)
        {
            state_ = EnemyControll.EnemyControllState.MOVE;
            moveValue_ = new Vector3(-moveSpeed, 0, 0);
        }

    }
}
