using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAChaseMove : MonoBehaviour
{
    NavMeshAgent Agent;
    Animator animator;
    //以下主にTarget追跡の際に使用する変数
    [SerializeField] GameObject Target; //最終的な目的地となるオブジェクト
    float AgentDefaultSpeed;//初期移動速度保存用
    [SerializeField] float Spd; //移動速度変更後に使用するSpeed
    [SerializeField] public float SensingRange; //感知範囲
    [SerializeField] float SpdChangeRange; //感知後移動速度を変える距離
    [SerializeField] float searchAngle; //Agentの視野(50と入力すると片側50度なので視界は100度になる)
    float Distance; //Agentと目的地オブジェクトの距離
    public bool IsDiscovery;//Targetを発見したか否かのフラグ
    public bool Fire;

    //以下主に巡回の際に使用する変数
    [SerializeField] Transform[] PatrolPoints;//巡回地点オブジェクトを格納する配列
    int DestObjects = 0;//巡回地点のオブジェクト数（初期値=0）
    void Start()
    {
        //AgentのNavMeshAgentを取得
        Agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        AgentDefaultSpeed = Agent.speed;
        IsDiscovery = false;
        Fire = false;
        //巡回地点間の移動を継続させるために自動ブレーキを無効化
        Agent.autoBraking = false;
        //目的地を初期化
        GotoNextPoint();
        animator.SetBool("IsRunning", true);//仮置き
    }

    void Update()
    {
        //2点間の距離を計測
        Distance = Vector3.Distance(Target.transform.position, this.transform.position);
        if (IsDiscovery)
        {
            if (Fire)
            {
                Agent.SetDestination(this.transform.position);
                this.transform.LookAt(Target.transform.position);
            }
            else
            {
                Agent.SetDestination(Target.transform.position);

                Vector3 dist = (Target.transform.position - this.transform.position).normalized;
                double a = transform.position.y + 0.2;
                Ray ray = new Ray(new Vector3(this.transform.position.x, (float)a, this.transform.position.z), dist);
                RaycastHit rayHit;
                //AgentからTargetへ向けて感知範囲の距離だけRayを飛ばす 結果をrayHitに格納
                Physics.Raycast(ray, out rayHit, SensingRange);
                //Debug用Ray表示
                Debug.DrawRay(ray.origin, ray.direction * SensingRange, Color.yellow, 1, false);
                if (rayHit.collider.transform.GetInstanceID() == Target.transform.GetInstanceID())
                {
                    animator.SetBool("IsRunning", false);
                    animator.SetBool("IsFire", true);
                    Fire = true;
                }
            }
            //if (Distance <= SpdChangeRange)
            //{
            //    //移動速度変更
            //    Agent.speed = Spd;
            //}
            //else
            //{
            //    //移動速度を戻す
            //    Agent.speed = AgentDefaultSpeed;
            //}
        }//IsDiscovery True
        else
        {
            //TargetとAgentの位置から向きベクトルを作成
            Vector3 dist = (Target.transform.position - this.transform.position).normalized;
            //Agentから見たTargetの方向をfloat値にする
            float angle = Vector3.Angle(transform.forward, dist);
            //TargetがAgentの視界内かつ感知範囲内か判断
            if (angle <= searchAngle && Distance <= SensingRange)
            {
                //rayにRayの原点とする座標とTargetの方向を格納
                //Ray ray = new Ray(this.transform.position, dist);
                double a = transform.position.y + 0.2;
                Ray ray = new Ray(new Vector3(this.transform.position.x, (float)a, this.transform.position.z), dist);
                RaycastHit rayHit;

                //AgentからTargetへ向けて感知範囲の距離だけRayを飛ばす 結果をrayHitに格納
                Physics.Raycast(ray, out rayHit, SensingRange);
                //Debug用Ray表示
                Debug.DrawRay(ray.origin, ray.direction * SensingRange, Color.red, 1, false);
                //if (rayHit.collider.tag == "Player")
                if (rayHit.collider.transform.GetInstanceID() == Target.transform.GetInstanceID())
                {
                    Debug.Log("Target発見");
                    animator.SetBool("IsRunning", false);
                    animator.SetBool("IsFire", true);
                    Fire = true;
                    IsDiscovery = true;
                }
            }

            //Agentが現在の巡回地点に到達したら
            if (!Agent.pathPending && Agent.remainingDistance < 0.5f)
            {
                //次の巡回地点を設定する
                GotoNextPoint();
            }
        }//IsDiscovery false
    }//Update
    void GotoNextPoint()
    {
        //巡回地点が設定されていなければリターン
        if (PatrolPoints.Length == 0)
        {
            return;
        }
        // 現在選択されている配列の座標を巡回地点の座標に代入
        Agent.SetDestination(PatrolPoints[DestObjects].position);
        // 配列の中から次の巡回地点を選択（必要に応じて繰り返し）
        DestObjects = (DestObjects + 1) % PatrolPoints.Length;
    }
    public void SetFireFlag(bool b)
    {
        Fire = b;
    }
    public void SetFireAnimation(bool b)
    {
        animator.SetBool("IsFire", b);
    }
    public void SetRunningAnimation(bool b)
    {
        animator.SetBool("IsRunning", b);
    }
}