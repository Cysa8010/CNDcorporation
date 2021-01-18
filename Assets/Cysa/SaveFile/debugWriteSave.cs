using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debugWriteSave : MonoBehaviour
{
    [SerializeField] SPlayerSave save = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
		{
            save.data.body = 500;
        }
        if (Input.GetKeyDown(KeyCode.O))
		{
            save.WriteData();

		}
    }
}
