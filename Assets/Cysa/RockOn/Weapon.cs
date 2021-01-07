using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour
{
	public GameObject bullet;
	public string fireButton = "Fire1";
	public int rateOfFire = 10;
	private int delayTime = 0;

	public float speed = 100;
	public float damage = 100;
	public int effectiveTime = 30;
	public int lockOnTime = 3;

	public int getLockOnTime()
	{
		return lockOnTime;
	}

	void Update()
	{


		/*射撃ボタン1が押されているとき*/
		bool fire = Input.GetButton(fireButton);
		if (fire == true)
		{
			if (delayTime <= 0)
			{
				delayTime = rateOfFire;
				//弾を出現させる位置を取得
				Vector3 placePosition = this.transform.position;
				//出現させる位置をずらす値
				Vector3 offsetGun = new Vector3(0, 0, 3);

				//武器の向きに合わせて弾の向きも調整
				Quaternion q1 = this.transform.rotation;
				//弾を90度回転させる処理
				Quaternion q2 = Quaternion.AngleAxis(90, new Vector3(1, 0, 0));
				Quaternion q = q1 * q2;

				//弾を出現させる位置を調整
				placePosition = q1 * offsetGun + placePosition;
				//弾生成！
				GameObject tmpBullet = Instantiate(bullet, placePosition, q) as GameObject;
				Bullet b = tmpBullet.GetComponent<Bullet>();
				b.Create(damage, speed, effectiveTime);
			}
		}
		if (0 < delayTime)
		{
			delayTime--;
		}
	}
}
