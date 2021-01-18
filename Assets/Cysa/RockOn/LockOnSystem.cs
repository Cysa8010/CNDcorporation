using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
 
public class LockOnSystem : MonoBehaviour
{
	public GameObject enemyUnit;
	//public GameObject Weapon;
	public int lockOnTime = 3;
	[SerializeField]private GameObject target;
	private int elapsedTime = 0;
	private const int MAX_LOCK_ON_TIME = 3600;
	private bool isLockOn = false;
	public float lockOnCircle = 150;

	[SerializeField] private GameObject player = null;

	void Start()
	{

	}


	void Update()
	{

		//lockOnProc();

		////ロックオン完了までの時間を越えた場合ロックオン！！
		//if (lockOnTime <= elapsedTime)
		//{
		//	isLockOn = true;
		//}
		//else
		//{
		//	isLockOn = false;
		//}

		//Debug.DrawLine(this.transform.position, enemyUnit.transform.position, Color.red);


		target = GetTargetClosestPlayer();
		//isLockOn = false;
		if(target==null)
		{
			isLockOn = false;
			elapsedTime = 0;
		}
		else
		{
			isLockOn = true;
			elapsedTime = 1;
		}
	}


	/*ロックオン処理*/
	private void lockOnProc()
	{

		//敵がゲーム画面内にいる場合
		if (enemyUnit.GetComponent<UnitManagement>().getIsRendered())
		{

			//敵との間に障害物無い場合
			if (Physics.Linecast(this.transform.position, enemyUnit.transform.position, LayerMask.GetMask("Field")) == false)
			{
				Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, enemyUnit.transform.position);
				//Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(null, enemyUnit.transform.position);
				screenPoint.x = screenPoint.x - (Screen.width / 2);
				screenPoint.y = screenPoint.y - (Screen.height / 2);

				//ロックオンサークル内の場合
				if (screenPoint.magnitude <= lockOnCircle)
				{

					if (elapsedTime < MAX_LOCK_ON_TIME)
					{
						elapsedTime++;
					}
					target = enemyUnit;
					return;     //処理終了

				}
			}

		}
		//敵がロックオンできない状態の場合
		elapsedTime = 0;
		return;
	}

	/* キメラ追記 */
	protected GameObject GetTargetClosestPlayer()
	{
		float search_radius = 10f;

		var hits = Physics.SphereCastAll(
			player.transform.position,
			search_radius,
			player.transform.forward,
			0.01f
	
		).Select(h => h.transform.gameObject).ToList();

		hits = FilterTargetObject(hits);

		if (0 < hits.Count())
		{
			float min_target_distance = float.MaxValue;
			GameObject target = null;

			foreach (var hit in hits)
			{
				float target_distance = Vector3.Distance(player.transform.position, hit.transform.position);

				if (target_distance < min_target_distance)
				{
					min_target_distance = target_distance;
					target = hit.transform.gameObject;
				}
			}

			return target;
		}
		else
		{
			return null;
		}
	}

	protected List<GameObject> FilterTargetObject(List<GameObject> hits)
	{
		return hits
			.Where(h => {
				Vector3 screenPoint = Camera.main.WorldToViewportPoint(h.transform.position);
				return screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
			})
			.Where(h => h.tag == "Enemy")
			.ToList();
	}

	public int getElapsedTime()
	{
		return elapsedTime;
	}

	public GameObject getTarget()
	{
		return target;
	}

	public bool getIsLockOn()
	{
		return isLockOn;
	}
}
