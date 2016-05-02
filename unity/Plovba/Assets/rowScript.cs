using UnityEngine;
using System.Collections;

public class rowScript : MonoBehaviour {
    Rigidbody rb;
    public float RowForce = 10;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetKey("a"))
        {

            rb.AddForceAtPosition(transform.forward * RowForce, transform.TransformPoint( new Vector3(-0.2f , 0, 0 )));
        }
        else if (Input.GetKey("d"))
        {
            rb.AddForceAtPosition(transform.forward * RowForce, transform.TransformPoint( new Vector3(0.2f , 0, 0 )));
        }
    }
}
