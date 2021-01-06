using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
	public Quaternion firstRotation;
	public float speedY = 100;
	public float damage=0;
	public int effectiveTime = 3;

	public void Create(float damage,float speed,int time)
	{

	}

	void Start()
	{
		firstRotation = transform.rotation;
		Rigidbody rigidbody = GetComponent<Rigidbody>();

		Vector3 movementSpeed = new Vector3(0, speedY, 0);

		movementSpeed = firstRotation * movementSpeed;

		rigidbody.AddForce(movementSpeed);

		Invoke("Del", effectiveTime);
	}

	void Update()
	{


	}

	void Del()
	{
		Destroy(this);
	}
}
