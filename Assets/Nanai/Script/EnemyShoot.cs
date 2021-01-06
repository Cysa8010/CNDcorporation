using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] GameObject Target;
    EnemyAChaseMove ChaseMove;
    private float sec;
    [SerializeField] float FireInterval;
    [SerializeField] float FireRange; //攻撃範囲
    void Awake()
    {
        ChaseMove = this.GetComponent<EnemyAChaseMove>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Target発見時
        if (ChaseMove.IsDiscovery)
        {
            if (ChaseMove.Fire)
            {
                sec += Time.deltaTime;
                if (sec >= FireInterval)
                {
                    sec = 0.0f;
                    Shot();
                }
            }
        }
    }
    // 弾発射
    void Shot()
    {
        //TargetとAgentの位置から向きベクトルを作成
        Vector3 dist = (Target.transform.position - this.transform.position).normalized;
        //rayにRayの原点とする座標座標とTargetの方向を格納
        //Ray ray = new Ray(this.transform.position, dist);
        double a = transform.position.y + 0.2;
        Ray ray = new Ray(new Vector3(this.transform.position.x, (float)a, this.transform.position.z), dist);
        RaycastHit rayHit;

        //AgentからTargetへ向けて攻撃範囲の距離だけRayを飛ばす 結果をrayHitに格納
        Physics.Raycast(ray, out rayHit, FireRange);
        //Debug用Ray表示
        Debug.DrawRay(ray.origin, ray.direction * FireRange, Color.blue, 1, false);
        if (rayHit.collider == null || rayHit.collider.transform.GetInstanceID() != Target.transform.GetInstanceID())
        {
            ChaseMove.SetFireAnimation(false);
            ChaseMove.SetRunningAnimation(true);
            ChaseMove.SetFireFlag(false);
        }
        if (rayHit.collider.transform.GetInstanceID() == Target.transform.GetInstanceID())
        {
            Debug.Log("Shoot(Enemy)");
            GameObject go = Instantiate(bullet, emitter.transform.position, Quaternion.identity);
            go.GetComponent<Rigidbody>().AddForce(new Vector3(dist.x * 500, dist.y * 50, dist.z * 500));
            //go.transform.position = Vector3.MoveTowards(go.transform.position,
            //    new Vector3(Target.transform.position.x, Target.transform.position.y, Target.transform.position.z), 1.5f);
        }
    }
    // 撃つ弾
    [SerializeField] private GameObject bullet = null;
    // 発射口
    [SerializeField] private GameObject emitter = null;
}
