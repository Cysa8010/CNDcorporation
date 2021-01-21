using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniqueGravityCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        rigidbody.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void FixedUpdate()
	{
        SetGravity();
	}

    private void SetGravity()
	{
        rigidbody.AddForce(gravity, ForceMode.Acceleration);
	}

    [SerializeField] private Vector3 gravity = new Vector3(0, -9.81f, 0);
    [SerializeField] private Rigidbody rigidbody = null;
}
