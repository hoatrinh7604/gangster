using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	Rigidbody rb;
	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
		rb.AddForce (transform.forward * 10,ForceMode.Impulse);
	}
	
	void Update () 
	{
	
	}
}
