using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
 
public class LockOnSystem : MonoBehaviour
{
	//public GameObject enemyUnit;
	[SerializeField] private string searchTag = null;
	public int lockOnTime = 3;
	private int elapsedTime = 0;
	private const int MAX_LOCK_ON_TIME = 3600;
	private bool isLockOn = false;
	//public float lockOnCircle = 150;

	[SerializeField] private GameObject player = null;
	[SerializeField] private GameObject weapon;
	private GameObject target;


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

			Transform trans = target.transform;
			trans.position += new Vector3(0, 0.5f, 0);

			weapon.transform.LookAt(trans);
		}

	}


	
	/* キメラ追記 */
	protected GameObject GetTargetClosestPlayer()
	{
		float search_radius = 10f;

		//var hits = Physics.SphereCastAll(
		//	player.transform.position,
		//	search_radius,
		//	player.transform.forward,
		//	0.01f
	
		//).Select(h => h.transform.gameObject).ToList();

		var hits = Physics.SphereCastAll(
			player.transform.position,
			search_radius,
			player.transform.forward,
			0.01f//,
			//LayerMask.NameToLayer("Ignore Raycast")
			//2

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
			.Where(h => h.tag == searchTag)
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
